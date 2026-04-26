using GameCube.GCI;
using GameCube.GFZ.GCI;
using Manifold.IO;

namespace GameCube.GFZ.Emblem;

/// <summary>
///     
/// </summary>
public class EmblemGCIv2 :
    IBinarySerializable
{
    // before you do squat you should get various save data

    // CONSTANTS
    public const byte PaddingByte = 0xAA;

    // FIELDS
    private GciFstEntry gciFstEntry;
    private GfzGciMetadata gfzGciMetadata = new();
    private Emblem emblem = new();
    private byte[] padding = [];

    // PROPERTIES
    public GciFstEntry GciFstEntry { get => gciFstEntry; set => gciFstEntry = value; }
    public GfzGciMetadata GfzGciMetadata { get => gfzGciMetadata; set => gfzGciMetadata = value; }
    public Emblem Emblem { get => emblem; set => emblem = value; }
    public byte[] Padding { get => padding; set => padding = value; }


    public void Deserialize(EndianBinaryReader reader)
    {
        // Record which binary data is where
        AddressRange fstEntryRange = new();
        AddressRange metadataRange = new();
        AddressRange emblemRange = new();
        AddressRange paddingRange = new();
        AddressRange blocksRange = new();

        // HEADER
        fstEntryRange.RecordStartAddress(reader);
        reader.Read(ref gciFstEntry);
        fstEntryRange.RecordEndAddress(reader);
        // BLOCKS START
        blocksRange.RecordStartAddress(reader);
        // METADATA
        metadataRange.RecordStartAddress(reader);
        reader.Read(ref gfzGciMetadata);
        metadataRange.RecordEndAddress(reader);
        // EMBLEM
        emblemRange.RecordStartAddress(reader);
        reader.Read(ref emblem);
        emblemRange.RecordEndAddress(reader);
        // PADDING
        int paddingLength = GciFstEntry.ComputeGciPaddingLength(reader.GetPositionAsPointer());
        paddingRange.RecordStartAddress(reader);
        reader.Read(ref padding, paddingLength);
        paddingRange.RecordEndAddress(reader);
        // BLOCKS END
        blocksRange.RecordEndAddress(reader);

        // Validation
        Assert.IsTrue(fstEntryRange.Size == GciFstEntry.Size);
        //Assert.IsTrue(metadataRange.Size == ???);
        Assert.IsTrue(emblemRange.Size == Emblem.Size);
        Assert.IsTrue(paddingRange.Size == paddingLength);
        Assert.IsTrue(blocksRange.Size == GciFstEntry.BlockSize * gciFstEntry.BlockCount);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        // Record which binary data is where
        AddressRange fstEntryRange = new();
        AddressRange metadataRange = new();
        AddressRange emblemRange = new();
        AddressRange paddingRange = new();
        AddressRange blocksRange = new();

        // HEADER
        fstEntryRange.RecordStartAddress(writer);
        writer.Write(gciFstEntry);
        fstEntryRange.RecordEndAddress(writer);
        // BLOCKS START
        blocksRange.RecordStartAddress(writer);
        // METADATA
        metadataRange.RecordStartAddress(writer);
        writer.Write(gfzGciMetadata);
        metadataRange.RecordEndAddress(writer);
        // EMBLEM
        emblemRange.RecordStartAddress(writer);
        writer.Write(emblem);
        emblemRange.RecordEndAddress(writer);
        // PADDING
        int paddingLength = GciFstEntry.ComputeGciPaddingLength(writer.GetPositionAsPointer());
        paddingRange.RecordStartAddress(writer);
        writer.WritePadding(PaddingByte, paddingLength);
        paddingRange.RecordEndAddress(writer);
        // BLOCKS END
        blocksRange.RecordEndAddress(writer);

        // CRC
        ushort crc = GfzGciMetadata.ComputeCRC(writer.BaseStream, blocksRange);
        writer.JumpToAddress(metadataRange.startAddress);
        writer.Write(crc);
        writer.JumpToAddress(blocksRange.endAddress);
    }

}
