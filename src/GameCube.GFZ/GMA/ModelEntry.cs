using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Defines offsets to a model's name and GCMF data.
    /// </summary>
    public class ModelEntry :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference
    {
        // FIELDS
        private Offset gcmfPtrOffset;
        private Offset namePtrOffset;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public Pointer GcmfPtr { get => new Pointer(GcmfBasePtrOffset + GcmfRelPtr); }
        public Pointer NamePtr { get => new Pointer(NameBasePtrOffset + NameRelPtr); }
        public Offset GcmfRelPtr { get => gcmfPtrOffset; set => gcmfPtrOffset = value; }
        public Offset NameRelPtr { get => namePtrOffset; set => namePtrOffset = value; }
        public Offset GcmfBasePtrOffset { get; set; }
        public Offset NameBasePtrOffset { get; set; }
        public bool IsNull { get => gcmfPtrOffset == -1 && NameRelPtr == 0; }
        public bool IsNotNull { get => !IsNull; }

        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref gcmfPtrOffset);
                reader.Read(ref namePtrOffset);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(GcmfRelPtr);
                writer.Write(NameRelPtr);
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            throw new System.NotImplementedException();
        }
    }
}
