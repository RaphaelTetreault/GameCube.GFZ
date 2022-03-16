using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Conjecture: appears to 
    /// </summary>
    public class SkinBoneBinding :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference
    {
        // FIELDS
        private int count;
        private Offset[] verticePtrOffsets;

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public int Count { get => verticePtrOffsets.Length; }
        public Offset[] VerticePtrOffsets { get => verticePtrOffsets; set => verticePtrOffsets = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref count);
                reader.Read(ref verticePtrOffsets, count);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                // TODO: assign offsets here? (similar to getting pointers)
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(Count);
                writer.Write(verticePtrOffsets);
            }
            this.RecordEndAddress(writer);
            {
                //throw new NotImplementedException();
            }
        }

        public void ValidateReferences()
        {
            throw new NotImplementedException();
        }
    }

}