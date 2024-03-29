﻿using Manifold.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// A set of 8 matrix indexes. Root indexes read as -1/0xFF, indicating no parent bone/matrix.
    /// </summary>
    /// <remarks>
    /// Consider making the backing a uint64, contruct array on demand.
    /// </remarks>
    public class BoneIndexes8 :
        IBinaryAddressable,
        IBinarySerializable
    {
        // CONSTANTS
        private const int kIndexCount = 8;

        // FIELDS
        private sbyte[] indexes = new sbyte[] { -1, -1, -1, -1, -1, -1, -1, -1, };


        // INDEXERS
        public sbyte this[int index] { get => indexes[index]; set => indexes[index] = value; }

        // PROPERTIES
        public AddressRange AddressRange { get; set; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref indexes, kIndexCount);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                Assert.IsTrue(indexes.Length == kIndexCount);
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(indexes);
            }
            this.RecordEndAddress(writer);
        }

    }

}