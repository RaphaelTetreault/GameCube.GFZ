using GameCube.GCI;
using GameCube.GX.Texture;

namespace GameCube.GFZ.GCI;

public static class GfzGciMetadataDB
{
    public static readonly GfzGciMetadata Emblem = new()
    {
        ID = 0x0401,
        Banner = new Banner()
        {
            Format = GciTextureFormat.DirectColor,
        },
        Icons = new Icons()
        {
            Format = GciTextureFormat.DirectColor,
        },
    };
}
