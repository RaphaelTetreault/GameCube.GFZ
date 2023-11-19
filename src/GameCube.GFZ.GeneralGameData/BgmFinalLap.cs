using Manifold.IO;

namespace GameCuibe.GFZ.GeneralGameData
{
    public struct BgmFinalLap :
        IBinarySerializable
    {
        public byte songIndex;
        public byte unused;
        public ushort loopPointDataOffset;

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref songIndex);
            reader.Read(ref unused);
            reader.Read(ref loopPointDataOffset);
        }

        public readonly void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(songIndex);
            writer.Write(unused);
            writer.Write(loopPointDataOffset);
        }
    }
}