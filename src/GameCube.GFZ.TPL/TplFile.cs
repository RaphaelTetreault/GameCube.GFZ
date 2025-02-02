using Manifold.IO;

namespace GameCube.GFZ.TPL;

/// <summary>
///     File wrapper for <see cref="Tpl"/>.
/// </summary>
public class TplFile : BinaryFileWrapper<Tpl>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string extension = ".tpl";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = extension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS, override base helpers
    public TplFile() : base() { }
    public TplFile(string inputPath) : base(inputPath) { }
}
