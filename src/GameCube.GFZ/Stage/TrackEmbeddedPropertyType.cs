namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Denotes what kind of embeded property the animation data represents
    /// for this track segment.
    /// </summary>
    /// <remarks>
    /// Surface properties like ice, dirt, damage, and heal are flags that can be combined in the
    /// sense the game will respect all properties. This is undesirable game design, though.
    /// </remarks>
    public enum TrackEmbeddedPropertyType : byte
    {
        None                = 0,      //   0, 0x00
        //Unused_None       = 1 << 0, //   1, 0x01 // 2022/06/21: tested, does nothing :( - EXCEPT, try as "pipe" type?
        IsLava              = 1 << 1, //   2, 0x02
        IsDirt              = 1 << 2, //   4, 0x04
        IsIce               = 1 << 3, //   8, 0x08
        IsHealPit           = 1 << 4, //  16, 0x10
        IsModulated         = 1 << 5, //  32, 0x20
        IsCapsulePipe       = 1 << 6, //  64, 0x40
        IsPipeOrCylinder    = 1 << 7, // 128, 0x80
    }
}
