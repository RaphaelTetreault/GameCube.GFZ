using GameCube.GFZ.GMA;
using Manifold.IO;

namespace GameCube.GFZ.Asset;

/// <summary>
///     File wrapper for <see cref="GcmfAsset"/>.
/// </summary>
public class GcmfAssetFile : BinaryFileWrapper<GcmfAsset>
{
    // CONSTANTS
    public const Endianness endianness = GmaFile.endianness;
    public const string extension = ".gcmfx";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = extension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public GcmfAssetFile() : base() { }
    public GcmfAssetFile(string inputPath) : base(inputPath) { }
}
