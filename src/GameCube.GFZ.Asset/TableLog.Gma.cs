using GameCube.GFZ.GMA;
using Manifold;
using Manifold.IO;
using static Manifold.IO.TableLogger;

namespace GameCube.GFZ.Asset;

public class GmaTableLogger
{
    public static readonly LogFuncFile<GmaFile> LogGcmf = new(AnalyzeGcmf, $"{nameof(Gma)}-{nameof(Gcmf)}.tsv");
    public static readonly LogFuncFile<GmaFile> LogTextureConfigs = new(AnalyzeTextureConfigs, $"{nameof(Gma)}-{nameof(TevLayer)}.tsv");
    public static readonly LogFuncFile<GmaFile> LogMaterials = new(AnalyzeMaterials, $"{nameof(Gma)}-{nameof(Material)}.tsv");

    public static readonly LogFuncFile<GmaFile>[] AllLogFunctionFiles =
    [
        LogGcmf,
        LogTextureConfigs,
        LogMaterials,
    ];

    public static void AnalyzeGcmf(GmaFile[] gmas, string outputFileName)
    {
        using var writer = new StreamWriter(File.Create(outputFileName));
        // Write header
        writer.WriteNextCol("FileName");
        writer.WriteNextCol("Address");
        writer.WriteNextCol(nameof(Model.Name));
        writer.WriteNextCol("Model Index");
        writer.WriteNextCol("Debug Index");
        writer.WriteNextCol(nameof(Gcmf.Attributes));
        writer.WriteNextCol($"{nameof(Gcmf.BoundingSphere)}.Origin");
        writer.WriteNextCol($"{nameof(Gcmf.BoundingSphere)}.Radius");
        writer.WriteNextCol(nameof(Gcmf.TextureCount));
        writer.WriteNextCol(nameof(Gcmf.OpaqueMaterialCount));
        writer.WriteNextCol(nameof(Gcmf.TranslucidMaterialCount));
        writer.WriteNextCol(nameof(Gcmf.BoneCount));
        writer.WriteNextCol(nameof(Gcmf.SubmeshOffsetPtr));
        writer.WriteNextCol(nameof(Gcmf.SkinnedVertexDescriptor));
        writer.WriteNextCol(nameof(Gcmf.Submeshes));
        writer.WriteNextCol(nameof(Gcmf.SkinnedVerticesA));
        writer.WriteNextCol(nameof(Gcmf.SkinnedVerticesB));
        writer.WriteNextCol(nameof(Gcmf.SkinBoneBindings));
        writer.WriteNextCol(nameof(Gcmf.UnkBoneIndices));
        writer.WriteNextRow();

        foreach (var gma in gmas)
        {
            foreach (var model in gma.Value.Models.Iterate())
            {
                var gcmf = model.Value.Gcmf;
                writer.WriteNextCol(gma.FileName);
                writer.WriteNextCol(gcmf.AddressRange.PrintStartAddress());
                writer.WriteNextCol(model.Value.Name);
                writer.WriteNextCol(model.Index);
                writer.WriteNextCol(model.Value.DebugIndex);
                writer.WriteNextCol(gcmf.Attributes);
                writer.WriteNextCol(gcmf.BoundingSphere.origin);
                writer.WriteNextCol(gcmf.BoundingSphere.radius);
                writer.WriteNextCol(gcmf.TextureCount);
                writer.WriteNextCol(gcmf.OpaqueMaterialCount);
                writer.WriteNextCol(gcmf.TranslucidMaterialCount);
                writer.WriteNextCol(gcmf.BoneCount);
                writer.WriteNextCol(gcmf.SubmeshOffsetPtr);
                writer.WriteNextCol(gcmf.SkinnedVertexDescriptor is not null);
                writer.WriteNextCol(gcmf.Submeshes.Length);
                writer.WriteNextCol(gcmf.SkinnedVerticesA.Length);
                writer.WriteNextCol(gcmf.SkinnedVerticesB.Length);
                writer.WriteNextCol(gcmf.SkinBoneBindings.Length);
                writer.WriteNextCol(gcmf.UnkBoneIndices.Length);
                writer.WriteNextRow();
            }
        }
        writer.Close();
    }

    public static void AnalyzeTextureConfigs(GmaFile[] gmas, string outputFileName)
    {
        using var writer = new StreamWriter(File.Create(outputFileName));

        // Write header
        writer.WriteNextCol("FileName");
        writer.WriteNextCol("Address");
        writer.WriteNextCol(nameof(Model.Name));
        writer.WriteNextCol("Model Index");
        writer.WriteNextCol("Model Debug Index");
        writer.WriteNextCol("Tex Index");
        writer.WriteNextCol("Tex Debug Index");
        writer.WriteNextCol(nameof(TevLayer.TextureFlags));
        foreach (var flag in Enum.GetNames<TevTextureFlags>())
            writer.WriteNextCol(flag);
        writer.WriteNextCol(nameof(TevLayer.TplTextureIndex));
        writer.WriteNextCol(nameof(TevLayer.LodBias));
        writer.WriteNextCol(nameof(TevLayer.AnisotropicFilter));
        writer.WriteNextCol(nameof(TevLayer.Unk0x0C));
        writer.WriteNextCol(nameof(TevLayer.IsSwappableTexture));
        writer.WriteNextCol(nameof(TevLayer.TevLayerIndex));
        writer.WriteNextCol(nameof(TevLayer.Unk0x12));
        writer.WriteNextRow();

        foreach (var gma in gmas)
        {
            foreach (var model in gma.Value.Models.Iterate())
            {
                int texIndex = 0;
                foreach (var tevLayer in model.Value.Gcmf.TevLayers.Iterate())
                {
                    writer.WriteNextCol(gma.FileName);
                    writer.WriteNextCol(tevLayer.Value.AddressRange.PrintStartAddress());
                    writer.WriteNextCol(model.Value.Name);
                    writer.WriteNextCol(model.Index);
                    writer.WriteNextCol(model.Value.DebugIndex);
                    writer.WriteNextCol(tevLayer.Index);
                    writer.WriteNextCol($"[{++texIndex}/{model.Value.Gcmf.TextureCount}]");
                    writer.WriteNextCol(tevLayer.Value.TextureFlags);
                    for (int i = 0; i < 32; i++)
                    {
                        TevTextureFlags flag = (TevTextureFlags)(1 << i);
                        if (tevLayer.Value.TextureFlags.HasFlag(flag))
                            writer.WriteNextCol(tevLayer.Value.TextureFlags & flag);
                        else
                            writer.WriteNextCol();
                    }
                    writer.WriteNextCol(tevLayer.Value.TplTextureIndex);
                    writer.WriteNextCol(tevLayer.Value.LodBias);
                    writer.WriteNextCol(tevLayer.Value.AnisotropicFilter);
                    writer.WriteNextCol(tevLayer.Value.Unk0x0C);
                    writer.WriteNextCol(tevLayer.Value.IsSwappableTexture);
                    writer.WriteNextCol(tevLayer.Value.TevLayerIndex);
                    writer.WriteNextCol(tevLayer.Value.Unk0x12);
                    writer.WriteNextRow();
                }
            }
        }
        writer.Close();
    }

    public static void AnalyzeMaterials(GmaFile[] gmas, string outputFileName)
    {
        using var writer = new StreamWriter(File.Create(outputFileName));
        // Write header
        writer.WriteNextCol("FileName");
        writer.WriteNextCol("Address");
        writer.WriteNextCol(nameof(Model.Name));
        writer.WriteNextCol("Model Index");
        writer.WriteNextCol("Debug Index");
        writer.WriteNextCol("Material Index");
        writer.WriteNextCol(nameof(Submesh.RenderFlags));
        writer.WriteNextCol(nameof(Material.MaterialColor));
        writer.WriteNextCol(nameof(Material.AmbientColor));
        writer.WriteNextCol(nameof(Material.SpecularColor));
        writer.WriteNextCol(nameof(Material.Unk0x10));
        writer.WriteNextCol(nameof(Material.Alpha));
        writer.WriteNextCol(nameof(Material.TevLayerCount));
        writer.WriteNextCol(nameof(Material.MaterialDestination));
        writer.WriteNextCol(nameof(Material.UnkAlpha0x14));
        writer.WriteNextCol(nameof(Material.Unk0x15));
        writer.WriteNextCol(nameof(Material.TevLayerIndex0));
        writer.WriteNextCol(nameof(Material.TevLayerIndex1));
        writer.WriteNextCol(nameof(Material.TevLayerIndex2));
        writer.WriteNextCol(nameof(Submesh.VertexAttributes));
        writer.WriteNextCol(nameof(UnkAlphaOptions.Origin));
        writer.WriteNextCol(nameof(UnkAlphaOptions.Unk0x0C));
        writer.WriteNextCol(nameof(UnkAlphaOptions.BlendFactors));
        writer.WriteNextRow();

        foreach (var gma in gmas)
        {
            foreach (var model in gma.Value.Models.Iterate())
            {
                foreach (var submesh in model.Value.Gcmf.Submeshes.Iterate())
                {
                    writer.WriteNextCol(gma.FileName);
                    writer.WriteNextCol(submesh.Value.AddressRange.PrintStartAddress());
                    writer.WriteNextCol(model.Value.Name);
                    writer.WriteNextCol(model.Index);
                    writer.WriteNextCol(model.Value.DebugIndex);
                    writer.WriteNextCol(submesh.Index);
                    writer.WriteNextCol(submesh.Value.RenderFlags);
                    writer.WriteNextCol(submesh.Value.Material.MaterialColor);
                    writer.WriteNextCol(submesh.Value.Material.AmbientColor);
                    writer.WriteNextCol(submesh.Value.Material.SpecularColor);
                    writer.WriteNextCol(submesh.Value.Material.Unk0x10);
                    writer.WriteNextCol(submesh.Value.Material.Alpha);
                    writer.WriteNextCol(submesh.Value.Material.TevLayerCount);
                    writer.WriteNextCol(submesh.Value.Material.MaterialDestination);
                    writer.WriteNextCol(submesh.Value.Material.UnkAlpha0x14);
                    writer.WriteNextCol(submesh.Value.Material.Unk0x15);
                    writer.WriteNextCol(submesh.Value.Material.TevLayerIndex0);
                    writer.WriteNextCol(submesh.Value.Material.TevLayerIndex1);
                    writer.WriteNextCol(submesh.Value.Material.TevLayerIndex2);
                    writer.WriteNextCol(submesh.Value.VertexAttributes);
                    writer.WriteNextCol(submesh.Value.UnkAlphaOptions.Origin);
                    writer.WriteNextCol(submesh.Value.UnkAlphaOptions.Unk0x0C);
                    writer.WriteNextCol(submesh.Value.UnkAlphaOptions.BlendFactors);
                    writer.WriteNextRow();
                }//submesh
            }//model
        }//gma
        writer.Close();
    }

}
