using System.Drawing;
using Unity.Mathematics;

namespace GameCube.GFZ.REL
{
    public sealed class MinimapProjection
    {
        public const int Size = 7 * 4;

        private float fov;
        private float3 cameraPosition;
        private float3 lookatPosition;

        public float FOV { get => fov; set => fov = value; }
        public float3 CameraPosition { get => cameraPosition; set => cameraPosition = value; }
        public float3 LookatPosition { get => lookatPosition; set => lookatPosition = value; }
    }
}
