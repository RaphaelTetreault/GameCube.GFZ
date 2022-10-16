namespace GameCube.GFZ.REL
{
    public class Information
    {
        public int Address { get; }
        public int Size { get; }

        public Information(int address, int size)
        {
            Address = address;
            Size = size;
        }
    }
}
