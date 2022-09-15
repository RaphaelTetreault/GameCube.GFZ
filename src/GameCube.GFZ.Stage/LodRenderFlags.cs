namespace GameCube.GFZ.Stage
{
    // NOTES:
    // Unique combinations:
    //   0: none.
    //   1: bit0.
    //   3: bit1 + bit0.
    //  64: bit6.
    //  65: bit6 + bit0.
    // 193: bit7 + bit0.
    // 194: bit7 + bit1.
    // 195: bit7 + bit1 + bit0.
    // 256: bit8.

    /// <summary>
    /// How to display Scene Object LODs.
    /// TODO: none = no LODS (?). There's going to be a pop-in option and a fade-in option.
    /// Some flags may also have to do with multiplayer (ie: do not render in 3/4 player mode).
    /// </summary>
    [System.Flags]
    public enum LodRenderFlags : uint
    {
        none = 0,
        SmoothTransparentFadeInOut = 1 << 0,
        unk1_RenderNearOnly = 1 << 1,
        // 2
        // 3
        // 4
        // 5
        unk6_SpecialAlpha = 1 << 6,         //  64 -- primarily on dash yellow transparent objects, or other "lights"
        unk7_SpecialAlphaAddOn = 1 << 7,    // 128 -- seems to be an add-on for above, used exclusively with it.
        unk8_PhantomRoadCube = 1 << 8,      // 256 -- used exclusively on cubes in Phantom Road
    }
}
