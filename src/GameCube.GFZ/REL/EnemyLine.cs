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
    public class CustomizableArea
    {
        public int Address { get; }
        public int Size { get; }
        public int Occupied { get; set; }
        public CustomizableArea(int address, int size)
        {
            Address = address;
            Size = size;
            Occupied = 0;
        }
    }

    public class Information
    {
        public int Address { get; }
        public int Size { get; }
        
        public Information(int address, int size)
        {
            Address = address;
            Size = size;
        }
    }
}
