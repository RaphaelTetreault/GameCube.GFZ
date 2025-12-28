using GameCube.GFZ.TPL;
using Manifold.IO;

namespace GameCube.GFZ.Asset;

/// <summary>
///     File wrapper for <see cref="GxTextureAsset"/>.
/// </summary>
public class GxTextureAssetFile : BinaryFileWrapper<GxTextureAsset>
{
    // CONSTANTS
    public const Endianness endianness = TplFile.endianness;
    public const string extension = ".gxtex";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = extension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS, override base helpers
    public GxTextureAssetFile() : base() { }
    public GxTextureAssetFile(string inputPath) : base(inputPath) { }
}
