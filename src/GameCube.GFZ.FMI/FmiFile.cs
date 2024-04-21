using Manifold.IO;
using System.Text.Json.Serialization;

namespace GameCube.GFZ.FMI
{
    public class FmiFile : BinaryFileWrapper<Fmi>
    {
        [JsonIgnore]
        public const Endianness endianness = Endianness.BigEndian;

        [JsonIgnore]
        public override Endianness Endianness => endianness;

        [JsonIgnore]
        public override string FileExtension => ".fmi";

        [JsonIgnore]
        public override string FileName { get; set; } = string.Empty;

        [JsonIgnore]
        public override string Version { get; } = "1";
    }
}
