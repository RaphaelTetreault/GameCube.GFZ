using Manifold.IO;
using System;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class FmiUnknown :
        IBinarySerializable,
        IBinaryAddressable
    {
        //
        public const int kPaddingSize = 0x34;

        // FIELDS
        public byte unk_0x00;   // 0x00 - 01
        public float unk_0x01;  // 0x01 - 05
        public byte unk_0x05;   // 0x05 - 06
        public float unk_0x06;  // 0x06 - 0A
        public ushort unk_0x0A; // 0x0A - 0C
        public byte[] zeroPadding_0x34;

        // PROPERTIES
        public AddressRange AddressRange { get; set; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref unk_0x00);
                reader.Read(ref unk_0x01);
                reader.Read(ref unk_0x05);
                reader.Read(ref unk_0x06);
                reader.Read(ref unk_0x0A);
                reader.Read(ref zeroPadding_0x34, kPaddingSize);
            }
            this.RecordEndAddress(reader);
            {
                for (int i = 0; i < zeroPadding_0x34.Length; i++)
                {
                    Assert.IsTrue(zeroPadding_0x34[i] == 0, i.ToString());
                }
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(unk_0x00);
                writer.Write(unk_0x01);
                writer.Write(unk_0x05);
                writer.Write(unk_0x06);
                writer.Write(unk_0x0A);
                writer.WritePadding(0x00, kPaddingSize);
            }
            this.RecordEndAddress(writer);
        }

    }
}