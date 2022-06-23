﻿using Manifold.IO;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// 
    /// </summary>
    public class UnkSubmeshType :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private float3 origin;
        private uint unk0x0C;
        private UnkFlags0x10 unk0x10; // 0xF bitmask for src blend factor, 0xF0 for dst blend factor


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public float3 Origin { get => origin; set => origin = value; }
        public uint Unk0x3C { get => unk0x0C; set => unk0x0C = value; }
        public UnkFlags0x10 Unk0x40 { get => unk0x10; set => unk0x10 = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref origin);
                reader.Read(ref unk0x0C);
                reader.Read(ref unk0x10);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(origin);
                writer.Write(unk0x0C);
                writer.Write(unk0x10);
            }
            this.RecordEndAddress(writer);
        }

    }

}