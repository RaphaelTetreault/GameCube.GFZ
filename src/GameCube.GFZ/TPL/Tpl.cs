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
        private Pointer textureDescriptionsEndPtr;
        private TextureDescription[] textureDescriptions;

        public Endianness Endianness => Endianness.BigEndian;
        public string FileExtension => ".tpl";
        public string FileName { get; set; }

        public int TextureDescriptionsSize { get; set; }
        public int NumTextures { get; set; }

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref textureDescriptionsEndPtr);
            TextureDescriptionsSize = textureDescriptionsEndPtr.address - 4; // 4 bytes
            NumTextures = TextureDescriptionsSize / TextureDescription.Size;
            reader.Read(ref textureDescriptions, NumTextures);
            // TODO
            // read padding GX aligned incremental

        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }

}
