using Manifold.IO;

namespace GameCube.GFZ.Ghosts;

/// <summary>
///     Course completion time in minutse, seconds, and milliseconds.
/// </summary>
public struct Time :
    IBinarySerializable
{
    public byte minutes;
    public byte seconds;
    public ushort milliseconds;

    public void Deserialize(EndianBinaryReader reader)
    {
        reader.Read(ref minutes);
        reader.Read(ref seconds);
        reader.Read(ref milliseconds);
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(minutes);
        writer.Write(seconds);
        writer.Write(milliseconds);
    }

    public override readonly string ToString()
    {
        return $"{minutes:0}\'{seconds:00}\"{milliseconds:000}";
    }
}
