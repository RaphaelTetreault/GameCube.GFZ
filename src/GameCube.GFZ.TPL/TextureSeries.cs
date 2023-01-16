using Manifold.IO;

namespace GameCube.GFZ.TPL
{
    public class TextureSeries
    {
        public TextureData[] Entries { get; internal set; }
        public AddressRange AddressRange { get; internal set; }

        public TextureData this[int i]
        {
            get => Entries[i];
            set => Entries[i] = value;
        }

        public int Length => Entries is null ? 0 : Entries.Length;

        public TextureSeries(int numTextures = 0)
        {
            Entries = new TextureData[numTextures];
            for (int i = 0; i < Entries.Length; i++)
                Entries[i] = new TextureData();
        }
    }
}
