using Manifold.IO;

namespace GameCube.GFZ.LineREL
{
    public class MemoryArea
    {
        public Pointer BaseAddress { get; }
        public AddressRange AddressRange { get; }
        public Pointer CurrentAddress => BaseAddress + CurrentOffset;
        public Offset CurrentOffset => MemorySize - RemainingMemorySize;
        public int MemorySize { get; }
        public int RemainingMemorySize { get; private set; }

        public MemoryArea(DataBlock dataBlock)
        {
            BaseAddress = dataBlock.Address;
            MemorySize = dataBlock.Size;
            AddressRange = new AddressRange()
            {
                startAddress = BaseAddress,
                endAddress = BaseAddress + MemorySize,
            };
            RemainingMemorySize = MemorySize;
        }

        public MemoryArea(Pointer address, int size)
        {
            BaseAddress = address;
            MemorySize = size;
            AddressRange = new AddressRange()
            {
                startAddress = address,
                endAddress = address + size,
            };
            RemainingMemorySize = size;
        }

        public MemoryArea(AddressRange addressRange)
        {
            AddressRange = addressRange;
            BaseAddress = AddressRange.startAddress;
            MemorySize = AddressRange.Size;
            RemainingMemorySize = MemorySize;
        }


        /// Returns the length required to align stream to alignment
        /// name="stream">The stream to base alignment length from.
        /// name="alignment">The alignment stride.
        public static int GetLengthOfAlignment(MemoryArea memoryArea, int alignment)
        {
            // Skip cases where no alignment is needed
            if (alignment < 2)
                return 0;

            // Get number of bytes needed to aligment
            int bytesToPad = alignment - (memoryArea.CurrentAddress % alignment);
            // If bytes to pad is alignment size, then no bytes to pad
            int finalAlignment = (bytesToPad == alignment) ? 0 : bytesToPad;
            return finalAlignment;
        }

        public int GetAlignedSize(int size, int alignment)
        {
            int bytesToPad = GetLengthOfAlignment(this, alignment);
            int alignedSize = size + bytesToPad;
            return alignedSize;
        }

        public bool CanAllocateSize(int size)
        {
            bool canAllocateSize = size < RemainingMemorySize;
            return canAllocateSize;
        }

        public Pointer AllocateMemory(int size)
        {
            if (CanAllocateSize(size))
            {
                Offset offset = MemorySize - RemainingMemorySize;
                Pointer address = BaseAddress + offset;
                RemainingMemorySize -= size;
                return address;
            }
            else
            {
                return Pointer.Null;
            }
        }

        public Pointer AllocateMemory(int size, int alignment)
        {
            int alignedSize = GetAlignedSize(size, alignment);
            Pointer pointer = AllocateMemory(alignedSize);
            return pointer;
        }

        public Pointer AllocateMemoryWithError(int size)
        {
            Pointer pointer = AllocateMemory(size);

            if (pointer.IsNull)
            {
                string msg = $"{nameof(MemoryArea)} ran out of memory.";
                throw new System.InsufficientMemoryException();
            }

            return pointer;
        }

        public Pointer AllocateMemoryWithError(int size, int alignment)
        {
            int alignedSize = GetAlignedSize(size, alignment);
            Pointer pointer = AllocateMemoryWithError(alignedSize);
            return pointer;
        }

        public void PadEmptyMemory(EndianBinaryWriter writer, byte padding = 0x00)
        {
            writer.JumpToAddress(CurrentAddress);
            writer.WritePadding(padding, RemainingMemorySize);
        }
    }
}
