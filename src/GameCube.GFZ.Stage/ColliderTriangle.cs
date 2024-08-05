using Manifold;
using Manifold.IO;
using System;
using System.IO;
using System.Numerics;

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
        private Vector3 normal;
        private Vector3 vertex0;
        private Vector3 vertex1;
        private Vector3 vertex2;
        private Vector3 edgeNormal0;
        private Vector3 edgeNormal1;
        private Vector3 edgeNormal2;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        /// <remarks>
        /// The dot product of dot(normal, vertex0/1/2). All result in the same scalar.
        /// </remarks>
        public float PlaneDistance { get => planeDistance; set => planeDistance = value; }
        public Vector3 Normal { get => normal; set => normal = value; }
        public Vector3 EdgeNormal0 { get => edgeNormal0; set => edgeNormal0 = value; }
        public Vector3 EdgeNormal1 { get => edgeNormal1; set => edgeNormal1 = value; }
        public Vector3 EdgeNormal2 { get => edgeNormal2; set => edgeNormal2 = value; }
        public Vector3 Vertex0 { get => vertex0; set => vertex0 = value; }
        public Vector3 Vertex1 { get => vertex1; set => vertex1 = value; }
        public Vector3 Vertex2 { get => vertex2; set => vertex2 = value; }


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

        public Vector3[] GetVertices()
        {
            return new Vector3[] { Vertex0, Vertex1, Vertex2 };
        }
        public Vector3[] GetEdgeNormals()
        {
            return new Vector3[] { EdgeNormal0, EdgeNormal1, EdgeNormal2};
        }

        public Vector3 Center()
        {
            const float oneThird = 1 / 3f;
            Vector3 center =
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
            Vector3 v0v1 = vertex0 - vertex1;
            Vector3 v1v2 = vertex1 - vertex2;
            Vector3 v2v0 = vertex2 - vertex0;
            edgeNormal0 = math.cross(normal, v0v1);
            edgeNormal1 = math.cross(normal, v1v2);
            edgeNormal2 = math.cross(normal, v2v0);
            //edgeNormal0 = math.normalize(edgeNormal0);
            //edgeNormal1 = math.normalize(edgeNormal1);
            //edgeNormal2 = math.normalize(edgeNormal2);
        }

        public void UpdateNormal()
        {
            Vector3 v0v1 = vertex0 - vertex1; // dir v0 -> v1
            Vector3 v0v2 = vertex0 - vertex2; // dir v0 -> v2
            normal = -math.cross(v0v1, v0v2);
            normal = math.normalize(normal);
        }

        public void Update()
        {
            // Update nromal first since other data relies on it.
            UpdateNormal();
            UpdateEdgeNormals();
            UpdatePlaneDistance();
        }

        // bounds x/z
        // TODO: deprecate, use different moeth so triangle can go between bounds.
        public float GetMinPositionX()
        {
            float min = math.min(vertex0.X, vertex1.X);
            min = math.min(min, vertex2.X);
            return min;
        }
        public float GetMinPositionZ()
        {
            float min = math.min(vertex0.Z, vertex1.Z);
            min = math.min(min, vertex2.Z);
            return min;
        }
        public float GetMaxPositionX()
        {
            float max = math.max(vertex0.X, vertex1.X);
            max = math.max(max, vertex2.X);
            return max;
        }
        public float GetMaxPositionZ()
        {
            float max = math.max(vertex0.Z, vertex1.Z);
            max = math.max(max, vertex2.Z);
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