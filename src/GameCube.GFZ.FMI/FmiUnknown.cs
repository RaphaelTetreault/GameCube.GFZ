using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class FmiUnknown :
        IBinarySerializable,
        IBinaryAddressable,
        ITextSerializable
    {
        //
        public const int kPaddingSize = 0x34;

        // FIELDS
        private byte unk_0x00;   // 0x00 - 01
        private float unk_0x01;  // 0x01 - 05
        private byte unk_0x05;   // 0x05 - 06
        private float unk_0x06;  // 0x06 - 0A
        private ushort unk_0x0A; // 0x0A - 0C
        private byte[] zeroPadding_0x34 = Array.Empty<byte>();

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public byte Unk_0x00 { get => unk_0x00; set => unk_0x00 = value; }
        public float Unk_0x01 { get => unk_0x01; set => unk_0x01 = value; }
        public byte Unk_0x05 { get => unk_0x05; set => unk_0x05 = value; }
        public float Unk_0x06 { get => unk_0x06; set => unk_0x06 = value; }
        public ushort Unk_0x0A { get => unk_0x0A; set => unk_0x0A = value; }


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

        public void Deserialize(StreamReader reader)
        {
            throw new NotImplementedException();
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

        public void Serialize(StreamWriter writer)
        {
            writer.WriteLine($"{nameof(unk_0x00)}: {unk_0x00}");
            writer.WriteLine($"{nameof(unk_0x01)}: {unk_0x01}");
            writer.WriteLine($"{nameof(unk_0x05)}: {unk_0x05}");
            writer.WriteLine($"{nameof(unk_0x06)}: {unk_0x06}");
            writer.WriteLine($"{nameof(unk_0x0A)}: {unk_0x0A}");
        }
    }
}