using GameCube.GX.Texture;
using Manifold.IO;
using System;

namespace GameCube.GFZ.TPL
{
    public class TextureBundleElement
    {
        private static readonly Texture defaultTexture = new();

        public AddressRange AddressRange { get; internal set; }
        public string CRC32 { get; internal set; } = string.Empty;
        public bool IsCorrupted => !IsValid;
        /// <summary>
        ///     True if this texture was deserialized without any issues.
        /// </summary>
        public bool IsValid { get; internal set; } = false;
        public bool HasRawData => RawTextureData.Length > 0;
        public byte[] RawTextureData { get; internal set; } = Array.Empty<byte>();
        public Texture Texture { get; internal set; } = defaultTexture;

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
