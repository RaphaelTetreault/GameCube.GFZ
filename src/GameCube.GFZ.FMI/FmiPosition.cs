using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class FmiPosition :
        IBinarySerializable,
        IBinaryAddressable,
        IPlainTextSerializable
    {
        // FIELDS
        private float3 position;
        private int zero_0x0C;
        private FmiPositionType positionType;


        // PROEPRTIES
        public AddressRange AddressRange { get; set; }
        public float3 Position { get => position; set => position = value; }
        public FmiPositionType PositionType { get => positionType; set => positionType = value; }


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

        public void Deserialize(PlainTextReader reader)
        {
            reader.ReadValue(ref position.x);
            reader.ReadValue(ref position.y);
            reader.ReadValue(ref position.z);
            reader.ReadValue(ref positionType);
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

        public void Serialize(PlainTextWriter writer)
        {
            writer.WriteLineValue(nameof(position) + ".X", position.x);
            writer.WriteLineValue(nameof(position) + ".Y", position.y);
            writer.WriteLineValue(nameof(position) + ".Z", position.z);
            writer.WriteLineValue(nameof(positionType), positionType);
            writer.WriteLineComment($"{positionType} is value {(uint)PositionType}");
        }
    }
}