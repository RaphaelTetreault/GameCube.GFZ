using Manifold;
using Manifold.IO;
using System;
using System.IO;
using System.Numerics;

namespace GameCube.GFZ.Stage
{
    [Serializable]
    public struct Plane : 
        IBinarySerializable
    {
        /// <summary>
        /// The dot product of this plane. 'dot(direction, position)'
        /// </summary>
        public float distance;
        /// <summary>
        /// The facing direction of this plane.
        /// </summary>
        public Vector3 normal;
        /// <summary>
        /// The origin position of this plane.
        /// </summary>
        public Vector3 origin;


        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref distance);
            reader.Read(ref normal);
            reader.Read(ref origin);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(distance);
            writer.Write(normal);
            writer.Write(origin);
        }

        /// <summary>
        /// Computes and stores the dotProduct of this Plane.
        /// </summary>
        public void ComputeDotProduct()
        {
            float dotProduct =
                normal.x * origin.x +
                normal.y * origin.y +
                normal.z * origin.z;

            // dot product is inverted
            this.distance = -dotProduct;
        }

        public Plane GetMirror()
        {
            return GetPlaneMirrored(this);
        }

        public static Plane GetPlaneMirrored(Plane plane)
        {
            var mirroredPlane = new Plane();
            mirroredPlane.distance = -plane.distance;
            mirroredPlane.normal = -plane.normal;
            mirroredPlane.origin = plane.origin;
            return mirroredPlane;
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(Plane));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(origin)}: {origin}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(normal)}: {normal}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(distance)}: {distance}");
        }

        public string PrintSingleLine()
        {
            return nameof(Plane);
        }

        public override string ToString() => PrintSingleLine();

    }
}
