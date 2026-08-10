using Manifold.IO;
using System.Numerics;

namespace GameCube.GFZ.GameData;

/// <summary>
///     Represents the projection of the vehicles onto the minimap.
/// </summary>
public struct MinimapProjection :
    IBinarySerializable
{
    public const int StructSize = 7 * 4;

    private float fov;
    private Vector3 cameraPosition;
    private Vector3 lookatPosition;

    public float FOV { readonly get => fov; set => fov = value; }
    public Vector3 CameraPosition { readonly get => cameraPosition; set => cameraPosition = value; }
    public Vector3 LookatPosition { readonly get => lookatPosition; set => lookatPosition = value; }

    public void Deserialize(EndianBinaryReader reader)
    {
        reader.Read(ref fov);
        reader.Read(ref cameraPosition);
        reader.Read(ref lookatPosition);
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(fov);
        writer.Write(cameraPosition);
        writer.Write(lookatPosition);
    }
}
