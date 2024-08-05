using Manifold;
using Manifold.IO;
using System;
using System.IO;
using System.Numerics;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// An individual quad as part of a collider mesh.
    /// </summary>
    [Serializable]
    public sealed class ColliderQuad :
        IBinaryAddressable,
        IBinarySerializable,
        ITextPrintable
    {
        // FIELDS
        private float planeDistance;
        private Vector3 normal;
        private Vector3 vertex0;
        private Vector3 vertex1;
        private Vector3 vertex2;
        private Vector3 vertex3;
        private Vector3 edgeNormal0;
        private Vector3 edgeNormal1;
        private Vector3 edgeNormal2;
        private Vector3 edgeNormal3;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        /// <remarks>
        /// The dot product of dot(normal, vertex0/1/2/4). All result in the same scalar.
        /// </remarks>
        public float PlaneDistance { get => planeDistance; set => planeDistance = value; }
        public Vector3 Normal { get => normal; set => normal = value; }
        public Vector3 Vertex0 { get => vertex0; set => vertex0 = value; }
        public Vector3 Vertex1 { get => vertex1; set => vertex1 = value; }
        public Vector3 Vertex2 { get => vertex2; set => vertex2 = value; }
        public Vector3 Vertex3 { get => vertex3; set => vertex3 = value; }
        public Vector3 EdgeNormal0 { get => edgeNormal0; set => edgeNormal0 = value; }
        public Vector3 EdgeNormal1 { get => edgeNormal1; set => edgeNormal1 = value; }
        public Vector3 EdgeNormal2 { get => edgeNormal2; set => edgeNormal2 = value; }
        public Vector3 EdgeNormal3 { get => edgeNormal3; set => edgeNormal3 = value; }

        // METHODS

        /// <summary>
        /// Computes and stores the dotProduct of this quadrangle.
        /// </summary>
        public void UpdatePlaneDistance()
        {
            // NOTE you can dot any of the vertices you want with
            //      the normal and will always get the same scalar.
            PlaneDistance = -math.dot(normal, vertex0);
        }

        public void UpdateEdgeNormals()
        {
            // The edge normal is the cross product between the quad normal and
            // the direction from seqeuntial vertices. Since the normal can be
            // flipped, do cross(nrm, dir) if the direction is from, say, v1 to v0,
            // or cross(dir, nrm) if the direction is from, say, v0 to v1.
            Vector3 v0v1 = vertex0 - vertex1;
            Vector3 v1v2 = vertex1 - vertex2;
            Vector3 v2v3 = vertex2 - vertex3;
            Vector3 v3v0 = vertex3 - vertex0;
            edgeNormal0 = math.cross(normal, v0v1);
            edgeNormal1 = math.cross(normal, v1v2);
            edgeNormal2 = math.cross(normal, v2v3);
            edgeNormal3 = math.cross(normal, v3v0);
            //edgeNormal0  = math.normalize(edgeNormal0);
            //edgeNormal1  = math.normalize(edgeNormal1);
            //edgeNormal2  = math.normalize(edgeNormal2);
            //edgeNormal3  = math.normalize(edgeNormal3);
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

        public Vector3[] GetVertices()
        {
            return new Vector3[] { Vertex0, Vertex1, Vertex2, Vertex3 };
        }
        public Vector3[] GetEdgeNormals()
        {
            return new Vector3[] { EdgeNormal0, EdgeNormal1, EdgeNormal2, EdgeNormal3 };
        }

        public Vector3 Center()
        {
            const float oneQuarter = 1 / 4f;
            Vector3 center =
                Vertex0 * oneQuarter +
                Vertex1 * oneQuarter +
                Vertex2 * oneQuarter +
                Vertex3 * oneQuarter;
            return center;
        }

        // bounds x/z
        public float GetMinPositionX()
        {
            float min = math.min(vertex0.x, vertex1.x);
            min = math.min(min, vertex2.x);
            min = math.min(min, vertex3.x);
            return min;
        }
        public float GetMinPositionZ()
        {
            float min = math.min(vertex0.z, vertex1.z);
            min = math.min(min, vertex2.z);
            min = math.min(min, vertex3.z);
            return min;
        }
        public float GetMaxPositionX()
        {
            float max = math.max(vertex0.x, vertex1.x);
            max = math.max(max, vertex2.x);
            max = math.max(max, vertex3.x);
            return max;
        }
        public float GetMaxPositionZ()
        {
            float max = math.max(vertex0.z, vertex1.z);
            max = math.max(max, vertex2.z);
            max = math.max(max, vertex3.z);
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
                reader.Read(ref vertex3);
                reader.Read(ref edgeNormal0);
                reader.Read(ref edgeNormal1);
                reader.Read(ref edgeNormal2);
                reader.Read(ref edgeNormal3);
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
                writer.Write(vertex3);
                writer.Write(edgeNormal0);
                writer.Write(edgeNormal1);
                writer.Write(edgeNormal2);
                writer.Write(edgeNormal3);
            }
            this.RecordEndAddress(writer);
        }

        public override string ToString() => PrintSingleLine();

        public string PrintSingleLine()
        {
            return nameof(ColliderQuad);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(ColliderQuad));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(planeDistance)}: {planeDistance}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(vertex0)}: {vertex0}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(vertex1)}: {vertex1}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(vertex2)}: {vertex2}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(vertex3)}: {vertex3}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(edgeNormal0)}: {edgeNormal0}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(edgeNormal1)}: {edgeNormal1}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(edgeNormal2)}: {edgeNormal2}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(edgeNormal3)}: {edgeNormal3}");
        }
    }
}