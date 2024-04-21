using Manifold.IO;

namespace GameCube.GFZ.LineREL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     Based off of: https://wiki.tockdom.com/wiki/REL_(File_Format)#Relocation_Data
    /// </remarks>
    public struct RelocationEntry :
        IBinarySerializable
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


        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref offset);
            reader.Read(ref type);
            reader.Read(ref section);
            reader.Read(ref addEnd);
        }

        public Pointer ResolveAddress(int baseAddress)
        {
            Pointer pointer = baseAddress + addEnd;
            return pointer;
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(offset);
            writer.Write(type);
            writer.Write(section);
            writer.Write(addEnd);
        }
    }
}


