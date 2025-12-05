using GameCube.GX;

namespace GameCube.GFZ;

/// <summary>
///     GameCube GX (GPU) data values for GFZ games.
/// </summary>
public static class GfzGX
{
    /// <summary>
    /// GFZ's Vertex Attribute Table
    /// </summary>
    public static GXVertexAttributeTable VAT { get => vat; }

    private static readonly GXVertexAttributeTable vat = new(
        [
            // VAT 0
            new GXVertexAttributeFormat()
            {
                pos = new GXVertexAttribute(GXComponentCount.GX_POS_XYZ, GXComponentType.GX_F32),
                nrm = new GXVertexAttribute(GXComponentCount.GX_NRM_XYZ, GXComponentType.GX_F32),
                nbt = new GXVertexAttribute(GXComponentCount.GX_NRM_NBT, GXComponentType.GX_F32),
                clr0 = new GXVertexAttribute(GXComponentCount.GX_CLR_RGBA, GXComponentType.GX_RGBA8),
                tex0 = new GXVertexAttribute(GXComponentCount.GX_TEX_ST, GXComponentType.GX_F32),
                tex1 = new GXVertexAttribute(GXComponentCount.GX_TEX_ST, GXComponentType.GX_F32),
                tex2 = new GXVertexAttribute(GXComponentCount.GX_TEX_ST, GXComponentType.GX_F32),
            },

            // VAT 1
            new GXVertexAttributeFormat()
            {
                pos = new GXVertexAttribute(GXComponentCount.GX_POS_XYZ, GXComponentType.GX_S16, 13), // CHMCL verified 2024/09/05
                nrm = new GXVertexAttribute(GXComponentCount.GX_NRM_XYZ, GXComponentType.GX_S16, 14), // TODO: RE-VERIFY
                clr0 = new GXVertexAttribute(GXComponentCount.GX_CLR_RGBA, GXComponentType.GX_RGBA8), // Raphaël verified 2019 (?)
                tex0 = new GXVertexAttribute(GXComponentCount.GX_TEX_ST, GXComponentType.GX_S16, 13), // CHMCL verified 2024/09/05
                tex1 = new GXVertexAttribute(GXComponentCount.GX_TEX_ST, GXComponentType.GX_S16, 13), // UNVERIFIED
                tex2 = new GXVertexAttribute(GXComponentCount.GX_TEX_ST, GXComponentType.GX_S16, 13), // UNVERIFIED
            },
        ]
    );
}