using GameCube.GFZ.GCI;

namespace GameCube.GFZ.Ghosts
{
    public class GhostDataGCI : GfzGci<GhostData>
    {
        public GhostData GhostData { get => FileData; set => FileData = value; }
    }
}
