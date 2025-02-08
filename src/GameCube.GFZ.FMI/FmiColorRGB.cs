using Manifold.IO;

namespace GameCube.GFZ.FMI;

/// <summary>
///     RGB color struct for FMI particles.
/// </summary>
public struct FmiColorRGB :
    IBinarySerializable
{
    public float r;
    public float g;
    public float b;

    public void Deserialize(EndianBinaryReader reader)
    {
        reader.Read(ref r);
        reader.Read(ref g);
        reader.Read(ref b);
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(r);
        writer.Write(g);
        writer.Write(b);
    }

    public override readonly string ToString()
    {
        return $"{nameof(FmiColorRGB)}({r}, {g}, {b})";
    }
}
