using GameCube.GCI;

namespace GameCube.GFZ.Replay
{
    public class ReplayGCI : Gci<Replay>
    {
        /// <summary>
        ///     Unique ID
        /// </summary>
        public const ushort UID = 0x0504;
        public Replay Replay { get => fileData; set => fileData = value; }
        public override ushort UniqueID => UID;
    }
}
