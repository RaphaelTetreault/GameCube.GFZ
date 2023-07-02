using Manifold.IO;
using System.Collections;
using System.IO;

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
        private uint unknown2; // 31, 84
        private byte racerCount; // 5, 89
        private byte grandPrixDifficulty; // 3, 92
        private byte gameMode; // 2, 94
        private bool unknown3; // 1, 95
        private byte lapCount; // 2, 97
        private RaceParticipant[] racers = Array.Empty<RaceParticipant>(); // not fixed. 1 OR 30 x? bits. Will be class
        private bool unknown4; // 1
        private byte unknown5; // 2
        private uint frameCount; // 20
        private byte checkpointCount; // 8
        private byte[] checkpoints; // 4*441
        private ushort inputCount; // 14
        private byte[] inputs; // count defined above


        public void Deserialize(EndianBinaryReader reader)
        {
            var bitReader = new BitStreamReader(reader);
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
            bitReader.Read(ref racers, racerCount);
            bitReader.Read(ref unknown4);
            bitReader.Read(ref unknown5, 2);
            bitReader.Read(ref frameCount, 20);
            bitReader.Read(ref checkpointCount, 8);
            bitReader.Read(ref checkpoints, checkpointCount);
            bitReader.Read(ref inputCount, 14);
            bitReader.Read(ref inputs, inputCount);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new NotImplementedException();
        }

    }
}