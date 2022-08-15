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
        private float planeDistance; // plane distance from origin
        private float3 normal;
        private float3 vertex0;
        private float3 vertex1;
        private float3 vertex2;
        private float3 edgeNormal0;
        private float3 edgeNormal1;
        private float3 edgeNormal2;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        /// <remarks>
        /// The dot product of dot(normal, vertex0/1/2). All result in the same scalar.
        /// </remarks>
        public float PlaneDistance { get => planeDistance; set => planeDistance = value; }
        public float3 Normal { get => normal; set => normal = value; }
        public float3 EdgeNormal0 { get => edgeNormal0; set => edgeNormal0 = value; }
        public float3 EdgeNormal1 { get => edgeNormal1; set => edgeNormal1 = value; }
        public float3 EdgeNormal2 { get => edgeNormal2; set => edgeNormal2 = value; }
        public float3 Vertex0 { get => vertex0; set => vertex0 = value; }
        public float3 Vertex1 { get => vertex1; set => vertex1 = value; }
        public float3 Vertex2 { get => vertex2; set => vertex2 = value; }


        // METHODS

        /// <summary>
        /// Computes and stores the dotProduct of this triangle.
        /// </summary>
        public void UpdatePlaneDistance()
        {
            // NOTE you can dot any of the vertices you want with
            //      the normal and will always get the same scalar.
            PlaneDistance = -math.dot(normal, vertex0);
        }

        public float3[] GetVertices()
        {
            return new float3[] { Vertex0, Vertex1, Vertex2 };
        }
        public float3[] GetEdgeNormals()
        {
            return new float3[] { EdgeNormal0, EdgeNormal1, EdgeNormal2};
        }

        public float3 Center()
        {
            const float oneThird = 1 / 3f;
            float3 center =
                Vertex0 * oneThird +
                Vertex1 * oneThird +
                Vertex2 * oneThird;
            return center;
        }

        public void UpdateEdgeNormals()
        {
            // The edge normal is the cross product between the triangle normal
            // and the direction from seqeuntial vertices. Since the normal can be
            // flipped, do cross(nrm, dir) if the direction is from, say, v1 to v0,
            // or cross(dir, nrm) if the direction is from, say, v0 to v1.
            float3 v0v1 = vertex0 - vertex1;
            float3 v1v2 = vertex1 - vertex2;
            float3 v2v0 = vertex2 - vertex0;
            edgeNormal0 = math.cross(normal, v0v1);
            edgeNormal1 = math.cross(normal, v1v2);
            edgeNormal2 = math.cross(normal, v2v0);
        }

        // bounds x/z
        // TODO: deprecate, use different moeth so triangle can go between bounds.
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
                reader.Read(ref planeDistance);
                reader.Read(ref normal);
                reader.Read(ref vertex0);
                reader.Read(ref vertex1);
                reader.Read(ref vertex2);
                reader.Read(ref edgeNormal0);
                reader.Read(ref edgeNormal1);
                reader.Read(ref edgeNormal2);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(planeDistance);
                writer.Write(normal);
                writer.Write(vertex0);
                writer.Write(vertex1);
                writer.Write(vertex2);
                writer.Write(edgeNormal0);
                writer.Write(edgeNormal1);
                writer.Write(edgeNormal2);
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
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(EdgeNormal0)}: {EdgeNormal0}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(EdgeNormal1)}: {EdgeNormal1}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(EdgeNormal2)}: {EdgeNormal2}");
        }
    }
}