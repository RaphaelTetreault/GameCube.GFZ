using System;

namespace GameCube.GFZ.GameData;

public readonly record struct BgmMusic()
{
    public required BgmIndex BgmIndex { get; init; }
    public BgmIndex BgmFinalLapIndex { get; init; } = BgmIndex.metadata_no_final_lap_bgm;
    public ushort BgmFinalLapLoopOffset { get; init; } = 0xFFFF;
    public required string OfficialName { get; init; }
    public required BgmGroup Group { get; init; }

    public BgmFinalLap GetAsBgmFinalLap()
    {
        return new BgmFinalLap()
        {
            songIndex = (byte)BgmFinalLapIndex,
            loopPointDataOffset = BgmFinalLapLoopOffset,
        };
    }
    public string FileName => BgmIndex.ToString();
    public string Directory => Group switch
    {
        BgmGroup.None => string.Empty,
        BgmGroup.Disc1_CharBgm => "char_bgm",
        BgmGroup.Disc2_AdxBgm => "adx_bgm",
        _ => throw new ArgumentException($"Invalid {nameof(BgmGroup)} value {Group}"),
    };
}
