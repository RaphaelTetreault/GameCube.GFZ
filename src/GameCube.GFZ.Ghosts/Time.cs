using Manifold.IO;

namespace GameCube.GFZ.Ghosts
{
    public struct Time :
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

}
