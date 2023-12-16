using Manifold.IO;
using System;
using System.Runtime.InteropServices;

namespace GameCube.GFZ.LineREL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     Based off of: https://wiki.tockdom.com/wiki/REL_(File_Format)#Relocation_Data
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct RelocationEntry :
        IBinarySerializable
    {
        /// <summary>
        ///     Offset in bytes from the previous relocation to this one.
        ///     If this is the first relocation in the section, this is relative to the section start.
        /// </summary>
        [FieldOffset(0x00)] public ushort offset;

        /// <summary>
        ///     The relocation type.
        /// </summary>
        [FieldOffset(0x02)] public RelocationType type;

        /// <summary>
        ///     The section of the symbol to relocate against. For the special relocation type 202, this is
        ///     instead the number of the section in this file which the following relocation entries apply to.
        /// </summary>
        [FieldOffset(0x03)] public byte section;

        /// <summary>
        ///     Offset in bytes of the symbol to relocate against, relative to the start of its section.
        ///     This is an absolute address instead for relocations against main.dol.
        /// </summary>
        [FieldOffset(0x04)] public Offset addEnd;

        /// <summary>
        ///     The value raw (64 bit).
        /// </summary>
        [FieldOffset(0x00)] public ulong raw;


        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref raw);
        }

        public Pointer ResolveAddress(int baseAddress)
        {
            Pointer pointer = baseAddress + addEnd;
            return pointer;
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(raw);
        }
    }
}


