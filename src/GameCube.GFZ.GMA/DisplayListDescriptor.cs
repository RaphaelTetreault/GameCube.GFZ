using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// Defines the size of display lists and (presumably)
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
        private BoneIndexes8 boneIndices = new BoneIndexes8();
        private int frontFaceCullingDisplayListSize;
        private int backtFaceCullingDisplayListSize;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public int FrontFaceCullingDisplayListSize { get => frontFaceCullingDisplayListSize; set => frontFaceCullingDisplayListSize = value; }
        public BoneIndexes8 BoneIndices { get => boneIndices; set => boneIndices = value; }
        public int BackFaceCullingDisplayListSize { get => backtFaceCullingDisplayListSize; set => backtFaceCullingDisplayListSize = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref boneIndices);
                reader.Read(ref frontFaceCullingDisplayListSize);
                reader.Read(ref backtFaceCullingDisplayListSize);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(boneIndices);
                writer.Write(frontFaceCullingDisplayListSize);
                writer.Write(backtFaceCullingDisplayListSize);
            }
            this.RecordEndAddress(writer);
        }

    }
}