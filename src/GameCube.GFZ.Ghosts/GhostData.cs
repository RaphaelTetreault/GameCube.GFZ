using GameCube.GFZ.CarData;
using GameCube.GFZ.Stage;
using Manifold.IO;

namespace GameCube.GFZ.Ghosts
{
    public class GhostData :
        IBinarySerializable,
        IBinaryFileType
    {
        public const Endianness endianness = Endianness.BigEndian;
        public const int BlockSize = 4000;

        // FIELDS
        // TODO: make private, add accessors
        public MachineID machineID; // 0x00
        public byte courseID; // 0x01
        public ushort one0x02; // 0x02, bool?
        public uint zero0x04; // 0x04, 4 bytes (TODO: use byte[])
        public ShiftJisCString playerName; // 0x08, len:16 bytes, ei: 8x16bit shift jis characters
        public byte totalBlocks; // usually 3 (16KB), can be 2 (12KB). Math: (value+1)*4KB
        public byte unk0x19; //  maybe a checksum?
        public uint zero0x1A; // zero. Ptr to emblem dat?
        public byte[] unkData0x1E; // 6 bytes
        public Time time;
        public GhostFrame[] frames;

        public string FileExtension => ".bin";
        public string FileName { get; set; } = string.Empty;
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

            int nCount = BlockSize * (totalBlocks + 1) / GhostFrame.Size;
            reader.Read(ref frames, nCount);

            Assert.IsTrue(reader.BaseStream.IsAtEndOfStream(), $"{FileName} {reader.GetPositionAsPointer():x8}/{reader.BaseStream.Length:x8} ");
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            int intervalsSizeInBytes = GhostFrame.Size * frames.Length;
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
            writer.Write(frames);
        }
    }
}
