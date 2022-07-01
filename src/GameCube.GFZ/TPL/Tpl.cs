using GameCube.GX.Texture;
using Manifold;
using Manifold.IO;
using System;
using System.Collections;
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
        private List<Texture> textures = new List<Texture>();

        public Endianness Endianness => Endianness.BigEndian;
        public string FileExtension => ".tpl";
        public string FileName { get; set; }

        public int TextureDescriptionsSize { get; set; }
        public int NumTextures { get; set; }
        public Texture[] Textures => textures.ToArray();

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref textureDescriptionsCount);
            TextureDescriptionsSize = textureDescriptionsCount; // 4 bytes
            reader.Read(ref textureDescriptions, TextureDescriptionsSize);

            foreach (var textureDescription in textureDescriptions)
            {
                if (textureDescription.IsNull)
                    continue;

                var encoding = Encoding.GetEncoding(textureDescription.TextureFormat);
                int blocksW = textureDescription.Width / encoding.BlockWidth;
                int blocksH = textureDescription.Height / encoding.BlockHeight;

                reader.JumpToAddress(textureDescription.TexturePtr);

                if (encoding.IsDirect)
                {
                    var directBlocks = encoding.ReadBlocks<DirectBlock>(reader, blocksW, blocksH, encoding);
                    var texture = Texture.FromDirectBlocks(directBlocks, blocksW, blocksH);
                    textures.Add(texture);
                }
                if (encoding.IsIndirect)
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }

}
