namespace GameCube.GFZ.REL
{
    public class CustomizableArea
    {
        public int Address { get; }
        public int Size { get; }
        public int Occupied { get; set; }
        public CustomizableArea(int address, int size)
        {
            Address = address;
            Size = size;
            Occupied = 0;
        }
    }
}
