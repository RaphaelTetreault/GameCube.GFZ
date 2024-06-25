using Manifold.IO;

namespace GameCube.GFZ.FMI;

/// <summary>
///     File wrapper for <see cref="Fmi"/>.
/// </summary>
public class FmiFile : BinaryFileWrapper<Fmi>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string extension = ".fmi";

    // PROPERTIES
    public override Endianness Endianness => endianness;
    public override string FileExtension => extension;
    public override string FileName { get; set; } = string.Empty;
}
