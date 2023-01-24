using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Camera
{
    [Serializable]
    public sealed class CameraPan :
        IBinaryAddressable,
        IBinarySerializable
    {
        // CONSTANTS
        public const int kStructureSize = 0x54;
        private const int kZeroes0x08 = 4;

        // METADATA
        private AddressRange addressRange;

        // FIELDS
        private int frameCount;
        private float lerpSpeed;
        private byte[] zeroes0x08;
        private CameraPanTarget from = new CameraPanTarget();
        private CameraPanTarget to = new CameraPanTarget();


        // PROPERTIES
        public AddressRange AddressRange
        {
            get => addressRange;
            set => addressRange = value;
        }

        public int FrameCount
        {
            get => frameCount;
            set => frameCount = value;
        }
        public float LerpSpeed
        {
            get => lerpSpeed;
            set => lerpSpeed = value;
        }
        public CameraPanTarget From
        {
            get => from;
            set => from = value;
        }
        public CameraPanTarget To
        {
            get => to;
            set => to = value;
        }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref frameCount);
                reader.Read(ref lerpSpeed);
                reader.Read(ref zeroes0x08, kZeroes0x08);
                reader.Read(ref from);
                reader.Read(ref to);
            }
            this.RecordEndAddress(reader);

            // Assertions
            foreach (var @byte in zeroes0x08)
                Assert.IsTrue(@byte == 0);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(frameCount);
                writer.Write(lerpSpeed);
                writer.Write(new byte[kZeroes0x08]);
                writer.Write(from);
                writer.Write(to);
            }
            this.RecordEndAddress(writer);
        }
    }
}
