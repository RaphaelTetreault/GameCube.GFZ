using Manifold;
using Manifold.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GameCube.GFZ.REL
{
    public class EnemyLine :
        IBinaryFileType
    {
        public const Endianness endianness = Endianness.BigEndian;
        private string fileName;

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
