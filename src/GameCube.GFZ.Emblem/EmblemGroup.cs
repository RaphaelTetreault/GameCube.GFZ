using Manifold.IO;
using System;

namespace GameCube.GFZ.Emblem;

/// <summary>
///     A grouping of emblems.
/// </summary>
/// <remarks>
///     This, in conjunctions with <see cref="EmblemBIN"/> define
///     the emblem.bin files.
/// </remarks>
public class EmblemGroup :
    IBinarySerializable
{
    // FIELDS
    private Emblem[] emblems = [];

    // PROPERTIES
    public Emblem[] Emblems { get => emblems; set => emblems = value; }
    public int Length => emblems.Length;
    
    // INDEXER
    public Emblem this[int i]
    {
        get => emblems[i];
        set => emblems[i] = value;
    }

    // CONSTRUCTORS
    public EmblemGroup() { }

    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        bool isValidFileSize = (int)(reader.BaseStream.Length % Emblem.Size) == 0;
        if (!isValidFileSize)
        {
            string msg = $"File is not an exact multiple of 0x{Emblem.Size:x4}.";
            throw new ArgumentException(msg);
        }

        int count = (int)(reader.BaseStream.Length / Emblem.Size);
        reader.Read(ref emblems, count);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(emblems);
    }
}
