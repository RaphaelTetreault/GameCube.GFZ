using GameCube.GCI;

namespace GameCube.GFZ.Ghosts
{
    public class GhostDataGCI : Gci<GhostData>
    {
        public const ushort UID0 = 0x0201; // but also 0401: perhaps GFZ-J/E/P?
        public const ushort UID1 = 0x0401; // but also 0401: perhaps GFZ-J/E/P?
        public readonly ushort[] UIDs = { UID0, UID1 };
        public override ushort[] UniqueIDs => UIDs;
        public GhostData GhostData { get => fileData; set => fileData = value; }
    }
}
