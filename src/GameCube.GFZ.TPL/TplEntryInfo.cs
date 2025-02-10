namespace GameCube.GFZ.TPL;

/// <summary>
///     A helper struct to link texture CRC32 and data for a TPL texture.
/// </summary>
public readonly record struct TplEntryInfo
{
    public TplEntryInfo(string crc32Name, TextureBundle textureBundle)
    {
        Crc32Name = crc32Name;
        TextureBundle = textureBundle;
    }

    public required string Crc32Name { get; init; }
    public required TextureBundle TextureBundle { get; init; }
}

public static class TplEntryInfoExt
{
    public static string[] GetCrc32Names(this TplEntryInfo[] infos)
    {
        var crc32Names = new string[infos.Length];
        for (int i = 0; i < infos.Length; i++)
            crc32Names[i] = infos[i].Crc32Name;
        return crc32Names;
    }

    public static TextureBundle[] GetTextureBundles(this TplEntryInfo[] infos)
    {
        var textureBundle = new TextureBundle[infos.Length];
        for (int i = 0; i < infos.Length; i++)
            textureBundle[i] = infos[i].TextureBundle;
        return textureBundle;
    }
}