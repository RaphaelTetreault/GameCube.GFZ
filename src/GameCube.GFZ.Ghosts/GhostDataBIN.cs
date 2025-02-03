using Manifold.IO;

namespace GameCube.GFZ.Ghosts;

/// <summary>
///     File wrapper for <see cref="GhostData"/>.
/// </summary>
/// <remarks>
///     Staff ghost file.
/// </remarks>
public class GhostDataBIN : BinaryFileWrapper<GhostData>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string extension = ".bin";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = extension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public GhostDataBIN() : base() { }
    public GhostDataBIN(string inputPath) : base(inputPath) { }
}
