using GameCube.GCI;

namespace GameCube.GFZ.Replay
{
    public class ReplayGCI : Gci<Replay>
    {
        public Replay Replay { get => fileData; set => fileData = value; }
        public override ushort UniqueID => 0x0504;
    }
}
