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
                return 0;
            }
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

        public void PadEmptyMemory(EndianBinaryWriter writer, byte padding = 0x00)
        {
            writer.JumpToAddress(CurrentAddress);
            writer.WritePadding(padding, RemainingMemorySize);
        }
    }
}
