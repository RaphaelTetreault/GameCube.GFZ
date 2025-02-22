using Manifold.IO;

namespace GameCube.GFZ.Camera;

/// <summary>
///     File wrapper for <see cref="LiveCameraStage"/>.
/// </summary>
public class LiveCameraStageFile : BinaryFileWrapper<LiveCameraStage>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string fileExtension = ".bin";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = fileExtension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public LiveCameraStageFile() : base() { }
    public LiveCameraStageFile(string inputPath) : base(inputPath) { }
}