using GameCube.GCI;

namespace GameCube.GFZ.Replay
{
    public class ReplayGCI : Gci<Replay>
    {
        /// <summary>
        ///     Unique ID.
        /// </summary>
        public const ushort UID = 0x0504;
        public readonly ushort[] UIDs = { UID };
        public override ushort[] UniqueIDs => UIDs;
        public Replay Replay { get => fileData; set => fileData = value; }
    }
}
