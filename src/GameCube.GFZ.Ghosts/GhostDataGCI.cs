using GameCube.DiskImage;
using GameCube.GFZ.GCI;

namespace GameCube.GFZ.Ghosts
{
    public class GhostDataGCI : GfzGci<GhostData>
    {
        public const ushort UID0 = 0x0201; // but also 0401: perhaps GFZ-J/E/P?
        public const ushort UID1 = 0x0401; // but also 0201: perhaps GFZ-J/E/P?

        public readonly ushort[] UIDs = { UID0, UID1 };
        public override ushort[] UniqueIDs => UIDs; // NOT A UNIQUE ID!!

        public GhostData GhostData { get => FileData; set => FileData = value; }


        public GhostDataGCI() : base() { }
        public GhostDataGCI(Region region) : base(region) { }
    }
}
