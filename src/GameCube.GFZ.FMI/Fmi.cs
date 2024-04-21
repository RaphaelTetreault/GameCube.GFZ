using Manifold.IO;
using System;

namespace GameCube.GFZ.FMI
{
    [Serializable]
    public class Fmi :
        IBinaryAddressable,
        IBinarySerializable,
        IBinaryFileType
        //IFileType
    {
        // CONSTANTS
        private const uint kParticlesAbsPtr = 0x0044;
        private const uint kAnimationAbsPtr = 0x0208;
        private const uint kAnimationNameAbsPtr = 0x02A0;
        public const Endianness endianness = Endianness.BigEndian;

        // FIELDS
        public ushort zero_0x00;
        public byte animationCount; 
        public byte exhaustCount;
        public FmiUnknown unknown = new();
        // REFERENCES
        public FmiEmitter[] emitters = Array.Empty<FmiEmitter>();
        public FmiPosition[] positions = Array.Empty<FmiPosition>();
        public ShiftJisCString[] names = Array.Empty<ShiftJisCString>();

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileExtension => ".fmi";
        public Endianness Endianness => endianness;


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref zero_0x00);
                reader.Read(ref animationCount);
                reader.Read(ref exhaustCount);
                reader.Read(ref unknown);
            }
            this.RecordEndAddress(reader);
            {
                bool hasExhaustEmitters = exhaustCount > 0;
                if (hasExhaustEmitters)
                {
                    reader.JumpToAddress(kParticlesAbsPtr);
                    reader.Read(ref emitters, animationCount);
                }

                bool hasAnimations = animationCount > 0;
                if (hasAnimations)
                {
                    reader.JumpToAddress(kAnimationAbsPtr);
                    reader.Read(ref positions, animationCount);

                    reader.JumpToAddress(kAnimationNameAbsPtr);
                    reader.Read(ref names, animationCount);
                }
            }
            this.SetReaderToEndAddress(reader);
            {
                Assert.IsTrue(zero_0x00 == 0);
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new NotImplementedException();
        }


    }
}