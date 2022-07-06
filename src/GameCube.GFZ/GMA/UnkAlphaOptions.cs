using Manifold.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// 
    /// </summary>
    public class UnkAlphaOptions :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private float3 origin;
        private float unk0x0C; // if GCMF flags at 0x00 are set with bit 9, this value exists. All values: 0f, 1f.
        private BlendFactors unk0x10; // 0xF bitmask for src blend factor, 0xF0 for dst blend factor


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public float3 Origin { get => origin; set => origin = value; }
        public float Unk0x0C { get => unk0x0C; set => unk0x0C = value; }
        public BlendFactors Unk0x10 { get => unk0x10; set => unk0x10 = value; }


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