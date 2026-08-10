using Manifold.IO;

namespace GameCube.GFZ.Camera;

/// <summary>
///     File wrapper for <see cref="LiveCamera"/>.
/// </summary>
public class LiveCameraFile : BinaryFileWrapper<LiveCamera>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string fileExtension = ".bin";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = fileExtension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public LiveCameraFile() : base() { }
    public LiveCameraFile(string inputPath) : base(inputPath) { }
}