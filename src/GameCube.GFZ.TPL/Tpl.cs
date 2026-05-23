using GameCube.GX;
using GameCube.GX.Texture;
using Manifold.IO;
using System;

namespace GameCube.GFZ.TPL;

/// <summary>
///     GFZ custom Texture Palette (TPL).
/// </summary>
public class Tpl :
    IBinarySerializable
{
    // CONSTS
    private static readonly TexturePixel Magenta = new(255, 0, 255);

    // MEMBERS
    private int textureDescriptionsCount;
    private TextureSequenceDescription[] textureSequencesDescription = [];
    private TextureSequence[] textureSequences = [];

    // PROPERTIES
    public TextureSequenceDescription[] TextureSequenceDescriptions { get => textureSequencesDescription; set => textureSequencesDescription = value; }
    public TextureSequence[] TextureSequences { get => textureSequences; set => textureSequences = value; }


    public void Deserialize(EndianBinaryReader reader)
    {
        // Read `count` texture descriptions
        reader.Read(ref textureDescriptionsCount);
        reader.Read(ref textureSequencesDescription, textureDescriptionsCount);
        textureSequences = new TextureSequence[textureDescriptionsCount];

        // File has padding after descriptions that increment continuously.
        int paddingLength = (int)StreamExtensions.GetLengthOfAlignment(reader.BaseStream, GXUtility.GX_FIFO_ALIGN);
        var padding = reader.ReadBytes(paddingLength);
        for (byte i = 0; i < padding.Length; i++)
            Assert.IsTrue(padding[i] == i);

        for (int i = 0; i < textureSequencesDescription.Length; i++)
        {
            var textureSequenceDescription = this.textureSequencesDescription[i];
            if (textureSequenceDescription.IsNull)
                continue;

            // Some TPLs come with garbage data in the first 0x30 bytes of the file (in the first 4 bytes of affected descriptions).
            // Make sure the null check above never fails. Otherwise you need to find a different way to check for this garbage.
            var msg1 = $"Uncaught garbage entry {i} addr {textureSequenceDescription.AddressRange.PrintStartAddress()}";
            Assert.IsFalse(textureSequenceDescription.IsGarbageEntry, msg1);

            // Get encoding and ensure it comforms to expectations. No indirect textures are used (only GFZJ tested).
            DirectTextureFormat format = textureSequenceDescription.TextureFormat;
            format.Validate();
            var encoding = DirectEncoding.MapDirectFormatToEncoding[format];

            // Assert game uses all power-of-two textures.
            int isWidthPowerOfTwo = textureSequenceDescription.Width % encoding.BlockPixelWidth;
            int isHeightPowerOfTwo = textureSequenceDescription.Height % encoding.BlockPixelHeight;
            if (isWidthPowerOfTwo != 0 || isHeightPowerOfTwo != 0)
            {
                string msg3 =
                    $"Texture index {i} has size (x:{textureSequenceDescription.Width}, " +
                    $"y:{textureSequenceDescription.Height}). Not a power of two.";
                Assert.IsTrue(false, msg3);
            }

            // Read the texture sequence.
            reader.JumpToAddress(textureSequenceDescription.TextureSequencePtr);
            textureSequences[i] = ReadDirectTextureSequence(reader, textureSequenceDescription);
            // Record some useful metadata about this texture
            textureSequences[i].AddressRange = new AddressRange()
            {
                startAddress = textureSequenceDescription.TextureSequencePtr,
                endAddress = reader.GetPositionAsPointer(),
            };
        }
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        // Write texture sequence descriptions
        writer.Write(TextureSequenceDescriptions.Length);
        writer.Write(TextureSequenceDescriptions);

        // Write incrementing padding
        int paddingLength = (int)StreamExtensions.GetLengthOfAlignment(writer.BaseStream, GXUtility.GX_FIFO_ALIGN);
        for (byte i = 0; i < paddingLength; i++)
            writer.Write(i);

        // Write each texture of each texture sequence
        foreach (var textureSequence in textureSequences)
        {
            // Skip any null sequence
            // TODO: consider making the entries in the array zeroed rather than leave true nulls around.
            if (textureSequence is null)
                continue;

            DirectTextureFormat format = textureSequence.Description.TextureFormat;
            var directEncoding = DirectEncoding.MapDirectFormatToEncoding[format];
            foreach (var entry in textureSequence.Elements)
                Texture.WriteDirectColorTexture(writer, entry.Texture, format);
        }
    }

    /// <summary>
    /// Reads texture and all associated mipmaps.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="textureSequenceDescription"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static TextureSequence ReadDirectTextureSequence(EndianBinaryReader reader, TextureSequenceDescription textureSequenceDescription)
    {
        DirectTextureFormat format = textureSequenceDescription.TextureFormat;
        var directEncoding = DirectEncoding.MapDirectFormatToEncoding[format];
        int pixelWidth = textureSequenceDescription.Width;
        int pixelHeight = textureSequenceDescription.Height;
        var textureSequence = new TextureSequence(textureSequenceDescription);

        int totalBlocksEncoded = GetTotalBlocksEncodedCount(textureSequenceDescription);
        int totalBlocksRead = 0;

        for (int i = 0; i < textureSequence.Elements.Length; i++)
        {
            // Some textures have invalid mipmap sizes. Prevent them from doing anything.
            bool isInvalidTextureSize = pixelWidth == 0 || pixelHeight == 0;
            if (isInvalidTextureSize)
            {
                textureSequence.Elements[i].Texture = new Texture(0, 0);
                pixelWidth >>= 1;
                pixelHeight >>= 1;
                continue;
            }

            // Record where this texture is; for future use.
            AddressRange textureRange = new();
            textureRange.startAddress = reader.BaseStream.Position;

            // Make sure we can read that many blocks. Exceptions will occur on some CMPR textures.
            TextureBlocksInfo blocksInfo = TextureBlocksInfo.FromPixelDimensions(pixelWidth, pixelHeight, directEncoding);
            int blocksRequired = blocksInfo.BlockCount;
            bool canReadRequiredBlocks = (totalBlocksRead + blocksRequired) <= totalBlocksEncoded;
            if (!canReadRequiredBlocks)
            {
                // See how many blocks remain in the stream.
                int blocksInStream = totalBlocksEncoded - totalBlocksRead;
                bool canReadMoreBlocks = blocksInStream > 0;
                if (canReadMoreBlocks)
                {
                    // Read the remaining blocks
                    var invalidDirectBlocks = directEncoding.ReadBlocks(reader, blocksInStream);
                    totalBlocksRead += blocksRequired;
                    // Create an invalid texture. Unset pixels will be magenta.
                    // TODO: consider just making blank texture and discard the read blocks...
                    var invalidTexture = GfzFromPartialDirectBlocks(invalidDirectBlocks, blocksInfo, Magenta);
                    textureSequence.Elements[i].Texture = Texture.Crop(invalidTexture, pixelWidth, pixelHeight);
                }
                else
                {
                    // If no more blocks to read, make fully magenta texture.
                    textureSequence.Elements[i].Texture = new Texture(pixelWidth, pixelHeight, Magenta);
                }
                pixelWidth >>= 1;
                pixelHeight >>= 1;
                continue;
            }

            // If we succeed, proceed to deserialize blocks for texture.
            var directBlocks = directEncoding.ReadBlocks(reader, blocksRequired);
            Assert.IsTrue(blocksRequired != 0);
            Assert.IsTrue(directBlocks.Length == blocksRequired);
            totalBlocksRead += blocksRequired;

            // Make new texture. Crop it to width/height on occasions where pixel width or height
            // is lesser than the block size.
            var texture = Texture.FromDirectBlocks(directBlocks, blocksInfo);
            textureSequence.Elements[i].Texture = Texture.Crop(texture, pixelWidth, pixelHeight);
            textureSequence.Elements[i].IsValid = true;

            // Halve the size for the next mipmap.
            pixelWidth >>= 1;
            pixelHeight >>= 1;

            // Compute texture area in file, collect raw bytes
            textureRange.endAddress = reader.BaseStream.Position;
            reader.JumpToAddress(textureRange.startAddress);
            var bytes = reader.ReadBytes(textureRange.Size);
            reader.JumpToAddress(textureRange.endAddress);
            textureSequence.Elements[i].RawTextureData = bytes;

            // Compute CRC32 hash of texture data
            byte[] hashBytes = System.IO.Hashing.Crc32.Hash(bytes);
            // Convert to big endian (the good endianness; fight me)
            if (BitConverter.IsLittleEndian)
                Array.Reverse(hashBytes);
            uint crc32 = BitConverter.ToUInt32(hashBytes, 0);
            textureSequence.Elements[i].CRC32 = crc32;

            // Some mipmaps are "valid" but in fact are just black.
            // Catch those cases and flag validity afterwards.
            System.Collections.Generic.List<uint> invalidTextureCRC32s =
                [0xad550a19]; // 2x8 or 8x2 black
            bool isInvalid = invalidTextureCRC32s.Contains(crc32);
            textureSequence.Elements[i].IsValid = !isInvalid;
        }
        return textureSequence;
    }

    /// <summary>
    ///     GFZ has an error where it computes an incorrect number of blocks to encode a texture.
    ///     This method provides the amount of blocks the game thinks is needed to encoded for CMPR.
    /// </summary>
    /// <param name="pixelWidth">The pixel width of the texture to encode.</param>
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
    public static int GfzCmprBlocksEncodedCount(int pixelWidth, int pixelHeight)
    {
        // CMPR has a block size of 8x8, split into quadrants (2x2), in each we have
        // a 4x4 grid of pixels. CMPR /should/ use params (8, 8), but instead uses the
        // quadrant size (4, 4) instead.
        TextureBlocksInfo blocksInfo = TextureBlocksInfo.FromPixelDimensions(pixelWidth, pixelHeight, BadCmprEncoding);
        int nBlocks4x4 = blocksInfo.BlockCount;
        // The number of blocks we get out is now 4 times the size since we specify a block
        // as only one quarter (1/4) the resolution. To compensate and convert to comparitive
        // terms with other blocks, we divide by 4.
        int nBlocks8x8 = (int)Math.Ceiling(nBlocks4x4 / 4f);
        return nBlocks8x8;
    }

    private static readonly DirectEncoding BadCmprEncoding = new()
    {
        DirectFormat = DirectTextureFormat.CMPR,
        BlockPixelWidth = 4, // not 8!
        BlockPixelHeight = 4, // not 8!
        BitsPerPixel = 4,
        BytesPerBlock = 8, // 4 * 4 * 0.5(4bpp)
        ReadDirectBlock = null!, // We won't use this for actually reading anything
        WriteDirectBlock = null!, // We won't use this for actually writing anything
    };

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
    public static int GetGfzBlocksEncodedCount(int pixelWidth, int pixelHeight, DirectEncoding encoding)
    {
        bool isCmprTexture = encoding.Format == TextureFormat.CMPR;

        // Calculate the amount of blocks required to store image in encoding/format
        int blocksRequiredForTexture = isCmprTexture
            ? GfzCmprBlocksEncodedCount(pixelWidth, pixelHeight)
            : TextureBlocksInfo.FromPixelDimensions(pixelWidth, pixelHeight, encoding).BlockCount;

        return blocksRequiredForTexture;
    }

    /// <summary>
    ///     Provides the amount of blocks the game used to encode <paramref name="textureSequenceDescription"/>'s
    ///     texture data using <paramref name="encoding"/>.
    /// </summary>
    /// <param name="textureSequenceDescription"></param>
    /// <returns>
    /// 
    /// </returns>
    public static int GetTotalBlocksEncodedCount(TextureSequenceDescription textureSequenceDescription)
    {
        var encoding = DirectEncoding.MapDirectFormatToEncoding[textureSequenceDescription.TextureFormat];
        int pixelWidth = textureSequenceDescription.Width;
        int pixelHeight = textureSequenceDescription.Height;
        int numTextures = textureSequenceDescription.NumberOfTextures;
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
    public static Texture GfzFromPartialDirectBlocks(DirectBlock[] directBlocks, TextureBlocksInfo blocksInfo, TexturePixel defaultColor)
    {
        // Compute pixel sizes needed for texture constrained to block pixel size
        int width = Math.Max(blocksInfo.TexturePixelWidth, blocksInfo.BlockPixelWidth);
        int height = Math.Max(blocksInfo.TexturePixelHeight, blocksInfo.BlockPixelHeight);
        var texture = new Texture(width, height, defaultColor);
        int pixelIndex = 0;
        // Linearize texture pixels
        for (int h = 0; h < blocksInfo.BlockCountY; h++)
        {
            for (int y = 0; y < blocksInfo.BlockPixelWidth; y++)
            {
                for (int w = 0; w < blocksInfo.BlockCountX; w++)
                {
                    // Which block we are sampling
                    int blockIndex = w + h * blocksInfo.BlockCountX;
                    for (int x = 0; x < blocksInfo.BlockPixelWidth; x++)
                    {
                        // If we don't have this block, skip.
                        // This is kinda hacky, but useful for GFZ
                        if (blockIndex >= directBlocks.Length)
                        {
                            pixelIndex++;
                            continue;
                        }

                        // Which sub-block we are sampling
                        int colorIndex = x + y * blocksInfo.BlockPixelWidth;
                        var block = directBlocks[blockIndex];
                        var color = block.Pixels[colorIndex];
                        texture.Pixels[pixelIndex++] = color;
                    }
                }
            }
        }
        return texture;
    }

}
