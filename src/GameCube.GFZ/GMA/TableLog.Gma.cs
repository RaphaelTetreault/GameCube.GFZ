using Manifold.IO;
using System.IO;

namespace GameCube.GFZ.GMA
{
    public class GmaTableLogger
    {
        // Names of files generated
        public static readonly string tsvGcmf = $"{nameof(Gma)}-{nameof(Gcmf)}.tsv";
        public static readonly string tsvTextureConfigs = $"{nameof(Gma)}-{nameof(TevLayer)}.tsv";
        public static readonly string tsvMaterials = $"{nameof(Gma)}-{nameof(Material)}.tsv";

        public static void PrintGmaAll(Gma[] gmas, string outputDirectory)
        {
            {
                var fileName = Path.Combine(outputDirectory, tsvGcmf);
                PrintGcmf(gmas, fileName);
            }
            {
                var fileName = Path.Combine(outputDirectory, tsvTextureConfigs);
                PrintTextureConfigs(gmas, fileName);
            }
            {
                var fileName = Path.Combine(outputDirectory, tsvMaterials);
                PrintMaterials(gmas, fileName);
            }
        }

        public static void PrintGcmf(Gma[] gmas, string outputFileName)
        {
            using (var writer = new StreamWriter(File.Create(outputFileName)))
            {
                // Write header
                writer.WriteNextCol("FileName");
                writer.WriteNextCol("Address");
                writer.WriteNextCol(nameof(Model.Name));
                writer.WriteNextCol("Model Index");
                writer.WriteNextCol("Debug Index");
                writer.WriteNextCol(nameof(Gcmf.Attributes));
                writer.WriteNextCol($"{nameof(Gcmf.BoundingSphere)}.Origin");
                writer.WriteNextCol($"{nameof(Gcmf.BoundingSphere)}.Radius");
                writer.WriteNextCol(nameof(Gcmf.TextureConfigsCount));
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
                    int modelIndex = 0;
                    foreach (var model in gma.Models)
                    {
                        var gcmf = model.Gcmf;
                        writer.WriteNextCol(gma.FileName);
                        writer.WriteNextCol(gcmf.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(model.Name);
                        writer.WriteNextCol(modelIndex++);
                        writer.WriteNextCol(model.DebugIndex);
                        writer.WriteNextCol(gcmf.Attributes);
                        writer.WriteNextCol(gcmf.BoundingSphere.origin);
                        writer.WriteNextCol(gcmf.BoundingSphere.radius);
                        writer.WriteNextCol(gcmf.TextureConfigsCount);
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
                writer.Flush();
            }
        }

        public static void PrintTextureConfigs(Gma[] gmas, string outputFileName)
        {
            using (var writer = new StreamWriter(File.Create(outputFileName)))
            {
                // Write header
                writer.WriteNextCol("FileName");
                writer.WriteNextCol("Address");
                writer.WriteNextCol(nameof(Model.Name));
                writer.WriteNextCol("Model Index");
                writer.WriteNextCol("Debug Index");
                writer.WriteNextCol("Tex Index");
                writer.WriteNextCol(nameof(TevLayer.Unk0x00));
                writer.WriteNextCol(nameof(TevLayer.MipmapSetting));
                writer.WriteNextCol(nameof(TevLayer.WrapMode));
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
                    int modelIndex = 0;
                    foreach (var model in gma.Models)
                    {
                        int textureConfigIndex = 0;
                        foreach (var textureConfif in model.Gcmf.TevLayers)
                        {
                            writer.WriteNextCol(gma.FileName);
                            writer.WriteNextCol(textureConfif.AddressRange.PrintStartAddress());
                            writer.WriteNextCol(model.Name);
                            writer.WriteNextCol(modelIndex);
                            writer.WriteNextCol(model.DebugIndex);
                            writer.WriteNextCol(textureConfigIndex++);
                            writer.WriteNextCol(textureConfif.Unk0x00);
                            writer.WriteNextCol(textureConfif.MipmapSetting);
                            writer.WriteNextCol(textureConfif.WrapMode);
                            writer.WriteNextCol(textureConfif.TplTextureIndex);
                            writer.WriteNextCol(textureConfif.LodBias);
                            writer.WriteNextCol(textureConfif.AnisotropicFilter);
                            writer.WriteNextCol(textureConfif.Unk0x0C);
                            writer.WriteNextCol(textureConfif.IsSwappableTexture);
                            writer.WriteNextCol(textureConfif.TevLayerIndex);
                            writer.WriteNextCol(textureConfif.Unk0x12);
                            writer.WriteNextRow();
                        }
                        modelIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void PrintMaterials(Gma[] gmas, string outputFileName)
        {
            using (var writer = new StreamWriter(File.Create(outputFileName)))
            {
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
                writer.WriteNextCol(nameof(UnkAlphaOptions.Unk0x10));
                writer.WriteNextRow();

                foreach (var gma in gmas)
                {
                    int modelIndex = 0;
                    foreach (var model in gma.Models)
                    {
                        int submeshIndex = 0;
                        foreach (var submesh in model.Gcmf.Submeshes)
                        {
                            writer.WriteNextCol(gma.FileName);
                            writer.WriteNextCol(submesh.AddressRange.PrintStartAddress());
                            writer.WriteNextCol(model.Name);
                            writer.WriteNextCol(modelIndex);
                            writer.WriteNextCol(model.DebugIndex);
                            writer.WriteNextCol(submeshIndex++);
                            writer.WriteNextCol(submesh.RenderFlags);
                            writer.WriteNextCol(submesh.Material.MaterialColor);
                            writer.WriteNextCol(submesh.Material.AmbientColor);
                            writer.WriteNextCol(submesh.Material.SpecularColor);
                            writer.WriteNextCol(submesh.Material.Unk0x10);
                            writer.WriteNextCol(submesh.Material.Alpha);
                            writer.WriteNextCol(submesh.Material.TevLayerCount);
                            writer.WriteNextCol(submesh.Material.MaterialDestination);
                            writer.WriteNextCol(submesh.Material.UnkAlpha0x14);
                            writer.WriteNextCol(submesh.Material.Unk0x15);
                            writer.WriteNextCol(submesh.Material.TevLayerIndex0);
                            writer.WriteNextCol(submesh.Material.TevLayerIndex1);
                            writer.WriteNextCol(submesh.Material.TevLayerIndex2);
                            writer.WriteNextCol(submesh.VertexAttributes);
                            writer.WriteNextCol(submesh.UnkAlphaOptions.Origin);
                            writer.WriteNextCol(submesh.UnkAlphaOptions.Unk0x0C);
                            writer.WriteNextCol(submesh.UnkAlphaOptions.Unk0x10);
                            writer.WriteNextRow();
                        }
                        modelIndex++;
                    }
                }
                writer.Flush();
            }
        }

    }
}
