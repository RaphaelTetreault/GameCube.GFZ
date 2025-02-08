using Manifold.IO;
using System.Runtime.InteropServices;

namespace GameCube.GFZ.FMI;

/// <summary>
///     A float/uint32 union for <see cref="Fmi"/>.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct FloatUintUnion :
    IBinarySerializable
{
    [FieldOffset(0)] public float AsFloat;
    [FieldOffset(0)] public uint AsUint;

    public void Deserialize(EndianBinaryReader reader)
    {
        reader.Read(ref AsUint);
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(AsUint);
    }
}
