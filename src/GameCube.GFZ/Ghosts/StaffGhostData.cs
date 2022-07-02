using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.StaffGhost
{
    [System.Serializable]
    public struct StaffGhostData :
        IBinarySerializable,
        IFileType
    {
        // METADATA
        private string timeDisplay;


        // FIELDS
        public byte machineID; // 0x00
        public byte courseID; // 0x01
        public byte[] unk_1; // size: 6
        public ShiftJisCString username; // 0x08
        public byte timeMinutes; // 0x24
        public byte timeSeconds; // 0x25
        public short timeMilliseconds; // 0x26

        public string FileExtension => ".bin";
        public string FileName { get; set; }
        public string TimeDisplay { get => timeDisplay; set => timeDisplay = value; }

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref machineID);
            reader.Read(ref courseID);
            reader.Read(ref unk_1, 6);
            reader.Read(ref username);

            reader.BaseStream.Seek(0x24, SeekOrigin.Begin);
            reader.Read(ref timeMinutes);
            reader.Read(ref timeSeconds);
            reader.Read(ref timeMilliseconds);

            timeDisplay = $"{timeMinutes:0}\'{timeSeconds:00}\"{timeMilliseconds:000}";
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
