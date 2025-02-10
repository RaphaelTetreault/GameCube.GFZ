using GameCube.GX.Texture;
using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.TPL;

/// <summary>
///     An assetized version of <see cref="Texture"/>.
/// </summary>
/// <remarks>
///     TODO: Convert this to BinaryFileWrapper(Texture)?
/// </remarks>
public class GxTexture :
    IBinarySerializable
{
    private const uint magic = 0x47585458; // GXTX
    private const int alignment = 32;

    // Fields
    private ushort width;
    private ushort height;
    private byte count;
    private TextureFormat format;
    private int dataLength;
    private byte[] data = [];

    // Properties
    public byte Count { get => count; set => count = value; }
    public byte[] Data { get => data; set => data = value; }
    public int DataLength { get => dataLength; set => dataLength = value; }
    public TextureFormat Format { get => format; set => format = value; }
    public ushort Height { get => height; set => height = value; }
    public ushort Width { get => width; set => width = value; }

    // Methods
    public void Deserialize(EndianBinaryReader reader)
    {
        uint magicConst = 0;
        reader.Read(ref magicConst);
        Assert.IsTrue(magicConst == magic);
        reader.Read(ref width);
        reader.Read(ref height);
        reader.Read(ref format);
        reader.Read(ref count);
        reader.Read(ref dataLength);
        reader.AlignTo(alignment);
        reader.Read(ref data, dataLength);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(magic);
        writer.Write(width);
        writer.Write(height);
        writer.Write(format);
        writer.Write(count);
        writer.Write(dataLength);
        writer.AlignTo(alignment, 0xFF);
        writer.Write(data);
    }

    public TextureBundleDescription GetDescription()
    {
        TextureBundleDescription description = new()
        {
            IsNull = false,
            TextureFormat = format,
            Width = width,
            Height = height,
            MipmapLevels = checked((ushort)(count - 1)),
        };
        return description;
    }
    public Texture[] GetTexturesFromData()
    {
        using var reader = new EndianBinaryReader(new MemoryStream(data), TplFile.endianness);

        Texture[] textures = new Texture[count];
        int width = this.width;
        int height = this.height;

        for (int i = 0; i < textures.Length; i++)
        {
            textures[i] = Texture.ReadDirectColorTexture(reader, format, width, height);
            // Dive by two
            width >>= 1;
            height >>= 1;
        }

        return textures;
    }

}
