using GameCube.GCI;
using Manifold.IO;

namespace GameCube.GFZ.Replay
{
    public class ReplayGCI : GciWithUniqueID<Replay>
    {
        /// <summary>
        ///     Unique ID.
        /// </summary>
        public const ushort UID = 0x0504;
        public readonly ushort[] UIDs = { UID };
        public override ushort UniqueID => throw new NotImplementedException();
        public override ushort[] UniqueIDs => UIDs;
        public Replay Replay { get => fileData; set => fileData = value; }

        public override ushort Unknown => throw new NotImplementedException();

        public override string Comment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public override void DeserializeCommentAndImages(EndianBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void SerializeCommentAndImages(EndianBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
