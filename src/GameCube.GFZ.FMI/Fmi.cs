using Manifold.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

// There is strange data in "header"
// When 0x04 is 0x01 or 0x01429544, then next bytes are always 40_01_4d_98 (float?) and 00_00_00_03 (uint?)
// When 0x04 is 0                 , then next bytes are 2 floats
// The latter only applies to Sonic Phantom (error?) and cockpits 1, 3, 4, 9, 10, 11, 12, 13, 14, 17, 22, 23, 24
// Dolphin has thrown PC address errors when messing with this. Maybe pointers inside file?

namespace GameCube.GFZ.FMI
{
    public class Fmi :
        IBinaryAddressable,
        IBinarySerializable,
        IPlainTextSerializable
    {
        // CONSTANTS
        private const uint kEmittersAbsPtr = 0x0044;
        private const uint kPositionsAbsPtr = 0x0208;
        private const uint kAnimationNameAbsPtr = 0x02A0;
        private const int kMinFileSize = 0x02A0;
        private const int kPaddingSize = 0x34;
        private readonly int PlainTextVersionNumber = 1;

        // FIELDS
        private ushort zero_0x00;
        private byte positionsCount;
        private byte emittersCount;
        private uint unk0x04;
        private FloatUintUnion unk0x08;
        private FloatUintUnion unk0x0C;
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
        public uint Unk_value0x04 { get => unk0x04; set => unk0x04 = value; }
        public FloatUintUnion Unk_float0x08 { get => unk0x08; set => unk0x08 = value; }
        public FloatUintUnion Unk_union0x0C { get => unk0x0C; set => unk0x0C = value; }
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
                reader.Read(ref unk0x04);
                reader.Read(ref unk0x08);
                reader.Read(ref unk0x0C);
                reader.Read(ref zeroPadding_0x34, kPaddingSize);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero_0x00 == 0);

                if (unk0x04 == 1)
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
            int version = reader.ReadInt32();
            string errorMsg = $"Unsupported version {version} (using deserializer version {PlainTextVersionNumber}).";
            Assert.IsTrue(version == PlainTextVersionNumber, errorMsg);

            // Unknown header data
            unk0x04 = uint.Parse(reader.ReadValue(), System.Globalization.NumberStyles.HexNumber);
            if (unk0x04 == 0)
            {
                unk0x08.AsFloat = reader.ReadSingle();
                unk0x0C.AsFloat = reader.ReadSingle();
            }
            else
            {
                unk0x08.AsUint = uint.Parse(reader.ReadValue(), System.Globalization.NumberStyles.HexNumber);
                unk0x0C.AsUint = uint.Parse(reader.ReadValue(), System.Globalization.NumberStyles.HexNumber);
            }

            // Emitters
            List<FmiEmitter> emitters = new();
            while (!reader.IsArrayEndMarker())
            {
                var emitter = new FmiEmitter();
                emitter.Deserialize(reader);
                emitters.Add(emitter);
            }
            this.emitters = emitters.ToArray();

            // Positions (with name)
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

        public void Serialize(EndianBinaryWriter writer)
        {
            // Prepare dependant data
            {
                emittersCount = checked((byte)emitters.Length);
                positionsCount = checked((byte)positions.Length);
                const string msg = $"Not an equal amount of {nameof(positions)} and {nameof(names)}.";
                Assert.IsTrue(positions.Length == names.Length, msg);
            }

            // Prepare file. There is a minimum size even if "empty"
            writer.WritePadding(0x00, kMinFileSize);
            writer.JumpToZero();

            this.RecordStartAddress(writer);
            {
                writer.Write(zero_0x00);
                writer.Write(positionsCount);
                writer.Write(emittersCount);
                writer.Write(unk0x04);
                writer.Write(unk0x08);
                writer.Write(unk0x0C);
                writer.WritePadding(0x00, kPaddingSize);
            }
            this.RecordEndAddress(writer);
            {
                writer.JumpToAddress(kEmittersAbsPtr);
                foreach (FmiEmitter emitter in emitters)
                    writer.Write(emitter);

                writer.JumpToAddress(kPositionsAbsPtr);
                foreach (FmiPosition position in positions)
                    writer.Write(position);

                writer.JumpToAddress(kAnimationNameAbsPtr);
                foreach (ShiftJisCString name in names)
                    name.Serialize(writer);
                // Above: temp until solve string/cstring write error
            }
        }
        public void Serialize(PlainTextWriter writer)
        {
            writer.WriteLineComment("FMI");
            writer.WriteLineValue("Version", PlainTextVersionNumber);
            writer.WriteLine();

            writer.WriteLineComment("Unknown header data. When Unk0 is 0, Unk1/2 are floats.");
            writer.WriteLineValue("Unk0", $"{unk0x04:x8}");
            writer.WriteLineValue("Unk1", unk0x04 == 0 ? unk0x08.AsFloat : $"{unk0x08.AsUint:x8}");
            writer.WriteLineValue("Unk2", unk0x04 == 0 ? unk0x0C.AsFloat : $"{unk0x0C.AsUint:x8}");
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
    }
}