using GameCube.GCI;
using GameCube.GFZ.GCI;
using GameCube.GX.Texture;
using Manifold.IO;
using System;

namespace GameCube.GFZ.Emblem;

/// <summary>
///     
/// </summary>
public class EmblemGCI :
    IBinarySerializable,
    IBinaryFileType
{
    // before you do squat you should get various save data

    // CONSTANTS
    public const Endianness endianness = Endianness.BigEndian;
    public const string extension = "gci";
    public const byte PaddingByte = 0x00;
    public const uint ImageDataOffset = 0x00000060;
    public const uint CommentOffset = 0x00000004;

    // FIELDS
    private GciFstEntry fstEntry;
    private GfzGciMetadata metadata = new() { ID = 0x0401 };
    private Emblem emblem = new();
    private byte[] padding = [];

    // PROPERTIES
    public GciFstEntry GciFstEntry { get => fstEntry; set => fstEntry = value; }
    public GfzGciMetadata GfzGciMetadata { get => metadata; set => metadata = value; }
    public Emblem Emblem { get => emblem; set => emblem = value; }
    public byte[] Padding { get => padding; set => padding = value; }

    public bool AutoComment1 { get; set; } = false;
    public bool AutoComment2 { get; set; } = false;
    public bool AutoInternalFileName { get; set; } = false;
    public bool AutoModificationTime { get; set; } = false;

    // IBinaryFileType
    public Endianness Endianness => Endianness.BigEndian;
    public string FileExtension => extension;
    public string FileName { get; set; } = string.Empty;


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
        reader.Read(ref fstEntry);
        fstEntryRange.RecordEndAddress(reader);
        // BLOCKS START
        blocksRange.RecordStartAddress(reader);
        // METADATA
        metadataRange.RecordStartAddress(reader);
        reader.Read(ref metadata);
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
        // Validate block size
        Assert.IsTrue(blocksRange.Size == GciFstEntry.BlockSize * fstEntry.BlockCount);
        Assert.IsTrue(blocksRange.Size % GciFstEntry.BlockSize == 0, "Not exact block size.");

        // Assert CRC
        AddressRange crcRange = blocksRange with { startAddress = blocksRange.startAddress + 2 };
        ushort crc = GfzGciMetadata.ComputeCRC(reader.BaseStream, crcRange);
        Assert.IsTrue(crc == metadata.CRC, $"Loaded CRC was {metadata.CRC} but expected {crc}.");

        ValidateAssumptions();
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        DoAutomations(DateTime.Now);

        // Record which binary data is where
        AddressRange fstEntryRange = new();
        AddressRange metadataRange = new();
        AddressRange emblemRange = new();
        AddressRange paddingRange = new();
        AddressRange blocksRange = new();

        // HEADER
        fstEntryRange.RecordStartAddress(writer);
        writer.Write(fstEntry);
        fstEntryRange.RecordEndAddress(writer);
        // BLOCKS START
        blocksRange.RecordStartAddress(writer);
        // METADATA
        metadataRange.RecordStartAddress(writer);
        writer.Write(metadata);
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

        // FST UPDATE: block count and comment offset
        Assert.IsTrue(blocksRange.Size % GciFstEntry.BlockSize == 0, "Not exact block size.");
        fstEntry.BlockCount = (ushort)(blocksRange.Size / GciFstEntry.BlockSize);
        fstEntry.ImageDataOffset = ImageDataOffset;
        fstEntry.CommentOffset = CommentOffset;
        writer.JumpToAddress(fstEntryRange.startAddress, true);
        writer.Write(fstEntry);
        // CRC
        AddressRange crcRange = blocksRange with { startAddress = blocksRange.startAddress + 2 };
        metadata.CRC = GfzGciMetadata.ComputeCRC(writer.BaseStream, crcRange);
        writer.JumpToAddress(metadataRange.startAddress);
        writer.Write(metadata.CRC);
        // Reset address
        writer.JumpToAddress(blocksRange.endAddress);

        ValidateAssumptions();
    }

    public void ValidateAssumptions()
    {
        // FST assumptions
        Assert.IsTrue(fstEntry.BannerIconFlags == GfzGciFstEntryDB.Emblem.BannerIconFlags);
        Assert.IsTrue(fstEntry.ModificationTime != GfzGciFstEntryDB.Emblem.ModificationTime);
        Assert.IsTrue(fstEntry.ImageDataOffset != GfzGciFstEntryDB.Emblem.ImageDataOffset);
        Assert.IsTrue(fstEntry.GciImageFormat == GfzGciFstEntryDB.Emblem.GciImageFormat);
        Assert.IsTrue(fstEntry.GciAnimationSpeed == GfzGciFstEntryDB.Emblem.GciAnimationSpeed);
        Assert.IsTrue(fstEntry.GciPermissionFlags == GfzGciFstEntryDB.Emblem.GciPermissionFlags);
        Assert.IsTrue(fstEntry.BlockCount != GfzGciFstEntryDB.Emblem.BlockCount);
        Assert.IsTrue(fstEntry.CommentOffset != GfzGciFstEntryDB.Emblem.CommentOffset);

        // Metadata assumptions
        Assert.IsTrue(metadata.Banner.Format == GciTextureFormat.DirectColor);
        Assert.IsTrue(metadata.Icons.Format == GciTextureFormat.DirectColor);
        Assert.IsTrue(metadata.Icons.CountIcons() == GfzGciMetadata.IconsCount);
    }

    private void DoAutomations(DateTime time)
    {
        if (AutoInternalFileName)
            fstEntry.InternalFileName = CreateEmblemFileName(time);

        if (AutoModificationTime)
            fstEntry.ModificationTime = GciFstEntry.GetModificationTime(time);

        if (AutoComment1)
            metadata.Comment1 = "F-ZERO GX";

        if (AutoComment2)
            metadata.Comment2 = GciFstEntry.GetDefaultComment(time);
    }

    public static string CreateEmblemFileName(DateTime dateTime)
    {
        string dateTimeHex = dateTime.ToBinary().ToString("x16");
        string value = $"fze02000020{dateTimeHex}.dat"; // 31 chars, leaves last char for null terminator
        return value;
    }

}
