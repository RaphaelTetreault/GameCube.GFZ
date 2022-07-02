using GameCube.GX;
using GameCube.GX.Texture;
using Manifold;
using Manifold.IO;
using System;
using System.Collections.Generic;


namespace GameCube.GFZ.TPL
{
    [Serializable]
    public class Tpl :
        IBinaryFileType,
        IBinarySerializable
    {
        private int textureDescriptionsCount;
        private TextureDescription[] textureDescriptions;
        private readonly List<Texture[]> textureAndMipmaps = new();

        public Endianness Endianness => Endianness.BigEndian;
        public string FileExtension => ".tpl";
        public string FileName { get; set; }

        public int TextureDescriptionsSize { get; set; }
        public int NumTextures { get; set; }
        public Texture[][] TextureAndMipmaps => textureAndMipmaps.ToArray();

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref textureDescriptionsCount);
            TextureDescriptionsSize = textureDescriptionsCount; // 4 bytes
            reader.Read(ref textureDescriptions, TextureDescriptionsSize);
            
            int paddingLength = (int)StreamExtensions.GetLengthOfAlignment(reader.BaseStream, GXUtility.GX_FIFO_ALIGN);
            var padding = reader.ReadBytes(paddingLength);
            for (byte i = 0; i < padding.Length; i++)
                Assert.IsTrue(padding[i] == i);

            int texIndex = 0;
            foreach (var textureDescription in textureDescriptions)
            {
                if (textureDescription.IsNull)
                    continue;

                if (textureDescription.IsGarbageEntry)
                {
                    DebugConsole.Log($"Uncaught garbage entry {FileName} entry {texIndex} addr {textureDescription.AddressRange.PrintStartAddress()}");
                    continue;
                }

                var encoding = Encoding.GetEncoding(textureDescription.TextureFormat);

                int blocksW = textureDescription.Width / encoding.BlockWidth;
                int blocksH = textureDescription.Height / encoding.BlockHeight;
                int mipmapCount = textureDescription.MipmapLevels;

                int isWidthPowerOfTwo = textureDescription.Width % encoding.BlockWidth;
                int isHeightPowerOfTwo = textureDescription.Height % encoding.BlockHeight;
                Assert.IsTrue(isWidthPowerOfTwo == 0);
                Assert.IsTrue(isHeightPowerOfTwo == 0);

                reader.JumpToAddress(textureDescription.TexturePtr);

                if (encoding.IsDirect)
                {
                    try
                    {
                        var textureAndMipmaps = ReadDirectTextures(reader, textureDescription, encoding);
                        this.textureAndMipmaps.Add(textureAndMipmaps);
                    }
                    catch (Exception ex)
                    {
                        DebugConsole.Log($"Errored on {FileName} on texture {texIndex} addr({textureDescription.AddressRange.PrintStartAddress()})");
                        //throw ex;
                    }
                    //var directBlocks = encoding.ReadBlocks<DirectBlock>(reader, blocksW, blocksH, encoding);
                    //var texture = Texture.FromDirectBlocks(directBlocks, blocksW, blocksH);
                    //textures.Add(texture);
                }
                if (encoding.IsIndirect)
                {
                    throw new NotImplementedException();
                }
                texIndex++;
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }


        private Texture[] ReadDirectTextures(EndianBinaryReader reader, TextureDescription textureDescription, Encoding encoding)
        {
            int pixelWidth = textureDescription.Width;
            int pixelHeight = textureDescription.Height;
            int numTextures = textureDescription.NumberOfTextures;
            var textures = new Texture[numTextures];
            for (int i = 0; i < textures.Length; i++)
            {
                bool isCMPR = textureDescription.TextureFormat == TextureFormat.CMPR;
                bool isMipmap = i > 0;
                if (isCMPR && isMipmap)
                {
                    bool isNotSquare = pixelWidth != pixelHeight;
                    if (isNotSquare)
                    {
                        //float blockSizeRatio = (float)(blocksW * blocksH) / (parentW * parentH);
                        //bool isQuarterSizeOrLess = blockSizeRatio > 0.25f;
                        //bool isCmprErrorW = pixelWidth / 32f < 0.5f;
                        //bool isCmprErrorH = pixelHeight / 32f < 0.5f;
                        //bool isCmprError = isCmprErrorW || isCmprErrorH;

                        if (false)
                        {
                            // Texture stays null
                            //textures[i] = new Texture(1, 1, TextureFormat.CMPR);
                            pixelWidth >>= 1;
                            pixelHeight >>= 1;
                            continue;
                        }
                    }
                }

                int blocksW = (int)Math.Ceiling((double)pixelWidth / encoding.BlockWidth);
                int blocksH = (int)Math.Ceiling((double)pixelHeight / encoding.BlockHeight);

                try
                {
                    var directBlocks = encoding.ReadBlocks<DirectBlock>(reader, blocksW, blocksH, encoding);
                    var texture = Texture.FromDirectBlocks(directBlocks, blocksW, blocksH);
                    textures[i] = Texture.Crop(texture, pixelWidth, pixelHeight);
                }
                catch (Exception ex)
                {
                    DebugConsole.Log("Errored on texture mipmap.");
                }

                pixelWidth >>= 1;
                pixelHeight >>= 1;
            }
            return textures;
        }

        //public static int CalcRequiredBlocks(TextureDescription textureDescription, Encoding encoding)
        //{
        //    int blocksRequired = 0;

        //    int pixelWidth = textureDescription.Width;
        //    int pixelHeight = textureDescription.Height;

        //    for (int i = 0; i < textureDescription.NumberOfTextures; i++)
        //    {
        //        int blocksW = (int)Math.Ceiling((double)pixelWidth / encoding.BlockWidth);
        //        int blocksH = (int)Math.Ceiling((double)pixelHeight / encoding.BlockHeight);
        //        blocksRequired += blocksW * blocksH;

        //        // mipmap below is halved
        //        pixelWidth >>= 1;
        //        pixelHeight >>= 1;
        //    }

        //    return blocksRequired;
        //}

        //public static int CalcBytesRequired(TextureDescription textureDescription, Encoding encoding)
        //{
        //    int bytesRequired = CalcRequiredBlocks(textureDescription, encoding);
        //    bytesRequired *= encoding.BytesPerBlock;
        //    return bytesRequired;
        //}

    }

}
