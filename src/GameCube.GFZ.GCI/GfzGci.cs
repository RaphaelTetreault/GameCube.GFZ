using GameCube.DiskImage;
using GameCube.GCI;
using GameCube.GX.Texture;
using Manifold.IO;

namespace GameCube.GFZ.GCI
{
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
        private ushort unknown;
        private ushort uniqueID;

        // PROPERTIES
        public override ushort UniqueID => uniqueID;
        public override string Comment { get => comment; set => comment = value; }
        public override ushort Unknown => unknown;
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
            gameID[3] = GameID.GetRegionChar(region);
            gameID[4] = '0';
            gameID[5] = '1';
            Header.GameID = gameID;
        }


        public override void DeserializeCommentAndImages(EndianBinaryReader reader)
        {
            var textEncoding = Header.GetTextEncoding();

            reader.Read(ref unknown);
            reader.Read(ref uniqueID);
            Assert.IsTrue(reader.GetPositionAsPointer() == Header.CommentPtr);
            reader.Read(ref gameTitle, textEncoding, GameTitleLength);
            reader.Read(ref comment, textEncoding, CommentLength);
            Assert.IsTrue(reader.GetPositionAsPointer() == Header.ImageDataPtr);
            Banner = ReadDirectColorBanner(reader);
            Icons = new Texture[]
            {
                ReadDirectColorIcon(reader),
            };
            Assert.IsTrue(Header.ImageFormat == ImageFormat.DirectColor);
            Assert.IsTrue(Icons.Length == IconsCount);
            Assert.IsTrue(Header.GetAnimationFrameCount() == Icons.Length);
        }

        public override void SerializeCommentAndImages(EndianBinaryWriter writer)
        {
            var textEncoding = Header.GetTextEncoding();

            writer.Write(unknown);
            writer.Write(uniqueID);
            writer.Write(gameTitle, textEncoding, false);
            writer.WritePadding(0x00, GameTitleLength - gameTitle.Length);
            writer.Write(comment, textEncoding, false);
            writer.WritePadding(0x00, CommentLength - comment.Length);
            Assert.IsTrue(Header.ImageFormat == ImageFormat.DirectColor);
            Assert.IsTrue(Icons.Length == IconsCount);
            WriteDirectColorBanner(writer);
            WriteDirectColorIcons(writer);
        }

        public static string FormatGciFileName(GfzGciFileType fileType, GciHeader gciHeader, string fileNameWithoutExtension, out string fileName)
        {
            char regionChar = gciHeader.GameID.RegionCode;
            string prefix = $"8P-GFZ{regionChar}-";
            fileName = GfzGciDesignator(fileType);

            switch (fileType)
            {
                // These types have file names after the default name
                case GfzGciFileType.Emblem: // 24
                case GfzGciFileType.Ghost: // 16
                case GfzGciFileType.Replay: // 22
                    // TODO: old tests showed game needed 3 numbers post baseName. Still needed?
                    //       ie: "8P-GFZE-fze02000020003FBF71CA629FFA.dat.gci"
                    //       I used to do: *fze020_[filename].dat.gci
                    //       ALSO, you need to confrim if the filename can be shorter. If so, fill it in.
                    int maxChars = GetHashLength(fileType);
                    string hashName = fileNameWithoutExtension.Length > maxChars
                        ? fileNameWithoutExtension.Substring(0, maxChars)
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
            switch (fileType)
            {
                case GfzGciFileType.Emblem: return "fze";
                case GfzGciFileType.Garage: return "fzc";
                case GfzGciFileType.Ghost: return "fzg";
                case GfzGciFileType.Replay: return "fzr";

                case GfzGciFileType.Save:
                default:
                    return "";
            }
        }
        private static int GetHashLength(GfzGciFileType fileType)
        {
            switch (fileType)
            {
                case GfzGciFileType.Emblem: return 24;
                case GfzGciFileType.Ghost: return 16;
                case GfzGciFileType.Replay: return 22;

                default:
                    return -1;
            }
        }
    }
}
