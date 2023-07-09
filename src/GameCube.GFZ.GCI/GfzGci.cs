using GameCube.GCI;
using GameCube.GX.Texture;
using Manifold.IO;

namespace GameCube.GFZ.GCI
{
    public class GfzGci<TBinarySerializable> : GciWithUniqueID<TBinarySerializable>
        where TBinarySerializable : IBinarySerializable, IBinaryFileType, new()
    {
        public const ushort UID0 = 0x0201; // but also 0401: perhaps GFZ-J/E/P?
        public const ushort UID1 = 0x0401; // but also 0201: perhaps GFZ-J/E/P?
        public const int GameTitleLength = 32;
        public const int CommentLength = 60;
        public const int IconsCount = 1;

        private string gameTitle = string.Empty;
        private string comment = string.Empty;
        private ushort unknown;
        private ushort uniqueID;


        public readonly ushort[] UIDs = { UID0, UID1 };
        public override ushort UniqueID => uniqueID;
        public override ushort[] UniqueIDs => UIDs;
        public override string Comment { get => comment; set => comment = value; }
        public override ushort Unknown => unknown;
        public string GameTitle { get => gameTitle; set => gameTitle = value; }


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
    }
}
