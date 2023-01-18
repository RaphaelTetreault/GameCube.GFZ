using GameCube.GX.Texture;
using Manifold.IO;

namespace GameCube.GFZ.TPL
{
    public class TextureSeries
    {
        public TextureData[] Entries { get; internal set; }
        public TextureSeriesDescription Description { get; internal set; }
        public string[] MD5TextureHashes { get; internal set; }
        public AddressRange AddressRange { get; internal set; }

        public TextureData this[int i]
        {
            get => Entries[i];
            set => Entries[i] = value;
        }

        public int Length => Entries is null ? 0 : Entries.Length;

        public TextureSeries(TextureSeriesDescription textureSeriesDescription)
        {
            Description = textureSeriesDescription;

            // Initialiize texture series
            int numberOfTextures = textureSeriesDescription.NumberOfTextures;
            MD5TextureHashes = new string[numberOfTextures];
            Entries = new TextureData[numberOfTextures];
            for (int i = 0; i < Entries.Length; i++)
                Entries[i] = new TextureData();
        }
    }
}
