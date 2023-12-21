using Manifold.IO;
using System.Collections.Generic;

namespace GameCube.GFZ.LineREL
{
    public class MemoryPool
    {
        public List<MemoryArea> MemoryAreas = new();

        public MemoryPool()
        {
        }
        public MemoryPool(params MemoryArea[] memoryAreas)
        {
            MemoryAreas.AddRange(memoryAreas);
        }

        public bool CanAllocateSize(int size)
        {
            // Find largest contiguous size that can be allocated
            int maxContiguousSize = 0;
            foreach (MemoryArea memoryArea in MemoryAreas)
            {
                bool isLarger = memoryArea.RemainingMemorySize > maxContiguousSize;
                if (isLarger)
                {
                    maxContiguousSize = memoryArea.RemainingMemorySize;
                }
            }

            // See if size fits in it
            bool canAllocateSize = size <= maxContiguousSize;
            return canAllocateSize;
        }

        public Pointer AllocateMemory(int size, int alignment = 0)
        {
            foreach (MemoryArea memoryArea in MemoryAreas)
            {
                int alignedSize = memoryArea.GetAlignedSize(size, alignment);
                if (!memoryArea.CanAllocateSize(alignedSize))
                    continue;

                Pointer pointer = memoryArea.AllocateMemory(alignedSize);
                return pointer;
            }

            // If it fails, return null
            return Pointer.Null;
        }

        public Pointer AllocateMemoryWithError(int size, int alignment = 0)
        {
            Pointer pointer = AllocateMemory(size, alignment);

            if (pointer.IsNull)
            {
                string msg = $"{nameof(MemoryPool)} ran out of memory.";
                throw new System.InsufficientMemoryException();
            }

            return pointer;
        }

        public int TotalMemorySize()
        {
            int size = 0;
            foreach(MemoryArea memoryArea in MemoryAreas)
                size += memoryArea.MemorySize;
            return size;
        }

        public int RemainingMemorySize()
        {
            int remainingMemory = 0;
            foreach (MemoryArea memoryArea in MemoryAreas)
                remainingMemory += memoryArea.RemainingMemorySize;
            return remainingMemory;
        }

        public void PadEmptyMemory(EndianBinaryWriter writer, byte padding = 0x00)
        {
            foreach (MemoryArea memoryArea in MemoryAreas)
            {
                memoryArea.PadEmptyMemory(writer, padding);
            }
        }
    }
}
