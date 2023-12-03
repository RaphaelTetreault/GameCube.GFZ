using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class Fmi :
        IBinaryAddressable,
        IBinarySerializable,
        ITextSerializable
    {
        // CONSTANTS
        private const uint kEmittersAbsPtr = 0x0044;
        private const uint kPositionsAbsPtr = 0x0208;
        private const uint kAnimationNameAbsPtr = 0x02A0;

        // FIELDS
        private ushort zero_0x00;
        private byte positionsCount;
        private byte emittersCount;
        private FmiUnknown unknown = new();
        // REFERENCES
        private FmiEmitter[] emitters = Array.Empty<FmiEmitter>();
        private FmiPosition[] positions = Array.Empty<FmiPosition>();
        private ShiftJisCString[] names = Array.Empty<ShiftJisCString>();

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public byte PositionsCount { get => positionsCount; set => positionsCount = value; }
        public byte EmittersCount { get => emittersCount; set => emittersCount = value; }
        public FmiUnknown Unknown { get => unknown; set => unknown = value; }
        public FmiEmitter[] Emitters { get => emitters; set => emitters = value; }
        public FmiPosition[] Positions { get => positions; set => positions = value; }
        public ShiftJisCString[] Names { get => names; set => names = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref zero_0x00);
                reader.Read(ref positionsCount);
                reader.Read(ref emittersCount);
                reader.Read(ref unknown);
            }
            this.RecordEndAddress(reader);
            {
                bool hasExhaustEmitters = emittersCount > 0;
                if (hasExhaustEmitters)
                {
                    reader.JumpToAddress(kEmittersAbsPtr);
                    reader.Read(ref emitters, emittersCount);
                }

                bool hasAnimations = positionsCount > 0;
                if (hasAnimations)
                {
                    reader.JumpToAddress(kPositionsAbsPtr);
                    reader.Read(ref positions, positionsCount);

                    reader.JumpToAddress(kAnimationNameAbsPtr);
                    reader.Read(ref names, positionsCount);
                }
            }
            this.SetReaderToEndAddress(reader);
            {
                Assert.IsTrue(zero_0x00 == 0);
            }
        }

        public void Deserialize(StreamReader reader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(StreamWriter writer)
        {
            const int h1 = 64;

            writer.WriteLineWithTail("General Data (Unknown)", '#', h1);
            unknown.Serialize(writer);
            writer.WriteLine();

            writer.WriteLineWithTail("Emitters", '#', h1);
            foreach (var emitter in emitters)
            {
                writer.WriteLineWithTail("Emitter", '-', h1);
                emitter.Serialize(writer);
            }
            writer.WriteLine();

            writer.WriteLineWithTail("Positions", '#', h1);
            for (int i = 0; i < positionsCount; i++)
            {
                writer.WriteLineWithTail("Position", '-', h1);
                writer.WriteLine($"Position Object Name: \"{names[i]}\"");
                positions[i].Serialize(writer);
            }
            writer.WriteLine();
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new NotImplementedException();
        }

    }
}