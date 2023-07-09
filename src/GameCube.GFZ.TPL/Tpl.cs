using GameCube.GX;
using GameCube.GX.Texture;
using Manifold;
using Manifold.IO;
using System;

namespace GameCube.GFZ.TPL
{
    [Serializable]
    public class Tpl :
        IBinaryFileType,
        IBinarySerializable
    {
        public const Endianness endianness = Endianness.BigEndian;

        private int textureDescriptionsCount;
        private TextureSeriesDescription[] textureSeriesDescription = new TextureSeriesDescription[0];
        private TextureSeries[] textureSeries = new TextureSeries[0];

        public Endianness Endianness => endianness;
        public string FileExtension => ".tpl";
        public string FileName { get; set; } = "";

        public TextureSeriesDescription[] TextureSeriesDescriptions => textureSeriesDescription;
        public TextureSeries[] TextureSeries => textureSeries;

        private static readonly TextureColor Magenta = new TextureColor(255, 0, 255);

        public void Deserialize(EndianBinaryReader reader)
        {
            // Read `count` texture descriptions
            reader.Read(ref textureDescriptionsCount);
            reader.Read(ref textureSeriesDescription, textureDescriptionsCount);
            textureSeries = new TextureSeries[textureDescriptionsCount];

            // File has padding after descriptions that increment continuously.
            int paddingLength = (int)StreamExtensions.GetLengthOfAlignment(reader.BaseStream, GXUtility.GX_FIFO_ALIGN);
            var padding = reader.ReadBytes(paddingLength);
            for (byte i = 0; i < padding.Length; i++)
                Assert.IsTrue(padding[i] == i);

            for (int i = 0; i < textureSeriesDescription.Length; i++)
            {
                var textureSeriesDescription = this.textureSeriesDescription[i];
                if (textureSeriesDescription.IsNull)
                    continue;

                // Some TPLs come with garbage data in the first 0x30 bytes of the file (in the first 4 bytes of affected descriptions).
                // Make sure the null check above never fails. Otherwise you need to find a different way to check for this garbage.
                var msg = $"Uncaught garbage entry {FileName} entry {i} addr {textureSeriesDescription.AddressRange.PrintStartAddress()}";
                Assert.IsFalse(textureSeriesDescription.IsGarbageEntry, msg);

                // Get encoding and ensure it comforms to expectations. No indirect textures are used (only GFZJ tested).
                var encoding = Encoding.GetEncoding(textureSeriesDescription.TextureFormat);
                Assert.IsTrue(encoding.IsDirect, "Encoding is not direct. GFZ does not use (handle?) indirect modes.");

                // Assert game uses all power-of-two textures.
                int isWidthPowerOfTwo = textureSeriesDescription.Width % encoding.BlockWidth;
                int isHeightPowerOfTwo = textureSeriesDescription.Height % encoding.BlockHeight;
                if (isWidthPowerOfTwo != 0 || isHeightPowerOfTwo != 0)
                    DebugConsole.Log($"{FileName}.tpl: Texture index {i} has size (x:{textureSeriesDescription.Width}, y:{textureSeriesDescription.Height}). Not a power of two.");

                // Read the texture series.
                reader.JumpToAddress(textureSeriesDescription.TextureSeriesPtr);
                textureSeries[i] = ReadDirectTextureSeries(reader, textureSeriesDescription);
                // Record some useful metadata about this texture
                textureSeries[i].AddressRange = new AddressRange()
                {
                    startAddress = textureSeriesDescription.TextureSeriesPtr,
                    endAddress = reader.GetPositionAsPointer(),
                };
            }
        }
        public void Serialize(EndianBinaryWriter writer)
        {
            // Write texture series descriptions
            writer.Write(TextureSeriesDescriptions.Length);
            writer.Write(TextureSeriesDescriptions);

            // Write incrementing padding
            int paddingLength = (int)StreamExtensions.GetLengthOfAlignment(writer.BaseStream, GXUtility.GX_FIFO_ALIGN);
            for (byte i = 0; i < paddingLength; i++)
                writer.Write(i);

            // Write each texture of each texture series
            foreach (var textureSerie in textureSeries)
            {
                var directEncoding = DirectEncoding.GetEncoding(textureSerie.Description.TextureFormat);
                foreach (var entry in textureSerie.Entries)
                {
                    var texture = entry.Texture;
                    var blocks = Texture.CreateDirectColorBlocksFromTexture(texture, directEncoding);
                    directEncoding.WriteBlocks(writer, blocks);
                }
            }
        }

        /// <summary>
        /// Reads texture and all associated mipmaps.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="textureSeriesDescription"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static TextureSeries ReadDirectTextureSeries(EndianBinaryReader reader, TextureSeriesDescription textureSeriesDescription)
        {
            var encoding = Encoding.GetEncoding(textureSeriesDescription.TextureFormat);
            int pixelWidth = textureSeriesDescription.Width;
            int pixelHeight = textureSeriesDescription.Height;
            var textureSeries = new TextureSeries(textureSeriesDescription);

            int totalBlocksEncoded = GetTotalBlocksEncodedCount(textureSeriesDescription);
            int totalBlocksRead = 0;

            for (int i = 0; i < textureSeries.Entries.Length; i++)
            {
                // Some textures have invalid mipmap sizes. Prevent them from doing anything.
                bool isInvalidTextureSize = pixelWidth == 0 || pixelHeight == 0;
                if (isInvalidTextureSize)
                {
                    textureSeries[i].Texture = new Texture(1, 1, Magenta, textureSeriesDescription.TextureFormat);
                    pixelWidth >>= 1;
                    pixelHeight >>= 1;
                    continue;
                }

                //
                AddressRange textureRange = new AddressRange();
                textureRange.startAddress = (int)reader.BaseStream.Position;

                // Get how many blocks to read (width and height separated)
                int widthBlocks = (int)Math.Ceiling((double)pixelWidth / encoding.BlockWidth);
                int heightBlocks = (int)Math.Ceiling((double)pixelHeight / encoding.BlockHeight);

                // Make sure we can read that many blocks. Exceptions will occur on some CMPR textures.
                int blocksRequired = encoding.GetTotalBlocksToEncode(pixelWidth, pixelHeight);
                bool canReadRequiredBlocks = (totalBlocksRead + blocksRequired) <= totalBlocksEncoded;
                if (!canReadRequiredBlocks)
                {
                    // See how many blocks remain in the stream.
                    int blocksInStream = totalBlocksEncoded - totalBlocksRead;
                    bool canReadMoreBlocks = blocksInStream > 0;
                    if (canReadMoreBlocks)
                    {
                        // Read the remaining blocks
                        var invalidDirectBlocks = encoding.ReadBlocks<DirectBlock>(reader, encoding, blocksInStream);
                        totalBlocksRead += blocksRequired;
                        // Create an invalid texture. Unset pixels will be magenta.
                        var invalidTexture = GfzFromPartialDirectBlocks(invalidDirectBlocks, widthBlocks, heightBlocks, Magenta);
                        textureSeries[i].Texture = Texture.Crop(invalidTexture, pixelWidth, pixelHeight);
                    }
                    else
                    {
                        // If no more blocks to read, make fully magenta texture.
                        textureSeries[i].Texture = new Texture(pixelWidth, pixelHeight, Magenta, textureSeriesDescription.TextureFormat);
                    }
                    textureSeries[i].IsValid = false;

                    pixelWidth >>= 1;
                    pixelHeight >>= 1;
                    continue;
                }

                // If we succeed, proceed to deserialize blocks for texture.
                //var directBlocks = encoding.ReadBlocks<DirectBlock>(reader, blocksW, blocksH, encoding);
                var directBlocks = encoding.ReadBlocks<DirectBlock>(reader, encoding, blocksRequired);
                Assert.IsTrue(blocksRequired != 0);
                Assert.IsTrue(directBlocks.Length == blocksRequired);
                Assert.IsTrue(widthBlocks * heightBlocks == blocksRequired);
                totalBlocksRead += blocksRequired;

                // Make new texture. Crop it to width/height on occasions where pixel width or height
                // is lesser than the block size.
                var texture = Texture.FromDirectBlocks(directBlocks, widthBlocks, heightBlocks);
                textureSeries[i].Texture = Texture.Crop(texture, pixelWidth, pixelHeight);
                textureSeries[i].IsValid = true;

                // Halve the size for the next mipmap.
                pixelWidth >>= 1;
                pixelHeight >>= 1;

                // Get texture hash ONLY for valid textures
                textureRange.endAddress = (int)reader.BaseStream.Position;
                reader.JumpToAddress(textureRange.startAddress);
                var bytes = reader.ReadBytes(textureRange.Size);
                reader.JumpToAddress(textureRange.endAddress);
                //
                var MD5 = System.Security.Cryptography.MD5.Create();
                var hashedBytes = MD5.ComputeHash(bytes);
                textureSeries.MD5TextureHashes[i] = hashedBytes.ConcatElements((byte b) => { return b.ToString("x2"); }); // gross...?
            }
            return textureSeries;
        }

        /// <summary>
        ///     GFZ has an error where it computes an incorrect number of blocks to encode a texture.
        ///     This method provides the amount of blocks the game thinks is needed to encoded for CMPR.
        /// </summary>
        /// <param name="pixelWidtth">The pixel width of the texture to encode.</param>
        /// <param name="pixelHeight">The pixel height of the texture to encode.</param>
        /// <returns>
        ///     The GFZ-specific number of blocks encoded for the texture. This amount may vary
        ///     from the number of blocks actually needed to encode a correctly formated CMPR image.
        /// </returns>
        /// <example>
        ///     CMPR (BC1/DXT1) has a block size of 8x8 split into quadrants (2x2); in each we have
        ///     a 4x4 grid of pixels. GFZ incorrectly formats CMPR textures as though it only requires
        ///     one single 4x4 block. This leads to encoding issues for textures with a width or height
        ///     that is not divisible by 8 or that has a side less than or equal to 4.
        ///
        ///     Examples:
        ///     Note values in divisions are clamped upwards to fit existing pixels in a block
        ///     of greater size. ei: 1x1 pixels still takes one 8x8 block, the rest is just padding.
        ///     
        ///     Image.
        ///     Sample A size: 16x16 pixels. (square)
        ///     Sample B size: 64x4 pixels. (rectangle)
        ///     
        ///     CMPR block size is 8x8.
        ///     A: 16/8 * 16/8 == 2 * 2 == 4 blocks required.
        ///     B: 64/8 *  4/8 == 8 * 1 == 8 blocks required.
        ///     
        ///     GFZ CMPR block size is 4x4
        ///     A: 16/4 * 16/4 ==  4 * 4 == 16 blocks.
        ///     B: 64/4 *  4/4 == 16 * 1 == 16 blocks required.
        ///     However, the game knows this is incorrect and tries to adjust the value.
        ///     A: 16 blocks / 4 = 4 blocks required (hey, correct!)
        ///     B: 16 blocks / 4 = 4 blocks (uh-oh, that's half what we need!)
        ///     
        ///     Even large textures suffer if "side length" % 8 is less than or equal to 4.
        ///     Texture size: 60x64 pixels.
        ///     GC  CMPR: 60/8 * 64/8 == ceil(7.5) * 8 ==  8 *  8 ==              == 64 blocks
        ///     GFZ CMPR: 60/4 * 64/4 ==               == 15 * 16 == 240/4 blocks == 60 blocks
        ///     
        ///     Because of this, GFZ regularly under-allocates space for rectangular CMPR
        ///     textures. The game calculates the full size it thinks a CMPR with mipmaps
        ///     will take and set pixel data up until the buffer is full (prematurely) and then
        ///     stops. This results in the odd behaviour of some mipmaps being only partially
        ///     written (say, 3/4 of the texture). The rest is either the next texture's data
        ///     or we hit End-Of-File, which when loaded could be anything else in RAM after it.
        ///     
        ///     Below is a breakdown of what a 512x32 texture with mipmaps requires in terms of blocks.
        ///     You can see the algorithm begin to fail when we get to below 8 pixels in w or h.
        ///     
        ///     CMPR 8x8 block requirement
        ///     pixels      div8    blocks
        ///     512 x 32 == 64x4 == 256 blocks
        ///     256 x 16 == 32x2 ==  64 blocks
        ///     128 x  8 == 16x1 ==  16 blocks
        ///      64 x  4 ==  8x1 ==   8 blocks
        ///      32 x  2 ==  4x1 ==   4 blocks
        ///      16 x  1 ==  2x1 ==   2 blocks
        ///     TOTAL:              350 blocks
        ///     
        ///     GFZ CMPR 4x4 block "requirement"
        ///     pixels      div4    hackfix   blocks
        ///     512 x 32 == 128x8 == 512/4 == 256 blocks 
        ///     256 x 16 ==  64x4 == 256/4 ==  64 blocks
        ///     128 x  8 ==  32x2 ==  64/4 ==  16 blocks
        ///      64 x  4 ==  16x1 ==  16/4 ==   4 blocks // 4 blocks less (begins under-allocating here)
        ///      32 x  2 ==   8x1 ==   8/4 ==   2 blocks // 2 blocks less
        ///      16 x  1 ==   4x1 ==   4/4 ==   1 block  // 1 block less
        ///     TOTAL:                        343 blocks // 7 blocks under-allocated
        /// </example>
        public static int GfzCmprBlocksEncodedCount(int pixelWidtth, int pixelHeight)
        {
            // CMPR has a block size of 8x8, split into quadrants (2x2), in each we have
            // a 4x4 grid of pixels. CMPR /should/ use params (8, 8), but instead uses the
            // quadrant size (4, 4) instead.
            int nBlocks4x4 = Encoding.GetTotalBlocksToEncode(pixelWidtth, pixelHeight, 4, 4);
            // The number of blocks we get out is now 4 times the size since we specify a block
            // as only one quarter (1/4) the resolution. To compensate and convert to comparitive
            // terms with other blocks, we divide by 4.
            int nBlocks8x8 = (int)Math.Ceiling(nBlocks4x4 / 4f);
            return nBlocks8x8;
        }

        /// <summary>
        ///     Provides the amount of blocks the game thinks it needs to encode a <paramref name="pixelWidth"/> by
        ///     <paramref name="pixelHeight"/> sized texture using <paramref name="encoding"/>.
        /// </summary>
        /// <param name="pixelWidth">The pixel width of the texture to encode.</param>
        /// <param name="pixelHeight">The pixel height of the texture to encode.</param>
        /// <param name="encoding">Which block encoding was used to encode blocks. </param>
        /// <returns>
        ///     The number of blocks encoded by GFZ for a texture of <paramref name="pixelWidth"/> by
        ///     <paramref name="pixelHeight"/> size using its own <paramref name="encoding"/> format.
        /// </returns>
        public static int GetGfzBlocksEncodedCount(int pixelWidth, int pixelHeight, Encoding encoding)
        {
            bool isCmprTexture = encoding.Format == TextureFormat.CMPR;

            // Calculate the amount of blocks required to store image in encoding/format
            int blocksRequiredForTexture = isCmprTexture
                ? GfzCmprBlocksEncodedCount(pixelWidth, pixelHeight)
                : encoding.GetTotalBlocksToEncode(pixelWidth, pixelHeight);

            return blocksRequiredForTexture;
        }

        /// <summary>
        ///     Provides the amount of blocks the game used to encode <paramref name="textureSeriesDescription"/>'s
        ///     texture data using <paramref name="encoding"/>.
        /// </summary>
        /// <param name="textureSeriesDescription"></param>
        /// <returns>
        /// 
        /// </returns>
        public static int GetTotalBlocksEncodedCount(TextureSeriesDescription textureSeriesDescription)
        {
            var encoding = Encoding.GetEncoding(textureSeriesDescription.TextureFormat);
            int pixelWidth = textureSeriesDescription.Width;
            int pixelHeight = textureSeriesDescription.Height;
            int numTextures = textureSeriesDescription.NumberOfTextures;
            int totalBlocksRead = 0;

            for (int i = 0; i < numTextures; i++)
            {
                // Once mipmap reaches any side length of 0, there are no more mipmaps
                bool isInvalidSizeW = pixelWidth <= 0;
                bool isInvalidSizeH = pixelHeight <= 0;
                if (isInvalidSizeW && isInvalidSizeH)
                    break;

                // Calculate the amount of blocks required to store image in encoding/format
                int blocksRequiredForTexture = GetGfzBlocksEncodedCount(pixelWidth, pixelHeight, encoding);
                totalBlocksRead += blocksRequiredForTexture;

                // Halve image dimensions per axis -> next mip size
                pixelWidth >>= 1;
                pixelHeight >>= 1;
            }

            return totalBlocksRead;
        }

        /// <summary>
        ///     Save blocks to texture even if insufficient amount of <paramref name="directBlocks"/> are provided.
        ///     Unset pixels will be <paramref name="defaultColor"/>.
        /// </summary>
        /// <param name="directBlocks">The blocks used to reconstruct the texture.</param>
        /// <param name="blocksWidth">The width of the texture in blocks.</param>
        /// <param name="blocksHeight">The height of the texture in blocks.</param>
        /// <param name="defaultColor">The default color of unset pixels.</param>
        /// <returns>
        ///     A texture whose unset pixels are <paramref name="defaultColor"/>.
        /// </returns>
        public static Texture GfzFromPartialDirectBlocks(DirectBlock[] directBlocks, int blocksWidth, int blocksHeight, TextureColor defaultColor)
        {
            int subBlockWidth = directBlocks[0].Width;
            int subBlockHeight = directBlocks[0].Height;
            var format = directBlocks[0].Format;

            int pixelsWidth = blocksWidth * subBlockWidth;
            int pixelsHeight = blocksHeight * subBlockHeight;
            var texture = new Texture(pixelsWidth, pixelsHeight, defaultColor, format);

            int pixelIndex = 0;
            // Linearize texture pixels
            for (int h = 0; h < blocksHeight; h++)
            {
                for (int y = 0; y < subBlockHeight; y++)
                {
                    for (int w = 0; w < blocksWidth; w++)
                    {
                        // Which block we are sampling
                        int blockIndex = w + h * blocksWidth;
                        for (int x = 0; x < subBlockWidth; x++)
                        {
                            // If we don't have this block, skip.
                            // This is kinda hacky, but useful for GFZ
                            if (blockIndex >= directBlocks.Length)
                            {
                                pixelIndex++;
                                continue;
                            }

                            // Which sub-block we are sampling
                            int colorIndex = x + y * subBlockWidth;
                            var block = directBlocks[blockIndex];
                            var color = block.Colors[colorIndex];
                            texture.Pixels[pixelIndex++] = color;
                        }
                    }
                }
            }
            return texture;
        }

    }
}
