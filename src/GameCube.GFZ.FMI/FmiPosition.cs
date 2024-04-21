using Manifold.IO;
using System;
using Unity.Mathematics;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class FmiPosition :
        IBinarySerializable,
        IBinaryAddressable
    {
        // FIELDS
        public float3 position;
        public int zero_0x0C;
        public FmiPositionType positionType;


        // PROEPRTIES
        public AddressRange AddressRange { get; set; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref position);
                reader.Read(ref zero_0x0C);
                reader.Read(ref positionType);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(zero_0x0C == 0);
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(position);
                writer.Write(zero_0x0C);
                writer.Write(positionType);
            }
            this.RecordEndAddress(writer);
        }

    }
}