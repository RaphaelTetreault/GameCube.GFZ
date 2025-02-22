using Manifold.IO;

namespace GameCube.GFZ.CarData;

/// <summary>
///     File wrapper for <see cref="CarData"/>.
/// </summary>
/// <remarks>
///     File only exists in F-Zero GX.
/// </remarks>
public class CarDataFile : BinaryFileWrapper<CarData>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.LittleEndian;
    /// <summary>
    ///     The file extension for CarData file. There is none.
    /// </summary>
    public const string fileExtension = "";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = fileExtension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public CarDataFile() : base() { }
    public CarDataFile(string inputPath) : base(inputPath) { }
}