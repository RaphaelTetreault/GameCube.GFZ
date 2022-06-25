using Manifold;
using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Configures how a texcture is meant to be processed by the GameCube GX.
    /// </summary>
    /// <remarks>
    /// SMB decomp: GMATevLayer
    /// </remarks>
    public class TextureConfig :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private TexFlags0x00 unk0x00;
        private MipmapSetting mipmapSetting;
        private TextureWrapMode wrapMode;
        private ushort tplTextureIndex;
        private sbyte lodBias;
        private GXAnisotropy anisotropicFilter;
        private uint zero0x08; // GXTexObj*, ei: ptr
        private byte unk0x0C; // 2022/06/23: all possible values 0-256. 0 is most common (~50%).
        private bool isSwappableTexture; // perhaps a "cache texture" flag
        private ushort configIndex;
        private ushort zero0x10;
        private TexFlags0x10 unk0x12;

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public TexFlags0x00 Unk0x00 { get => unk0x00; set => unk0x00 = value; }
        public MipmapSetting MipmapSetting { get => mipmapSetting; set => mipmapSetting = value; }
        public TextureWrapMode WrapMode { get => wrapMode; set => wrapMode = value; }
        public ushort TplTextureIndex { get => tplTextureIndex; set => tplTextureIndex = value; }
        public sbyte LodBias { get => lodBias; set => lodBias = value; }
        public GXAnisotropy AnisotropicFilter { get => anisotropicFilter; set => anisotropicFilter = value; }
        public uint Zero0x08 { get => zero0x08; set => zero0x08 = value; }
        public byte Unk0x0C { get => unk0x0C; set => unk0x0C = value; }
        public bool IsSwappableTexture { get => isSwappableTexture; set => isSwappableTexture = value; }
        public ushort ConfigIndex { get => configIndex; set => configIndex = value; }
        public ushort Zero0x10 { get => zero0x10; set => zero0x10 = value; }
        public TexFlags0x10 Unk0x12 { get => unk0x12; set => unk0x12 = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref unk0x00);
                reader.Read(ref mipmapSetting);
                reader.Read(ref wrapMode);
                reader.Read(ref tplTextureIndex);
                reader.Read(ref lodBias);
                reader.Read(ref anisotropicFilter);
                reader.Read(ref zero0x08);
                reader.Read(ref unk0x0C);
                reader.Read(ref isSwappableTexture);
                reader.Read(ref configIndex);
                reader.Read(ref zero0x10);
                reader.Read(ref unk0x12);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero0x08 == 0);
                Assert.IsTrue(zero0x10 == 0);
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                Assert.IsTrue(zero0x08 == 0);
                Assert.IsTrue(zero0x10 == 0);
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(unk0x00);
                writer.Write(mipmapSetting);
                writer.Write(wrapMode);
                writer.Write(tplTextureIndex);
                writer.Write(lodBias);
                writer.Write(anisotropicFilter);
                writer.Write(zero0x08);
                writer.Write(unk0x0C);
                writer.Write(isSwappableTexture);
                writer.Write(configIndex);
                writer.Write(zero0x10);
                writer.Write(unk0x12);
            }
            this.RecordEndAddress(writer);
        }

    }

}