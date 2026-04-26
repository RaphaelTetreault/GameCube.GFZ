using GameCube.GCI;
using GameCube.GX.Texture;
using Manifold.IO;
using System;
using System.IO;
using System.Text;
using GameCube.Common;

namespace GameCube.GFZ.GCI;

public class GfzGciMetadata :
    IBinarySerializable
{
    // CONSTANTS
    public const int Comment1MaxLength = 32;
    public const int Comment2MaxLength = 60;
    public const int IconsCount = 1;
    public const ushort TempCRC = 0xDEAD;
    public const string TempComment = "TEMP";
    public const byte TextPadByte = 0x00;

    // FIELDS
    private ushort crc;                    // 0x00
    private ushort id;                     // 0x02
    private string comment1 = TempComment; // 0x04, size 0x20, 32 bytes - game title
    private string comment2 = TempComment; // 0x24, size 0x3C, 60 bytes - date and sometimes other metadata
    private Banner banner = new();         // 0x60 GCI banner
    private Icons icons = new();           // ---- GCI icon(s)

    // PROPERTIES
    public Encoding Encoding { get; set; } = TextEncoding.ShiftJIS;

    // PROPERTIES (FORWARD)
    public ushort CRC { get => crc; set => crc = value; }
    public ushort ID { get => id; set => id = value; }
    public string Comment1 { get => comment1; set => comment1 = SanitizeString(value, Comment1MaxLength); }
    public string Comment2 { get => comment2; set => comment2 = SanitizeString(value, Comment2MaxLength); }
    public Banner Banner { get => banner; set => banner = value; }
    public Icons Icons { get => icons; set => icons = value; }

    public void Deserialize(EndianBinaryReader reader)
    {
        // Create new CStrings with the desired encoding
        banner = new Banner() { Format = GciTextureFormat.DirectColor, };
        icons = new Icons() { Format = GciTextureFormat.DirectColor, };

        Pointer baseAddress = reader.GetPositionAsPointer();
        reader.Read(ref crc);
        reader.Read(ref id);
        reader.Read(ref comment1, Encoding);
        reader.JumpToAddress(baseAddress + 0x24);
        reader.Read(ref comment2, Encoding);
        reader.JumpToAddress(baseAddress + 0x60);
        banner.ReadBanner(reader);
        icons.ReadIcons(reader, IconsCount);

        //Assert.IsTrue(Header.ImageFormat == ImageFormat.DirectColor);
        //Assert.IsTrue(Icons.Length == IconsCount);
        //Assert.IsTrue(Header.GetAnimationFrameCount() == Icons.Length);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        int bytesPadComment1 = Comment1MaxLength - comment1.Length;
        int bytesPadComment2 = Comment2MaxLength - comment2.Length;
        Assert.IsTrue(icons.CountIcons() == 1, "Not exactly 1 icon!");
        AddressRange addressRange = new();

        addressRange.RecordStartAddress(writer);
        writer.Write(TempCRC);
        writer.Write(id);
        writer.Write(comment1, Encoding, false);
        writer.WritePadding(TextPadByte, bytesPadComment1);
        writer.Write(comment2, Encoding, false);
        writer.WritePadding(TextPadByte, bytesPadComment2);
        banner.WriteBanner(writer);
        icons.WriteIcons(writer);
        addressRange.RecordEndAddress(writer);
        // assert end pos / size
    }

    /// <summary>
    ///     Compute file CRC
    /// </summary>
    /// <returns>
    ///     
    /// </returns>
    public static ushort ComputeCRC(Stream stream, AddressRange addressRange)
    {
        bool isValidRange =
            0 <= addressRange.startAddress && addressRange.startAddress <= stream.Length &&
            0 <= addressRange.endAddress && addressRange.endAddress <= stream.Length;
        if (!isValidRange)
        {
            string msg = $"Address range {addressRange.startAddress:x8} to " +
                $"{addressRange.endAddress:x8} is not in range {0} to {stream.Position}.";
            throw new Exception(msg);
        }

        const int generatorPolynomial = 0x8408;
        int checksum = 0xFFFF;
        BinaryReader reader = new(stream);

        // Prepare stream for CRC, remember address to reset after
        long initialPosition = stream.Position;
        stream.Position = addressRange.startAddress;
        // For all data after checksum
        for (long i = addressRange.startAddress; i < addressRange.endAddress; i++)
        {
            // Get next byte
            byte value = reader.ReadByte();
            // XOR current CRC with byte
            checksum = checksum ^ value;
            // For each bit in byte
            for (int bitIndex = 8; bitIndex > 0; bitIndex--)
            {
                // If lowest bit is 1, XOR with const
                if ((checksum & 1) == 1)
                    checksum = (checksum >>> 1) ^ generatorPolynomial;
                // Else simple shift right without rightmost bit copy
                else
                    checksum = (checksum >>> 1);
            }
        }
        stream.Position = initialPosition;

        // Final operation: invert all bits
        checksum ^= 0xFFFF;
        // Result is lowest 16 bits of checksum
        return (ushort)checksum;
    }



    public static string SanitizeString(string value, int maxLengthIncludingNullTerminator)
    {
        bool isTooLong = value.Length > maxLengthIncludingNullTerminator;
        if (isTooLong)
        {
            string msg = $"File name provided is too large. Length is {value.Length}, " +
                $"max is {maxLengthIncludingNullTerminator}.";
            throw new ArgumentException(msg);
        }

        return value;
    }

}
