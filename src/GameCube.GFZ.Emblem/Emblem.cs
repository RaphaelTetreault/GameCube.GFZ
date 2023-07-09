using GameCube.GX.Texture;
using Manifold.IO;
using System;

namespace GameCube.GFZ.Emblem
{
    // this is really jus a serializable texture... maybe worth doing generic Tex with serialize?
    public class Emblem :
        IBinarySerializable,
        IBinaryFileType
    {
        // Consts
        public const int Width = 64;
        public const int Height = 64;
        public const TextureFormat Format = TextureFormat.RGB5A3;
        public const Endianness endianness = Endianness.BigEndian;
        public static readonly int Size = 64 * 64 * sizeof(ushort);
        public static readonly DirectEncoding DirectEncoding = DirectEncoding.GetEncoding(Format);

        // Properties
        public Texture Texture { get; set; } = new Texture();

        public Endianness Endianness => endianness;
        public string FileExtension => ".bin";
        public string FileName { get; set; } = string.Empty;

        // Constructors
        public Emblem()
        {
            Texture = new Texture(Width, Height, Format);
            ThrowErrorIfInvalid();
        }
        public Emblem(Texture texture)
        {
            Texture = texture;
            ThrowErrorIfInvalid();
        }

        // Methods
        public void Deserialize(EndianBinaryReader reader)
        {
            Texture = Texture.ReadDirectColorTexture(reader, Format, Width, Height);
            ThrowErrorIfInvalid();
        }
        public void Serialize(EndianBinaryWriter writer)
        {
            ThrowErrorIfInvalid();
            var blocks = Texture.CreateDirectColorBlocksFromTexture(Texture, DirectEncoding);
            DirectEncoding.WriteBlocks(writer, blocks);
        }

        private void ThrowErrorIfInvalid()
        {
            bool hasInvalidWidth = Texture.Width != Width;
            bool hasInvalidHeight = Texture.Height != Height;
            bool hasInvalidDimensions = hasInvalidWidth || hasInvalidHeight;
            if (hasInvalidDimensions)
            {
                string msg =
                    $"{GetType().Name} has invalid dimensions ({Texture.Width},{Texture.Height}). " +
                    $"{GetType().Name} must have a dimension of exactly ({Width}, {Height}).";
                throw new ArgumentException(msg);
            }
        }
    }
}
