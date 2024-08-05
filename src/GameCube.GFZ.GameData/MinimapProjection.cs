using Manifold.IO;
using System.Numerics;

namespace GameCube.GFZ.GameData
{
    public sealed class MinimapProjection :
        IBinarySerializable
    {
        public const int Size = 7 * 4;

        private float fov;
        private Vector3 cameraPosition;
        private Vector3 lookatPosition;

        public float FOV { get => fov; set => fov = value; }
        public Vector3 CameraPosition { get => cameraPosition; set => cameraPosition = value; }
        public Vector3 LookatPosition { get => lookatPosition; set => lookatPosition = value; }

        public void Deserialize(EndianBinaryReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
