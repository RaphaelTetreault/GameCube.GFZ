using GameCube.GFZ.GMA;
using Manifold.IO;
using System.Text;

namespace GameCube.GFZ.Asset;

/// <summary>
///     An assetized version of <see cref="GMA.Gcmf"/> with text references
///     to its textures.
/// </summary>
public class GcmfAsset :
    IBinarySerializable
{
    // CONSTANTS
    private const uint kMagic = 0x47434D58; // GCMX
    private const int kAlignment = 32;

    // READONLY
    private static readonly Gcmf defaultGcmf = new();
    private static readonly Encoding encoding = Encoding.UTF8;

    // FIELDS
    private string name = string.Empty;
    private string[] tevTextures = [];
    private int tevTexturesCount = 0;
    private Gcmf gcmf = defaultGcmf;

    // PROPERTIES
    public string[] TevTextureReferences { get => tevTextures; set => tevTextures = value; }
    public Gcmf Gcmf { get => gcmf; set => gcmf = value; }
    public string Name { get => name; set => name = value; }

    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        int magic = 0;
        reader.Read(ref magic);
        Assert.IsTrue(magic == kMagic);
        reader.Read(ref name, encoding);
        reader.Read(ref tevTexturesCount);
        reader.Read(ref tevTextures, tevTexturesCount, encoding);
        reader.AlignTo(kAlignment);
        reader.Read(ref gcmf);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(kMagic);
        writer.Write(name, encoding, true);
        writer.Write(tevTextures.Length);
        writer.Write(tevTextures, encoding);
        writer.AlignTo(kAlignment, 0xFF);
        writer.Write(gcmf);
    }
}
