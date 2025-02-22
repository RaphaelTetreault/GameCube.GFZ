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
    public static VertexAttributeTable VAT { get => vat; }

    private static readonly VertexAttributeTable vat = new(
        [
            // VAT 0
            new VertexAttributeFormat()
            {
                pos = new VertexAttribute(ComponentCount.GX_POS_XYZ, ComponentType.GX_F32),
                nrm = new VertexAttribute(ComponentCount.GX_NRM_XYZ, ComponentType.GX_F32),
                nbt = new VertexAttribute(ComponentCount.GX_NRM_NBT, ComponentType.GX_F32),
                clr0 = new VertexAttribute(ComponentCount.GX_CLR_RGBA, ComponentType.GX_RGBA8),
                tex0 = new VertexAttribute(ComponentCount.GX_TEX_ST, ComponentType.GX_F32),
                tex1 = new VertexAttribute(ComponentCount.GX_TEX_ST, ComponentType.GX_F32),
                tex2 = new VertexAttribute(ComponentCount.GX_TEX_ST, ComponentType.GX_F32),
            },

            // VAT 1
            new VertexAttributeFormat()
            {
                pos = new VertexAttribute(ComponentCount.GX_POS_XYZ, ComponentType.GX_S16, 13), // CHMCL verified 2024/09/05
                nrm = new VertexAttribute(ComponentCount.GX_NRM_XYZ, ComponentType.GX_S16, 14), // TODO: RE-VERIFY
                clr0 = new VertexAttribute(ComponentCount.GX_CLR_RGBA, ComponentType.GX_RGBA8), // Raphaël verified 2019 (?)
                tex0 = new VertexAttribute(ComponentCount.GX_TEX_ST, ComponentType.GX_S16, 13), // CHMCL verified 2024/09/05
                tex1 = new VertexAttribute(ComponentCount.GX_TEX_ST, ComponentType.GX_S16, 13), // UNVERIFIED
                tex2 = new VertexAttribute(ComponentCount.GX_TEX_ST, ComponentType.GX_S16, 13), // UNVERIFIED
            },
        ]
    );
}