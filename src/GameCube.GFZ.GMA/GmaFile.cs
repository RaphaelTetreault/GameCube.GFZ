using Manifold.IO;

namespace GameCube.GFZ.GMA;

/// <summary>
///     File wrapper for <see cref="Gma"/>.
/// </summary>
public class GmaFile : BinaryFileWrapper<Gma>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string extension = ".gma";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = extension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public GmaFile() : base() { }
    public GmaFile(string inputPath) : base(inputPath) { }
}
