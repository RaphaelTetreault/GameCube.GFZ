// Resources
// https://docs.google.com/document/d/1c4a7d6xZ-rnK-E5p7d__6V1qhLxcdOHwAmv-FR8K50s/edit

using GameCube.DiskImage;
using GameCube.GFZ.GCI;
using Manifold.IO;

namespace GameCube.GFZ.Emblem;

/// <summary>
///     Player-created emblem.
/// </summary>
public class EmblemGCI : GfzGci<EmblemFile>
{
    public const ushort UID = 0x0401; // NOT A UNIQUE ID
    public override ushort UniqueID => UID;
    public override ushort[] UniqueIDs => [UID];

    public Emblem Emblem
    {
        get => FileData.Value;
        set => FileData.Value = value;
    }

    public EmblemGCI() : base() { }
    public EmblemGCI(Region region) : base(region) { }

    public override void Serialize(EndianBinaryWriter writer)
    {
        base.Serialize(writer);
        // Correct checksum
        writer.Flush();
        ushort checksum = ComputeCRC(writer.BaseStream);
        writer.JumpToAddress(0x40);
        writer.Write(checksum);
        writer.Flush();
    }
}
