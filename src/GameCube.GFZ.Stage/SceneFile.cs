using Manifold.IO;

namespace GameCube.GFZ.Stage;

/// <summary>
///     File wrapper for <see cref="Scene"/>.
/// </summary>
public class SceneFile : BinaryFileWrapper<Scene>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    /// <summary>
    ///     The file extension for Scene (COLI_COURSE##). There is none.
    /// </summary>
    public const string fileExtension = "";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = fileExtension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public SceneFile() : base() { }
    public SceneFile(string inputPath) : base(inputPath) { }
}