﻿using Manifold;
using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Configures how a texcture is meant to be processed by the GameCube GX.
    /// </summary>
    public class TextureConfig :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private TexFlags0x00 unk0x00;
        private MipmapSetting mipmapSetting;
        private TextureWrapMode wrapMode;
        private ushort tplTextureIndex;
        private TexFlags0x06 unk0x06;
        private GXAnisotropy anisotropicFilter;
        private uint zero0x08;
        private TexFlags0x0C unk0x0C;
        private bool isSwappableTexture; // perhaps a "cache texture" flag
        private ushort configIndex;
        private TexFlags0x10 unk0x10;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public TexFlags0x00 Unk0x00 { get => unk0x00; set => unk0x00 = value; }
        public MipmapSetting MipmapSetting { get => mipmapSetting; set => mipmapSetting = value; }
        public TextureWrapMode WrapMode { get => wrapMode; set => wrapMode = value; }
        public ushort TplTextureIndex { get => tplTextureIndex; set => tplTextureIndex = value; }
        public TexFlags0x06 Unk0x06 { get => unk0x06; set => unk0x06 = value; }
        public GXAnisotropy AnisotropicFilter { get => anisotropicFilter; set => anisotropicFilter = value; }
        public uint Zero0x08 { get => zero0x08; set => zero0x08 = value; }
        public TexFlags0x0C Unk0x0C { get => unk0x0C; set => unk0x0C = value; }
        public bool IsSwappableTexture { get => isSwappableTexture; set => isSwappableTexture = value; }
        public ushort ConfigIndex { get => configIndex; set => configIndex = value; }
        public TexFlags0x10 Unk0x10 { get => unk0x10; set => unk0x10 = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref unk0x00);
                reader.Read(ref mipmapSetting);
                reader.Read(ref wrapMode);
                reader.Read(ref tplTextureIndex);
                reader.Read(ref unk0x06);
                reader.Read(ref anisotropicFilter);
                reader.Read(ref zero0x08);
                reader.Read(ref unk0x0C);
                reader.Read(ref isSwappableTexture);
                reader.Read(ref configIndex);
                reader.Read(ref unk0x10);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero0x08 == 0);
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                Assert.IsTrue(zero0x08 == 0);
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(unk0x00);
                writer.Write(mipmapSetting);
                writer.Write(wrapMode);
                writer.Write(tplTextureIndex);
                writer.Write(unk0x06);
                writer.Write(anisotropicFilter);
                writer.Write(zero0x08);
                writer.Write(unk0x0C);
                writer.Write(isSwappableTexture);
                writer.Write(configIndex);
                writer.Write(unk0x10);
            }
            this.RecordEndAddress(writer);
        }

    }

}