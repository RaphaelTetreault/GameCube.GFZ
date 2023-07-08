using GameCube.GCI;
using Manifold.IO;

namespace GameCube.GFZ.Ghosts
{
    public class GfzGci<TBinarySerializable> : GciWithUniqueID<TBinarySerializable>
        where TBinarySerializable : IBinarySerializable, IBinaryFileType, new()
    {
        public const ushort UID0 = 0x0201; // but also 0401: perhaps GFZ-J/E/P?
        public const ushort UID1 = 0x0401; // but also 0401: perhaps GFZ-J/E/P?
        public const int GameTitleLength = 32;
        public const int CommentLength = 60;

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
            var textEncoding = header.GetTextEncoding();

            reader.Read(ref unknown);
            reader.Read(ref uniqueID);
            Assert.IsTrue(reader.GetPositionAsPointer() == header.CommentPtr);
            reader.Read(ref gameTitle, textEncoding, GameTitleLength);
            reader.Read(ref comment, textEncoding, CommentLength);
            Assert.IsTrue(reader.GetPositionAsPointer() == header.ImageDataPtr);
            Banner = ReadDirectColorBanner(reader);
            Icons = new GX.Texture.Texture[]
            {
                ReadDirectColorIcon(reader),
            };
            Assert.IsTrue(header.ImageFormat == ImageFormat.DirectColor);
            Assert.IsTrue(header.GetAnimationFrameCount() == Icons.Length);
        }

        public override void SerializeCommentAndImages(EndianBinaryWriter writer)
        {
            var textEncoding = header.GetTextEncoding();

            writer.Write(unknown);
            writer.Write(uniqueID);
            writer.Write(gameTitle, textEncoding, false);
            writer.WritePadding(0x00, GameTitleLength - gameTitle.Length);
            writer.Write(comment, textEncoding, false);
            writer.WritePadding(0x00, CommentLength - comment.Length);

            throw new System.NotImplementedException();
            // TODO: weite banner and icons
        }
    }
}
