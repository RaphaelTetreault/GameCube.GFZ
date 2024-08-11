using GameCube.GX.Texture;

namespace GameCube.GFZ.TPL
{
    public class TextureData
    {
        private static readonly Texture texture = new();

        public Texture Texture { get; internal set; } = texture;
        public bool IsValid { get; internal set; } = false;
        public bool IsCorrupted => !IsValid;
        public string CRC32 { get; internal set; } = string.Empty;
    }
}
