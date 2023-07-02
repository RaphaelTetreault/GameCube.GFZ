using GameCube.GFZ.CarData;
using GameCube.GFZ.Stage;
using Manifold.IO;
using System;
using System.Reflection.PortableExecutable;

namespace GameCube.GFZ.Ghosts
{
    public struct Timestamp :
        IBinarySerializable
    {
        public byte minutes;
        public byte seconds;
        public ushort milliseconds;

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref minutes);
            reader.Read(ref seconds);
            reader.Read(ref milliseconds);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(minutes);
            writer.Write(seconds);
            writer.Write(milliseconds);
        }

        public override string ToString()
        {
            return $"{minutes:0}\'{seconds:00}\"{milliseconds:000}";
        }
    }

    public struct Interval :
        IBinarySerializable
    {
        public const int Size = 0x10; // 16

        //public short unk0;
        //public short unk1;
        //public short unk2;
        //public short unk3;
        //public short unk4;
        //public short unk5;
        //public short unk6;
        //public short unk7;
        public byte[] data;

        public void Deserialize(EndianBinaryReader reader)
        {
            //reader.Read(ref unk0);
            //reader.Read(ref unk1);
            //reader.Read(ref unk2);
            //reader.Read(ref unk3);
            //reader.Read(ref unk4);
            //reader.Read(ref unk5);
            //reader.Read(ref unk6);
            //reader.Read(ref unk7);
            reader.Read(ref data, Size);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            //writer.Write(unk0);
            //writer.Write(unk1);
            //writer.Write(unk2);
            //writer.Write(unk3);
            //writer.Write(unk4);
            //writer.Write(unk5);
            //writer.Write(unk6);
            //writer.Write(unk7);
            writer.Write(data);
        }
    }


    public class GhostData :
        IBinarySerializable,
        IBinaryFileType
    {
        public const Endianness endianness = Endianness.BigEndian;
        public const int BlockSize = 4000;

        // FIELDS
        public MachineID machineID; // 0x00
        public byte courseID; // 0x01
        public ushort one0x02; // 0x02, bool?
        public uint zero0x04; // 0x04, 4 bytes (TODO: use byte[])
        public ShiftJisCString playerName; // 0x08, len:16 bytes, ei: 8 16-bit shift jis characters
        public byte totalBlocks; // usually 3 (16KB), can be 2 (12KB). Math: (value+1)*4KB
        public byte unk0x19; //  maybe a checksum?
        public uint zero0x1A; // zero. Ptr to emblem dat?
        public byte[] unkData0x1E; // 6 bytes
        public Timestamp time;
        public Interval[] intervals;

        public string FileExtension => ".bin";
        public string FileName { get; set; }
        public string TimeDisplay => time.ToString();
        public CourseIndexAX CourseIndexAX => (CourseIndexAX)courseID;
        public CourseIndexGX CourseIndexGX => (CourseIndexGX)courseID;
        public Endianness Endianness => endianness;

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref machineID);
            reader.Read(ref courseID);
            reader.Read(ref one0x02);
            reader.Read(ref zero0x04);
            reader.Read(ref playerName);
            reader.JumpToAddress(0x18);
            reader.Read(ref totalBlocks);
            reader.Read(ref unk0x19);
            reader.Read(ref zero0x1A);
            reader.Read(ref unkData0x1E, 6);
            reader.Read(ref time);

            Assert.IsTrue(one0x02 == 1);
            Assert.IsTrue(zero0x04 == 0);
            Assert.IsTrue(zero0x1A == 0);

            int nCount = BlockSize * (totalBlocks + 1) / Interval.Size;
            reader.Read(ref intervals, nCount);

            Assert.IsTrue(reader.BaseStream.IsAtEndOfStream());
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            int intervalsSizeInBytes = Interval.Size * intervals.Length;
            Assert.IsTrue(intervalsSizeInBytes % BlockSize == 0);
            totalBlocks = (byte)(intervalsSizeInBytes / BlockSize);
            totalBlocks--;

            Assert.IsTrue(unkData0x1E.Length == 6);

            writer.Write(machineID);
            writer.Write(courseID);
            writer.Write(one0x02);
            writer.Write(zero0x04);
            writer.Write<ShiftJisCString>(playerName);
            writer.AlignTo(0x18, 0x00);
            writer.Write(totalBlocks);
            writer.Write(unk0x19);
            writer.Write(zero0x1A);
            writer.Write(unkData0x1E);
            writer.Write(time);
            writer.Write(intervals);
        }

        public void Mutate(int offset, byte value)
        {
            foreach (var interval in intervals)
            {
                interval.data[offset] = value;
            }
        }
    }
}
