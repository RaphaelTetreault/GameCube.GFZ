using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// A grid of index lists. Grid is used as base class for other *Grid types
    /// to index collider triangles/quads (static meshes) and checkpoint nodes.
    /// </summary>
    [Serializable]
    public abstract class IndexGrid :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // FIELDS
        private Pointer[] indexListPtrs;
        // REFERENCE FIELDS
        private IndexList[] indexLists;


        public IndexGrid()
        {
            // Initialize array to default/const size.
            // Requires inheriter to finalize count.
            IndexLists = new IndexList[Count];
            for (int i = 0; i < IndexLists.Length; i++)
            {
                IndexLists[i] = new IndexList();
            }
            
            IndexListPtrs = new Pointer[Count];
        }


        // INDEXERS
        public IndexList this[int index] { get => indexLists[index]; set => indexLists[index] = value; }
        public IndexList this[int x, int z] { get => indexLists[z * SubdivisionsX + x]; set => indexLists[z * SubdivisionsX + x] = value; }

        // ABSTRACT PROPERTIES
        public abstract int SubdivisionsX { get; }
        public abstract int SubdivisionsZ { get; }

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public int Count => SubdivisionsX * SubdivisionsZ;
        public ushort LargestIndex { get; private set; }
        public bool HasIndexes { get; private set; }
        public ushort IndexesLength
        {
            get
            {
                // Returns index length based on metadata
                return HasIndexes
                    ? (ushort)(LargestIndex + 1)
                    : (ushort)(0);
            }
        }
        public Pointer[] IndexListPtrs { get => indexListPtrs; set => indexListPtrs = value; }
        public IndexList[] IndexLists
        {
            get => indexLists;
            set  { indexLists = value; UpdateHasIndexes(); }
        }


        // METHODS
        private bool HasAnyIndexes(IndexList[] indexLists)
        {
            foreach (var indexList in indexLists)
            {
                if (indexList is null)
                    continue;

                if (indexList.Length > 0)
                    return true;
            }

            return false;
        }

        private void UpdateHasIndexes()
        {
            HasIndexes = HasAnyIndexes(indexLists);
        }

        private ushort GetLargestIndex(IndexList[] indexLists)
        {
            // Find the largest known index to use as tri/quad array size
            // The game probably just reads indices dynamically using address + index * tri/quad size

            // Record largest idnex
            ushort largestIndex = 0;

            // Iterate through all indices to find largest
            foreach (var indexList in indexLists)
            {
                foreach (var index in indexList.Indexes)
                {
                    if (index > largestIndex)
                    {
                        largestIndex = index;
                    }
                }
            }

            return largestIndex;
        }

        private ushort TotalNonZeroLists()
        {
            ushort count = 0;
            foreach (var indexList in indexLists)
            {
                if (indexList.Length > 0)
                    count++;
            }

            return count;
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            // Read index arrays
            this.RecordStartAddress(reader);
            {
                reader.Read(ref indexListPtrs, Count);
            }
            this.RecordEndAddress(reader);
            {
                indexLists = new IndexList[Count];

                // Should always be init to const size by now
                Assert.IsTrue(indexListPtrs.Length == Count);
                Assert.IsTrue(indexLists.Length == Count);

                for (int i = 0; i < Count; i++)
                {
                    // init value
                    indexLists[i] = new IndexList();

                    var indexArrayPtr = IndexListPtrs[i];
                    if (indexArrayPtr.IsNotNull)
                    {
                        reader.JumpToAddress(indexArrayPtr);
                        indexLists[i].Deserialize(reader);
                    }
                }

                // Calculate metadata
                LargestIndex = GetLargestIndex(indexLists);
                HasIndexes = HasAnyIndexes(indexLists);
            }
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                // Ensure we have the correct amount of lists before indexing
                Assert.IsTrue(indexLists.Length == Count);

                // Construct Pointer[]
                var pointers = new Pointer[Count];
                for (int i = 0; i < pointers.Length; i++)
                    pointers[i] = indexLists[i].GetPointer();
                indexListPtrs = pointers;
            }
            this.RecordStartAddress(writer);
            {
                //ValidateReferences();

                // Write all pointers
                for (int ptrIndex = 0; ptrIndex < indexListPtrs.Length; ptrIndex++)
                {
                    var ptr = indexListPtrs[ptrIndex];
                    writer.Write(ptr);
                }
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            // Should always be init to const size
            Assert.IsTrue(indexListPtrs.Length == Count);
            Assert.IsTrue(indexLists.Length == Count);

            for (int i = 0; i < Count; i++)
            {
                var indexList = indexLists[i];
                var pointer = indexListPtrs[i];

                if (indexList.Length != 0)
                    Assert.ReferencePointer(indexList, pointer);
            }
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            var countNonZeroLists = TotalNonZeroLists();

            builder.AppendLineIndented(indent, indentLevel, GetType().Name);
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(SubdivisionsX)}: {SubdivisionsX}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(SubdivisionsZ)}: {SubdivisionsZ}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Count)}: {Count}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(LargestIndex)}: {LargestIndex}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(IndexLists)}[{Count}] (Non-zero index lists: {countNonZeroLists})");
            indentLevel++;

            for (int i = 0; i < indexLists.Length; i++)
            {
                var indexList = indexLists[i];
                if (indexList.Length == 0)
                    continue;

                // Write a little header with an [index] marker
                builder.AppendRepeat(indent, indentLevel);
                // Print "IndexList[A][B]", A = 64 or 256 (grid size), B = number of indexes in group
                builder.Append($"{nameof(IndexList)}[{i}][{indexList.Length}]".PadRight(16) + "\t");
                // Write all the values from the index list consecutively
                foreach (var value in indexList.Indexes)
                    builder.Append($" {value,5},");
                builder.AppendLine();
            }

        }

        public string PrintSingleLine()
        {
            return $"{GetType().Name}({SubdivisionsX}: {SubdivisionsX}, {SubdivisionsZ}: {SubdivisionsZ})";
        }

        public override string ToString() => PrintSingleLine();

    }
}
