namespace GameCube.GFZ.TPL
{
    public class TextureSeries
    {
        public TextureData[] TextureData { get; internal set; }

        public TextureData this[int i]
        {
            get => TextureData[i];
            set => TextureData[i] = value;
        }

        public int Length => TextureData is null ? 0 : TextureData.Length;

        public TextureSeries(int numTextures = 0)
        {
            TextureData = new TextureData[numTextures];
            for (int i = 0; i < TextureData.Length; i++)
                TextureData[i] = new TextureData();
        }
    }
}
