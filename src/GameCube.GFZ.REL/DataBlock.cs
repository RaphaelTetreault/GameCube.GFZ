namespace GameCube.GFZ.REL;

/// <summary>
///     Repsents an arbitrary block/area of data/memory.
/// </summary>
public readonly record struct DataBlock
{
    /// <summary>
    ///     A null data block(<see cref="Address"/> and <see cref="Size"/> of 0).
    /// </summary>
    public static readonly DataBlock Null = new(0, 0);

    /// <summary>
    ///     Base address of this <see cref="DataBlock"/>.
    /// </summary>
    public int Address { get; init; }

    /// <summary>
    ///     Size of this <see cref="DataBlock"/> in bytes.
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    ///     Create a new <see cref="DataBlock"/> representing base <paramref name="address"/>
    ///     of <paramref name="size"/> in bytes.
    /// </summary>
    /// <param name="address">Base address for this <see cref="DataBlock"/>.</param>
    /// <param name="size">Size in bytes of this <see cref="DataBlock"/>.</param>
    public DataBlock(int address, int size)
    {
        Address = address;
        Size = size;
    }
}
