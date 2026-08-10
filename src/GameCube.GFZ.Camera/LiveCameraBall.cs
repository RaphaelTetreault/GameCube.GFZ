using Manifold.IO;
using System.Numerics;

namespace GameCube.GFZ.Camera;

public sealed class LiveCameraBall :
    IBinarySerializable
{
    public const int Vector3Size = 4 * 3; // 4 bytes 3 times

    // Fields
    private Vector3[] points = [];
    
    // Properties
    public Vector3[] Points { get => points; set => points = value; }


    public void Deserialize(EndianBinaryReader reader)
    {
        int count = (int)reader.BaseStream.Length / Vector3Size;
        reader.Read(ref points, count);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(points);
    }
}
