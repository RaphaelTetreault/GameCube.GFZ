using Manifold.IO;

namespace GameCube.GFZ.TPL;

/// <summary>
///     File wrapper for <see cref="GxTexture"/>.
/// </summary>
public class GxTextureFile : BinaryFileWrapper<GxTexture>
{
    // CONSTANTS
    public const Endianness endianness = TplFile.endianness;
    public const string extension = ".gxtex";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = extension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS, override base helpers
    public GxTextureFile() : base() { }
    public GxTextureFile(string inputPath) : base(inputPath) { }
}
