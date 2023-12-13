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

        //public bool CanAllocateSize(int size)
        //{
        //    bool canAllocateSize = size < RemainingMemorySize();
        //    return canAllocateSize;
        //}
        public Pointer AllocateMemory(int size)
        {
            foreach (MemoryArea memoryArea in MemoryAreas)
            {
                if (!memoryArea.CanAllocateSize(size))
                    continue;

                Pointer pointer = memoryArea.AllocateMemory(size);
                return pointer;
            }

            // If it fails, return null
            return 0;
        }
        public Pointer AllocateMemoryWithError(int size)
        {
            Pointer pointer = AllocateMemory(size);

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
