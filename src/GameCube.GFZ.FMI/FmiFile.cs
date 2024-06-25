using Manifold.IO;

namespace GameCube.GFZ.FMI
{
    public class FmiFile : BinaryFileWrapper<Fmi>
    {
        // CONSTANTS
        public const Endianness endianness = Endianness.BigEndian;
        public const string extension = ".fmi";

        // PROPERTIES
        public override Endianness Endianness => endianness;
        public override string FileExtension => extension;
        public override string FileName { get; set; } = string.Empty;
        public override string Version { get; } = "1";
    }
}
