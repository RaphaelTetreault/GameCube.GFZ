using GameCube.GX.Texture;
using Manifold;
using Manifold.IO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameCube.GFZ.TPL
{

    [Serializable]
    public class TextureDescription :
        IBinaryAddressable,
        IBinarySerializable
    {
        public const ushort k0x1234 = 0x1234;
        public const int Size = 0x14; // 20 bytes

        private ushort const_zero;
        private bool isNull;
        private TextureFormat textureFormat; // 1 byte
        private Pointer texturePtr;
        private ushort width;
        private ushort height;
        private ushort mipmapCount;
        private ushort const_0x1234;

        public bool IsGarbageEntry => const_zero != 0;
        public bool IsNull => isNull;
        public TextureFormat TextureFormat => textureFormat;
        public Pointer TexturePtr => texturePtr;
        public ushort Width => width;
        public ushort Height => height;
        public ushort MipmapLevels => mipmapCount;

        /// <summary>
        /// Number of textures supposed to be stored given mipmap levels
        /// </summary>
        public int NumberOfTextures => MipmapLevels != 0 ? MipmapLevels : 1;


        public AddressRange AddressRange { get; set; }

        public int ComputeSize()
        {
            throw new NotImplementedException();
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref const_zero);
                reader.Read(ref isNull);
                reader.Read(ref textureFormat);
                reader.Read(ref texturePtr);
                reader.Read(ref width);
                reader.Read(ref height);
                reader.Read(ref mipmapCount);
                reader.Read(ref const_0x1234);
            }
            this.RecordEndAddress(reader);
            {
                //Assert.IsTrue(const_zero == 0);
                Assert.IsTrue(const_0x1234 == k0x1234);
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                Assert.IsTrue(const_zero == 0);
                Assert.IsTrue(const_0x1234 == k0x1234);
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(const_zero);
                writer.Write(isNull);
                writer.Write(textureFormat);
                writer.Write(texturePtr);
                writer.Write(width);
                writer.Write(height);
                writer.Write(mipmapCount);
                writer.Write(const_0x1234);
            }
            this.RecordEndAddress(writer);
        }
    }
}
