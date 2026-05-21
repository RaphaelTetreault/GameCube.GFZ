using Manifold.IO;
using System.Runtime.CompilerServices;

namespace GameCube.GFZ.GMA;

/// <summary>
///     A set of 8 matrix indexes. Root indexes read as -1/0xFF, indicating no parent bone/matrix.
/// </summary>
//[InlineArray(8)]
public class BoneIndexes8 :
    IBinarySerializable
{
    const int Size = 8;

    // FIELDS
    //public sbyte boneIndex = -1;
    public sbyte[] boneIndex = [-1, -1, -1, -1, -1, -1, -1, -1];

    // CONSTRUCTOR
    // Ensures default value is always -1.
    public BoneIndexes8() { }

    // METHODS
    //public void Deserialize(EndianBinaryReader reader) => reader.Read(ref boneIndex);
    //public readonly void Serialize(EndianBinaryWriter writer) => writer.Write(boneIndex);

    public void Deserialize(EndianBinaryReader reader) => reader.Read(ref boneIndex, Size);
    public void Serialize(EndianBinaryWriter writer) => writer.Write(boneIndex);

}