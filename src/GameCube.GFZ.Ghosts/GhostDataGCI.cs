using GameCube.GCI;

namespace GameCube.GFZ.Ghosts
{
    public class GhostDataGCI : Gci<GhostData>
    {
        public override ushort UniqueID => 0x0401;
        public GhostData GhostData { get => fileData; set => fileData = value; }
    }
}
