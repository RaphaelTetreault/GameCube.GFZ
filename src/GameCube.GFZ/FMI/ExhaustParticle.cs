using GameCube.GX;
using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class ExhaustParticle :
        IBinarySerializable,
        IBinaryAddressable
    {
        // FIELDS
        public float3 position;
        public uint unk_0x0C;
        public uint unk_0x10;
        public float scaleMin;
        public float scaleMax;
        // Engine Color of Normal Acceleration
        public GXColor colorMin;
        // Engine Color of Strong Acceleration
        public GXColor colorMax;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref position);
                reader.Read(ref unk_0x0C);
                reader.Read(ref unk_0x10);
                reader.Read(ref scaleMin);
                reader.Read(ref scaleMax);
                reader.Read(ref colorMin);
                reader.Read(ref colorMax);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(position);
                writer.Write(unk_0x0C);
                writer.Write(unk_0x10);
                writer.Write(scaleMin);
                writer.Write(scaleMax);
                writer.Write(colorMin);
                writer.Write(colorMax);
            }
            this.RecordEndAddress(writer);
        }

    }
}