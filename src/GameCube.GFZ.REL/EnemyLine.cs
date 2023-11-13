using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.REL
{
    public class EnemyLine :
        IBinaryFileType
    {
        public const Endianness endianness = Endianness.BigEndian;
        private string fileName = string.Empty;

        public Endianness Endianness => endianness;
        public string FileExtension => "bin";
        public string FileName { get => fileName; set => fileName = value; }

        public static EndianBinaryWriter Open(string filePath)
        {
            var fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            var writer = new EndianBinaryWriter(fs, endianness);
            return writer;
        }

    }
}
