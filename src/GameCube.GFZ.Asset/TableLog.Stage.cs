using GameCube.Common;
using GameCube.GFZ.GameData;
using GameCube.GFZ.Stage;
using Manifold;
using Manifold.IO;
using System.Text.RegularExpressions;
using static Manifold.IO.TableLogger;

namespace GameCube.GFZ.Asset;

/// <summary>
///     Library of log functions for <see cref="Scene"/> (stage) files.
/// </summary>
public static class StageTableLogger
{
    // Map functions to output file name
    public static readonly LogFuncFile<SceneFile> LogCullOverrideTrigger = new(AnalyzeCullOverrideTrigger, $"{nameof(CullOverrideTrigger)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogHeader = new(AnalyzeHeaders, $"{nameof(Scene)}-Header.tsv");
    public static readonly LogFuncFile<SceneFile> LogFog = new(AnalyzeFog, $"{nameof(Fog)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogFogCurves = new(AnalyzeFogCurves, $"{nameof(FogCurves)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogGeneralData = new(AnalyzeGeneralData, $"General Data.tsv");
    public static readonly LogFuncFile<SceneFile> LogMiscellaneousTrigger = new(AnalyzeMiscellaneousTriggers, $"{nameof(MiscellaneousTrigger)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSceneObjectDynamic = new(AnalyzeSceneObjectDynamic, $"{nameof(SceneObjectDynamic)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSceneObjectLODs = new(AnalyzeSceneObjectLODs, $"{nameof(SceneObjectLOD)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSceneObjects = new(AnalyzeSceneObjects, $"{nameof(SceneObject)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSceneObjectsAndLod = new(AnalyzeSceneObjectsAndLODs, $"{nameof(SceneObject)}-{nameof(SceneObjectLOD)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSodAnimationClip = new(AnalyzeAnimationClips, $"{nameof(SceneObjectDynamic)}-{nameof(AnimationClip)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSodTextureMetadata = new(AnalyzeTextureMetadata, $"{nameof(SceneObjectDynamic)}-{nameof(TextureScroll)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSodSkeletalAnimator = new(AnalyzeSkeletalAnimator, $"{nameof(SceneObjectDynamic)}-{nameof(SkeletalAnimator)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSodGeometryTri = new(AnalyzeColliderGeometryTri, $"{nameof(SceneObjectDynamic)}-{nameof(ColliderMesh)}-Tris.tsv");
    public static readonly LogFuncFile<SceneFile> LogSodGeometryQuad = new(AnalyzeColliderGeometryQuad, $"{nameof(SceneObjectDynamic)}-{nameof(ColliderMesh)}-Quads.tsv");
    public static readonly LogFuncFile<SceneFile> LogStaticColliderMeshManager = new(AnalyzeStaticColliderMeshManagers, $"{nameof(StaticColliderMeshManager)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogStaticQuads = new(AnalyzeStaticColliderQuads, $"{nameof(StaticColliderMeshManager)}-{nameof(ColliderQuad)}s.tsv");
    public static readonly LogFuncFile<SceneFile> LogStaticTriangles = new(AnalyzeStaticColliderTriangles, $"{nameof(StaticColliderMeshManager)}-{nameof(ColliderTriangle)}s.tsv");
    public static readonly LogFuncFile<SceneFile> LogStoryObjectTrigger = new(AnalyzeStoryObjectTrigger, $"{nameof(StoryObjectTrigger)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogSurfaceAttributeArea = new(AnalyzeSurfaceAttributeAreas, $"{nameof(EmbeddedTrackPropertyArea)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogTimeExtensionTrigger = new(AnalyzeTimeExtensionTriggers, $"{nameof(TimeExtensionTrigger)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogTrackKeyablesAll = new(AnalyzeTrackKeyablesAll, $"Track Keyables All.tsv");
    public static readonly LogFuncFile<SceneFile> LogTrackNode = new(AnalyzeTrackNodes, $"{nameof(TrackNode)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogTrackSegment = new(AnalyzeTrackSegments, $"{nameof(TrackSegment)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogTransform = new(AnalyzeSceneObjectTransforms, $"{nameof(TransformTRXS)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogUnknownCollider = new(AnalyzeUnknownColliders, $"{nameof(UnknownCollider)}.tsv");
    public static readonly LogFuncFile<SceneFile> LogVisualEffectTrigger = new(AnalyzeVisualEffectTriggers, $"{nameof(VisualEffectTrigger)}.tsv");

    public static readonly LogFuncFile<SceneFile>[] AllLogFunctionFiles =
    [
        LogCullOverrideTrigger,
        LogHeader,
        LogFog,
        LogFogCurves,
        LogGeneralData,
        LogMiscellaneousTrigger,
        LogSceneObjectDynamic,
        LogSceneObjectLODs,
        LogSceneObjects,
        LogSceneObjectsAndLod,
        LogSodAnimationClip,
        LogSodTextureMetadata,
        LogSodSkeletalAnimator,
        LogSodGeometryTri,
        LogSodGeometryQuad,
        LogStaticColliderMeshManager,
        LogStaticQuads,
        LogStaticTriangles,
        LogStoryObjectTrigger,
        LogSurfaceAttributeArea,
        LogTimeExtensionTrigger,
        LogTrackKeyablesAll,
        LogTrackNode,
        LogTrackSegment,
        LogTransform,
        LogUnknownCollider,
        LogVisualEffectTrigger
    ];

    #region Track Data / Transforms

    public static void AnalyzeTrackKeyablesAll(SceneFile[] sceneFiles, string filename)
    {
        using var writer = new StreamWriter(File.Create(filename));

        // Write header
        writer.WriteNextCol("FileName");
        writer.WriteNextCol("Game");

        writer.WriteNextCol(nameof(TrackSegment.SegmentType));
        writer.WriteNextCol(nameof(TrackSegment.EmbeddedPropertyType));
        writer.WriteNextCol(nameof(TrackSegment.PerimeterFlags));
        writer.WriteNextCol(nameof(TrackSegment.PipeCylinderFlags));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x38));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x3A));

        writer.WriteNextCol("TrackTransform Index");
        writer.WriteNextCol("Keyable /9");
        writer.WriteNextCol("Keyable Index");
        writer.WriteNextCol("Keyable Order");
        writer.WriteNextCol("Nested Depth");
        writer.WriteNextCol("Address");
        writer.WriteNextCol(nameof(KeyableAttribute.EaseMode));
        writer.WriteNextCol(nameof(KeyableAttribute.EaseMode));
        writer.WriteNextCol(nameof(KeyableAttribute.Time));
        writer.WriteNextCol(nameof(KeyableAttribute.Value));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentIn));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentOut));
        writer.WriteNextRow();

        // foreach File
        foreach (var sceneFile in sceneFiles)
        {
            // foreach Transform
            int trackIndex = 0;
            foreach (var trackTransform in sceneFile.Value.RootTrackSegments)
            {
                for (int keyablesIndex = 0; keyablesIndex < AnimationCurveTRS.kCurveCount; keyablesIndex++)
                {
                    WriteTrackKeyableAttributeRecursive(writer, sceneFile, 0, keyablesIndex, ++trackIndex, trackTransform);
                }
            }
        }

        writer.Flush();
    }
    public static void AnalyzeTrackKeyables(SceneFile[] sceneFiles, string filename, int keyablesSet)
    {
        using var writer = new StreamWriter(File.Create(filename));

        // Write header
        writer.WriteNextCol("FileName");
        writer.WriteNextCol("Game");

        writer.WriteNextCol(nameof(TrackSegment.SegmentType));
        writer.WriteNextCol(nameof(TrackSegment.EmbeddedPropertyType));
        writer.WriteNextCol(nameof(TrackSegment.PerimeterFlags));
        writer.WriteNextCol(nameof(TrackSegment.PipeCylinderFlags));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x38));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x3A));

        writer.WriteNextCol("TrackTransform Index");
        writer.WriteNextCol("Keyable /9");
        writer.WriteNextCol("Keyable Index");
        writer.WriteNextCol("Keyable Order");
        writer.WriteNextCol("Nested Depth");
        writer.WriteNextCol("Address");
        writer.WriteNextCol(nameof(KeyableAttribute.EaseMode));
        writer.WriteNextCol(nameof(KeyableAttribute.EaseMode));
        writer.WriteNextCol(nameof(KeyableAttribute.Time));
        writer.WriteNextCol(nameof(KeyableAttribute.Value));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentIn));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentOut));
        writer.WriteNextRow();

        // foreach File
        foreach (var sceneFile in sceneFiles)
        {
            // foreach Transform
            int trackTransformIndex = 0;
            foreach (var trackTransform in sceneFile.Value.RootTrackSegments)
            {
                WriteTrackKeyableAttributeRecursive(writer, sceneFile, nestedDepth: 0, keyablesSet, trackTransformIndex++, trackTransform);
            }
        }

        writer.Flush();
    }
    private static void WriteTrackKeyableAttributeRecursive(StreamWriter writer, SceneFile sceneFile, int nestedDepth, int animationCurveIndex, int trackTransformIndex, TrackSegment trackTransform)
    {
        var animationCurves = trackTransform.AnimationCurveTRS.AnimationCurves;
        var keyableIndex = 1; // 0-n, depends on number of keyables in array
        int keyableTotal = animationCurves[animationCurveIndex].Length;

        // Animation data of this curve
        foreach (var keyables in animationCurves[animationCurveIndex].KeyableAttributes)
        {
            WriteKeyableAttribute(writer, sceneFile, nestedDepth + 1, keyableIndex++, keyableTotal, animationCurveIndex, trackTransformIndex, keyables, trackTransform);
        }

        // TODO: do you even care to reimplement this at this point?
        // Go to track transform children, write their anim data (calls this function)
        //Debug.LogWarning("You refactored this analysis out!");
        //foreach (var child in trackTransform.children)
        //    WriteTrackKeyableAttributeRecursive(writer, sobj, nestedDepth + 1, animationCurveIndex, trackTransformIndex, child);
    }
    private static void WriteKeyableAttribute(StreamWriter writer, SceneFile sceneFile, int nestedDepth, int keyableIndex, int keyableTotal, int keyablesSet, int trackTransformIndex, KeyableAttribute param, TrackSegment tt)
    {
        writer.WriteNextCol(sceneFile.FileName);
        writer.WriteNextCol(sceneFile.FileFormatDescription);

        writer.WriteNextCol(tt.SegmentType);
        writer.WriteNextCol(tt.EmbeddedPropertyType);
        writer.WriteNextCol(tt.PerimeterFlags);
        writer.WriteNextCol(tt.PipeCylinderFlags);
        writer.WriteNextCol(tt.Root_unk_0x38);
        writer.WriteNextCol(tt.Root_unk_0x3A);

        writer.WriteNextCol(trackTransformIndex);
        writer.WriteNextCol(keyablesSet);
        writer.WriteNextCol(keyableIndex);
        writer.WriteNextCol($"[{keyableIndex}/{keyableTotal}]");
        writer.WriteNextCol($"{nestedDepth}");
        writer.WriteNextCol(param.AddressRange.PrintStartAddress());
        writer.WriteNextCol(param.EaseMode);
        writer.WriteNextCol((int)param.EaseMode);
        writer.WriteNextCol(param.Time);
        writer.WriteNextCol(param.Value);
        writer.WriteNextCol(param.TangentIn);
        writer.WriteNextCol(param.TangentOut);
        writer.WriteNextRow();
    }


    // Kicks off recursive write
    private static int s_order;
    public static void AnalyzeTrackSegments(SceneFile[] sceneFiles, string filename)
    {
        using var writer = new StreamWriter(File.Create(filename));
        //
        writer.WriteNextCol("Filename");
        writer.WriteNextCol("Order");
        writer.WriteNextCol("Root Index");
        writer.WriteNextCol("Transform Depth");
        writer.WriteNextCol("Address");
        //
        writer.WriteNextCol("PosX");
        writer.WriteNextCol("PosY");
        writer.WriteNextCol("PosZ");
        writer.WriteNextCol("RotX");
        writer.WriteNextCol("RotY");
        writer.WriteNextCol("RotZ");
        writer.WriteNextCol("SclX");
        writer.WriteNextCol("SclY");
        writer.WriteNextCol("SclZ");
        //
        writer.WriteNextCol(nameof(TrackSegment.SegmentType));
        writer.WriteNextCol(nameof(TrackSegment.EmbeddedPropertyType));
        writer.WriteNextCol(nameof(TrackSegment.PerimeterFlags));
        writer.WriteNextCol(nameof(TrackSegment.PipeCylinderFlags));
        writer.WriteNextCol(nameof(TrackSegment.AnimationCurvesTrsPtr));
        writer.WriteNextCol(nameof(TrackSegment.TrackCornerPtr));
        writer.WriteNextCol(nameof(TrackSegment.ChildrenPtr));
        writer.WriteNextCol(nameof(TrackSegment.FallbackScale));
        writer.WriteNextCol(nameof(TrackSegment.FallbackRotation));
        writer.WriteNextCol(nameof(TrackSegment.FallbackPosition));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x38));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x38));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x3A));
        writer.WriteNextCol(nameof(TrackSegment.Root_unk_0x3A));
        writer.WriteNextCol(nameof(TrackSegment.RailHeightRight));
        writer.WriteNextCol(nameof(TrackSegment.RailHeightLeft));
        writer.WriteNextCol(nameof(TrackSegment.BranchIndex));
        writer.WriteNextCol();
        writer.WriteNextColNicify(nameof(TrackCorner.Transform.Position));
        writer.WriteNextColNicify(nameof(TrackCorner.Transform.Rotation));
        writer.WriteNextColNicify(nameof(TrackCorner.Transform.Scale));
        writer.WriteNextColNicify(nameof(TrackCorner.Width));
        writer.WriteNextColNicify(nameof(TrackCorner.PerimeterOptions));
        //
        writer.WriteNextRow();

        // RESET static variable
        s_order = 0;

        foreach (var sceneFile in sceneFiles)
        {
            var index = 0;
            var total = sceneFile.Value.RootTrackSegments.Length;
            foreach (var trackTransform in sceneFile.Value.RootTrackSegments)
            {
                WriteTrackSegmentRecursive(writer, sceneFile, 0, ++index, total, trackTransform);
            }
        }

        writer.Flush();
    }
    // Writes self and children
    private static void WriteTrackSegmentRecursive(StreamWriter writer, SceneFile sceneFile, int depth, int index, int total, TrackSegment trackSegment)
    {
        // Write Parent
        WriteTrackSegment(writer, sceneFile, depth, index, total, trackSegment);

        // Write children
        if (trackSegment.Children is null)
            return;

        foreach (var child in trackSegment.Children)
        {
            WriteTrackSegmentRecursive(writer, sceneFile, depth + 1, index, total, child);
        }
    }
    // The actual writing to file
    private static void WriteTrackSegment(StreamWriter writer, SceneFile sceneFile, int depth, int index, int total, TrackSegment trackTransform)
    {
        writer.WriteNextCol(sceneFile.FileName);
        writer.WriteNextCol($"{s_order++}");
        writer.WriteNextCol($"[{index}/{total}]");
        writer.WriteNextCol($"{depth}");
        writer.WriteNextCol(trackTransform.AddressRange.PrintStartAddress());
        //
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.PositionX.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.PositionY.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.PositionZ.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.RotationX.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.RotationY.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.RotationZ.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.ScaleX.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.ScaleY.Length);
        writer.WriteNextCol(trackTransform.AnimationCurveTRS.ScaleZ.Length);
        //
        writer.WriteNextCol(trackTransform.SegmentType);
        writer.WriteNextCol(trackTransform.EmbeddedPropertyType);
        writer.WriteNextCol(trackTransform.PerimeterFlags);
        writer.WriteNextCol(trackTransform.PipeCylinderFlags);
        writer.WriteNextCol(trackTransform.AnimationCurvesTrsPtr);
        writer.WriteNextCol(trackTransform.TrackCornerPtr);
        writer.WriteNextCol(trackTransform.ChildrenPtr);
        writer.WriteNextCol(trackTransform.FallbackScale);
        writer.WriteNextCol(trackTransform.FallbackRotation);
        writer.WriteNextCol(trackTransform.FallbackPosition);
        writer.WriteNextCol(trackTransform.Root_unk_0x38);
        writer.WriteNextCol($"0x{trackTransform.Root_unk_0x38:x4}");
        writer.WriteNextCol(trackTransform.Root_unk_0x3A);
        writer.WriteNextCol($"0x{trackTransform.Root_unk_0x3A:x4}");
        writer.WriteNextCol(trackTransform.RailHeightRight);
        writer.WriteNextCol(trackTransform.RailHeightLeft);
        writer.WriteNextCol(trackTransform.BranchIndex);
        //
        if (trackTransform.TrackCornerPtr.IsNotNull)
        {
            writer.WriteNextCol();
            writer.WriteNextCol(trackTransform.TrackCorner.Transform.Position);
            writer.WriteNextCol(trackTransform.TrackCorner.Transform.RotationEuler);
            writer.WriteNextCol(trackTransform.TrackCorner.Transform.Scale);
            writer.WriteNextCol(trackTransform.TrackCorner.Width);
            writer.WriteNextCol(trackTransform.TrackCorner.PerimeterOptions);
        }
        //
        writer.WriteNextRow();
    }


    #endregion

    #region Scene Objects' Animation Clips

    public static void AnalyzeAnimationClips(SceneFile[] sceneFiles, string filename)
    {
        using var writer = new StreamWriter(File.Create(filename));

        // Write header
        writer.WriteNextCol("File Path");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");
        writer.WriteNextCol("Anim Addr");
        writer.WriteNextCol("Key Addr");
        writer.WriteNextCol("Anim Index [0-10]");
        writer.WriteNextCol("Key");
        writer.WriteNextCol(nameof(KeyableAttribute.EaseMode));
        writer.WriteNextCol(nameof(KeyableAttribute.Time));
        writer.WriteNextCol(nameof(KeyableAttribute.Value));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentIn));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentOut));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var dynamicSceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                if (dynamicSceneObject.Value.AnimationClip is null)
                    continue;
                if (dynamicSceneObject.Value.AnimationClip.Curves is null)
                    continue;

                foreach (var animationClipCurve in dynamicSceneObject.Value.AnimationClip.Curves.Iterate())
                {
                    if (animationClipCurve.Value.AnimationCurve is null)
                        continue;

                    foreach (var keyable in animationClipCurve.Value.AnimationCurve.KeyableAttributes.Iterate(out int keysCount))
                    {
                        writer.WriteNextCol(sceneFile.FileName);
                        writer.WriteNextCol(sceneFile.CourseName);
                        writer.WriteNextCol(dynamicSceneObject.Index);
                        writer.WriteNextCol(dynamicSceneObject.Value.Name);
                        writer.WriteNextCol(animationClipCurve.Value.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(keyable.Value.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(animationClipCurve.Index);
                        writer.WriteNextCol($"[{keyable.Index + 1}/{keysCount}]");
                        writer.WriteNextCol(keyable.Value.EaseMode);
                        writer.WriteNextCol(keyable.Value.Time);
                        writer.WriteNextCol(keyable.Value.Value);
                        writer.WriteNextCol(keyable.Value.TangentIn);
                        writer.WriteNextCol(keyable.Value.TangentOut);
                        writer.WriteNextRow();
                    }
                }
            }
        }
        writer.Flush();
    }

    public static void AnalyzeGameObjectAnimationClipIndex(SceneFile[] sceneFiles, string filename, int index)
    {
        using var writer = new StreamWriter(File.Create(filename));

        // Write header
        writer.WriteNextCol("File Path");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");
        writer.WriteNextCol("Anim Addr");
        writer.WriteNextCol("Key Addr");
        writer.WriteNextColNicify(nameof(AnimationClipCurve.Unk_0x00));
        writer.WriteNextColNicify(nameof(AnimationClipCurve.Unk_0x04));
        writer.WriteNextColNicify(nameof(AnimationClipCurve.Unk_0x08));
        writer.WriteNextColNicify(nameof(AnimationClipCurve.Unk_0x0C));
        writer.WriteNextCol("AnimClip Metadata");
        writer.WriteNextCol("AnimClip Metadata");
        writer.WriteNextCol("AnimClip Metadata");
        writer.WriteNextCol("Anim Index [0-10]");
        writer.WriteNextColNicify(nameof(KeyableAttribute.EaseMode));
        writer.WriteNextColNicify(nameof(KeyableAttribute.Time));
        writer.WriteNextColNicify(nameof(KeyableAttribute.Value));
        writer.WriteNextColNicify(nameof(KeyableAttribute.TangentIn));
        writer.WriteNextColNicify(nameof(KeyableAttribute.TangentOut));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var dynamicSceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                if (dynamicSceneObject.Value.AnimationClip is null)
                    continue;
                //if (dynamicSceneObject.Value.AnimationClip.Curves is null)
                //    continue;

                foreach (var animationClipCurve in dynamicSceneObject.Value.AnimationClip.Curves.Iterate())
                {
                    if (animationClipCurve.Value.AnimationCurve is null)
                        continue;

                    foreach (var keyable in animationClipCurve.Value.AnimationCurve.KeyableAttributes.Iterate())
                    {
                        /// HACK, write each anim index as separate file
                        //if (animIndex != index)
                        //    continue;

                        writer.WriteNextCol(sceneFile.FileName);
                        writer.WriteNextCol(dynamicSceneObject.Index);
                        writer.WriteNextCol(dynamicSceneObject.Value.Name);
                        writer.WriteNextCol(animationClipCurve.Value.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(animationClipCurve.Value.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(animationClipCurve.Value.Unk_0x00);
                        writer.WriteNextCol(animationClipCurve.Value.Unk_0x04);
                        writer.WriteNextCol(animationClipCurve.Value.Unk_0x08);
                        writer.WriteNextCol(animationClipCurve.Value.Unk_0x0C);
                        writer.WriteNextCol(keyable.Index);
                        writer.WriteNextCol(keyable.Value.EaseMode);
                        writer.WriteNextCol(keyable.Value.Time);
                        writer.WriteNextCol(keyable.Value.Value);
                        writer.WriteNextCol(keyable.Value.TangentIn);
                        writer.WriteNextCol(keyable.Value.TangentOut);
                        writer.WriteNextRow();
                    }
                }
            }
        }
        writer.Flush();
    }

    #endregion

    #region Dynamic Scene Objects

    public static void AnalyzeSceneObjectDynamic(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");
        writer.WriteNextCol("Addr");
        writer.WriteNextCol(nameof(SceneObjectDynamic.ObjectRenderFlags0x00));
        writer.WriteNextCol(nameof(SceneObjectDynamic.ObjectRenderFlags0x00));
        writer.WriteNextCol(nameof(SceneObjectDynamic.ObjectRenderFlags0x04));
        writer.WriteNextCol(nameof(SceneObjectDynamic.ObjectRenderFlags0x04));
        writer.WriteNextCol(nameof(SceneObjectDynamic.SceneObjectPtr));
        writer.WriteNextCol(nameof(SceneObjectDynamic.TransformTRXS.Position));
        writer.WriteNextCol(nameof(SceneObjectDynamic.TransformTRXS.RotationEuler));
        writer.WriteNextCol(nameof(SceneObjectDynamic.TransformTRXS.Scale));
        //writer.WriteNextCol(nameof(SceneObjectDynamic.zero_0x2C));
        writer.WriteNextCol(nameof(SceneObjectDynamic.AnimationClipPtr));
        writer.WriteNextCol(nameof(SceneObjectDynamic.TextureScrollPtr));
        writer.WriteNextCol(nameof(SceneObjectDynamic.SkeletalAnimatorPtr));
        writer.WriteNextCol(nameof(SceneObjectDynamic.TransformMatrix3x4Ptr));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var dynamicSceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(dynamicSceneObject.Index);
                writer.WriteNextCol(dynamicSceneObject.Value.Name);
                writer.WriteNextCol(dynamicSceneObject.Value.AddressRange.PrintStartAddress());
                writer.WriteNextCol(dynamicSceneObject.Value.ObjectRenderFlags0x00);
                writer.WriteNextCol($"0x{(uint)dynamicSceneObject.Value.ObjectRenderFlags0x00:x8}");
                writer.WriteNextCol(dynamicSceneObject.Value.ObjectRenderFlags0x04);
                writer.WriteNextCol($"0x{(uint)dynamicSceneObject.Value.ObjectRenderFlags0x04:x8}");
                writer.WriteNextCol(dynamicSceneObject.Value.SceneObjectPtr.PrintAddress);
                writer.WriteNextCol(dynamicSceneObject.Value.TransformTRXS.Position);
                writer.WriteNextCol(dynamicSceneObject.Value.TransformTRXS.RotationEuler);
                writer.WriteNextCol(dynamicSceneObject.Value.TransformTRXS.Scale);
                //writer.WriteNextCol(sceneObject.zero_0x2C);
                writer.WriteNextCol(dynamicSceneObject.Value.AnimationClipPtr.PrintAddress);
                writer.WriteNextCol(dynamicSceneObject.Value.TextureScrollPtr.PrintAddress);
                writer.WriteNextCol(dynamicSceneObject.Value.SkeletalAnimatorPtr.PrintAddress);
                writer.WriteNextCol(dynamicSceneObject.Value.TransformMatrix3x4Ptr.PrintAddress);
                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }

    public static void AnalyzeTextureMetadata(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");
        writer.WriteNextCol("Unknown 1 Index");
        writer.WriteNextColNicify(nameof(TextureScrollField.u));
        writer.WriteNextColNicify(nameof(TextureScrollField.v));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var dynamicSceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                if (dynamicSceneObject.Value.TextureScroll is null)
                    continue;
                if (dynamicSceneObject.Value.TextureScroll.Fields is null)
                    continue;

                foreach (var field in dynamicSceneObject.Value.TextureScroll.Fields.Iterate())
                {
                    if (field.Value is null)
                        continue;

                    writer.WriteNextCol(sceneFile.FileName);
                    writer.WriteNextCol(dynamicSceneObject.Index);
                    writer.WriteNextCol(dynamicSceneObject.Value.Name);
                    writer.WriteNextCol(field.Index);
                    writer.WriteNextCol(field.Value.u);
                    writer.WriteNextCol(field.Value.v);
                    writer.WriteNextRow();
                }
            }
        }
        writer.Flush();
    }

    public static void AnalyzeSkeletalAnimator(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");

        //writer.WriteNextColNicify(nameof(SkeletalAnimator.zero_0x00));
        //writer.WriteNextColNicify(nameof(SkeletalAnimator.zero_0x04));
        //writer.WriteNextColNicify(nameof(SkeletalAnimator.one_0x08));
        writer.WriteNextColNicify(nameof(SkeletalAnimator.PropertiesPtr));

        writer.WriteNextColNicify(nameof(SkeletalProperties.Unk_0x00));
        writer.WriteNextColNicify(nameof(SkeletalProperties.Unk_0x04));
        writer.WriteFlagNames<EnumFlags32>();
        writer.WriteNextColNicify(nameof(SkeletalProperties.Unk_0x08));
        writer.WriteFlagNames<EnumFlags32>();
        //writer.WriteNextColNicify(nameof(SkeletalProperties.zero_0x0C));
        //writer.WriteNextColNicify(nameof(SkeletalProperties.zero_0x10));
        //writer.WriteNextColNicify(nameof(SkeletalProperties.zero_0x14));
        //writer.WriteNextColNicify(nameof(SkeletalProperties.zero_0x18));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var dynamicSceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                if (dynamicSceneObject.Value.SkeletalAnimator is null)
                    continue;
                if (dynamicSceneObject.Value.SkeletalAnimator.Properties is null)
                    continue;

                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(dynamicSceneObject.Index);
                writer.WriteNextCol(dynamicSceneObject.Value.Name);

                //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.zero_0x00);
                //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.zero_0x04);
                //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.one_0x08);
                writer.WriteNextCol(dynamicSceneObject.Value.SkeletalAnimator.PropertiesPtr);
                writer.WriteNextCol(dynamicSceneObject.Value.SkeletalAnimator.Properties.Unk_0x00);
                writer.WriteNextCol(dynamicSceneObject.Value.SkeletalAnimator.Properties.Unk_0x04);
                writer.WriteFlags(dynamicSceneObject.Value.SkeletalAnimator.Properties.Unk_0x04);
                writer.WriteNextCol(dynamicSceneObject.Value.SkeletalAnimator.Properties.Unk_0x08);
                writer.WriteFlags(dynamicSceneObject.Value.SkeletalAnimator.Properties.Unk_0x08);
                //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x0C);
                //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x10);
                //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x14);
                //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x18);
                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }

    public static void AnalyzeColliderGeometryTri(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        writer.WriteNextCol("File");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");
        // Where
        writer.WriteNextCol("Tri Index");
        writer.WriteNextCol("Addr");
        // Tri Data
        writer.WriteNextColNicify(nameof(ColliderTriangle.PlaneDistance));
        writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".Z");
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var dynamicSceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                if (dynamicSceneObject.Value.SceneObject.ColliderMesh is null ||
                    dynamicSceneObject.Value.SceneObject.ColliderMesh.Tris is null ||
                    dynamicSceneObject.Value.SceneObject.ColliderMesh.Tris.Length == 0)
                    continue;

                foreach (var tri in dynamicSceneObject.Value.SceneObject.ColliderMesh.Tris.Iterate())
                {
                    writer.WriteNextCol(sceneFile.FileName);
                    writer.WriteNextCol(dynamicSceneObject.Index);
                    writer.WriteNextCol(dynamicSceneObject.Value.Name);
                    // 
                    writer.WriteNextCol(tri.Index);
                    writer.WriteStartAddress(tri.Value);
                    // 
                    writer.WriteNextCol(tri.Value.PlaneDistance);
                    writer.WriteNextCol(tri.Value.Normal.X);
                    writer.WriteNextCol(tri.Value.Normal.Y);
                    writer.WriteNextCol(tri.Value.Normal.Z);
                    writer.WriteNextCol(tri.Value.Vertex0.X);
                    writer.WriteNextCol(tri.Value.Vertex0.Y);
                    writer.WriteNextCol(tri.Value.Vertex0.Z);
                    writer.WriteNextCol(tri.Value.Vertex1.X);
                    writer.WriteNextCol(tri.Value.Vertex1.Y);
                    writer.WriteNextCol(tri.Value.Vertex1.Z);
                    writer.WriteNextCol(tri.Value.Vertex2.X);
                    writer.WriteNextCol(tri.Value.Vertex2.Y);
                    writer.WriteNextCol(tri.Value.Vertex2.Z);
                    writer.WriteNextCol(tri.Value.EdgeNormal0.X);
                    writer.WriteNextCol(tri.Value.EdgeNormal0.Y);
                    writer.WriteNextCol(tri.Value.EdgeNormal0.Z);
                    writer.WriteNextCol(tri.Value.EdgeNormal1.X);
                    writer.WriteNextCol(tri.Value.EdgeNormal1.Y);
                    writer.WriteNextCol(tri.Value.EdgeNormal1.Z);
                    writer.WriteNextCol(tri.Value.EdgeNormal2.X);
                    writer.WriteNextCol(tri.Value.EdgeNormal2.Y);
                    writer.WriteNextCol(tri.Value.EdgeNormal2.Z);
                    writer.WriteNextRow();
                }
            }
        }
        writer.Flush();
    }

    public static void AnalyzeColliderGeometryQuad(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");
        //
        writer.WriteNextCol("Quad Index");
        writer.WriteNextCol("Addr");
        //
        writer.WriteNextColNicify(nameof(ColliderQuad.PlaneDistance));
        writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".Z");

        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var dynamicSceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                if (dynamicSceneObject.Value.SceneObject.ColliderMesh is null ||
                    dynamicSceneObject.Value.SceneObject.ColliderMesh.Quads is null ||
                    dynamicSceneObject.Value.SceneObject.ColliderMesh.Quads.Length == 0)
                    continue;

                foreach (var quad in dynamicSceneObject.Value.SceneObject.ColliderMesh.Quads.Iterate())
                { 
                    writer.WriteNextCol(sceneFile.FileName);
                    writer.WriteNextCol(dynamicSceneObject.Index);
                    writer.WriteNextCol(dynamicSceneObject.Value.Name);
                    // 
                    writer.WriteNextCol(quad.Index);
                    writer.WriteStartAddress(quad.Value);
                    // 
                    writer.WriteNextCol(quad.Value.PlaneDistance);
                    writer.WriteNextCol(quad.Value.Normal.X);
                    writer.WriteNextCol(quad.Value.Normal.Y);
                    writer.WriteNextCol(quad.Value.Normal.Z);
                    writer.WriteNextCol(quad.Value.Vertex0.X);
                    writer.WriteNextCol(quad.Value.Vertex0.Y);
                    writer.WriteNextCol(quad.Value.Vertex0.Z);
                    writer.WriteNextCol(quad.Value.Vertex1.X);
                    writer.WriteNextCol(quad.Value.Vertex1.Y);
                    writer.WriteNextCol(quad.Value.Vertex1.Z);
                    writer.WriteNextCol(quad.Value.Vertex2.X);
                    writer.WriteNextCol(quad.Value.Vertex2.Y);
                    writer.WriteNextCol(quad.Value.Vertex2.Z);
                    writer.WriteNextCol(quad.Value.Vertex3.X);
                    writer.WriteNextCol(quad.Value.Vertex3.Y);
                    writer.WriteNextCol(quad.Value.Vertex3.Z);
                    writer.WriteNextCol(quad.Value.EdgeNormal0.X);
                    writer.WriteNextCol(quad.Value.EdgeNormal0.Y);
                    writer.WriteNextCol(quad.Value.EdgeNormal0.Z);
                    writer.WriteNextCol(quad.Value.EdgeNormal1.X);
                    writer.WriteNextCol(quad.Value.EdgeNormal1.Y);
                    writer.WriteNextCol(quad.Value.EdgeNormal1.Z);
                    writer.WriteNextCol(quad.Value.EdgeNormal2.X);
                    writer.WriteNextCol(quad.Value.EdgeNormal2.Y);
                    writer.WriteNextCol(quad.Value.EdgeNormal2.Z);
                    writer.WriteNextCol(quad.Value.EdgeNormal3.X);
                    writer.WriteNextCol(quad.Value.EdgeNormal3.Y);
                    writer.WriteNextCol(quad.Value.EdgeNormal3.Z);
                    writer.WriteNextRow();
                }
            }
        }
        writer.Flush();
    }

    #endregion

    public static void AnalyzeHeaders(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        //
        writer.WriteNextCol(nameof(Scene.UnkRange0x00) + "." + nameof(ViewRange.near));
        writer.WriteNextCol(nameof(Scene.UnkRange0x00) + "." + nameof(ViewRange.far));
        writer.WriteNextCol(nameof(Scene.TrackNodesPtr));
        writer.WriteNextCol(nameof(Scene.TrackNodesPtr));
        writer.WriteNextCol(nameof(Scene.EmbeddedTrackPropertyAreasPtr));
        writer.WriteNextCol(nameof(Scene.EmbeddedTrackPropertyAreasPtr));
        writer.WriteNextCol(nameof(Scene.StaticColliderMeshManagerActive));
        writer.WriteNextCol(nameof(Scene.EmbeddedTrackPropertyAreasPtr));
        //writer.WriteNextCol(nameof(Scene.zeroes0x20Ptr));
        writer.WriteNextCol(nameof(Scene.TrackMinHeightPtr));
        //writer.WriteNextCol(nameof(Scene.zeroes0x28));
        writer.WriteNextCol(nameof(Scene.DynamicSceneObjectCount));
        writer.WriteNextCol(nameof(Scene.Unk_sceneObjectCount1));
        writer.WriteNextCol(nameof(Scene.Unk_sceneObjectCount2));
        writer.WriteNextCol(nameof(Scene.DynamicSceneObjectsPtr));
        writer.WriteNextCol(nameof(Scene.UnkBool32_0x58));
        writer.WriteNextCol(nameof(Scene.UnknownCollidersPtr));
        writer.WriteNextCol(nameof(Scene.UnknownCollidersPtr));
        writer.WriteNextCol(nameof(Scene.SceneObjectsPtr));
        writer.WriteNextCol(nameof(Scene.SceneObjectsPtr));
        writer.WriteNextCol(nameof(Scene.StaticSceneObjectsPtr));
        writer.WriteNextCol(nameof(Scene.StaticSceneObjectsPtr));
        //writer.WriteNextCol(nameof(Scene.zero0x74));
        //writer.WriteNextCol(nameof(Scene.zero0x78));
        writer.WriteNextCol(nameof(Scene.CircuitType));
        writer.WriteNextCol(nameof(Scene.FogCurvesPtr));
        writer.WriteNextCol(nameof(Scene.FogPtr));
        //writer.WriteNextCol(nameof(Scene.zero0x88));
        //writer.WriteNextCol(nameof(Scene.zero0x8C));
        writer.WriteNextCol(nameof(Scene.TrackLengthPtr));
        writer.WriteNextCol(nameof(Scene.UnknownTriggersPtr)); // len
        writer.WriteNextCol(nameof(Scene.UnknownTriggersPtr)); // adr
        writer.WriteNextCol(nameof(Scene.VisualEffectTriggersPtr)); // len
        writer.WriteNextCol(nameof(Scene.VisualEffectTriggersPtr)); // adr
        writer.WriteNextCol(nameof(Scene.MiscellaneousTriggersPtr)); // len
        writer.WriteNextCol(nameof(Scene.MiscellaneousTriggersPtr)); // adr
        writer.WriteNextCol(nameof(Scene.TimeExtensionTriggersPtr)); // len
        writer.WriteNextCol(nameof(Scene.TimeExtensionTriggersPtr)); // adr
        writer.WriteNextCol(nameof(Scene.StoryObjectTriggersPtr)); // len
        writer.WriteNextCol(nameof(Scene.StoryObjectTriggersPtr)); // adr
        writer.WriteNextCol(nameof(Scene.CheckpointGridPtr));
        // Structure
        writer.WriteNextCol(nameof(Scene.CheckpointGridXZ) + "." + nameof(Scene.CheckpointGridXZ.Left));
        writer.WriteNextCol(nameof(Scene.CheckpointGridXZ) + "." + nameof(Scene.CheckpointGridXZ.Top));
        writer.WriteNextCol(nameof(Scene.CheckpointGridXZ) + "." + nameof(Scene.CheckpointGridXZ.SubdivisionWidth));
        writer.WriteNextCol(nameof(Scene.CheckpointGridXZ) + "." + nameof(Scene.CheckpointGridXZ.SubdivisionLength));
        writer.WriteNextCol(nameof(Scene.CheckpointGridXZ) + "." + nameof(Scene.CheckpointGridXZ.NumSubdivisionsX));
        writer.WriteNextCol(nameof(Scene.CheckpointGridXZ) + "." + nameof(Scene.CheckpointGridXZ.NumSubdivisionsZ));
        // 
        //writer.WriteNextCol(nameof(Scene.zeroes0xD8));
        writer.WriteNextCol(nameof(Scene.trackMinHeight));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            Scene scene = sceneFile;
            writer.WriteNextCol(sceneFile.FileName);
            writer.WriteNextCol(sceneFile.CourseIndex);
            writer.WriteNextCol(sceneFile.VenueName);
            writer.WriteNextCol(sceneFile.CourseName);
            writer.WriteNextCol(sceneFile.FileFormatDescription);
            //
            writer.WriteNextCol(scene.UnkRange0x00.near);
            writer.WriteNextCol(scene.UnkRange0x00.far);
            writer.WriteNextCol(scene.TrackNodesPtr.length);
            writer.WriteNextCol(scene.TrackNodesPtr.PrintAddress);
            writer.WriteNextCol(scene.EmbeddedTrackPropertyAreasPtr.length);
            writer.WriteNextCol(scene.EmbeddedTrackPropertyAreasPtr.PrintAddress);
            writer.WriteNextCol(scene.StaticColliderMeshManagerActive);
            writer.WriteNextCol(scene.StaticColliderMeshManagerPtr.PrintAddress);
            //writer.WriteNextCol(scene.zeroes0x20Ptr.PrintAddress);
            writer.WriteNextCol(scene.TrackMinHeightPtr.PrintAddress);
            //writer.WriteNextCol(0);// coliHeader.zero_0x28);
            writer.WriteNextCol(scene.DynamicSceneObjectCount);
            if (scene.IsFormatGX)
            {
                writer.WriteNextCol(scene.Unk_sceneObjectCount1);
            }
            else // is AX
            {
                writer.WriteNextCol();
            }
            writer.WriteNextCol(scene.Unk_sceneObjectCount2);
            writer.WriteNextCol(scene.DynamicSceneObjectsPtr.PrintAddress);
            writer.WriteNextCol(scene.UnkBool32_0x58);
            writer.WriteNextCol(scene.UnknownCollidersPtr.length);
            writer.WriteNextCol(scene.UnknownCollidersPtr.PrintAddress);
            writer.WriteNextCol(scene.SceneObjectsPtr.length);
            writer.WriteNextCol(scene.SceneObjectsPtr.PrintAddress);
            writer.WriteNextCol(scene.StaticSceneObjectsPtr.length);
            writer.WriteNextCol(scene.StaticSceneObjectsPtr.PrintAddress);
            //writer.WriteNextCol(scene.zero0x74);
            //writer.WriteNextCol(scene.zero0x78);
            writer.WriteNextCol(scene.CircuitType);
            writer.WriteNextCol(scene.FogCurvesPtr.PrintAddress);
            writer.WriteNextCol(scene.FogPtr.PrintAddress);
            //writer.WriteNextCol(scene.zero0x88);
            //writer.WriteNextCol(scene.zero0x8C);
            writer.WriteNextCol(scene.TrackLengthPtr.PrintAddress);
            writer.WriteNextCol(scene.UnknownTriggersPtr.length);
            writer.WriteNextCol(scene.UnknownTriggersPtr.PrintAddress);
            writer.WriteNextCol(scene.VisualEffectTriggersPtr.length);
            writer.WriteNextCol(scene.VisualEffectTriggersPtr.PrintAddress);
            writer.WriteNextCol(scene.MiscellaneousTriggersPtr.length);
            writer.WriteNextCol(scene.MiscellaneousTriggersPtr.PrintAddress);
            writer.WriteNextCol(scene.TimeExtensionTriggersPtr.length);
            writer.WriteNextCol(scene.TimeExtensionTriggersPtr.PrintAddress);
            writer.WriteNextCol(scene.StoryObjectTriggersPtr.length);
            writer.WriteNextCol(scene.StoryObjectTriggersPtr.PrintAddress);
            writer.WriteNextCol(scene.CheckpointGridPtr.PrintAddress);
            // Structure
            writer.WriteNextCol(scene.CheckpointGridXZ.Left);
            writer.WriteNextCol(scene.CheckpointGridXZ.Top);
            writer.WriteNextCol(scene.CheckpointGridXZ.SubdivisionWidth);
            writer.WriteNextCol(scene.CheckpointGridXZ.SubdivisionLength);
            writer.WriteNextCol(scene.CheckpointGridXZ.NumSubdivisionsX);
            writer.WriteNextCol(scene.CheckpointGridXZ.NumSubdivisionsZ);
            //
            //writer.WriteNextCol(0);// coliHeader.zero_0xD8);
            writer.WriteNextCol(scene.trackMinHeight.Value);
            writer.WriteNextRow();
        }
        writer.Flush();
    }


    #region TRIGGERS

    public static void AnalyzeTimeExtensionTriggers(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.Position));
        writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.RotationEuler));
        writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.Scale));
        writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.UnknownOption));
        writer.WriteNextCol(nameof(TimeExtensionTrigger.Option));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var arcadeCheckpoint in sceneFile.Value.timeExtensionTriggers)
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                writer.WriteNextCol(arcadeCheckpoint.Transform.Position);
                writer.WriteNextCol(arcadeCheckpoint.Transform.RotationEuler);
                writer.WriteNextCol(arcadeCheckpoint.Transform.Scale);
                writer.WriteNextCol(arcadeCheckpoint.Transform.UnknownOption);
                writer.WriteNextCol(arcadeCheckpoint.Option);
                writer.WriteNextRow();
            }
            writer.Flush();
        }
    }

    public static void AnalyzeMiscellaneousTriggers(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol(nameof(MiscellaneousTrigger.Position));
        writer.WriteNextCol(nameof(MiscellaneousTrigger.RotationEuler));
        writer.WriteNextCol(nameof(MiscellaneousTrigger.Scale) + " / PositionTo");
        writer.WriteNextCol(nameof(MiscellaneousTrigger.Transform.UnknownOption));
        writer.WriteNextCol(nameof(MiscellaneousTrigger.MetadataType));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var miscellaneousTrigger in sceneFile.Value.miscellaneousTriggers)
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                writer.WriteNextCol(miscellaneousTrigger.Position);
                writer.WriteNextCol(miscellaneousTrigger.RotationEuler);
                writer.WriteNextCol(miscellaneousTrigger.Scale);
                writer.WriteNextCol(miscellaneousTrigger.Transform.UnknownOption);
                writer.WriteNextCol(miscellaneousTrigger.MetadataType);
                writer.WriteNextRow();
            }
            writer.Flush();
        }
    }

    public static void AnalyzeStoryObjectTrigger(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        //writer.WriteNextCol(nameof(StoryObjectTrigger.zero_0x00));
        writer.WriteNextCol(nameof(StoryObjectTrigger.BoulderGroupOrderIndex));
        writer.WriteNextCol(nameof(StoryObjectTrigger.BoulderGroup));
        writer.WriteNextCol(nameof(StoryObjectTrigger.Difficulty));
        writer.WriteNextCol(nameof(StoryObjectTrigger.Story2BoulderScale));
        writer.WriteNextCol(nameof(StoryObjectTrigger.Story2BoulderPathPtr));
        writer.WriteNextCol(nameof(StoryObjectTrigger.Scale));
        writer.WriteNextCol(nameof(StoryObjectTrigger.Rotation));
        writer.WriteNextCol(nameof(StoryObjectTrigger.Position));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var storyObjectTrigger in sceneFile.Value.storyObjectTriggers.Iterate())
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                //writer.WriteNextCol(item.zero_0x00);
                writer.WriteNextCol(storyObjectTrigger.Value.BoulderGroupOrderIndex);
                writer.WriteNextCol(storyObjectTrigger.Value.BoulderGroup);
                writer.WriteNextCol(storyObjectTrigger.Value.Difficulty);
                writer.WriteNextCol(storyObjectTrigger.Value.Story2BoulderScale);
                writer.WriteNextCol(storyObjectTrigger.Value.Story2BoulderPathPtr);
                writer.WriteNextCol(storyObjectTrigger.Value.Scale);
                writer.WriteNextCol(storyObjectTrigger.Value.Rotation);
                writer.WriteNextCol(storyObjectTrigger.Value.Position);
                writer.WriteNextRow();
            }
            writer.Flush();
        }
    }

    public static void AnalyzeCullOverrideTrigger(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol("Addr Start");
        writer.WriteNextCol("Addr End");
        writer.WriteNextCol(nameof(CullOverrideTrigger.Unk_0x20));
        writer.WriteNextCol(nameof(CullOverrideTrigger.Unk_0x20));
        writer.WriteNextCol("Order");
        writer.WriteNextCol("Index");
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var cullOverrideTrigger in sceneFile.Value.cullOverrideTriggers.Iterate(out int total))
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                writer.WriteNextCol(cullOverrideTrigger.Value.AddressRange.PrintStartAddress());
                writer.WriteNextCol(cullOverrideTrigger.Value.AddressRange.PrintEndAddress());
                writer.WriteNextCol(cullOverrideTrigger.Value.Unk_0x20);
                writer.WriteNextCol($"0x{(int)cullOverrideTrigger.Value.Unk_0x20:X8}");
                writer.WriteNextCol(cullOverrideTrigger.Index);
                writer.WriteNextCol($"[{cullOverrideTrigger.Index + 1}/{total}]");
                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }

    public static void AnalyzeVisualEffectTriggers(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.Position));
        writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.RotationEuler));
        writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.Scale));
        writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.UnknownOption));
        writer.WriteNextCol(nameof(VisualEffectTrigger.Animation));
        writer.WriteNextCol(nameof(VisualEffectTrigger.VisualEffect));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var visualEffectTrigger in sceneFile.Value.visualEffectTriggers)
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                writer.WriteNextCol(visualEffectTrigger.Transform.Position);
                writer.WriteNextCol(visualEffectTrigger.Transform.RotationEuler);
                writer.WriteNextCol(visualEffectTrigger.Transform.Scale);
                writer.WriteNextCol(visualEffectTrigger.Transform.UnknownOption);
                writer.WriteNextCol(visualEffectTrigger.Animation);
                writer.WriteNextCol(visualEffectTrigger.VisualEffect);
                writer.WriteNextRow();
            }
            writer.Flush();
        }
    }

    #endregion

    #region FOG

    public static void AnalyzeFogCurves(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol("Addr");
        writer.WriteNextCol("Index");
        writer.WriteNextCol(nameof(KeyableAttribute.EaseMode));
        writer.WriteNextCol(nameof(KeyableAttribute.Time));
        writer.WriteNextCol(nameof(KeyableAttribute.Value));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentIn));
        writer.WriteNextCol(nameof(KeyableAttribute.TangentOut));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            if (sceneFile.Value.fogCurves is null)
                continue;
            if (sceneFile.Value.fogCurves.animationCurves is null)
                continue;

            foreach (var fogAnimationCurve in sceneFile.Value.fogCurves.animationCurves.Iterate(out int count))
            {
                foreach (var keyableAttribute in fogAnimationCurve.Value.KeyableAttributes)
                {
                    writer.WriteNextCol(sceneFile.FileName);
                    writer.WriteNextCol(sceneFile.CourseIndex);
                    writer.WriteNextCol(sceneFile.VenueName);
                    writer.WriteNextCol(sceneFile.CourseName);
                    writer.WriteNextCol(sceneFile.FileFormatDescription);
                    writer.WriteNextCol(keyableAttribute.AddressRange.PrintStartAddress());
                    writer.WriteNextCol($"[{fogAnimationCurve.Index}/{count}]");
                    writer.WriteNextCol(keyableAttribute.EaseMode);
                    writer.WriteNextCol(keyableAttribute.Time);
                    writer.WriteNextCol(keyableAttribute.Value);
                    writer.WriteNextCol(keyableAttribute.TangentIn);
                    writer.WriteNextCol(keyableAttribute.TangentOut);
                    writer.WriteNextRow();
                }
            }
            writer.Flush();
        }
    }

    public static void AnalyzeFog(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol("Addr");
        writer.WriteNextCol(nameof(Fog.Interpolation));
        writer.WriteNextCol(nameof(Fog.FogRange) + "." + nameof(ViewRange.near));
        writer.WriteNextCol(nameof(Fog.FogRange) + "." + nameof(ViewRange.far));
        writer.WriteNextCol(nameof(Fog.ColorRGB) + ".R");
        writer.WriteNextCol(nameof(Fog.ColorRGB) + ".G");
        writer.WriteNextCol(nameof(Fog.ColorRGB) + ".B");
        //writer.WriteNextCol(nameof(Fog.zero0x18) + ".X");
        //writer.WriteNextCol(nameof(Fog.zero0x18) + ".Y");
        //writer.WriteNextCol(nameof(Fog.zero0x18) + ".Z");
        //
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            Scene scene = sceneFile;
            writer.WriteNextCol(sceneFile.FileName);
            writer.WriteNextCol(sceneFile.CourseIndex);
            writer.WriteNextCol(sceneFile.VenueName);
            writer.WriteNextCol(sceneFile.CourseName);
            writer.WriteNextCol(sceneFile.FileFormatDescription);
            writer.WriteNextCol(sceneFile.Value.fog.AddressRange.PrintStartAddress());
            writer.WriteNextCol(sceneFile.Value.fog.Interpolation);
            writer.WriteNextCol(sceneFile.Value.fog.FogRange.near);
            writer.WriteNextCol(sceneFile.Value.fog.FogRange.far);
            writer.WriteNextCol(sceneFile.Value.fog.ColorRGB.X);
            writer.WriteNextCol(sceneFile.Value.fog.ColorRGB.Y);
            writer.WriteNextCol(sceneFile.Value.fog.ColorRGB.Z);
            //writer.WriteNextCol(scene.fog.zero0x18.X);
            //writer.WriteNextCol(scene.fog.zero0x18.Y);
            //writer.WriteNextCol(scene.fog.zero0x18.Z);
            //
            writer.WriteNextRow();
        }
        writer.Flush();
    }

    #endregion


    public static void AnalyzeSceneObjectTransforms(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Game Object #");
        writer.WriteNextCol("Game Object");
        writer.WriteNextCol($"matrix.X");
        writer.WriteNextCol($"matrix.Y");
        writer.WriteNextCol($"matrix.Z");
        writer.WriteNextCol($"euler.X");
        writer.WriteNextCol($"euler.Y");
        writer.WriteNextCol($"euler.Z");
        writer.WriteNextCol("Decomposed phi");
        writer.WriteNextCol("Decomposed theta");
        writer.WriteNextCol("Decomposed psi");
        writer.WriteNextCol(nameof(UnknownTransformOption));
        writer.WriteNextCol(nameof(ObjectActiveOverride));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var sceneObject in sceneFile.Value.dynamicSceneObjects.Iterate())
            {
                // Skip objects that don't have both matrix and decomposed rotation
                // These are not helpful for comparision
                if (sceneObject.Value.TransformMatrix3x4 is null)
                    continue;

                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneObject.Index);
                writer.WriteNextCol(sceneObject.Value.Name);

                // Rotation values from clean, uncompressed matrix
                var matrix = sceneObject.Value.TransformMatrix3x4.RotationEuler;
                writer.WriteNextCol(matrix.X);
                writer.WriteNextCol(matrix.Y);
                writer.WriteNextCol(matrix.Z);

                // Rotation values as reconstructed
                var euler = sceneObject.Value.TransformTRXS.CompressedRotation.Eulers;
                writer.WriteNextCol(euler.X);
                writer.WriteNextCol(euler.Y);
                writer.WriteNextCol(euler.Z);

                // Decomposed rotation values, raw, requires processing to be used
                var decomposed = sceneObject.Value.TransformTRXS.CompressedRotation;
                writer.WriteNextCol(decomposed.X);
                writer.WriteNextCol(decomposed.Y);
                writer.WriteNextCol(decomposed.Z);
                // The other parameters that go with the structure
                writer.WriteNextCol(sceneObject.Value.TransformTRXS.UnknownOption);
                writer.WriteNextCol(sceneObject.Value.TransformTRXS.ObjectActiveOverride);

                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }

    public static void AnalyzeTrackNodes(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Track Node");
        writer.WriteNextCol("Track Point");
        writer.WriteNextColNicify(nameof(Checkpoint.CurveTimeStart));
        writer.WriteNextColNicify(nameof(Checkpoint.CurveTimeEnd));
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.distance));
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.normal) + ".X");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.normal) + ".Y");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.normal) + ".Z");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.origin) + ".X");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.origin) + ".Y");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.origin) + ".Z");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.distance));
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.normal) + ".X");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.normal) + ".Y");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.normal) + ".Z");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.origin) + ".X");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.origin) + ".Y");
        writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.origin) + ".Z");
        writer.WriteNextColNicify(nameof(Checkpoint.StartDistance));
        writer.WriteNextColNicify(nameof(Checkpoint.EndDistance));
        writer.WriteNextColNicify(nameof(Checkpoint.TrackWidth));
        writer.WriteNextColNicify(nameof(Checkpoint.ConnectToTrackIn));
        writer.WriteNextColNicify(nameof(Checkpoint.ConnectToTrackOut));
        //writer.WriteNextColNicify(nameof(Checkpoint.zero_0x4E));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var trackNode in sceneFile.Value.trackNodes.Iterate(out int trackNodesLength))
            {
                foreach (var checkpoint in trackNode.Value.Checkpoints.Iterate(out int checkpointsLength))
                {

                    writer.WriteNextCol($"COLI_COURSE{sceneFile.CourseIndex:d2}");
                    writer.WriteNextCol($"[{trackNode.Index}/{trackNodesLength}]");
                    writer.WriteNextCol($"[{checkpoint.Index}/{checkpointsLength}]");
                    writer.WriteNextCol(checkpoint.Value.CurveTimeStart);
                    writer.WriteNextCol(checkpoint.Value.CurveTimeEnd);
                    writer.WriteNextCol(checkpoint.Value.PlaneStart.distance);
                    writer.WriteNextCol(checkpoint.Value.PlaneStart.normal.X);
                    writer.WriteNextCol(checkpoint.Value.PlaneStart.normal.Y);
                    writer.WriteNextCol(checkpoint.Value.PlaneStart.normal.Z);
                    writer.WriteNextCol(checkpoint.Value.PlaneStart.origin.X);
                    writer.WriteNextCol(checkpoint.Value.PlaneStart.origin.Y);
                    writer.WriteNextCol(checkpoint.Value.PlaneStart.origin.Z);
                    writer.WriteNextCol(checkpoint.Value.PlaneEnd.distance);
                    writer.WriteNextCol(checkpoint.Value.PlaneEnd.normal.X);
                    writer.WriteNextCol(checkpoint.Value.PlaneEnd.normal.Y);
                    writer.WriteNextCol(checkpoint.Value.PlaneEnd.normal.Z);
                    writer.WriteNextCol(checkpoint.Value.PlaneEnd.origin.X);
                    writer.WriteNextCol(checkpoint.Value.PlaneEnd.origin.Y);
                    writer.WriteNextCol(checkpoint.Value.PlaneEnd.origin.Z);
                    writer.WriteNextCol(checkpoint.Value.StartDistance);
                    writer.WriteNextCol(checkpoint.Value.EndDistance);
                    writer.WriteNextCol(checkpoint.Value.TrackWidth);
                    writer.WriteNextCol(checkpoint.Value.ConnectToTrackIn);
                    writer.WriteNextCol(checkpoint.Value.ConnectToTrackOut);
                    //writer.WriteNextCol(trackPoint.zero_0x4E);
                    writer.WriteNextRow();
                }
            }
        }
        writer.Flush();
    }

    public static void AnalyzeStaticColliderMeshManagers(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Addr");
        writer.WriteNextCol("Index");
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.StaticColliderTrisPtr));
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.TriMeshGridPtrs));
        writer.WriteNextColNicify(nameof(GridXZ.Left));
        writer.WriteNextColNicify(nameof(GridXZ.Top));
        writer.WriteNextColNicify(nameof(GridXZ.SubdivisionWidth));
        writer.WriteNextColNicify(nameof(GridXZ.SubdivisionLength));
        writer.WriteNextColNicify(nameof(GridXZ.NumSubdivisionsX));
        writer.WriteNextColNicify(nameof(GridXZ.NumSubdivisionsZ));
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.StaticColliderQuadsPtr));
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.QuadMeshGridPtrs));
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.BoundingSpherePtr));
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.StaticSceneObjectsPtr));
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.UnknownCollidersPtr));
        writer.WriteNextColNicify(nameof(StaticColliderMeshManager.Unk_float));
        writer.WriteNextCol();
        writer.WriteNextColNicify(nameof(BoundingSphere.origin) + ".X");
        writer.WriteNextColNicify(nameof(BoundingSphere.origin) + ".Y");
        writer.WriteNextColNicify(nameof(BoundingSphere.origin) + ".Z");
        writer.WriteNextColNicify(nameof(BoundingSphere.radius));
        writer.WriteNextRow();

        int index = 0;
        foreach (var sceneFile in sceneFiles)
        {
            var staticColliderMeshes = sceneFile.Value.staticColliderMeshManager;
            writer.WriteNextCol($"COLI_COURSE{sceneFile.CourseIndex:d2}");
            writer.WriteNextCol(staticColliderMeshes.AddressRange.PrintStartAddress());
            writer.WriteNextCol(index++);
            writer.WriteNextCol(staticColliderMeshes.StaticColliderTrisPtr.PrintAddress);
            writer.WriteNextCol(staticColliderMeshes.TriMeshGridPtrs.Length);
            writer.WriteNextCol(staticColliderMeshes.MeshGridXZ.Left);
            writer.WriteNextCol(staticColliderMeshes.MeshGridXZ.Top);
            writer.WriteNextCol(staticColliderMeshes.MeshGridXZ.SubdivisionWidth);
            writer.WriteNextCol(staticColliderMeshes.MeshGridXZ.SubdivisionLength);
            writer.WriteNextCol(staticColliderMeshes.MeshGridXZ.NumSubdivisionsX);
            writer.WriteNextCol(staticColliderMeshes.MeshGridXZ.NumSubdivisionsZ);
            writer.WriteNextCol(staticColliderMeshes.StaticColliderQuadsPtr.PrintAddress);
            writer.WriteNextCol(staticColliderMeshes.QuadMeshGridPtrs.Length);
            writer.WriteNextCol(staticColliderMeshes.BoundingSpherePtr.PrintAddress);
            writer.WriteNextCol(staticColliderMeshes.StaticSceneObjectsPtr.PrintAddress);
            writer.WriteNextCol(staticColliderMeshes.UnknownCollidersPtr.PrintAddress);
            writer.WriteNextCol(staticColliderMeshes.Unk_float);
            writer.WriteNextCol();
            writer.WriteNextCol(staticColliderMeshes.BoundingSphere.origin.X);
            writer.WriteNextCol(staticColliderMeshes.BoundingSphere.origin.Y);
            writer.WriteNextCol(staticColliderMeshes.BoundingSphere.origin.Z);
            writer.WriteNextCol(staticColliderMeshes.BoundingSphere.radius);
            writer.WriteNextRow();
        }
        writer.Flush();
    }

    public static void AnalyzeSceneObjectLODs(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Name");
        writer.WriteNextCol("Object Type");
        writer.WriteNextCol("Addr");
        //writer.WriteNextCol(nameof(SceneObjectLOD.zero_0x00));
        writer.WriteNextCol(nameof(SceneObjectLOD.LodNamePtr));
        //writer.WriteNextCol(nameof(SceneObjectLOD.zero_0x08));
        writer.WriteNextCol(nameof(SceneObjectLOD.LodDistance));
        //
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var sceneObject in sceneFile.Value.sceneObjects)
            {
                foreach (var sceneObjectLOD in sceneObject.LODs.Iterate())
                {
                    writer.WriteNextCol(sceneFile.FileName);
                    writer.WriteNextCol(sceneFile.CourseIndex);
                    writer.WriteNextCol(sceneFile.VenueName);
                    writer.WriteNextCol(sceneFile.CourseName);
                    writer.WriteNextCol(sceneFile.FileFormatDescription);
                    writer.WriteNextCol(sceneObjectLOD.Index);
                    writer.WriteNextCol(sceneObjectLOD.Value.Name);
                    writer.WriteNextCol(sceneObjectLOD.Value.AddressRange.PrintStartAddress());
                    //writer.WriteNextCol(sceneObjectReference.zero_0x00);
                    writer.WriteNextCol(sceneObjectLOD.Value.LodNamePtr);
                    //writer.WriteNextCol(sceneObjectReference.zero_0x08);
                    writer.WriteNextCol(sceneObjectLOD.Value.LodDistance);
                    //
                    writer.WriteNextRow();
                }
            }
        }
        writer.Flush();
    }

    public static void AnalyzeSceneObjects(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        //
        writer.WriteNextCol("Name");
        writer.WriteNextCol("Object Type");
        writer.WriteNextCol("Addr");
        writer.WriteNextCol(nameof(SceneObject.LodRenderFlags));
        writer.WriteNextCol(nameof(SceneObject.LodsPtr));
        writer.WriteNextCol(nameof(SceneObject.ColliderMeshPtr));
        //
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            // Get all the scene object references
            var sceneObjectsList = new List<(SceneObject sceneObject, string soCategory)>();
            foreach (var sceneInstance in sceneFile.Value.sceneObjects)
            {
                sceneObjectsList.Add((sceneInstance, "Instance"));
            }
            foreach (var sceneOriginObject in sceneFile.Value.staticSceneObjects)
            {
                var sceneInstance = sceneOriginObject.SceneObject;
                sceneObjectsList.Add((sceneInstance, "Origin"));
            }

            // iterate, breaking out typle in loop
            foreach (var (sceneObject, soCategory) in sceneObjectsList)
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                //
                writer.WriteNextCol(sceneObject.PrimaryLOD.Name);
                writer.WriteNextCol(soCategory);
                writer.WriteNextCol(sceneObject.AddressRange.PrintStartAddress());
                writer.WriteNextCol(sceneObject.LodRenderFlags);
                writer.WriteNextCol(sceneObject.LodsPtr);
                writer.WriteNextCol(sceneObject.ColliderMeshPtr);
                //
                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }

    public static void AnalyzeSceneObjectsAndLODs(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        //
        writer.WriteNextCol("name");
        writer.WriteNextCol(nameof(SceneObject.LodRenderFlags));
        writer.WriteNextCol(nameof(SceneObject.LodsPtr) + " Len");
        writer.WriteNextCol(nameof(SceneObject.LodsPtr) + " Adr");
        writer.WriteNextCol(nameof(SceneObject.ColliderMeshPtr));
        writer.WriteNextCol(nameof(SceneObjectLOD) + " IDX");
        //writer.WriteNextCol(nameof(SceneObjectLOD.zero_0x00));
        writer.WriteNextCol(nameof(SceneObjectLOD.LodNamePtr));
        //writer.WriteNextCol(nameof(SceneObjectLOD.zero_0x08));
        writer.WriteNextCol(nameof(SceneObjectLOD.LodDistance));
        writer.WriteNextCol(nameof(SceneObjectLOD.Name));
        //
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var template in sceneFile.Value.sceneObjects)
            {
                foreach (var sceneObject in template.LODs.Iterate(out int length))
                {
                    writer.WriteNextCol(sceneFile.FileName);
                    writer.WriteNextCol(sceneFile.CourseIndex);
                    writer.WriteNextCol(sceneFile.VenueName);
                    writer.WriteNextCol(sceneFile.CourseName);
                    writer.WriteNextCol(sceneFile.FileFormatDescription);
                    //
                    writer.WriteNextCol(template.Name);
                    writer.WriteNextCol(template.LodRenderFlags);
                    writer.WriteNextCol(template.LodsPtr.length);
                    writer.WriteNextCol(template.LodsPtr.PrintAddress);
                    writer.WriteNextCol(template.ColliderMeshPtr);
                    writer.WriteNextCol($"[{sceneObject.Index+1}/{length}]");
                    //writer.WriteNextCol(sceneObject.zero_0x00);
                    writer.WriteNextCol(sceneObject.Value.LodNamePtr);
                    //writer.WriteNextCol(sceneObject.zero_0x08);
                    writer.WriteNextCol(sceneObject.Value.LodDistance);
                    writer.WriteNextCol(sceneObject.Value.Name);
                    writer.WriteNextRow();
                }
            }
        }
        writer.Flush();
    }


    public static void AnalyzeGeneralData(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol(nameof(ViewRange) + "." + nameof(ViewRange.near));
        writer.WriteNextCol(nameof(ViewRange) + "." + nameof(ViewRange.far));
        writer.WriteNextCol(nameof(Scene.trackMinHeight));
        writer.WriteNextCol(nameof(Scene.trackLength));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            writer.WriteNextCol(sceneFile.FileName);
            writer.WriteNextCol(sceneFile.CourseIndex);
            writer.WriteNextCol(sceneFile.VenueName);
            writer.WriteNextCol(sceneFile.CourseName);
            writer.WriteNextCol(sceneFile.FileFormatDescription);
            writer.WriteNextCol(sceneFile.Value.UnkRange0x00.near);
            writer.WriteNextCol(sceneFile.Value.UnkRange0x00.far);
            writer.WriteNextCol(sceneFile.Value.trackMinHeight.Value);
            writer.WriteNextCol(sceneFile.Value.trackLength.Value);
            writer.WriteNextRow();
        }
        writer.Flush();
    }

    public static void AnalyzeSurfaceAttributeAreas(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol("Index");
        writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.LengthFrom));
        writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.LengthTo));
        writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.WidthLeft));
        writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.WidthRight));
        writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.PropertyType));
        writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.TrackBranchID));
        //writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.zero_0x12));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var embeddedPropertyArea in sceneFile.Value.embeddedPropertyAreas.Iterate(out int length))
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                writer.WriteNextCol($"[{embeddedPropertyArea.Index+1}/{length}]");
                writer.WriteNextCol(embeddedPropertyArea.Value.LengthFrom);
                writer.WriteNextCol(embeddedPropertyArea.Value.LengthTo);
                writer.WriteNextCol(embeddedPropertyArea.Value.WidthLeft);
                writer.WriteNextCol(embeddedPropertyArea.Value.WidthRight);
                writer.WriteNextCol(embeddedPropertyArea.Value.PropertyType);
                writer.WriteNextCol(embeddedPropertyArea.Value.TrackBranchID);
                //writer.WriteNextCol(surfaceAttributeArea.zero_0x12);
                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }


    public static void AnalyzeUnknownColliders(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Index");
        writer.WriteNextCol("Venue");
        writer.WriteNextCol("Course");
        writer.WriteNextCol("AX/GX");
        writer.WriteNextCol(nameof(UnknownCollider.SceneObjectPtr));
        writer.WriteNextCol(nameof(UnknownCollider.Transform.Position));
        writer.WriteNextCol(nameof(UnknownCollider.Transform.RotationEuler));
        writer.WriteNextCol(nameof(UnknownCollider.Transform.Scale));
        writer.WriteNextCol(nameof(UnknownCollider.Transform.UnknownOption));
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var unkSols in sceneFile.Value.unknownColliders)
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteNextCol(sceneFile.CourseIndex);
                writer.WriteNextCol(sceneFile.VenueName);
                writer.WriteNextCol(sceneFile.CourseName);
                writer.WriteNextCol(sceneFile.FileFormatDescription);
                writer.WriteNextCol(unkSols.SceneObjectPtr);
                writer.WriteNextCol(unkSols.Transform.Position);
                writer.WriteNextCol(unkSols.Transform.RotationEuler);
                writer.WriteNextCol(unkSols.Transform.Scale);
                writer.WriteNextCol(unkSols.Transform.UnknownOption);
                writer.WriteNextRow();
            }
            writer.Flush();
        }
    }



    public static void AnalyzeStaticColliderTriangles(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Addr");
        writer.WriteNextCol("Tri Index");
        writer.WriteNextColNicify(nameof(ColliderTriangle.PlaneDistance));
        writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".Z");
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var tri in sceneFile.Value.staticColliderMeshManager.ColliderTris.Iterate())
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteStartAddress(tri.Value);
                writer.WriteNextCol(tri.Index);
                writer.WriteNextCol(tri.Value.PlaneDistance);
                writer.WriteNextCol(tri.Value.Normal.X);
                writer.WriteNextCol(tri.Value.Normal.Y);
                writer.WriteNextCol(tri.Value.Normal.Z);
                writer.WriteNextCol(tri.Value.Vertex0.X);
                writer.WriteNextCol(tri.Value.Vertex0.Y);
                writer.WriteNextCol(tri.Value.Vertex0.Z);
                writer.WriteNextCol(tri.Value.Vertex1.X);
                writer.WriteNextCol(tri.Value.Vertex1.Y);
                writer.WriteNextCol(tri.Value.Vertex1.Z);
                writer.WriteNextCol(tri.Value.Vertex2.X);
                writer.WriteNextCol(tri.Value.Vertex2.Y);
                writer.WriteNextCol(tri.Value.Vertex2.Z);
                writer.WriteNextCol(tri.Value.EdgeNormal0.X);
                writer.WriteNextCol(tri.Value.EdgeNormal0.Y);
                writer.WriteNextCol(tri.Value.EdgeNormal0.Z);
                writer.WriteNextCol(tri.Value.EdgeNormal1.X);
                writer.WriteNextCol(tri.Value.EdgeNormal1.Y);
                writer.WriteNextCol(tri.Value.EdgeNormal1.Z);
                writer.WriteNextCol(tri.Value.EdgeNormal2.X);
                writer.WriteNextCol(tri.Value.EdgeNormal2.Y);
                writer.WriteNextCol(tri.Value.EdgeNormal2.Z);
                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }

    public static void AnalyzeStaticColliderQuads(SceneFile[] sceneFiles, string fileName)
    {
        using var writer = new StreamWriter(File.Create(fileName));

        // Write header
        writer.WriteNextCol("File");
        writer.WriteNextCol("Addr");
        writer.WriteNextCol("Quad Index");
        writer.WriteNextColNicify(nameof(ColliderQuad.PlaneDistance));
        writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".Z");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".X");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".Y");
        writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".Z");
        writer.WriteNextRow();

        foreach (var sceneFile in sceneFiles)
        {
            foreach (var quad in sceneFile.Value.staticColliderMeshManager.ColliderQuads.Iterate())
            {
                writer.WriteNextCol(sceneFile.FileName);
                writer.WriteStartAddress(quad.Value);
                writer.WriteNextCol(quad.Index);
                writer.WriteNextCol(quad.Value.PlaneDistance);
                writer.WriteNextCol(quad.Value.Normal.X);
                writer.WriteNextCol(quad.Value.Normal.Y);
                writer.WriteNextCol(quad.Value.Normal.Z);
                writer.WriteNextCol(quad.Value.Vertex0.X);
                writer.WriteNextCol(quad.Value.Vertex0.Y);
                writer.WriteNextCol(quad.Value.Vertex0.Z);
                writer.WriteNextCol(quad.Value.Vertex1.X);
                writer.WriteNextCol(quad.Value.Vertex1.Y);
                writer.WriteNextCol(quad.Value.Vertex1.Z);
                writer.WriteNextCol(quad.Value.Vertex2.X);
                writer.WriteNextCol(quad.Value.Vertex2.Y);
                writer.WriteNextCol(quad.Value.Vertex2.Z);
                writer.WriteNextCol(quad.Value.Vertex3.X);
                writer.WriteNextCol(quad.Value.Vertex3.Y);
                writer.WriteNextCol(quad.Value.Vertex3.Z);
                writer.WriteNextCol(quad.Value.EdgeNormal0.X);
                writer.WriteNextCol(quad.Value.EdgeNormal0.Y);
                writer.WriteNextCol(quad.Value.EdgeNormal0.Z);
                writer.WriteNextCol(quad.Value.EdgeNormal1.X);
                writer.WriteNextCol(quad.Value.EdgeNormal1.Y);
                writer.WriteNextCol(quad.Value.EdgeNormal1.Z);
                writer.WriteNextCol(quad.Value.EdgeNormal2.X);
                writer.WriteNextCol(quad.Value.EdgeNormal2.Y);
                writer.WriteNextCol(quad.Value.EdgeNormal2.Z);
                writer.WriteNextCol(quad.Value.EdgeNormal3.X);
                writer.WriteNextCol(quad.Value.EdgeNormal3.Y);
                writer.WriteNextCol(quad.Value.EdgeNormal3.Z);
                writer.WriteNextRow();
            }
        }
        writer.Flush();
    }

}
