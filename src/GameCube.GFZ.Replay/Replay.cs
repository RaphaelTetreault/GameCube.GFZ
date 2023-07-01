using Manifold.IO;

namespace GameCube.GFZ.Replay
{
    public class Replay :
        IBinaryAddressable,
        IBinarySerializable
    {
        public AddressRange AddressRange { get; set; }

        // COMMENTS DENOTE BITS
        private byte timestamp8bits; // 8, 8
        private byte racerArrayType; // 7, 15
        private byte courseID; // 6, 21
        private uint unknown1; // 32, 53
        private uint unknown2; // 32, 85
        private byte racerCount; // 5, 90
        private byte grandPrixDifficulty; // 3, 93
        private byte gameMode; // 2, 95
        private bool unknown3; // 1, 96
        private byte lapCount; // 2, 98
        private byte[] racers; // not fixed. 1 OR 30 x? bits. Will be class
        private bool unknown4; // 1
        private byte unknown5; // 2
        private uint frameCount; // 20
        private byte checkpointCount; // 8
        private byte[] checkpoints; // 4*441
        private ushort inputCount; // 14
        private byte[] inputs; // count defined above


        public void Deserialize(EndianBinaryReader reader)
        {
            var bytes = reader.ReadBytes(13);
            var bitReader = new BitStreamReader(bytes);
            bitReader.MirrorBytes();
            bitReader.Read(ref timestamp8bits, 8);
            bitReader.Read(ref racerArrayType, 7);
            bitReader.Read(ref courseID, 6);
            bitReader.Read(ref unknown1, 32);
            bitReader.Read(ref unknown2, 32);
            bitReader.Read(ref racerCount, 5);
            bitReader.Read(ref grandPrixDifficulty, 3);
            bitReader.Read(ref gameMode, 2);
            bitReader.Read(ref unknown3);
            bitReader.Read(ref lapCount, 2);
            // racers
            bitReader.Read(ref unknown4);
            bitReader.Read(ref unknown5, 2);
            bitReader.Read(ref frameCount, 20);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new NotImplementedException();
        }


    }
}