using Manifold.IO;

namespace GameCube.GFZ.Ghosts
{
    public struct GhostFrame :
        IBinarySerializable
    {
        public const int Size = 0x10; // 16

        public GhostFlags0x00 unk0x00; // Interpolation mode?
        public GhostFlags0x01 unk0x01; // Almost like lap index, but does some weird sh*t
        public CompressedRotation orientation;
        public short positionX;
        public short positionY;
        public short positionZ;
        public short unk0x14;

        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref unk0x00);
            reader.Read(ref unk0x01);
            reader.Read(ref orientation);
            reader.Read(ref positionX);
            reader.Read(ref positionY);
            reader.Read(ref positionZ);
            reader.Read(ref unk0x14);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(unk0x00);
            writer.Write(unk0x01);
            writer.Write(orientation);
            writer.Write(positionX);
            writer.Write(positionY);
            writer.Write(positionZ);
            writer.Write(unk0x14);
        }
    }

}
