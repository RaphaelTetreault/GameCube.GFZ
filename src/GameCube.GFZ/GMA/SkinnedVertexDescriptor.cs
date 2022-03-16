using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.GMA
{
    public class SkinnedVertexDescriptor :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private int skinnedVertexBCount;
        private Offset skinnedVerticesAPtrOffset;
        private Offset skinnedVerticesBPtrOffset;
        private Offset skinBoneBindingsPtrOffset;
        private Offset unkBoneIndicesPtrOffset;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public int SkinnedVerticesACount
        {
            get => (UnkBoneIndicesPtrOffset - SkinnedVerticesAPtrOffset) / 0x20;
        }
        public int SkinnedVerticesBCount { get => skinnedVertexBCount; set => skinnedVertexBCount = value; }
        public Offset SkinnedVerticesAPtrOffset { get => skinnedVerticesAPtrOffset; set => skinnedVerticesAPtrOffset = value; }
        public Offset SkinnedVerticesBPtrOffset { get => skinnedVerticesBPtrOffset; set => skinnedVerticesBPtrOffset = value; }
        public Offset SkinBoneBindingsPtrOffset { get => skinBoneBindingsPtrOffset; set => skinBoneBindingsPtrOffset = value; }
        public Offset UnkBoneIndicesPtrOffset { get => unkBoneIndicesPtrOffset; set => unkBoneIndicesPtrOffset = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref skinnedVertexBCount);
                reader.Read(ref skinnedVerticesAPtrOffset);
                reader.Read(ref skinnedVerticesBPtrOffset);
                reader.Read(ref skinBoneBindingsPtrOffset);
                reader.Read(ref unkBoneIndicesPtrOffset);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(skinnedVertexBCount);
                writer.Write(skinnedVerticesAPtrOffset);
                writer.Write(skinnedVerticesBPtrOffset);
                writer.Write(skinBoneBindingsPtrOffset);
                writer.Write(unkBoneIndicesPtrOffset);
            }
            this.RecordEndAddress(writer);
        }

    }

}