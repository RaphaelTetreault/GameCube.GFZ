namespace GameCube.GFZ.LineREL;

public readonly record struct DataBlock
{
    public int Address { get; init; }
    public int Size { get; init; }

    public DataBlock(int address, int size)
    {
        Address = address;
        Size = size;
    }
}
