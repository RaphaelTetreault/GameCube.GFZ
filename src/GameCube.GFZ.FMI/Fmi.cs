using Manifold.IO;
using System;
using System.Collections.Generic;
using System.IO;

// There is strange data in "header"
// When 0x04 is 0x01 or 0x01429544, then next bytes are always 40_01_4d_98 (float?) and 00_00_00_03 (uint?)
// When 0x04 is 0                 , then next bytes are 2 floats
// The latter only applies to Sonic Phantom (error?) and cockpits 1, 3, 4, 9, 10, 11, 12, 13, 14, 17, 22, 23, 24
// Dolphin has thrown PC address errors when messing with this. Maybe pointers inside file?

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class Fmi :
        IBinaryAddressable,
        IBinarySerializable,
        IPlainTextSerializable
    {
        // CONSTANTS
        private const uint kEmittersAbsPtr = 0x0044;
        private const uint kPositionsAbsPtr = 0x0208;
        private const uint kAnimationNameAbsPtr = 0x02A0;
        private const int kPaddingSize = 0x34;

        // FIELDS
        private ushort zero_0x00;
        private byte positionsCount;
        private byte emittersCount;
        private uint unk_value0x04;
        private float unk_float0x08;
        private FloatUintUnion unk_union0x0C;
        private byte[] zeroPadding_0x34 = Array.Empty<byte>();
        // REFERENCES
        private FmiEmitter[] emitters = Array.Empty<FmiEmitter>();
        private FmiPosition[] positions = Array.Empty<FmiPosition>();
        private ShiftJisCString[] names = Array.Empty<ShiftJisCString>();

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public byte PositionsCount { get => positionsCount; set => positionsCount = value; }
        public byte EmittersCount { get => emittersCount; set => emittersCount = value; }
        public FmiEmitter[] Emitters { get => emitters; set => emitters = value; }
        public FmiPosition[] Positions { get => positions; set => positions = value; }
        public ShiftJisCString[] Names { get => names; set => names = value; }
        public uint Unk_value0x04 { get => unk_value0x04; set => unk_value0x04 = value; }
        public float Unk_float0x08 { get => unk_float0x08; set => unk_float0x08 = value; }
        public FloatUintUnion Unk_union0x0C { get => unk_union0x0C; set => unk_union0x0C = value; }
        // Temp metadata
        public bool UnionIsFloat => Unk_value0x04 == 0;
        public bool UnionIsUint => Unk_value0x04 == 0x01429544; // might only be first byte
        public bool UnionIsUnrecognized => !UnionIsFloat && !UnionIsUint;


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref zero_0x00);
                reader.Read(ref positionsCount);
                reader.Read(ref emittersCount);
                reader.Read(ref unk_value0x04);
                reader.Read(ref unk_float0x08);
                reader.Read(ref unk_union0x0C);
                reader.Read(ref zeroPadding_0x34, kPaddingSize);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero_0x00 == 0);

                if (unk_value0x04 == 1)
                    for (int i = 0; i < zeroPadding_0x34.Length; i++)
                    {
                        Assert.IsTrue(zeroPadding_0x34[i] == 0, i.ToString());
                    }
            }
            //
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

        public void Deserialize(PlainTextReader reader)
        {
            List<FmiEmitter> emitters = new();
            while (!reader.IsArrayEndMarker())
            {
                var emitter = new FmiEmitter();
                emitter.Deserialize(reader);
                emitters.Add(emitter);
            }
            this.emitters = emitters.ToArray();

            List<string> names = new();
            List<FmiPosition> positions = new();
            while (!reader.IsArrayEndMarker())
            {
                // Get param name
                names.Add(reader.ReadValue());
                // Get param position
                var position = new FmiPosition();
                position.Deserialize(reader);
                positions.Add(position);
            }
            this.names = names.ConvertAll(x => (ShiftJisCString)x).ToArray();
            this.positions = positions.ToArray();
        }

        public void Serialize(PlainTextWriter writer)
        {
            writer.WriteLineComment("FMI");
            writer.WriteLine();
            
            writer.WriteLineComment("Emitters");
            writer.IncrementIndent();
            for (int i = 0; i < emitters.Length; i++)
            {
                var emitter = emitters[i];
                writer.WriteLineComment($"Emitter {i + 1}");
                writer.SerializeIndent(emitter);
            }
            writer.DecrementIndent();
            writer.WriteLineArrayEnd();
            writer.WriteLine();

            writer.WriteLineComment("Positions");
            writer.IncrementIndent();
            for (int i = 0; i < positionsCount; i++)
            {
                var name = names[i];
                var position = positions[i];
                writer.WriteLineComment($"Position {i + 1}");
                writer.IncrementIndent();
                writer.WriteLineValue(nameof(name), name);
                writer.DecrementIndent();
                writer.SerializeIndent(position);
            }
            writer.DecrementIndent();
            writer.WriteLineArrayEnd();
            writer.WriteLine();
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new NotImplementedException();
        }

    }
}