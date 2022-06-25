namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// 
    /// </summary>
    [System.Flags]
    public enum MatFlags0x02 : ushort
    {
        /// <summary>
        /// 
        /// </summary>
        unlit = 1 << 0,

        /// <summary>
        /// All faces of the associated display lists render on both sides.
        /// </summary>
        /// <remarks>
        /// Knowledge from GXUtils.
        /// </remarks>
        doubleSidedFaces = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        noFog = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        customMaterialUseAmbientColor = 1 << 3,

        /// <summary>
        /// 
        /// </summary>
        unk4 = 1 << 4,

        /// <summary>
        /// 
        /// </summary>
        customBlendSource = 1 << 5,

        /// <summary>
        /// 
        /// </summary>
        customBlendDestination = 1 << 6,

        /// <summary>
        /// 
        /// </summary>
        simpleMaterial = 1 << 7,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// IIRC not used in GFZ, from SMB decomp.
        /// </remarks>
        vertexColors = 1 << 8,

        /// <summary>
        /// Data at address 0x3C is non-null.
        /// </summary>
        /// <remarks>
        /// 2022-06-24: verified on single occurence.
        /// </remarks>
        hasAlphaFlags0x3C = 1 << 9, // 0x0200
    }
}