using GameCube.DiskImage;
using GameCube.GFZ.GCI;
using Manifold.IO;

namespace GameCube.GFZ.Ghosts;

/// <summary>
///     Player-created ghost data.
/// </summary>
public class GhostDataGCI : GfzGci<GhostDataBIN>
{
    public const ushort UID0 = 0x0201; // but also 0401: perhaps GFZ-J/E/P?
    public const ushort UID1 = 0x0401; // but also 0201: perhaps GFZ-J/E/P?

    public readonly ushort[] UIDs = [UID0, UID1];
    public override ushort[] UniqueIDs => UIDs; // NOT A UNIQUE ID!!

    public GhostData GhostData
    {
        get => FileData.Value;
        set => FileData.Value = value;
    }

    public GhostDataGCI() : base() { }
    public GhostDataGCI(Region region) : base(region) { }
}
