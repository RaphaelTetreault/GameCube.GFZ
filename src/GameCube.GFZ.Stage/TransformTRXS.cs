using Manifold;
using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Represents a transformation. This is used on scene objects specifically. TRXS is 
    /// indicative of the values stored. T:translation, R:rotation, X:extra, S: scale.
    /// As noted, it contains "extra" bits.
    /// </summary>
    [Serializable]
    public sealed class TransformTRXS :
        IBinarySerializable,
        IBinaryAddressable,
        IDeepCopyable<TransformTRXS>,
        ITextPrintable
    {
        // FIELDS
        private float3 position;
        private CompressedRotation compressedRotation;
        private UnknownTransformOption unknownOption;
        private ObjectActiveOverride objectActiveOverride;
        private float3 scale = new float3(1,1,1);


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public CompressedRotation CompressedRotation { get => compressedRotation; set => compressedRotation = value; }
        public ObjectActiveOverride ObjectActiveOverride { get => objectActiveOverride; set => objectActiveOverride = value; }
        public float3 Position { get => position; set => position = value; }
        public quaternion Rotation => compressedRotation.Quaternion;
        public float3 RotationEuler => compressedRotation.Eulers;
        public float3 Scale { get => scale; set => scale = value; }
        public UnknownTransformOption UnknownOption { get => unknownOption; set => unknownOption = value; }


        // METHODS
        public TransformTRXS CreateDeepCopy()
        {
            var newInstance = new TransformTRXS()
            {
                position = position,
                compressedRotation = compressedRotation,
                scale = scale,
                unknownOption = unknownOption,
                objectActiveOverride = objectActiveOverride,
            };
            return newInstance;
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref position);
                reader.Read(ref compressedRotation);
                reader.Read(ref unknownOption);
                reader.Read(ref objectActiveOverride);
                reader.Read(ref scale);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(position);
                writer.Write(compressedRotation);
                writer.Write(unknownOption);
                writer.Write(objectActiveOverride);
                writer.Write(scale);
            }
            this.RecordEndAddress(writer);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(TransformTRXS));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Position)}: {Position}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Rotation)}: {RotationEuler}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Scale)}: {Scale}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(unknownOption)}: {unknownOption}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(objectActiveOverride)}: {objectActiveOverride}");
        }

        public string PrintSingleLine()
        {
            return $"{nameof(TransformTRXS)}({nameof(unknownOption)}: {unknownOption}, {nameof(objectActiveOverride)}: {objectActiveOverride})";
        }

        public override string ToString() => PrintSingleLine();

    }
}