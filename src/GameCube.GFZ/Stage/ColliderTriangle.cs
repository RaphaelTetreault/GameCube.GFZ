using Manifold;
using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// An individual triangle as part of a collider mesh.
    /// </summary>
    [Serializable]
    public sealed class ColliderTriangle :
        IBinaryAddressable,
        IBinarySerializable,
        ITextPrintable
    {
        // FEILDS
        private float dotProduct;
        private float3 normal;
        private float3 vertex0;
        private float3 vertex1;
        private float3 vertex2;
        private float3 precomputed0;
        private float3 precomputed1;
        private float3 precomputed2;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        /// <remarks>
        /// The dot product of dot(normal, vertex0/1/2). All result in the same scalar.
        /// </remarks>
        public float PlaneDistance { get => dotProduct; set => dotProduct = value; }
        public float3 Normal { get => normal; set => normal = value; }
        public float3 Precomputed0 { get => precomputed0; set => precomputed0 = value; }
        public float3 Precomputed1 { get => precomputed1; set => precomputed1 = value; }
        public float3 Precomputed2 { get => precomputed2; set => precomputed2 = value; }
        public float3 Vertex0 { get => vertex0; set => vertex0 = value; }
        public float3 Vertex1 { get => vertex1; set => vertex1 = value; }
        public float3 Vertex2 { get => vertex2; set => vertex2 = value; }


        // METHODS

        /// <summary>
        /// Computes and stores the dotProduct of this triangle.
        /// </summary>
        public void ComputePlaneDistance()
        {
            //// NOTE you can dot any of the vertices you want with
            ////      the normal and will always get the same scalar.
            //float planeDistance =
            //    Normal.x * Vertex0.x +
            //    Normal.y * Vertex0.y +
            //    Normal.z * Vertex0.z;

            //this.PlaneDistance = -planeDistance;

            PlaneDistance = -math.dot(normal, vertex0);
        }

        public float3[] GetVerts()
        {
            return new float3[] { Vertex0, Vertex1, Vertex2 };
        }
        public float3[] GetPrecomputes()
        {
            return new float3[] { Precomputed0, Precomputed1, Precomputed2};
        }

        public float3 VertCenter()
        {
            return (Vertex0 + Vertex1 + Vertex2) / 3f;
        }

        public float3 PrecomputeCenter()
        {
            // Division inline since the values are BIG and would
            // lose more precision if summed first.
            return
                Precomputed0 / 3f +
                Precomputed1 / 3f +
                Precomputed2 / 3f;
        }

        // bounds x/z
        public float GetMinPositionX()
        {
            float min = math.min(vertex0.x, vertex1.x);
            min = math.min(min, vertex2.x);
            return min;
        }
        public float GetMinPositionZ()
        {
            float min = math.min(vertex0.z, vertex1.z);
            min = math.min(min, vertex2.z);
            return min;
        }
        public float GetMaxPositionX()
        {
            float max = math.max(vertex0.x, vertex1.x);
            max = math.max(max, vertex2.x);
            return max;
        }
        public float GetMaxPositionZ()
        {
            float max = math.max(vertex0.z, vertex1.z);
            max = math.max(max, vertex2.z);
            return max;
        }


        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref dotProduct);
                reader.Read(ref normal);
                reader.Read(ref vertex0);
                reader.Read(ref vertex1);
                reader.Read(ref vertex2);
                reader.Read(ref precomputed0);
                reader.Read(ref precomputed1);
                reader.Read(ref precomputed2);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(dotProduct);
                writer.Write(normal);
                writer.Write(vertex0);
                writer.Write(vertex1);
                writer.Write(vertex2);
                writer.Write(precomputed0);
                writer.Write(precomputed1);
                writer.Write(precomputed2);
            }
            this.RecordEndAddress(writer);
        }

        public override string ToString() => PrintSingleLine();

        public string PrintSingleLine()
        {
            return nameof(ColliderTriangle);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(ColliderTriangle));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(PlaneDistance)}: {PlaneDistance}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Vertex0)}: {Vertex0}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Vertex1)}: {Vertex1}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Vertex2)}: {Vertex2}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Precomputed0)}: {Precomputed0}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Precomputed1)}: {Precomputed1}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Precomputed2)}: {Precomputed2}");
        }
    }
}