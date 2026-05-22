using Manifold.IO;

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
    private sbyte[] boneIndexes = [-1, -1, -1, -1, -1, -1, -1, -1];

    //
    public sbyte[] BoneIndexes
    {
        get => boneIndexes;
        set => boneIndexes = value;
    }

    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        reader.Read(ref boneIndexes, Size);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(boneIndexes);
    }

}