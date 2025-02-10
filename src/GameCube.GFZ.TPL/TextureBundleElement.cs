using GameCube.GX.Texture;
using Manifold.IO;

namespace GameCube.GFZ.TPL
{
    public class TextureBundleElement
    {
        private static readonly Texture defaultTexture = new();

        public AddressRange AddressRange { get; set; }
        public uint CRC32 { get; set; } = 0;
        /// <summary>
        ///     True if this texture was deserialized without any issues.
        /// </summary>
        public bool IsValid { get; set; } = false;
        public byte[] RawTextureData { get; set; } = [];
        public Texture Texture { get; set; } = defaultTexture;


        public string Crc32Text => CRC32.ToString("x8");
        public bool HasRawData => RawTextureData.Length > 0;
        public bool IsCorrupted => !IsValid;


        ///// <summary>
        /////      
        ///// </summary>
        //public TextureBundleElement() { }

        ///// <summary>
        /////     
        ///// </summary>
        ///// <param name="texture"></param>
        ///// <param name="isValid"></param>
        //public TextureBundleElement(Texture texture, bool isValid = true)
        //{
        //    Texture = texture;
        //    IsValid = isValid;
        //}
    }
}
