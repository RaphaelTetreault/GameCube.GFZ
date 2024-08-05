using Manifold.IO;
using System.IO;
using System.Numerics;
using static System.MathF;

namespace GameCube.GFZ
{
    // Resource that got me through this:
    // https://nghiaho.com/?page_id=846

    /// <summary>
    /// Amusement Vision's oddball rotation struct. Rather than store 3 floats for XYZ, they
    /// serialize 3 int16 values normalized. Conceptually, these int16 represent -pi to +pi.
    /// Moreover, this rotation "raw" is decomposed, meaning that the values are not suitable
    /// as-is and must be recomposed in a matrix (reconstructed) to be a valid rotation.
    /// </summary>
    [System.Serializable]
    public struct CompressedRotation :
        IBinarySerializable
    {
        // METADATA
        private Quaternion quaternion;
        private Vector3 eulers;


        // FIELDS
        private Int16Rotation x; // phi
        private Int16Rotation y; // theta
        private Int16Rotation z; // psi


        // PROPERTIES
        public Int16Rotation X
        {
            get => x;
            set
            {
                x = value;
                ComputeProperties();
            }
        }
        public Int16Rotation Y
        {
            get => y;
            set
            {
                y = value;
                ComputeProperties();
            }
        }
        public Int16Rotation Z
        {
            get => z;
            set
            {
                z = value;
                ComputeProperties();
            }
        }
        public Quaternion Quaternion
        {
            get => quaternion;
            // TODO: implement when System.Numerics supports quaternion <=> eulers
            // set {}
        }
        public Vector3 Eulers
        {
            get => eulers;
            set
            {
                x = eulers.X;
                y = eulers.Y;
                z = eulers.Z;
                ComputeProperties();
            }
        }


        // METHODS

        /// <summary>
        /// Reconstruct the rotation
        /// </summary>
        /// <param name="xRadians"></param>
        /// <param name="yRadians"></param>
        /// <param name="zRadians"></param>
        /// <returns></returns>
        public static Quaternion RecomposeRotation(float xRadians, float yRadians, float zRadians)
        {
            // Reconstruct componenets as matrices 3x3
            var mtxX = new Matrix4x4(
                1, 0, 0, 0,
                0, Cos(xRadians), -Sin(xRadians), 0,
                0, Sin(xRadians), Cos(xRadians), 0,
                0, 0, 0, 1
                );
            var mtxY = new Matrix4x4(
                Cos(yRadians), 0, Sin(yRadians), 0,
                0, 1, 0, 0,
                -Sin(yRadians), 0, Cos(yRadians), 0,
                0, 0, 0, 1
                );
            var mtxZ = new Matrix4x4(
                Cos(zRadians), -Sin(zRadians), 0, 0,
                Sin(zRadians), Cos(zRadians), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );

            // 
            var mtx = mtxZ * mtxY * mtxX;
            var rotation = mtx.Rotation();

            return rotation;
        }

        public static Vector3 DecomposeRotation(Matrix4x4 matrix)
        {
            // Get the relevant parts of the rotation from the matrix
            // https://nghiaho.com/?page_id=846
            float r11 = matrix.M11;
            float r21 = matrix.M21;
            float r31 = matrix.M31;
            float r32 = matrix.M32;
            float r33 = matrix.M33;

            // Compute discrete rotation steps
            float xRadians = Atan2(r32, r33);
            float yRadians = Atan2(-r31, Sqrt(Pow(r32, 2) + Pow(r33, 2)));
            float zRadians = Atan2(r21, r11);

            // Put in Vector3
            Vector3 decomposedEulers = new Vector3(xRadians, yRadians, zRadians);
            return decomposedEulers;
        }

        public static Vector3 DecomposeRotationDegrees(Matrix4x4 matrix)
        {
            // Convert matrix into distinct radian rotations
            Vector3 radians = DecomposeRotation(matrix);

            // Set angles to be in degrees, not radians
            float x = MathFX.RadiansToDegrees(radians.X);
            float y = MathFX.RadiansToDegrees(radians.Y);
            float z = MathFX.RadiansToDegrees(radians.Z);
            Vector3 decomposedEulers = new Vector3(x, y, z);

            return decomposedEulers;
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref x);
            reader.Read(ref y);
            reader.Read(ref z);

            // Initializes quaternion and euler properties
            ComputeProperties();
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(x);
            writer.Write(y);
            writer.Write(z);
        }

        private void ComputeProperties()
        {
            quaternion = RecomposeRotation(x.Radians, y.Radians, z.Radians);
            eulers = new Vector3(x.Degrees, y.Degrees, z.Degrees);
        }

        public override string ToString()
        {
            return $"({eulers.X:0.0}, {eulers.Y:0.0}, {eulers.Z:0.0})";
        }
    }
}
