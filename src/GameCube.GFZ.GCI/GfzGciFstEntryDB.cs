using GameCube.DiskImage;
using GameCube.GCI;
using System;

namespace GameCube.GFZ.GCI;

/// <summary>
///         
/// </summary>
public static class GfzGciFstEntryDB
{
    public static readonly GciFstEntry Emblem = new()
    {
        GameID = "XXXXXX",
        BannerIconFlags = GciBannerIconFlags.DirectColorRGB5A3,
        InternalFileName = "x.dat",
        ModificationTime = 0xFAAAAAAA,
        ImageDataOffset = 0xFBBBBBBB,
        GciImageFormat = GciImageFormat.DirectColor,
        GciAnimationSpeed = GciAnimationSpeed.Icon0_FrameCount12,
        GciPermissionFlags = GciPermissionFlags.IsPublic,
        CopyCount = 0,
        FirstBlockIndex = 0,
        BlockCount = 0xFCCC,
        CommentOffset = 0xFDDDDDDD,
    };

    public static readonly GciFstEntry EmblemJP = Emblem with { GameID = "GFZJ8P" };
    public static readonly GciFstEntry EmblemNA = Emblem with { GameID = "GFZE8P" };
    public static readonly GciFstEntry EmblemEU = Emblem with { GameID = "GFZP8P" };

    public static GciFstEntry GetEmblemByRegion(Region region)
    {
        return region switch
        {
            Region.Japan => EmblemJP,
            Region.NorthAmerica => EmblemNA,
            Region.Europe => EmblemEU,
            _ => throw new NotImplementedException(),
        };
    }
}
