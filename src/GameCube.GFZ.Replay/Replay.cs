using Manifold.IO;

namespace GameCube.GFZ.Replay
{
    // https://github.com/JoselleAstrid/fzerogx-docs/blob/master/file_formats/gci_replay.md#replay-gci-file-format


    public class Replay :
        IBinaryAddressable,
        IBinarySerializable
    {
        public AddressRange AddressRange { get; set; }

        private byte timestamp8bits;
        private byte racerArrayType;
        private byte courseID;
        private uint unknown1;
        private uint unknown2;
        private byte racerCount;
        private byte grandPrixDifficulty;
        private byte gameMode;
        private bool unknown3;
        private byte lapCount;
        private RaceParticipant[] racers = Array.Empty<RaceParticipant>();
        private bool unknown4;
        private byte unknown5;
        private uint frameCount;
        private byte checkpointCount;
        private Checkpoint[] checkpoints = Array.Empty<Checkpoint>();
        private ushort inputCount;
        private Input[] inputs = Array.Empty<Input>();


        public void Deserialize(EndianBinaryReader reader)
        {
            var bitReader = new BitStreamReader(reader);
            bitReader.Read(ref timestamp8bits, 8); // +0x01
            bitReader.Read(ref racerArrayType, 7);
            bitReader.Read(ref courseID, 6);
            bitReader.Read(ref unknown1, 32);
            bitReader.Read(ref unknown2, 32);
            bitReader.Read(ref racerCount, 5);
            bitReader.Read(ref grandPrixDifficulty, 3);
            bitReader.Read(ref gameMode, 2);
            bitReader.Read(ref unknown3); // +0x0C
            bitReader.Read(ref lapCount, 7);
            Assert.IsTrue(lapCount == 3);
            bitReader.Read(ref racers, racerCount);
            //skip racers
            //byte[] _ = bitReader.ReadBytes(29 /*bits*/ * racerCount);
            bitReader.Read(ref unknown4);
            bitReader.Read(ref unknown5, 2);
            bitReader.Read(ref frameCount, 20);
            bitReader.Read(ref checkpointCount, 8);
            Assert.IsTrue(checkpointCount == 4);
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