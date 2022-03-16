using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Defines the size of opaque and translucide display lists and (presumably)
    /// the matrix indexes associate with it.
    /// </summary>
    /// <remarks>
    /// (TODO: find out exactly how. Theory: # mtx indexes = # display lists?
    /// </remarks>
    public class DisplayListDescriptor :
        IBinaryAddressable,
        IBinarySerializable
    {
        // FIELDS
        private BoneIndexes8 boneIndices;
        private int opaqueMaterialDisplayListSize;
        private int translucidMaterialDisplayListSize;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public int OpaqueMaterialSize { get => opaqueMaterialDisplayListSize; set => opaqueMaterialDisplayListSize = value; }
        public BoneIndexes8 BoneIndices { get => boneIndices; set => boneIndices = value; }
        public int TranslucidMaterialSize { get => translucidMaterialDisplayListSize; set => translucidMaterialDisplayListSize = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref boneIndices);
                reader.Read(ref opaqueMaterialDisplayListSize);
                reader.Read(ref translucidMaterialDisplayListSize);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(boneIndices);
                writer.Write(opaqueMaterialDisplayListSize);
                writer.Write(translucidMaterialDisplayListSize);
            }
            this.RecordEndAddress(writer);
        }

    }
}