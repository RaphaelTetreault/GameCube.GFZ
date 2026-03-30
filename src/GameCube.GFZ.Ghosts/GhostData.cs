using GameCube.GFZ.CarData;
using GameCube.GFZ.GameData;
using Manifold.IO;

namespace GameCube.GFZ.Ghosts;

/// <summary>
///     Vehicle ghost for stage.
/// </summary>
public class GhostData :
    IBinarySerializable
{
    public const int ChunkSize = 4000;

    // FIELDS
    // TODO: make private, add accessors
    public MachineID machineID; // 0x00
    public byte courseID; // 0x01
    public byte zero0x02; // Addr:0x02 always zero
    public bool unkBoolean; // 0x03, bool - usually True
    public uint unk_prob_custom_machine_indices; // 0x04, 4 bytes (TODO: use byte[])
    public ShiftJisCString playerName = string.Empty; // 0x08, len:16 bytes, ei: 8x16bit shift jis characters
    public byte totalChunks; // usually 3 (16KB), can be 2 (12KB). Math: (value+1)*4KB
    public byte unk0x19; // maybe a checksum?
    public uint zero0x1A; // Addr:0x1A always zero
    public byte[] unkData0x1E = []; // 6 bytes
    public Time time;
    public GhostFrame[] frames = [];

    public string TimeDisplay => time.ToString();

    public void Deserialize(EndianBinaryReader reader)
    {
        // been having a long day...
        const int rawAddress = 0x0000;
        const int gciAddress = 0x20a0;
        int ptr = reader.GetPositionAsPointer();
        Assert.IsTrue(ptr == rawAddress || ptr == gciAddress, $"Bad start address {ptr:x8}");

        reader.Read(ref machineID);
        reader.Read(ref courseID);
        reader.Read(ref zero0x02);
        reader.Read(ref unkBoolean);
        reader.Read(ref unk_prob_custom_machine_indices);
        Pointer nameAddress = reader.GetPositionAsPointer();
        reader.Read(ref playerName);
        reader.JumpToAddress(nameAddress + 0x10);
        reader.Read(ref totalChunks);
        reader.Read(ref unk0x19);
        reader.Read(ref zero0x1A);
        reader.Read(ref unkData0x1E, 6);
        reader.Read(ref time);

        Assert.IsTrue(zero0x02 == 0, $"0x02:{zero0x02:x2}");
        Assert.IsTrue(zero0x1A == 0, $"0x1A:{zero0x1A:x8}");
        Assert.IsTrue(totalChunks < 4); // never seen more than 3

        int frameCount = ChunkSize * (totalChunks + 1) / GhostFrame.Size;
        reader.Read(ref frames, frameCount);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        int intervalsSizeInBytes = GhostFrame.Size * frames.Length;
        Assert.IsTrue(intervalsSizeInBytes % ChunkSize == 0);
        totalChunks = (byte)(intervalsSizeInBytes / ChunkSize);
        totalChunks--;

        Assert.IsTrue(unkData0x1E.Length == 6);

        writer.Write(machineID);
        writer.Write(courseID);
        writer.Write(zero0x02);
        writer.Write(unkBoolean);
        writer.Write(unk_prob_custom_machine_indices);
        writer.Write(playerName);
        writer.AlignTo(0x18, 0x00); // hm
        writer.Write(totalChunks);
        writer.Write(unk0x19);
        writer.Write(zero0x1A);
        writer.Write(unkData0x1E);
        writer.Write(time);
        writer.Write(frames);
    }
}
