using GameCube.GCI;
using GameCube.GFZ.GCI;
using GameCube.GX.Texture;
using Manifold.IO;
using System;
using System.IO;

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
    private GfzGciMetadata metadata = new();
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
        // Write data
        reader.Read(ref fstEntry);
        reader.Read(ref metadata);
        reader.Read(ref emblem);

        // Write padding
        AddressRange paddingRange = new();
        int paddingLength = GciFstEntry.ComputeGciPaddingLength(reader.GetPositionAsPointer());
        paddingRange.RecordStartAddress(reader);
        reader.Read(ref padding, paddingLength);
        paddingRange.RecordEndAddress(reader);

        // Validate sizes
        Assert.IsTrue(fstEntry.AddressRange.Size == GciFstEntry.Size);
        Assert.IsTrue(metadata.AddressRange.Size == GfzGciMetadata.Size);
        Assert.IsTrue(emblem.AddressRange.Size == Emblem.Size);
        Assert.IsTrue(paddingRange.Size == paddingLength);
        // Validate GC block size
        AddressRange blocksRange = new()
        {
            startAddress = metadata.AddressRange.startAddress,
            endAddress = paddingRange.endAddress,
        };
        Assert.IsTrue(blocksRange.Size == GciFstEntry.BlockSize * fstEntry.BlockCount);
        Assert.IsTrue(blocksRange.Size % GciFstEntry.BlockSize == 0, "Not exact block size.");
        // Assert CRC
        AddressRange crcRange = blocksRange with { startAddress = blocksRange.startAddress + 0x02 };
        ushort crc = GfzGciMetadata.ComputeCRC(reader.BaseStream, crcRange);
        Assert.IsTrue(crc == metadata.CRC, $"Loaded CRC was {metadata.CRC} but expected {crc}.");
        // Validate all the things I assume about EmblemGCI
        ValidateAssumptions();
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        DoAutomations(DateTime.Now);

        // Write data
        writer.Write(fstEntry);
        writer.Write(metadata);
        writer.Write(emblem);

        // Write padding
        AddressRange paddingRange = new();
        int paddingLength = GciFstEntry.ComputeGciPaddingLength(writer.GetPositionAsPointer());
        paddingRange.RecordStartAddress(writer);
        writer.WritePadding(PaddingByte, paddingLength);
        paddingRange.RecordEndAddress(writer);

        // FST UPDATE: block count and comment offset
        AddressRange blocksRange = new()
        {
            startAddress = metadata.AddressRange.startAddress,
            endAddress = paddingRange.endAddress,
        };
        Assert.IsTrue(blocksRange.Size % GciFstEntry.BlockSize == 0, "Not exact block size.");
        fstEntry.BlockCount = (ushort)(blocksRange.Size / GciFstEntry.BlockSize);
        fstEntry.ImageDataOffset = ImageDataOffset;
        fstEntry.CommentOffset = CommentOffset;
        writer.JumpToAddress(fstEntry.AddressRange.startAddress, true);
        writer.Write(fstEntry);
        // CRC
        AddressRange crcRange = blocksRange with { startAddress = blocksRange.startAddress + 0x02 };
        metadata.CRC = GfzGciMetadata.ComputeCRC(writer.BaseStream, crcRange);
        writer.JumpToAddress(metadata.AddressRange.startAddress);
        writer.Write(metadata.CRC);
        // Reset address
        writer.JumpToAddress(blocksRange.endAddress);
        // Validate all the things I assume about EmblemGCI
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

        //TODO 2026/04/26:
        //  Key insight, internal file name is what hangs up game...
        //  Must be .dat extension in file. Causes file loading hang otherwise.
        //  Must have fze020 for whatever reason. Causes pointer issues.
        //  To that point. file is fze_02000_02000 (no _ in actual). 02000 repeats twice.

        //string fileName = Path.GetFileNameWithoutExtension(fstEntry.InternalFileName);
        string extension = Path.GetExtension(fstEntry.InternalFileName);
        if (extension != ".dat")
        {
            string msg = $"{nameof(EmblemGCI)} internal file name must end in \".dat!\"";
            throw new ArgumentException(msg);
        }
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
