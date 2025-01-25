using GameCube.GX.Texture;
using Manifold.IO;

namespace GameCube.GFZ.TPL
{
    public class TextureBundleElement
    {
        private static readonly Texture defaultTexture = new();

        public AddressRange AddressRange { get; internal set; }
        public uint CRC32 { get; internal set; } = 0;
        /// <summary>
        ///     True if this texture was deserialized without any issues.
        /// </summary>
        public bool IsValid { get; internal set; } = false;
        public byte[] RawTextureData { get; internal set; } = [];
        public Texture Texture { get; internal set; } = defaultTexture;


        public string Crc32Text => CRC32.ToString("x8");
        public bool HasRawData => RawTextureData.Length > 0;
        public bool IsCorrupted => !IsValid;


        /// <summary>
        ///      
        /// </summary>
        public TextureBundleElement() { }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="isValid"></param>
        public TextureBundleElement(Texture texture, bool isValid = true)
        {
            Texture = texture;
            IsValid = isValid;
        }
    }
}
