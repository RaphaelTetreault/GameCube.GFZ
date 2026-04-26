using GameCube.DiskImage;
using GameCube.GCI;
using GameCube.GX.Texture;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.GCI;

/// <summary>
///     
/// </summary>
/// <typeparam name="TBinarySerializable"></typeparam>
public abstract class GfzGci<TBinarySerializable> : GciWithUniqueID<TBinarySerializable>
    where TBinarySerializable : IBinarySerializable, IBinaryFileType, new()
{
    // CONSTS
    public const int GameTitleLength = 32;
    public const int CommentLength = 60;
    public const int IconsCount = 1;
    public const string InnerExtension = ".dat";
    public const string OuterExtension = ".gci";

    // FIELDS
    private string gameTitle = string.Empty;
    private string comment = string.Empty;
    private ushort checksum;
    private ushort uniqueID;

    // PROPERTIES
    public override ushort UniqueID => uniqueID;
    public override string Comment { get => comment; set => comment = value; }
    public override ushort Checksum => checksum;
    public string GameTitle { get => gameTitle; set => gameTitle = value; }

    public GfzGci() : this((Region)0)
    {
    }

    public GfzGci(Region region)
    {
        GameID gameID = new();
        gameID[0] = 'G';
        gameID[1] = 'F';
        gameID[2] = 'Z';
        gameID[3] = 'E'; // bad bad bad
        gameID[4] = '8';
        gameID[5] = 'P';
        Header.GameID = gameID;
    }


    public override void DeserializeCommentAndImages(EndianBinaryReader reader)
    {
        var textEncoding = Header.GetTextEncoding();

        reader.Read(ref checksum);
        reader.Read(ref uniqueID);
        Assert.IsTrue(reader.GetPositionAsPointer() == Header.CommentPtr);
        reader.Read(ref gameTitle, textEncoding, GameTitleLength);
        reader.Read(ref comment, textEncoding, CommentLength);
        Assert.IsTrue(reader.GetPositionAsPointer() == Header.ImageDataPtr);
        Banner = ReadDirectColorBanner(reader);
        Icons =
        [
            ReadDirectColorIcon(reader),
        ];
        Assert.IsTrue(Header.ImageFormat == ImageFormat.DirectColor);
        Assert.IsTrue(Icons.Length == IconsCount);
        Assert.IsTrue(Header.GetAnimationFrameCount() == Icons.Length);
    }

    public override void SerializeCommentAndImages(EndianBinaryWriter writer)
    {
        var textEncoding = Header.GetTextEncoding();
        GameTitle = "F-ZERO GX";
        Comment = Header.GetDefaultComment();

        writer.Write((ushort)0xDEAD);
        writer.Write(UniqueID);
        writer.Write(gameTitle, textEncoding, false);
        writer.WritePadding(0x00, GameTitleLength - gameTitle.Length);
        writer.Write(comment, textEncoding, false);
        writer.WritePadding(0x00, CommentLength - comment.Length);
        foreach (var icon in Icons)
            Assert.IsTrue(TextureEncoding.IsDirectEncoding(icon.Format));
        Header.ImageFormat = ImageFormat.DirectColor;
        //Assert.IsTrue(Header.ImageFormat == ImageFormat.DirectColor);
        Assert.IsTrue(Icons.Length == IconsCount);
        WriteDirectColorBanner(writer);
        WriteDirectColorIcons(writer);
    }

    public static string FormatGciFileName(GfzGciFileType fileType, Region region, string fileNameWithoutExtension, out string fileName)
    {
        char regionChar = region switch
        {
            Region.Japan => 'J',
            Region.NorthAmerica => 'E',
            Region.Europe => 'P',
            _ => throw new ArgumentException(),
        };
        string code = GfzGciDesignator(fileType);
        string prefix = $"8P-GFZ{regionChar}-{code}-";
        fileName = string.Empty;

        switch (fileType)
        {
            // These types have file names after the default name
            case GfzGciFileType.Emblem: // 24
            case GfzGciFileType.Ghost: // 16
            case GfzGciFileType.Replay: // 22
                // TODO: old tests showed game needed 3 numbers post baseName. Still needed?
                //       ie: "8P-GFZE-fze02000020003FBF71CA629FFA.dat.gci"
                //       I used to do: *fze020_[filename].dat.gci
                //       ALSO, you need to confirm if the filename can be shorter. If so, fill it in.
                int maxChars = GetHashLength(fileType);
                string hashName = fileNameWithoutExtension.Length > maxChars
                    ? fileNameWithoutExtension[..maxChars]
                    : fileNameWithoutExtension;
                fileName += hashName;
                break;

            // These types have fixed file names
            case GfzGciFileType.Garage:
                // File name is only designator
                break;
            case GfzGciFileType.Save:
                fileName += "f_zero";
                break;

            // Anything else requires error out
            default:
                string msg = $"Unhandled file type {fileType} (0x{fileType:x8}).";
                throw new ArgumentException(msg);
        }

        fileName += InnerExtension;
        string fullFileName = prefix + fileName + OuterExtension;
        return fullFileName;
    }
    private static string GfzGciDesignator(GfzGciFileType fileType)
    {
        return fileType switch
        {
            GfzGciFileType.Emblem => "fze",
            GfzGciFileType.Garage => "fzc",
            GfzGciFileType.Ghost => "fzg",
            GfzGciFileType.Replay => "fzr",
            _ => string.Empty,
        };
    }
    private static int GetHashLength(GfzGciFileType fileType)
    {
        return fileType switch
        {
            GfzGciFileType.Emblem => 24,
            GfzGciFileType.Ghost => 16,
            GfzGciFileType.Replay => 22,
            _ => -1,
        };
    }

    /// <summary>
    ///     Compute file CRC
    /// </summary>
    /// <returns>
    ///     
    /// </returns>
    public ushort ComputeCRC(Stream stream)
    {
        BinaryReader reader = new BinaryReader(stream);
        int checksum = 0xFFFF;
        int generatorPolynomial = 0x8408;

        //for all data after checksum
        reader.BaseStream.Position = 0x42;
        for (int i = 0x42; i < stream.Length; i++)
        {
            byte value = reader.ReadByte();
            checksum = checksum ^ value;

            //For each bit in byte
            for (int j = 8; j > 0; j--)
            {
                if ((checksum & 1) == 1)
                {
                    checksum = (checksum >>> 1) ^ generatorPolynomial;

                }
                else
                {
                    checksum = (checksum >>> 1);
                }
            }
        }

        //Final operation: flip all bits
        checksum ^= 0xFFFF;
        return (ushort)checksum;
    }
}
