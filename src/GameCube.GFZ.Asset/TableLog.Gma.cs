using GameCube.GFZ.GMA;
using Manifold;
using Manifold.IO;
using static Manifold.IO.TableLogger;

namespace GameCube.GFZ.Asset;

public class GmaTableLogger
{
    public static readonly LogFuncFile<GmaFile> LogGcmf = new(AnalyzeGcmf, $"{nameof(Gma)}-{nameof(Gcmf)}.tsv");
    public static readonly LogFuncFile<GmaFile> LogTextureConfigs = new(AnalyzeTevLayers, $"{nameof(Gma)}-{nameof(TevLayer)}.tsv");
    public static readonly LogFuncFile<GmaFile> LogSubmesh = new(AnalyzeSubmeshes, $"{nameof(Gma)}-{nameof(Submesh)}.tsv");

    public static readonly LogFuncFile<GmaFile>[] AllLogFunctionFiles =
    [
        LogGcmf,
        LogTextureConfigs,
        LogSubmesh,
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

    public static void AnalyzeTevLayers(GmaFile[] gmas, string outputFileName)
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
        writer.WriteNextCol(nameof(TevLayer.TevCombinerFlags));
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
                    writer.WriteNextCol(tevLayer.Value.TevCombinerFlags);
                    writer.WriteNextRow();
                }
            }
        }
        writer.Close();
    }

    public static void AnalyzeSubmeshes(GmaFile[] gmas, string outputFileName)
    {
        using var writer = new StreamWriter(File.Create(outputFileName));
        // Write header
        writer.WriteNextCol("FileName");
        writer.WriteNextCol("Address");
        writer.WriteNextCol("Model Name");
        writer.WriteNextCol("Model Index");
        writer.WriteNextCol("Debug Index");
        writer.WriteNextCol("Submesh Index");
        writer.WriteNextCol(nameof(Submesh.RenderFlags));
        writer.WriteNextCol(nameof(Submesh.MaterialColor));
        writer.WriteNextCol(nameof(Submesh.AmbientColor));
        writer.WriteNextCol(nameof(Submesh.SpecularColor));
        writer.WriteNextCol(nameof(Submesh.Unk0x10));
        writer.WriteNextCol(nameof(Submesh.Alpha));
        writer.WriteNextCol(nameof(Submesh.TevLayerCount));
        writer.WriteNextCol(nameof(Submesh.SubmeshDisplayListFlags));
        writer.WriteNextCol(nameof(Submesh.UnkAlpha0x14));
        writer.WriteNextCol(nameof(Submesh.Unk0x15));
        writer.WriteNextCol(nameof(Submesh.TevLayerIndex0));
        writer.WriteNextCol(nameof(Submesh.TevLayerIndex1));
        writer.WriteNextCol(nameof(Submesh.TevLayerIndex2));
        writer.WriteNextCol(nameof(Submesh.VertexAttributes));
        writer.WriteNextCol(nameof(Submesh.BlendDepthSortOrigin));
        writer.WriteNextCol(nameof(Submesh.BlendUnkFloat));
        writer.WriteNextCol(nameof(Submesh.BlendFactors));
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
                    writer.WriteNextCol(submesh.Value.MaterialColor);
                    writer.WriteNextCol(submesh.Value.AmbientColor);
                    writer.WriteNextCol(submesh.Value.SpecularColor);
                    writer.WriteNextCol(submesh.Value.Unk0x10);
                    writer.WriteNextCol(submesh.Value.Alpha);
                    writer.WriteNextCol(submesh.Value.TevLayerCount);
                    writer.WriteNextCol(submesh.Value.SubmeshDisplayListFlags);
                    writer.WriteNextCol(submesh.Value.UnkAlpha0x14);
                    writer.WriteNextCol(submesh.Value.Unk0x15);
                    writer.WriteNextCol(submesh.Value.TevLayerIndex0);
                    writer.WriteNextCol(submesh.Value.TevLayerIndex1);
                    writer.WriteNextCol(submesh.Value.TevLayerIndex2);
                    writer.WriteNextCol(submesh.Value.VertexAttributes);
                    writer.WriteNextCol(submesh.Value.BlendDepthSortOrigin);
                    writer.WriteNextCol(submesh.Value.BlendUnkFloat);
                    writer.WriteNextCol(submesh.Value.BlendFactors);
                    writer.WriteNextRow();
                }//submesh
            }//model
        }//gma
        writer.Close();
    }

}
