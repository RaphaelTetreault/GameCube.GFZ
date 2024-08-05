using Manifold;
using Manifold.IO;
using System;
using System.Numerics;

namespace GameCube.GFZ
{
    // TODO: this might be a built-in GameCube.GX type.

    /// <summary>
    /// Column-major transform matrix with 3 rows and 4 columns.
    /// </summary>
    [Serializable]
    public class TransformMatrix3x4 :
        IBinaryAddressable,
        IBinarySerializable,
        ITextPrintable
    {
        // "FIELDS" as reconstructed for ease of use (it's really 3 rows x 4 columns)
        private Matrix4x4 matrix = Matrix4x4.Identity;

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public Matrix4x4 Matrix { get => matrix; set => matrix = value; }
        public Vector3 Position => matrix.Position();
        public Quaternion Rotation => matrix.Rotation();
        public Vector3 RotationEuler => matrix.RotationEuler();
        public Vector3 Scale => matrix.Scale();


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            // The data is stored as rows. Matrix4x4 calls them M[R][C], where
            // [R] is row number and [C] is column number. eg. M24 is row 2, col 4.
            float M11, M12, M13, M14;
            float M21, M22, M23, M24;
            float M31, M32, M33, M34;
            // The final row is implied (constant) in a 3x4 matrix as (0, 0, 0, 1)

            this.RecordStartAddress(reader);
            {
                // Read rows
                // Row 1
                M11 = reader.ReadFloat();
                M12 = reader.ReadFloat();
                M13 = reader.ReadFloat();
                M14 = reader.ReadFloat();
                // Row 2
                M21 = reader.ReadFloat();
                M22 = reader.ReadFloat();
                M23 = reader.ReadFloat();
                M24 = reader.ReadFloat();
                // Row 2
                M31 = reader.ReadFloat();
                M32 = reader.ReadFloat();
                M33 = reader.ReadFloat();
                M34 = reader.ReadFloat();
            }
            this.RecordEndAddress(reader);
            {
                // matrix is constructed from 4 rows
                matrix = new Matrix4x4(
                    M11, M12, M13, M14,
                    M21, M22, M23, M24,
                    M31, M32, M33, M34,
                    0, 0, 0, 1);
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                // Write rows
                // Row 1
                writer.Write(matrix.M11);
                writer.Write(matrix.M12);
                writer.Write(matrix.M13);
                writer.Write(matrix.M14);
                // Row 2
                writer.Write(matrix.M21);
                writer.Write(matrix.M22);
                writer.Write(matrix.M23);
                writer.Write(matrix.M24);
                // Row 3
                writer.Write(matrix.M31);
                writer.Write(matrix.M32);
                writer.Write(matrix.M33);
                writer.Write(matrix.M34);
            }
            this.RecordEndAddress(writer);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(TransformMatrix3x4));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Position)}({Position})");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Rotation)}({RotationEuler})");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Scale)}({Scale})");
        }

        public string PrintSingleLine()
        {
            return nameof(TransformMatrix3x4);
        }

        public override string ToString() => PrintSingleLine();

    }
}