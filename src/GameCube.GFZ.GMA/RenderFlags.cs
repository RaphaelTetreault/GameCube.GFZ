namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// See: RenderFlags: https://github.com/bobjrsenior/GxUtils/blob/master/GxUtils/LibGxFormat/Gma/GcmfMesh.cs
    /// </remarks>
    [System.Flags]
    public enum RenderFlags : uint
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
        /// Do not apply fog to this mesh.
        /// </summary>
        noFog = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        customMaterialUseAmbientColor = 1 << 3,

        /// <summary>
        /// 
        /// </summary>
        perVertexShading = 1 << 4,

        /// <summary>
        /// 
        /// </summary>
        screenBlend = 1 << 5,

        /// <summary>
        /// 
        /// </summary>
        additiveBlend = 1 << 6,

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
        //vertexColors = 1 << 8,

        /// <summary>
        /// Data at address 0x3C is non-zero.
        /// </summary>
        /// <remarks>
        /// 2022-06-24: verified on single occurence.
        /// </remarks>
        hasAlphaFlags0x3C = 1 << 9, // 0x0200
    }
}