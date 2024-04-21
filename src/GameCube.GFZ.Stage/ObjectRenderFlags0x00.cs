namespace GameCube.GFZ.Stage
{
    [System.Flags]
    public enum ObjectRenderFlags0x00 : uint
    {
        renderObject1pFullScreen = 1 << 0,
        renderObject2pSplitScreen = 1 << 1, 
        renderObject3pSplitScreen = 1 << 2,
        renderObject4pSplitScreen = 1 << 3,
        unk_AlphaBlendMode = 1 << 4,
        unk_5 = 1 << 5, // disabled in track preview?
        unk_HideObject1 = 1 << 6,
        unk_HideObject2 = 1 << 7,
        renderAsBillboard = 1 << 8,
        renderInScreenSpace = 1 << 9,
        unk_ModifyRenderIndex = 1 << 10,
        alphaNoClipSelf = 1 << 11,
        unk_12 = 1 << 12,
        hasTextureScroll0 = 1 << 13,
        hasTextureScroll1 = 1 << 14,
        hasTextureScroll2 = 1 << 15,
        hasTextureScroll3 = 1 << 16,
        unk_17 = 1 << 17,
        unk_18 = 1 << 18, // crash?
        receiveEfbShadow = 1 << 19,
        unk_20 = 1 << 20,
        unk_21 = 1 << 21,
        unk_22 = 1 << 22,
        unk_23 = 1 << 23, // written to by game...?
        unk_24 = 1 << 24,
        unk_25 = 1 << 25,
        unk_26 = 1 << 26,
        unk_27 = 1 << 27,
        unk_28 = 1 << 28,
        unk_29 = 1 << 29,
        unk_DisableObject1 = 1 << 30,
        unk_DisableObject2 = unchecked((uint)(1 << 31)),
    }
}
