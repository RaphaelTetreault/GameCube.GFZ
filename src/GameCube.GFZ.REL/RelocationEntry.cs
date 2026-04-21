using Manifold.IO;

namespace GameCube.GFZ.REL;

/// <summary>
///     REL relocation entry.
/// </summary>
/// <remarks>
///     Based off of: https://wiki.tockdom.com/wiki/REL_(File_Format)#Relocation_Data
///     TODO Move to <see cref="GameCube.Common"/>.    
/// </remarks>
public struct RelocationEntry :
    IBinarySerializable,
    IBinaryAddressable
{
    public const int Size = 8;

    /// <summary>
    ///     Offset in bytes from the previous relocation to this one.
    ///     If this is the first relocation in the section, this is relative to the section start.
    /// </summary>
    public ushort offset;

    /// <summary>
    ///     The relocation type.
    /// </summary>
    public RelocationType type;

    /// <summary>
    ///     The section of the symbol to relocate against. For the special relocation type 202, this is
    ///     instead the number of the section in this file which the following relocation entries apply to.
    /// </summary>
    public byte section;

    /// <summary>
    ///     Offset in bytes of the symbol to relocate against, relative to the start of its section.
    ///     This is an absolute address instead for relocations against main.dol.
    /// </summary>
    public Offset addEnd;

    public AddressRange AddressRange { get; set; }


    public void Deserialize(EndianBinaryReader reader)
    {
        AddressRange.RecordStartAddress(reader);
        reader.Read(ref offset);
        reader.Read(ref type);
        reader.Read(ref section);
        reader.Read(ref addEnd);
        AddressRange.RecordEndAddress(reader);
    }

    public readonly Pointer ResolveAddress(int baseAddress)
    {
        Pointer pointer = baseAddress + addEnd;
        return pointer;
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        AddressRange.RecordStartAddress(writer);
        writer.Write(offset);
        writer.Write(type);
        writer.Write(section);
        writer.Write(addEnd);
        AddressRange.RecordEndAddress(writer);
    }
}


