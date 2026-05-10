namespace GameCube.GFZ.GMA;

/// <summary>
/// 
/// </summary>
/// <remarks>
///     See: RenderFlags: https://github.com/bobjrsenior/GxUtils/blob/master/GxUtils/LibGxFormat/Gma/GcmfMesh.cs
/// </remarks>
[System.Flags]
public enum RenderFlags : uint
{
    /// <summary>
    ///     Geometry is unlit.
    /// </summary>
    Unlit = 1 << 0,

    /// <summary>
    ///     All faces of the associated display lists render on both sides.
    /// </summary>
    DoubleSidedFaces = 1 << 1,

    /// <summary>
    ///     Do not apply fog to this mesh.
    /// </summary>
    NoFog = 1 << 2,

    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     https://github.com/camthesaxman/smb-decomp/blob/5cd6ffccf3f8508231c7cd9cce5b722be4465a2b/src/gma.h#L87
    /// </remarks>
    MaterialUseAmbientColor = 1 << 3,

    /// <summary>
    ///         
    /// </summary>
    /// <remarks>
    ///     I'm not sure if this originally came from SMB.
    ///     They no longer list this flag if they once did.
    /// </remarks>
    UNK_PerVertexShading = 1 << 4,

    /// <summary>
    /// 
    /// </summary>
    BlendSource = 1 << 5,

    /// <summary>
    /// 
    /// </summary>
    BlendDestination = 1 << 6,

    /// <summary>
    /// 
    /// </summary>
    SimpleMaterial = 1 << 7,

    /// <summary>
    ///     
    /// </summary>
    VertexColors = 1 << 8,

    /// <summary>
    ///     Data at address 0x3C is non-zero.
    /// </summary>
    /// <remarks>
    ///     2022-06-24: verified on single occurence.
    /// </remarks>
    HasAlphaFlags0x3C = 1 << 9, // 0x0200
}