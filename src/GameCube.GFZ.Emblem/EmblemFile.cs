// 2025-02-02: GCI currently requires IBinaryFileType, so emblem here is wrapped
// in that type to satisfy that constraint. Otherwise, not needed, just pass Emblem
// into GfzGci<Emblem>

using Manifold.IO;

namespace GameCube.GFZ.Emblem;

/// <summary>
///     A file wrapped <see cref="Emblem"/> to fit inside a GCI.
/// </summary>
public class EmblemFile : BinaryFileWrapper<Emblem>
{
    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string extension = ".bin";

    // PROPERTIES
    public override Endianness Endianness { get; set; } = endianness;
    public override string FileExtension { get; set; } = extension;
    public override string FileName { get; set; } = string.Empty;

    // CONSTRUCTORS
    public EmblemFile() : base() { }
    public EmblemFile(string inputPath) : base(inputPath) { }
}
