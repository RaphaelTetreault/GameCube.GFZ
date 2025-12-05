namespace GameCube.GFZ.TPL;

/// <summary>
///     A helper struct to link texture CRC32 and data for a TPL texture.
/// </summary>
public readonly record struct TplEntryInfo
{
    public TplEntryInfo(string crc32Name, TextureSequence textureSequence)
    {
        Crc32Name = crc32Name;
        TextureSequence = textureSequence;
    }

    public required string Crc32Name { get; init; }
    public required TextureSequence TextureSequence { get; init; }
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

    public static TextureSequence[] GetTextureSequences(this TplEntryInfo[] infos)
    {
        var textureSequence = new TextureSequence[infos.Length];
        for (int i = 0; i < infos.Length; i++)
            textureSequence[i] = infos[i].TextureSequence;
        return textureSequence;
    }
}