using GameCube.GX;
using GameCube.GX.Texture;
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
        private TextureDescription[] textureDescriptions;
        private TextureSeries[] textureSeries;

        public Endianness Endianness => endianness;
        public string FileExtension => ".tpl";
        public string FileName { get; set; }

        public TextureDescription[] TextureDescriptions => textureDescriptions;
        public TextureSeries[] TextureSeries => textureSeries;

        private static readonly TextureColor Magenta = new TextureColor(255, 0, 255);

        public void Deserialize(EndianBinaryReader reader)
        {
            // Read `count` texture descriptions
            reader.Read(ref textureDescriptionsCount);
            reader.Read(ref textureDescriptions, textureDescriptionsCount);
            textureSeries = new TextureSeries[textureDescriptionsCount];

            // File has padding after descriptions that increment continuously.
            int paddingLength = (int)StreamExtensions.GetLengthOfAlignment(reader.BaseStream, GXUtility.GX_FIFO_ALIGN);
            var padding = reader.ReadBytes(paddingLength);
            for (byte i = 0; i < padding.Length; i++)
                Assert.IsTrue(padding[i] == i);

            for (int i = 0; i < textureDescriptions.Length; i++)
            {
                var textureDescription = textureDescriptions[i];
                if (textureDescription.IsNull)
                    continue;

                // Somet TPLs come with garbage data in the first 0x30 bytes of the file (in the first 4 bytes of affected descriptions).
                // Make sure the null check above never fails. Otherwise you need to find a different way to check for this garbage.
                var msg = $"Uncaught garbage entry {FileName} entry {i} addr {textureDescription.AddressRange.PrintStartAddress()}";
                Assert.IsFalse(textureDescription.IsGarbageEntry, msg);

                // Get encoding and ensure it comforms to expectations. No indirect textures are used (only GFZJ tested).
                var encoding = Encoding.GetEncoding(textureDescription.TextureFormat);
                Assert.IsTrue(encoding.IsDirect, "Encoding is not direct. GFZ does not use (handle?) Indirect modes.");

                // Assert game uses all power-of-two textures.
                int isWidthPowerOfTwo = textureDescription.Width % encoding.BlockWidth;
                int isHeightPowerOfTwo = textureDescription.Height % encoding.BlockHeight;
                Assert.IsTrue(isWidthPowerOfTwo == 0);
                Assert.IsTrue(isHeightPowerOfTwo == 0);

                // Read the texture series.
                reader.JumpToAddress(textureDescription.TexturePtr);
                textureSeries[i] = ReadDirectTextureSeries(reader, textureDescription, encoding);
                // Record some useful metadata about this texture
                textureSeries[i].AddressRange = new AddressRange()
                {
                    startAddress = textureDescription.TexturePtr,
                    endAddress = reader.GetPositionAsPointer(),
                };
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Create a new texture series that has main texture and mipmaps.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="numMipmaps"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static TextureSeries CreateTextureSeries(Texture texture, int numMipmaps)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads texture and all associated mipmaps.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="textureDescription"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static TextureSeries ReadDirectTextureSeries(EndianBinaryReader reader, TextureDescription textureDescription, Encoding encoding)
        {
            int pixelWidth = textureDescription.Width;
            int pixelHeight = textureDescription.Height;
            int numTextures = textureDescription.NumberOfTextures;
            var textureSeries = new TextureSeries(numTextures);

            int totalBlocksEncoded = GetTotalBlocksEncoded(textureDescription, encoding);
            int totalBlocksRead = 0;

            for (int i = 0; i < textureSeries.TextureData.Length; i++)
            {
                // Some textures have invalid mipmap sizes. Prevent them from doing anything.
                bool isInvalidTextureSize = pixelWidth == 0 || pixelHeight == 0;
                if (isInvalidTextureSize)
                {
                    textureSeries[i].Texture = new Texture(1, 1, Magenta, textureDescription.TextureFormat);
                    pixelWidth >>= 1;
                    pixelHeight >>= 1;
                    continue;
                }

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
                        var invalidDirectBlocks = encoding.ReadBlocks<DirectBlock>(reader, blocksInStream, encoding);
                        totalBlocksRead += blocksRequired;
                        // Create an invalid texture. Unset pixels will be magenta.
                        var invalidTexture = GfzFromPartialDirectBlocks(invalidDirectBlocks, widthBlocks, heightBlocks, Magenta);
                        textureSeries[i].Texture = Texture.Crop(invalidTexture, pixelWidth, pixelHeight);
                    }
                    else
                    {
                        // If no more blocks to read, make fully magenta texture.
                        textureSeries[i].Texture = new Texture(pixelWidth, pixelHeight, Magenta, textureDescription.TextureFormat);
                    }
                    textureSeries[i].IsValid = false;

                    pixelWidth >>= 1;
                    pixelHeight >>= 1;
                    continue;
                }
               
                // If we succeed, proceed to deserialize blocks for texture.
                //var directBlocks = encoding.ReadBlocks<DirectBlock>(reader, blocksW, blocksH, encoding);
                var directBlocks = encoding.ReadBlocks<DirectBlock>(reader, blocksRequired, encoding);
                Assert.IsTrue(blocksRequired != 0);
                Assert.IsTrue(directBlocks.Length == blocksRequired);
                Assert.IsTrue(widthBlocks*heightBlocks == blocksRequired);
                totalBlocksRead += blocksRequired;

                // Make new texture. Crop it to width/height on occasions where pixel width or height
                // is lesser than the block size.
                var texture = Texture.FromDirectBlocks(directBlocks, widthBlocks, heightBlocks);
                textureSeries[i].Texture = Texture.Crop(texture, pixelWidth, pixelHeight);
                textureSeries[i].IsValid = true;

                // Halve the size for the next mipmap.
                pixelWidth >>= 1;
                pixelHeight >>= 1;
            }
            return textureSeries;
        }

        /// <summary>
        /// GFZ has an error where it computes the number of blocks needed to encode a texture. This method
        /// provides the amount of blocks the game thinks needed to be encoded for CMPR.
        /// incorrecly.
        /// </summary>
        /// <param name="widthPixels"></param>
        /// <param name="heightPixels"></param>
        /// <returns></returns>
        public static int GfzCmprBlocksEncoded(int widthPixels, int heightPixels)
        {
            // CMPR has a block size of 8x8, split into quadrants (2x2), in each we have
            // a 4x4 grid of pixels. CMPR /should/ use params (8, 8), but instead uses the
            // quadrant size 4x4 instead.
            int nBlocks4x4 = Encoding.GetTotalBlocksToEncode(widthPixels, heightPixels, 4, 4);
            // The number of blocks we get out is now 4 times the size since we specify a block
            // as only one quarter (1/4) the resolution. To compensate and convert to comparitive
            // terms with other blocks, we divide by 4.
            int nBlocks8x8 = (int)Math.Ceiling(nBlocks4x4 / (float)4);
            return nBlocks8x8;

            // Note that this will begin to fail on widths/height below 8 when width and height
            // are different. Notably this happens with mipmaps.

            // EXAMPLES. Note values in divisions are clamped upwards to fit existing pixels in a block
            // of greater size. ei: 1x1 pixels still takes one 8x8 block, the rest is just padding.
            //
            // Image.
            // Sample A size: 16x16 pixels. (square)
            // Sample B size: 64x4 pixels. (rectangle)
            //
            // CMPR block size is 8x8.
            // A: 16/8 * 16/8 == 2 * 2 == 4 blocks required.
            // B: 64/8 *  4/8 == 8 * 1 == 8 blocks required.
            //
            // GFZ CMPR block size is 4x4
            // A: 16/4 * 16/4 ==  4 * 4 == 16 blocks.
            // B: 64/4 *  4/4 == 16 * 1 == 16 blocks required.
            // However, the game knows this is incorrect and tries to adjust the value.
            // A: 16 blocks / 4 = 4 blocks required (hey, correct!)
            // B: 16 blocks / 4 = 4 blocks (uh-oh, that's half what we need!)
            //
            // Because of this, GFZ regularly under-allocates space for rectangular CMPR
            // textures. The game calculates the full size it thinks a CMPR with mipmaps
            // will take and pixel data up until the buffer is full (prematurely) and then
            // stops. This results in the odd behaviour of some mipmaps being only partially
            // writting (say, 3/4 of the testure), the the rest is another texture's data.
            //
            // Below is a breakdown of what a 512x32 texture would require in terms of blocks.
            // You can see the algorithm begin to fail when we get to below 8 pixels in w or h.
            //
            // CMPR 8x8 block requirement
            //  pixels  ||    blocks
            // 512 x 32 == 32x4 == 64 blocks
            // 256 x 16 == 16x2 == 16 blocks
            // 128 x  8 ==  8x1 ==  8 blocks
            //  64 x  4 ==  4x1 ==  4 blocks
            //  32 x  2 ==  2x1 ==  2 blocks
            //  16 x  1 ==  1x1 ==  1 block
            // TOTAL:              95 blocks
            //
            // GFZ CMPR 4x4 block "requirement"
            //  pixels  ||    blocks
            // 512 x 32 == 64x8 == 256/4 == 64 blocks 
            // 256 x 16 == 32x4 == 128/4 == 16 blocks
            // 128 x  8 == 16x2 ==  32/4 ==  8 blocks
            //  64 x  4 ==  8x1 ==   8/4 ==  2 blocks
            //  32 x  2 ==  4x1 ==   4/4 ==  1 block
            //  16 x  1 ==  2x1 ==   1/1 ==  1 block
            // TOTAL:                       92 blocks
        }

        /// <summary>
        /// Provides the amount of blocks the game thinks needed to be encoded for <paramref name="encoding"/>.
        /// </summary>
        /// <param name="pixelWidth"></param>
        /// <param name="pixelHeight"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static int GetGfzBlocksEncoded(int pixelWidth, int pixelHeight, Encoding encoding)
        {
            bool isCmprTexture = encoding.Format == TextureFormat.CMPR;

            // Calculate the amount of blocks required to store image in encoding/format
            int blocksRequiredForTexture = isCmprTexture
                ? GfzCmprBlocksEncoded(pixelWidth, pixelHeight)
                : encoding.GetTotalBlocksToEncode(pixelWidth, pixelHeight);

            return blocksRequiredForTexture;
        }

        /// <summary>
        /// Provides total amount of blocks the game thinks needed to be encoded for <paramref name="encoding"/>.
        /// </summary>
        /// <param name="textureDescription"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static int GetTotalBlocksEncoded(TextureDescription textureDescription, Encoding encoding)
        {
            int pixelWidth = textureDescription.Width;
            int pixelHeight = textureDescription.Height;
            int numTextures = textureDescription.NumberOfTextures;
            int totalBlocksRead = 0;

            for (int i = 0; i < numTextures; i++)
            {
                // Once mipmap reaches any side length of 0, there are no more mipmaps
                bool isInvalidSizeW = pixelWidth <= 0;
                bool isInvalidSizeH = pixelHeight <= 0;
                if (isInvalidSizeW && isInvalidSizeH)
                    break;

                // Calculate the amount of blocks required to store image in encoding/format
                int blocksRequiredForTexture = GetGfzBlocksEncoded(pixelWidth, pixelHeight, encoding);
                totalBlocksRead += blocksRequiredForTexture;

                // Halve image dimensions per axis -> next mip size
                pixelWidth >>= 1;
                pixelHeight >>= 1;
            }

            return totalBlocksRead;
        }

        /// <summary>
        /// Save blocks to texture even if insufficient amount of <paramref name="directBlocks"/> provided.
        /// Unset pixels will be <paramref name="defaultColor"/>.
        /// </summary>
        /// <param name="directBlocks"></param>
        /// <param name="blocksWidth"></param>
        /// <param name="blocksHeight"></param>
        /// <param name="defaultColor"></param>
        /// <returns></returns>
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
