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

    public class MinimapParameters
    {        
        public float FOV { get; }
        public float CamPosX { get; }
        public float CamPosY { get; }
        public float CamPosZ { get; }
        public float LookAtX { get; }            
        public float LookAtY { get; }
        public float LookAtZ { get; }

        public MinimapParameters(float fov, float camPosX, float camPosY, float camPosZ, float lookAtX, float lookAtY, float lookAtZ)
        {
            FOV = fov;
            CamPosX = camPosX;
            CamPosY = camPosY;
            CamPosZ = camPosZ;
            LookAtX = lookAtX;
            LookAtY = lookAtY;
            LookAtZ = lookAtZ;
        }

        public byte[] ToByteArray(bool BE)
        {
            byte[] paramBinary = new byte[4*7];
            byte[] buf = new byte[4];

            buf = BitConverter.GetBytes(FOV);
            Buffer.BlockCopy(buf, 0, paramBinary, 0, 4);
            buf = BitConverter.GetBytes(CamPosX);
            Buffer.BlockCopy(buf, 0, paramBinary, 4, 4);
            buf = BitConverter.GetBytes(CamPosY);
            Buffer.BlockCopy(buf, 0, paramBinary, 8, 4);
            buf = BitConverter.GetBytes(CamPosZ);
            Buffer.BlockCopy(buf, 0, paramBinary, 0xc, 4);
            buf = BitConverter.GetBytes(LookAtX);
            Buffer.BlockCopy(buf, 0, paramBinary, 0x10, 4);
            buf = BitConverter.GetBytes(LookAtY);
            Buffer.BlockCopy(buf, 0, paramBinary, 0x14, 4);
            buf = BitConverter.GetBytes(LookAtZ);
            Buffer.BlockCopy(buf, 0, paramBinary, 0x18, 4);

            if(BE)
            {
                for(int i = 0; i < 7; ++i)
                {
                    Buffer.BlockCopy(paramBinary, i*4, buf, 0, 4);
                    Array.Reverse(buf);
                    Buffer.BlockCopy(buf, 0, paramBinary, i*4, 4);
                }
            }

            return paramBinary;
        }

    };
}
