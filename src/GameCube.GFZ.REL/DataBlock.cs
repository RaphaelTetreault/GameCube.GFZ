namespace GameCube.GFZ.LineREL
{
    public class DataBlock
    {
        public int Address { get; }
        public int Size { get; }

        public DataBlock(int address, int size)
        {
            Address = address;
            Size = size;
        }
    }
}
