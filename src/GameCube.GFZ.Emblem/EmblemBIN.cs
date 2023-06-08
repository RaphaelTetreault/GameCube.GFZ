using Manifold.IO;
using System;

namespace GameCube.GFZ.Emblem
{
    public class EmblemBIN :
        IBinaryFileType,
        IBinarySerializable
    {
        public const Endianness endianness = Endianness.BigEndian;
        public const string Extension = ".bin";

        public Endianness Endianness => endianness;
        public string FileExtension => Extension;
        public string FileName { get; set; } = string.Empty;


        private Emblem[] emblems = new Emblem[0];
        public Emblem[] Emblems { get => emblems; set => emblems = value; }


        public void Deserialize(EndianBinaryReader reader)
        {
            bool isValidFileSize = (int)(reader.BaseStream.Length % Emblem.Size) == 0;
            if (!isValidFileSize)
            {
                string msg = $"File is not an exact multiple of 0x{Emblem.Size:x4}.";
                throw new ArgumentException(msg);
            }

            int count = (int)(reader.BaseStream.Length / Emblem.Size);
            reader.Read(ref emblems, count);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(emblems);
        }
    }
}
