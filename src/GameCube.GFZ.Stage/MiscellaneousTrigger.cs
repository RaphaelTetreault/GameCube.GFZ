using Manifold;
using Manifold.IO;
using System;
using System.IO;
using System.Numerics;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// A trigger volume that has many different use cases depending on
    /// which course it appears. Consult enum comments for more details.
    /// </summary>
    [Serializable]
    public sealed class MiscellaneousTrigger :
        IBinaryAddressable,
        IBinarySerializable,
        ITextPrintable
    {
        // FIELDS
        private TransformTRXS transform;
        private CourseMetadataType metadataType;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }

        // PROPERTIES USED TO MAKE SENSE OF THIS NONSENSE
        public Vector3 Position => Transform.Position;
        public Vector3 PositionFrom => Transform.Position;
        public Vector3 PositionTo => Transform.Scale;
        public Vector3 Scale => Transform.Scale;
        public Quaternion Rotation => Transform.Rotation;
        public Vector3 RotationEuler => Transform.RotationEuler;

        public TransformTRXS Transform { get => transform; set => transform = value; }
        public CourseMetadataType MetadataType { get => metadataType; set => metadataType = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref transform);
                reader.Read(ref metadataType);
            }
            this.RecordEndAddress(reader);
        }



        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(transform);
                writer.Write(metadataType);
            }
            this.RecordEndAddress(writer);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(MiscellaneousTrigger));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(metadataType)}: {metadataType}");
            builder.AppendMultiLineIndented(indent, indentLevel, transform);
        }

        public string PrintSingleLine()
        {
            return $"{nameof(MiscellaneousTrigger)}({nameof(MetadataType)}: {MetadataType})";
        }

        public override string ToString() => PrintSingleLine();

    }
}
