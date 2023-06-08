using GameCube.GX.Texture;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Emblem
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///     Primary source of information:
    ///     https://docs.google.com/document/d/1c4a7d6xZ-rnK-E5p7d__6V1qhLxcdOHwAmv-FR8K50s/edit
    /// </example>
    public class EmblemGCI :
        IBinaryFileType,
        IBinarySerializable
    {
        public const Endianness endianness = Endianness.BigEndian;
        public const string Extension = ".gci";
        public const string GameCodeGFZE01 = "GFZE01";
        public const string GameCodeGFZJ01 = "GFZJ01";
        public const string GameCodeGFZP01 = "GFZP01";
        public const int GameCodeLength = 6;
        public const int InternalFileNameLength = 32;
        public const int GameTitleLength = 32;
        public const int CommentLength = 60;
        public const int FileSize = 24_640; // bytes
        public static readonly System.Text.Encoding TextEncoding = System.Text.Encoding.GetEncoding(932); // shift jis

        public Endianness Endianness => endianness;
        public string FileExtension => Extension;
        public string FileName { get; set; } = string.Empty;

        // Fields
        private string gameCode = "";
        private byte const_0xFF;
        private byte regionCode;
        private string internalFileName = "";
        private uint timestamp;
        private byte[] unkData = new byte[9];
        private byte copyCount;
        private ushort memoryCardStartBlock;
        private int unk0;
        private int unk1;
        private ushort checksum;
        private ushort const_0x0401;
        private string gameTitle = "F-ZERO GX";
        private string comment = "";
        private MenuBanner banner = new MenuBanner();
        private MenuIcon icon = new MenuIcon();
        private Emblem emblem = new Emblem();

        // Accessors
        public string GameCode { get => gameCode; set => gameCode = value; }
        public byte Const_0xFF { get => const_0xFF; set => const_0xFF = value; }
        public byte RegionCode { get => regionCode; set => regionCode = value; }
        public string InternalFileName { get => internalFileName; set => internalFileName = value; }
        public uint Timestamp { get => timestamp; set => timestamp = value; }
        public byte[] UnkData { get => unkData; set => unkData = value; }
        public byte CopyCount { get => copyCount; set => copyCount = value; }
        public ushort MemoryCardStartBlock { get => memoryCardStartBlock; set => memoryCardStartBlock = value; }
        public int Unk0 { get => unk0; set => unk0 = value; }
        public int Unk1 { get => unk1; set => unk1 = value; }
        public ushort Checksum { get => checksum; set => checksum = value; }
        public ushort Const_0x0401 { get => const_0x0401; set => const_0x0401 = value; }
        public string GameTitle { get => gameTitle; set => gameTitle = value; }
        public string Comment { get => comment; set => comment = value; }
        public MenuBanner Banner { get => banner; set => banner = value; }
        public MenuIcon Icon { get => icon; set => icon = value; }
        public Emblem Emblem { get => emblem; set => emblem = value; }

        public void Deserialize(EndianBinaryReader reader)
        {
            bool isValidFileSize = reader.BaseStream.Length == FileSize;
            if (!isValidFileSize)
            {
                string msg = $"File size is not exactly {FileSize:n0} bytes.";
                throw new FileLoadException(msg);
            }

            reader.Read(ref gameCode, TextEncoding, GameCodeLength);
            reader.Read(ref const_0xFF);
            reader.Read(ref regionCode);
            reader.Read(ref internalFileName, TextEncoding, InternalFileNameLength);
            reader.Read(ref timestamp);
            reader.Read(ref unkData, 9);
            reader.Read(ref copyCount);
            reader.Read(ref memoryCardStartBlock);
            reader.Read(ref unk0);
            reader.Read(ref unk1);
            reader.Read(ref checksum);
            reader.Read(ref const_0x0401);
            reader.Read(ref gameTitle, TextEncoding, GameTitleLength);
            reader.Read(ref comment, TextEncoding, CommentLength);

            reader.Read(ref banner);
            reader.Read(ref icon);
            reader.Read(ref emblem);
        }
        public void Serialize(EndianBinaryWriter writer)
        {
            DateTime dateTime = DateTime.Now;
            SetDefaultComment(dateTime, false);
            SetTimestamp(dateTime);
            checksum = ComputeCRC();

            Assert.IsTrue(gameCode.Length == GameCodeLength);
            Assert.IsTrue(internalFileName.Length <= InternalFileNameLength);
            Assert.IsTrue(gameTitle.Length <= GameTitleLength);
            Assert.IsTrue(comment.Length <= CommentLength);
            Assert.IsTrue(unkData.Length <= 9);

            writer.Write(gameCode, TextEncoding, false);
            writer.Write(0xFF);
            writer.Write(regionCode);
            writer.Write(internalFileName, TextEncoding, false);
            writer.WritePadding(0x00, InternalFileNameLength - internalFileName.Length);
            writer.Write(timestamp);
            writer.Write(unkData);
            writer.Write(copyCount);
            writer.Write(memoryCardStartBlock);
            writer.Write(unk0);
            writer.Write(unk1);
            writer.Write(checksum);
            writer.Write(0x0401);
            writer.Write(gameTitle);
            writer.WritePadding(0x00, GameTitleLength - internalFileName.Length);
            writer.Write(comment);
            writer.WritePadding(0x00, CommentLength - internalFileName.Length);

            writer.Write(banner);
            writer.Write(icon);
            writer.Write(emblem);
        }

        public void SetRegionCode(GameCode gameCode)
        {
            switch (gameCode)
            {
                case GFZ.GameCode.GFZE01:
                    regionCode = 1;
                    GameCode = GameCodeGFZE01;
                    return;

                case GFZ.GameCode.GFZJ01:
                    regionCode = 0;
                    GameCode = GameCodeGFZJ01;
                    return;

                case GFZ.GameCode.GFZP01:
                    regionCode = 2;
                    GameCode = GameCodeGFZJ01;
                    return;

                case GFZ.GameCode.GFZJ8P:
                    throw new ArgumentException("AX is not supported");

                default:
                    throw new ArgumentException($"Unhandled region code {gameCode}");
            }
        }
        private void SetDefaultComment(DateTime dateTime, bool overwriteComment = false)
        {
            bool hasComment = !string.IsNullOrEmpty(comment);
            if (hasComment || !overwriteComment)
                return;

            string time = dateTime.ToString("yyyy/MM/dd hh:mm.ss");
            string name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            comment = $"Created by {name} at {time}.";
        }
        private void SetTimestamp(DateTime dateTime)
        {
            DateTimeOffset dateTimeOffset = dateTime;
            uint unixTime = (uint)dateTimeOffset.ToUnixTimeSeconds();
            timestamp = unixTime;
        }
        public ushort ComputeCRC()
        {
            return 0xFCDC;
        }
        public bool SafeSetInternalFileName(string internalFileName)
        {
            // Trim file name if it is too long
            bool fileNameFits = internalFileName.Length <= InternalFileNameLength;
            if (!fileNameFits)
            {
                internalFileName = internalFileName.Substring(0, InternalFileNameLength);
            }

            // Set file name
            InternalFileName = internalFileName;

            // provide info
            return fileNameFits;
        }
        public static string GetFileName(string fileName, GameCode gameCode)
        {
            const int maxChars = 20;
            string fileName20Char = fileName.PadLeft(maxChars, '-');
            fileName20Char = fileName20Char.Substring(0, maxChars);

            char regionChar = GetRegionChar(gameCode);
            char R = char.ToUpper(regionChar);
            char r = char.ToLower(regionChar);

            string name = $"8P-GFZ{R}-fz{r}020-{fileName20Char}";
            return name;
        }
        private static char GetRegionChar(GameCode gameCode)
        {
            switch (gameCode)
            {
                case GFZ.GameCode.GFZE01: return 'e';
                case GFZ.GameCode.GFZJ01: return 'j';
                case GFZ.GameCode.GFZP01: return 'p';

                case GFZ.GameCode.GFZJ8P:
                    throw new ArgumentException("AX is not supported");

                default:
                    throw new ArgumentException($"Unhandled region code {gameCode}");
            }
        }

    }
}
