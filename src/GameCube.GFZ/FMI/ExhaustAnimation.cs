using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class ExhaustAnimation :
        IBinarySerializable,
        IBinaryAddressable
    {
        // FIELDS
        public float3 position;
        public int unk_0x0C;
        public int animType;


        // PROEPRTIES
        public AddressRange AddressRange { get; set; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref position);
                reader.Read(ref unk_0x0C);
                reader.Read(ref animType);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(position);
                writer.Write(unk_0x0C);
                writer.Write(animType);
            }
            this.RecordEndAddress(writer);
        }

    }
}