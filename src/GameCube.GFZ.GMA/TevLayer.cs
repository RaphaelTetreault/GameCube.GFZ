using Manifold.IO;

namespace GameCube.GFZ.GMA;

/// <summary>
///     Layer for Texture Environment Unit (TEV). Configures how a texture is meant to be processed by the GameCube GX.
/// </summary>
public class TevLayer :
    IBinaryAddressable,
    IBinarySerializable
{
    // FIELDS
    private TevTextureFlags textureFlags;
    private ushort tplTextureIndex;
    private sbyte lodBias;
    private GXAnisotropy anisotropicFilter;
    private Pointer gxTextureObjectPtr; // Unused in GFZ, used by Super Monkey Ball
    private byte unk0x0C; // 2022/06/23: all possible values 0-256. 0 is most common (~50%).
    private bool isSwappableTexture; // perhaps a "cache texture" flag
    private ushort tevLayerIndex;
    private ushort zero0x10;
    private TevCombinerFlags tevCombinerFlags;

    // PROPERTIES
    public AddressRange AddressRange { get; set; }
    public TevTextureFlags TextureFlags { get => textureFlags; set => textureFlags = value; }
    public ushort TplTextureIndex { get => tplTextureIndex; set => tplTextureIndex = value; }
    public sbyte LodBias { get => lodBias; set => lodBias = value; }
    public GXAnisotropy AnisotropicFilter { get => anisotropicFilter; set => anisotropicFilter = value; }
    public Pointer GxTextureObjectPtr { get => gxTextureObjectPtr; set => gxTextureObjectPtr = value; }
    public byte Unk0x0C { get => unk0x0C; set => unk0x0C = value; }
    public bool IsSwappableTexture { get => isSwappableTexture; set => isSwappableTexture = value; }
    public ushort TevLayerIndex { get => tevLayerIndex; set => tevLayerIndex = value; }
    public ushort Zero0x10 { get => zero0x10; set => zero0x10 = value; }
    public TevCombinerFlags TevCombinerFlags { get => tevCombinerFlags; set => tevCombinerFlags = value; }


    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        this.RecordStartAddress(reader);
        {
            reader.Read(ref textureFlags);
            reader.Read(ref tplTextureIndex);
            reader.Read(ref lodBias);
            reader.Read(ref anisotropicFilter);
            reader.Read(ref gxTextureObjectPtr);
            reader.Read(ref unk0x0C);
            reader.Read(ref isSwappableTexture);
            reader.Read(ref tevLayerIndex);
            reader.Read(ref zero0x10);
            reader.Read(ref tevCombinerFlags);
        }
        this.RecordEndAddress(reader);
        {
            Assert.IsTrue(gxTextureObjectPtr == 0);
            Assert.IsTrue(zero0x10 == 0);
        }
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        {
            Assert.IsTrue(gxTextureObjectPtr == 0);
            Assert.IsTrue(zero0x10 == 0);
        }
        this.RecordStartAddress(writer);
        {
            writer.Write(textureFlags);
            writer.Write(tplTextureIndex);
            writer.Write(lodBias);
            writer.Write(anisotropicFilter);
            writer.Write(gxTextureObjectPtr);
            writer.Write(unk0x0C);
            writer.Write(isSwappableTexture);
            writer.Write(tevLayerIndex);
            writer.Write(zero0x10);
            writer.Write(tevCombinerFlags);
        }
        this.RecordEndAddress(writer);
    }

}