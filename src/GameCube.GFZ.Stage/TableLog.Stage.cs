﻿using Manifold;
using Manifold.IO;
using System.Collections.Generic;
using System.IO;

namespace GameCube.GFZ.Stage
{
    public static class StageTableLogger
    {
        // Names of files generated
        public static readonly string tsvHeader = $"{nameof(Scene)}-Header.tsv";
        public static readonly string tsvGeneralData = $"General Data.tsv";
        public static readonly string tsvTrackKeyablesAll = $"Track Keyables All.tsv";
        public static readonly string tsvTrackSegment = $"{nameof(TrackSegment)}.tsv";
        //public static readonly string tsvTrackSegmentCurves = $"{nameof(TrackSegment)}-{nameof(AnimationCurveTRS)}.tsv";
        public static readonly string tsvSurfaceAttributeArea = $"{nameof(EmbeddedTrackPropertyArea)}.tsv";
        public static readonly string tsvTrackNode = $"{nameof(TrackNode)}.tsv";
        public static readonly string tsvSceneObject = $"{nameof(SceneObject)}.tsv";
        public static readonly string tsvSceneObjectLod = $"{nameof(SceneObjectLOD)}.tsv";
        public static readonly string tsvSceneObjectsAndLod = $"{nameof(SceneObjectLOD)}.tsv";
        public static readonly string tsvSceneObjectDynamic = $"{nameof(SceneObjectDynamic)}.tsv";
        public static readonly string tsvAnimationClip = $"{nameof(SceneObjectDynamic)}-{nameof(AnimationClip)}.tsv";
        public static readonly string tsvTextureMetadata = $"{nameof(SceneObjectDynamic)}-{nameof(TextureScroll)}.tsv";
        public static readonly string tsvSkeletalAnimator = $"{nameof(SceneObjectDynamic)}-{nameof(SkeletalAnimator)}.tsv";
        public static readonly string tsvColliderGeometryTri = $"{nameof(SceneObjectDynamic)}-{nameof(ColliderMesh)}-Tris.tsv";
        public static readonly string tsvColliderGeometryQuad = $"{nameof(SceneObjectDynamic)}-{nameof(ColliderMesh)}-Quads.tsv";
        public static readonly string tsvTransform = $"{nameof(TransformTRXS)}.tsv";
        public static readonly string tsvTimeExtensionTrigger = $"{nameof(TimeExtensionTrigger)}.tsv";
        public static readonly string tsvMiscellaneousTrigger = $"{nameof(MiscellaneousTrigger)}.tsv";
        public static readonly string tsvStoryObjectTrigger = $"{nameof(StoryObjectTrigger)}.tsv";
        public static readonly string tsvUnknownTrigger = $"{nameof(CullOverrideTrigger)}.tsv";
        public static readonly string tsvVisualEffectTrigger = $"{nameof(VisualEffectTrigger)}.tsv";
        public static readonly string tsvFog = $"{nameof(Fog)}.tsv";
        public static readonly string tsvFogCurves = $"{nameof(FogCurves)}.tsv";
        public static readonly string tsvStaticColliderMeshes = $"{nameof(StaticColliderMeshManager)}.tsv";
        public static readonly string tsvUnknownCollider = $"{nameof(UnknownCollider)}.tsv";

        public static readonly string tsvStaticColliderTriangles = $"{nameof(StaticColliderMeshManager)}-{nameof(ColliderTriangle)}s.tsv";
        public static readonly string tsvStaticColliderQuads= $"{nameof(StaticColliderMeshManager)}-{nameof(ColliderQuad)}s.tsv";


        #region Track Data / Transforms

        public static void AnalyzeTrackKeyablesAll(Scene[] scenes, string filename)
        {
            using (var writer = new StreamWriter(File.Create(filename)))
            {
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
                foreach (var scene in scenes)
                {
                    // foreach Transform
                    int trackIndex = 0;
                    foreach (var trackTransform in scene.RootTrackSegments)
                    {
                        for (int keyablesIndex = 0; keyablesIndex < AnimationCurveTRS.kCurveCount; keyablesIndex++)
                        {
                            WriteTrackKeyableAttributeRecursive(writer, scene, 0, keyablesIndex, ++trackIndex, trackTransform);
                        }
                    }
                }

                writer.Flush();
            }
        }

        public static void AnalyzeTrackKeyables(Scene[] scenes, string filename, int keyablesSet)
        {
            using (var writer = new StreamWriter(File.Create(filename)))
            {
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
                foreach (var scene in scenes)
                {
                    // foreach Transform
                    int trackTransformIndex = 0;
                    foreach (var trackTransform in scene.RootTrackSegments)
                    {
                        WriteTrackKeyableAttributeRecursive(writer, scene, nestedDepth: 0, keyablesSet, trackTransformIndex++, trackTransform);
                    }
                }

                writer.Flush();
            }
        }
        public static void WriteTrackKeyableAttributeRecursive(StreamWriter writer, Scene scene, int nestedDepth, int animationCurveIndex, int trackTransformIndex, TrackSegment trackTransform)
        {
            var animationCurves = trackTransform.AnimationCurveTRS.AnimationCurves;
            var keyableIndex = 1; // 0-n, depends on number of keyables in array
            int keyableTotal = animationCurves[animationCurveIndex].Length;

            // Animation data of this curve
            foreach (var keyables in animationCurves[animationCurveIndex].KeyableAttributes)
            {
                WriteKeyableAttribute(writer, scene, nestedDepth + 1, keyableIndex++, keyableTotal, animationCurveIndex, trackTransformIndex, keyables, trackTransform);
            }

            // TODO: do you even care to reimplement this at this point?
            // Go to track transform children, write their anim data (calls this function)
            //Debug.LogWarning("You refactored this analysis out!");
            //foreach (var child in trackTransform.children)
            //    WriteTrackKeyableAttributeRecursive(writer, sobj, nestedDepth + 1, animationCurveIndex, trackTransformIndex, child);
        }
        public static void WriteKeyableAttribute(StreamWriter writer, Scene scene, int nestedDepth, int keyableIndex, int keyableTotal, int keyablesSet, int trackTransformIndex, KeyableAttribute param, TrackSegment tt)
        {
            string gameId = scene.IsFileGX ? "GX" : "AX";

            writer.WriteNextCol(scene.FileName);
            writer.WriteNextCol(gameId);

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
        public static void AnalyzeTrackSegments(Scene[] scenes, string filename)
        {
            using (var writer = new StreamWriter(File.Create(filename)))
            {
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

                foreach (var scene in scenes)
                {
                    var index = 0;
                    var total = scene.RootTrackSegments.Length;
                    foreach (var trackTransform in scene.RootTrackSegments)
                    {
                        WriteTrackSegmentRecursive(writer, scene, 0, ++index, total, trackTransform);
                    }
                }

                writer.Flush();
            }
        }
        // Writes self and children
        public static void WriteTrackSegmentRecursive(StreamWriter writer, Scene scene, int depth, int index, int total, TrackSegment trackSegment)
        {
            // Write Parent
            WriteTrackSegment(writer, scene, depth, index, total, trackSegment);

            // Write children
            if (trackSegment.Children == null)
                return;

            foreach (var child in trackSegment.Children)
            {
                WriteTrackSegmentRecursive(writer, scene, depth + 1, index, total, child);
            }
        }
        // The actual writing to file
        public static void WriteTrackSegment(StreamWriter writer, Scene scene, int depth, int index, int total, TrackSegment trackTransform)
        {
            writer.WriteNextCol(scene.FileName);
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

        public static void AnalyzeAnimationClips(Scene[] scenes, string filename)
        {
            using (var writer = new StreamWriter(File.Create(filename)))
            {
                // Write header
                writer.WriteNextCol("File Path");
                writer.WriteNextCol("Game Object #");
                writer.WriteNextCol("Game Object");
                writer.WriteNextCol("Anim Addr");
                writer.WriteNextCol("Key Addr");
                writer.WriteNextCol("Anim Index [0-10]");
                writer.WriteNextCol("Unk_0x00");
                writer.WriteNextCol("Time");
                writer.WriteNextCol("Value");
                writer.WriteNextCol("Unk_0x0C");
                writer.WriteNextCol("Unk_0x10");
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int gameObjectIndex = 0;
                    foreach (var gameObject in scene.dynamicSceneObjects)
                    {
                        if (gameObject.AnimationClip == null)
                            continue;
                        if (gameObject.AnimationClip.Curves == null)
                            continue;

                        int animIndex = 0;
                        foreach (var animationClipCurve in gameObject.AnimationClip.Curves)
                        {
                            if (animationClipCurve.AnimationCurve == null)
                                continue;

                            foreach (var keyable in animationClipCurve.AnimationCurve.KeyableAttributes)
                            {
                                writer.WriteNextCol(scene.FileName);
                                writer.WriteNextCol(gameObjectIndex);
                                writer.WriteNextCol(gameObject.Name);
                                writer.WriteNextCol(animationClipCurve.AddressRange.PrintStartAddress());
                                writer.WriteNextCol(keyable.AddressRange.PrintStartAddress());
                                writer.WriteNextCol(animIndex);
                                writer.WriteNextCol(keyable.EaseMode);
                                writer.WriteNextCol(keyable.Time);
                                writer.WriteNextCol(keyable.Value);
                                writer.WriteNextCol(keyable.TangentIn);
                                writer.WriteNextCol(keyable.TangentOut);
                                writer.WriteNextRow();
                            }
                            animIndex++;
                        }
                        gameObjectIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeGameObjectAnimationClipIndex(Scene[] scenes, string filename, int index)
        {
            using (var writer = new StreamWriter(File.Create(filename)))
            {
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

                foreach (var scene in scenes)
                {
                    int objIndex = 0;
                    foreach (var dynamicSceneObject in scene.dynamicSceneObjects)
                    {
                        if (dynamicSceneObject.AnimationClip == null)
                            continue;
                        //if (gameObject.animation.curve == null)
                        //    continue;

                        int animIndex = 0;
                        foreach (var animationClipCurve in dynamicSceneObject.AnimationClip.Curves)
                        {
                            // Failing for some reason on indexes 6+ :/
                            if (animationClipCurve.AnimationCurve == null)
                                continue;

                            foreach (var keyable in animationClipCurve.AnimationCurve.KeyableAttributes)
                            {
                                /// HACK, write each anim index as separate file
                                if (animIndex != index)
                                    continue;

                                writer.WriteNextCol(scene.FileName);
                                writer.WriteNextCol(objIndex);
                                writer.WriteNextCol(dynamicSceneObject.Name);
                                writer.WriteNextCol(animationClipCurve.AddressRange.PrintStartAddress());
                                writer.WriteNextCol(keyable.AddressRange.PrintStartAddress());
                                writer.WriteNextCol(animationClipCurve.Unk_0x00);
                                writer.WriteNextCol(animationClipCurve.Unk_0x04);
                                writer.WriteNextCol(animationClipCurve.Unk_0x08);
                                writer.WriteNextCol(animationClipCurve.Unk_0x0C);
                                writer.WriteNextCol(animIndex);
                                writer.WriteNextCol(keyable.EaseMode);
                                writer.WriteNextCol(keyable.Time);
                                writer.WriteNextCol(keyable.Value);
                                writer.WriteNextCol(keyable.TangentIn);
                                writer.WriteNextCol(keyable.TangentOut);
                                writer.WriteNextRow();
                            }
                            animIndex++;
                        }
                        objIndex++;
                    }
                }
                writer.Flush();
            }
        }

        #endregion

        #region Dynamic Scene Objects

        public static void AnalyzeSceneObjectDynamic(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
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

                foreach (var scene in scenes)
                {
                    int sceneObjectIndex = 0;
                    foreach (var sceneObject in scene.dynamicSceneObjects)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(sceneObjectIndex);
                        writer.WriteNextCol(sceneObject.Name);
                        writer.WriteNextCol(sceneObject.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(sceneObject.ObjectRenderFlags0x00);
                        writer.WriteNextCol($"0x{(uint)sceneObject.ObjectRenderFlags0x00:x8}");
                        writer.WriteNextCol(sceneObject.ObjectRenderFlags0x04);
                        writer.WriteNextCol($"0x{(uint)sceneObject.ObjectRenderFlags0x04:x8}");
                        writer.WriteNextCol(sceneObject.SceneObjectPtr.PrintAddress);
                        writer.WriteNextCol(sceneObject.TransformTRXS.Position);
                        writer.WriteNextCol(sceneObject.TransformTRXS.RotationEuler);
                        writer.WriteNextCol(sceneObject.TransformTRXS.Scale);
                        //writer.WriteNextCol(sceneObject.zero_0x2C);
                        writer.WriteNextCol(sceneObject.AnimationClipPtr.PrintAddress);
                        writer.WriteNextCol(sceneObject.TextureScrollPtr.PrintAddress);
                        writer.WriteNextCol(sceneObject.SkeletalAnimatorPtr.PrintAddress);
                        writer.WriteNextCol(sceneObject.TransformMatrix3x4Ptr.PrintAddress);
                        writer.WriteNextRow();

                        sceneObjectIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeTextureMetadata(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Game Object #");
                writer.WriteNextCol("Game Object");
                writer.WriteNextCol("Unknown 1 Index");
                writer.WriteNextColNicify(nameof(TextureScrollField.u));
                writer.WriteNextColNicify(nameof(TextureScrollField.v));
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int gameObjectIndex = 0;
                    foreach (var sceneObject in scene.dynamicSceneObjects)
                    {
                        if (sceneObject.TextureScroll == null)
                            continue;

                        int fieldArrayIndex = 0;
                        foreach (var field in sceneObject.TextureScroll.Fields)
                        {
                            if (field == null)
                                return;

                            writer.WriteNextCol(scene.FileName);
                            writer.WriteNextCol(gameObjectIndex);
                            writer.WriteNextCol(sceneObject.Name);
                            writer.WriteNextCol(fieldArrayIndex);
                            writer.WriteNextCol(field.u);
                            writer.WriteNextCol(field.v);
                            writer.WriteNextRow();
                            fieldArrayIndex++;
                        }
                        gameObjectIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeSkeletalAnimator(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
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

                foreach (var scene in scenes)
                {
                    int gameObjectIndex = 0;
                    foreach (var dynamicSceneObject in scene.dynamicSceneObjects)
                    {
                        if (dynamicSceneObject.SkeletalAnimator == null)
                            continue;
                        if (dynamicSceneObject.SkeletalAnimator.PropertiesPtr.IsNull)
                            continue;

                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(gameObjectIndex);
                        writer.WriteNextCol(dynamicSceneObject.Name);

                        //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.zero_0x00);
                        //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.zero_0x04);
                        //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.one_0x08);
                        writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.PropertiesPtr);

                        writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.Unk_0x00);
                        writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.Unk_0x04);
                        writer.WriteFlags(dynamicSceneObject.SkeletalAnimator.Properties.Unk_0x04);
                        writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.Unk_0x08);
                        writer.WriteFlags(dynamicSceneObject.SkeletalAnimator.Properties.Unk_0x08);
                        //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x0C);
                        //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x10);
                        //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x14);
                        //writer.WriteNextCol(dynamicSceneObject.SkeletalAnimator.Properties.zero_0x18);
                        writer.WriteNextRow();

                        gameObjectIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeColliderGeometryTri(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Game Object #");
                writer.WriteNextCol("Game Object");

                writer.WriteNextCol("Tri Index");
                writer.WriteNextCol("Addr");

                writer.WriteNextColNicify(nameof(ColliderTriangle.PlaneDistance));
                writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".z");

                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int gameObjectIndex = 0;
                    foreach (var dynamicSceneObject in scene.dynamicSceneObjects)
                    {
                        if (dynamicSceneObject.SceneObject.ColliderMesh is null ||
                            dynamicSceneObject.SceneObject.ColliderMesh.Tris is null ||
                            dynamicSceneObject.SceneObject.ColliderMesh.Tris.Length == 0)
                            continue;

                        int triIndex = 0;
                        foreach (var tri in dynamicSceneObject.SceneObject.ColliderMesh.Tris)
                        {
                            writer.WriteNextCol(scene.FileName);
                            writer.WriteNextCol(gameObjectIndex);
                            writer.WriteNextCol(dynamicSceneObject.Name);

                            writer.WriteNextCol(triIndex++);
                            writer.WriteStartAddress(tri);

                            writer.WriteNextCol(tri.PlaneDistance);
                            writer.WriteNextCol(tri.Normal.x);
                            writer.WriteNextCol(tri.Normal.y);
                            writer.WriteNextCol(tri.Normal.z);
                            writer.WriteNextCol(tri.Vertex0.x);
                            writer.WriteNextCol(tri.Vertex0.y);
                            writer.WriteNextCol(tri.Vertex0.z);
                            writer.WriteNextCol(tri.Vertex1.x);
                            writer.WriteNextCol(tri.Vertex1.y);
                            writer.WriteNextCol(tri.Vertex1.z);
                            writer.WriteNextCol(tri.Vertex2.x);
                            writer.WriteNextCol(tri.Vertex2.y);
                            writer.WriteNextCol(tri.Vertex2.z);
                            writer.WriteNextCol(tri.EdgeNormal0.x);
                            writer.WriteNextCol(tri.EdgeNormal0.y);
                            writer.WriteNextCol(tri.EdgeNormal0.z);
                            writer.WriteNextCol(tri.EdgeNormal1.x);
                            writer.WriteNextCol(tri.EdgeNormal1.y);
                            writer.WriteNextCol(tri.EdgeNormal1.z);
                            writer.WriteNextCol(tri.EdgeNormal2.x);
                            writer.WriteNextCol(tri.EdgeNormal2.y);
                            writer.WriteNextCol(tri.EdgeNormal2.z);

                            writer.WriteNextRow();
                        }
                        gameObjectIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeColliderGeometryQuad(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Game Object #");
                writer.WriteNextCol("Game Object");

                writer.WriteNextCol("Quad Index");
                writer.WriteNextCol("Addr");

                writer.WriteNextColNicify(nameof(ColliderQuad.PlaneDistance));
                writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".z");

                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int gameObjectIndex = 0;
                    foreach (var dynamicSceneObject in scene.dynamicSceneObjects)
                    {
                        if (dynamicSceneObject.SceneObject.ColliderMesh is null ||
                            dynamicSceneObject.SceneObject.ColliderMesh.Quads is null ||
                            dynamicSceneObject.SceneObject.ColliderMesh.Quads.Length == 0)
                            continue;

                        int quadIndex = 0;
                        foreach (var quad in dynamicSceneObject.SceneObject.ColliderMesh.Quads)
                        {
                            writer.WriteNextCol(scene.FileName);
                            writer.WriteNextCol(gameObjectIndex);
                            writer.WriteNextCol(dynamicSceneObject.Name);

                            writer.WriteNextCol(quadIndex++);
                            writer.WriteStartAddress(quad);

                            writer.WriteNextCol(quad.PlaneDistance);
                            writer.WriteNextCol(quad.Normal.x);
                            writer.WriteNextCol(quad.Normal.y);
                            writer.WriteNextCol(quad.Normal.z);
                            writer.WriteNextCol(quad.Vertex0.x);
                            writer.WriteNextCol(quad.Vertex0.y);
                            writer.WriteNextCol(quad.Vertex0.z);
                            writer.WriteNextCol(quad.Vertex1.x);
                            writer.WriteNextCol(quad.Vertex1.y);
                            writer.WriteNextCol(quad.Vertex1.z);
                            writer.WriteNextCol(quad.Vertex2.x);
                            writer.WriteNextCol(quad.Vertex2.y);
                            writer.WriteNextCol(quad.Vertex2.z);
                            writer.WriteNextCol(quad.Vertex3.x);
                            writer.WriteNextCol(quad.Vertex3.y);
                            writer.WriteNextCol(quad.Vertex3.z);
                            writer.WriteNextCol(quad.EdgeNormal0.x);
                            writer.WriteNextCol(quad.EdgeNormal0.y);
                            writer.WriteNextCol(quad.EdgeNormal0.z);
                            writer.WriteNextCol(quad.EdgeNormal1.x);
                            writer.WriteNextCol(quad.EdgeNormal1.y);
                            writer.WriteNextCol(quad.EdgeNormal1.z);
                            writer.WriteNextCol(quad.EdgeNormal2.x);
                            writer.WriteNextCol(quad.EdgeNormal2.y);
                            writer.WriteNextCol(quad.EdgeNormal2.z);
                            writer.WriteNextCol(quad.EdgeNormal3.x);
                            writer.WriteNextCol(quad.EdgeNormal3.y);
                            writer.WriteNextCol(quad.EdgeNormal3.z);

                            writer.WriteNextRow();
                        }
                        gameObjectIndex++;
                    }
                }
                writer.Flush();
            }
        }

        #endregion


        public static void AnalyzeHeaders(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
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

                foreach (var scene in scenes)
                {
                    writer.WriteNextCol(scene.FileName);
                    writer.WriteNextCol(scene.CourseIndex);
                    writer.WriteNextCol(CourseUtility.GetVenueID(scene.CourseIndex).GetDescription());
                    writer.WriteNextCol(((CourseIndexAX)scene.CourseIndex).GetDescription());
                    writer.WriteNextCol(scene.IsFileGX ? "GX" : "AX");

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
                    if (scene.IsFileGX)
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
        }


        #region TRIGGERS

        public static void AnalyzeArcadeCheckpointTriggers(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.Position));
                writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.RotationEuler));
                writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.Scale));
                writer.WriteNextCol(nameof(TimeExtensionTrigger.Transform.UnknownOption));
                writer.WriteNextCol(nameof(TimeExtensionTrigger.Option));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    foreach (var arcadeCheckpooint in scene.timeExtensionTriggers)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        writer.WriteNextCol(arcadeCheckpooint.Transform.Position);
                        writer.WriteNextCol(arcadeCheckpooint.Transform.RotationEuler);
                        writer.WriteNextCol(arcadeCheckpooint.Transform.Scale);
                        writer.WriteNextCol(arcadeCheckpooint.Transform.UnknownOption);
                        writer.WriteNextCol(arcadeCheckpooint.Option);
                        //
                        writer.WriteNextRow();
                    }
                    writer.Flush();
                }
            }
        }

        public static void AnalyzeCourseMetadataTriggers(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol(nameof(MiscellaneousTrigger.Position));
                writer.WriteNextCol(nameof(MiscellaneousTrigger.RotationEuler));
                writer.WriteNextCol(nameof(MiscellaneousTrigger.Scale) + " / PositionTo");
                writer.WriteNextCol(nameof(MiscellaneousTrigger.Transform.UnknownOption));
                writer.WriteNextCol(nameof(MiscellaneousTrigger.MetadataType));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    foreach (var cmt in scene.miscellaneousTriggers)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        writer.WriteNextCol(cmt.Position);
                        writer.WriteNextCol(cmt.RotationEuler);
                        writer.WriteNextCol(cmt.Scale);
                        writer.WriteNextCol(cmt.Transform.UnknownOption);
                        writer.WriteNextCol(cmt.MetadataType);
                        //
                        writer.WriteNextRow();
                    }
                    writer.Flush();
                }
            }
        }

        public static void AnalyzeStoryObjectTrigger(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                //writer.WriteNextCol(nameof(StoryObjectTrigger.zero_0x00));
                writer.WriteNextCol(nameof(StoryObjectTrigger.BoulderGroupOrderIndex));
                writer.WriteNextCol(nameof(StoryObjectTrigger.BoulderGroup));
                writer.WriteNextCol(nameof(StoryObjectTrigger.Difficulty));
                writer.WriteNextCol(nameof(StoryObjectTrigger.Story2BoulderScale));
                writer.WriteNextCol(nameof(StoryObjectTrigger.Story2BoulderPathPtr));
                writer.WriteNextCol(nameof(StoryObjectTrigger.Scale));
                writer.WriteNextCol(nameof(StoryObjectTrigger.Rotation));
                writer.WriteNextCol(nameof(StoryObjectTrigger.Position));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    foreach (var item in scene.storyObjectTriggers)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        //writer.WriteNextCol(item.zero_0x00);
                        writer.WriteNextCol(item.BoulderGroupOrderIndex);
                        writer.WriteNextCol(item.BoulderGroup);
                        writer.WriteNextCol(item.Difficulty);
                        writer.WriteNextCol(item.Story2BoulderScale);
                        writer.WriteNextCol(item.Story2BoulderPathPtr);
                        writer.WriteNextCol(item.Scale);
                        writer.WriteNextCol(item.Rotation);
                        writer.WriteNextCol(item.Position);
                        //
                        writer.WriteNextRow();
                    }
                    writer.Flush();
                }
            }
        }

        public static void AnalyzeUnknownTrigger(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol("Start");
                writer.WriteNextCol("End");
                //
                writer.WriteNextCol(nameof(CullOverrideTrigger.Unk_0x20));
                writer.WriteNextCol(nameof(CullOverrideTrigger.Unk_0x20));
                //
                writer.WriteNextCol("Order");
                writer.WriteNextCol("Index");
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    int count = 0;
                    int total = scene.cullOverrideTriggers.Length;
                    foreach (var item in scene.cullOverrideTriggers)
                    {
                        count++;

                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);

                        writer.WriteNextCol(item.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(item.AddressRange.PrintEndAddress());

                        writer.WriteNextCol(item.Unk_0x20);
                        writer.WriteNextCol($"0x{(int)item.Unk_0x20:X8}");

                        writer.WriteNextCol(count);
                        writer.WriteNextCol($"[{count}/{total}]");

                        writer.WriteNextRow();
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeVisualEffectTriggers(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.Position));
                writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.RotationEuler));
                writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.Scale));
                writer.WriteNextCol(nameof(VisualEffectTrigger.Transform.UnknownOption));
                writer.WriteNextCol(nameof(VisualEffectTrigger.Animation));
                writer.WriteNextCol(nameof(VisualEffectTrigger.VisualEffect));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    foreach (var vfx in scene.visualEffectTriggers)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        writer.WriteNextCol(vfx.Transform.Position);
                        writer.WriteNextCol(vfx.Transform.RotationEuler);
                        writer.WriteNextCol(vfx.Transform.Scale);
                        writer.WriteNextCol(vfx.Transform.UnknownOption);
                        writer.WriteNextCol(vfx.Animation);
                        writer.WriteNextCol(vfx.VisualEffect);
                        //
                        writer.WriteNextRow();
                    }
                    writer.Flush();
                }
            }
        }

        #endregion

        #region FOG

        public static void AnalyzeFogCurves(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol("Addr");
                writer.WriteNextCol("Index");
                //
                writer.WriteNextCol(nameof(KeyableAttribute.EaseMode));
                writer.WriteNextCol(nameof(KeyableAttribute.Time));
                writer.WriteNextCol(nameof(KeyableAttribute.Value));
                writer.WriteNextCol(nameof(KeyableAttribute.TangentIn));
                writer.WriteNextCol(nameof(KeyableAttribute.TangentOut));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    if (scene.fogCurves == null)
                        continue;

                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    var totalD1 = scene.fogCurves.animationCurves.Length;
                    var countD1 = 0;
                    foreach (var animationCurve in scene.fogCurves.animationCurves)
                    {
                        countD1++;
                        foreach (var keyableAttribute in animationCurve.KeyableAttributes)
                        {
                            writer.WriteNextCol(scene.FileName);
                            writer.WriteNextCol(scene.CourseIndex);
                            writer.WriteNextCol(venueID);
                            writer.WriteNextCol(courseID);
                            writer.WriteNextCol(isAxGx);
                            //
                            writer.WriteNextCol(keyableAttribute.AddressRange.PrintStartAddress());
                            writer.WriteNextCol($"[{countD1}/{totalD1}]");
                            //
                            writer.WriteNextCol(keyableAttribute.EaseMode);
                            writer.WriteNextCol(keyableAttribute.Time);
                            writer.WriteNextCol(keyableAttribute.Value);
                            writer.WriteNextCol(keyableAttribute.TangentIn);
                            writer.WriteNextCol(keyableAttribute.TangentOut);
                            //
                            writer.WriteNextRow();
                        }
                    }
                    writer.Flush();
                }
            }
        }

        public static void AnalyzeFog(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol("Addr");
                writer.WriteNextCol(nameof(Fog.Interpolation));
                writer.WriteNextCol(nameof(Fog.FogRange) + "." + nameof(ViewRange.near));
                writer.WriteNextCol(nameof(Fog.FogRange) + "." + nameof(ViewRange.far));
                writer.WriteNextCol(nameof(Fog.ColorRGB) + ".R");
                writer.WriteNextCol(nameof(Fog.ColorRGB) + ".G");
                writer.WriteNextCol(nameof(Fog.ColorRGB) + ".B");
                //writer.WriteNextCol(nameof(Fog.zero0x18) + ".x");
                //writer.WriteNextCol(nameof(Fog.zero0x18) + ".y");
                //writer.WriteNextCol(nameof(Fog.zero0x18) + ".z");
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    writer.WriteNextCol(scene.FileName);
                    writer.WriteNextCol(scene.CourseIndex);
                    writer.WriteNextCol(venueID);
                    writer.WriteNextCol(courseID);
                    writer.WriteNextCol(isAxGx);
                    //
                    writer.WriteNextCol(scene.fog.AddressRange.PrintStartAddress());
                    writer.WriteNextCol(scene.fog.Interpolation);
                    writer.WriteNextCol(scene.fog.FogRange.near);
                    writer.WriteNextCol(scene.fog.FogRange.far);
                    writer.WriteNextCol(scene.fog.ColorRGB.x);
                    writer.WriteNextCol(scene.fog.ColorRGB.y);
                    writer.WriteNextCol(scene.fog.ColorRGB.z);
                    //writer.WriteNextCol(scene.fog.zero0x18.x);
                    //writer.WriteNextCol(scene.fog.zero0x18.y);
                    //writer.WriteNextCol(scene.fog.zero0x18.z);
                    //
                    writer.WriteNextRow();
                }
                writer.Flush();
            }
        }

        #endregion


        public static void AnalyzeSceneObjectTransforms(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Game Object #");
                writer.WriteNextCol("Game Object");
                writer.WriteNextCol($"matrix.x");
                writer.WriteNextCol($"matrix.y");
                writer.WriteNextCol($"matrix.z");
                writer.WriteNextCol($"euler.x");
                writer.WriteNextCol($"euler.y");
                writer.WriteNextCol($"euler.z");
                writer.WriteNextCol("Decomposed phi");
                writer.WriteNextCol("Decomposed theta");
                writer.WriteNextCol("Decomposed psi");
                writer.WriteNextCol(nameof(UnknownTransformOption));
                writer.WriteNextCol(nameof(ObjectActiveOverride));
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int sceneObjectIndex = 0;
                    foreach (var sceneObject in scene.dynamicSceneObjects)
                    {
                        // Skip objects that don;'t have both matrix and decomposed rotation
                        // These are not helpful for comparision
                        if (!sceneObject.TransformMatrix3x4Ptr.IsNotNull)
                            continue;

                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(sceneObjectIndex);
                        writer.WriteNextCol(sceneObject.Name);

                        // Rotation values from clean, uncompressed matrix
                        var matrix = sceneObject.TransformMatrix3x4.RotationEuler;
                        writer.WriteNextCol(matrix.x);
                        writer.WriteNextCol(matrix.y);
                        writer.WriteNextCol(matrix.z);

                        // Rotation values as reconstructed
                        var euler = sceneObject.TransformTRXS.CompressedRotation.Eulers;
                        writer.WriteNextCol(euler.x);
                        writer.WriteNextCol(euler.y);
                        writer.WriteNextCol(euler.z);

                        // Decomposed rotation values, raw, requires processing to be used
                        var decomposed = sceneObject.TransformTRXS.CompressedRotation;
                        writer.WriteNextCol(decomposed.X);
                        writer.WriteNextCol(decomposed.Y);
                        writer.WriteNextCol(decomposed.Z);
                        // The other parameters that go with the structure
                        writer.WriteNextCol(sceneObject.TransformTRXS.UnknownOption);
                        writer.WriteNextCol(sceneObject.TransformTRXS.ObjectActiveOverride);

                        writer.WriteNextRow();
                        sceneObjectIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeTrackNodes(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Track Node");
                writer.WriteNextCol("Track Point");
                writer.WriteNextColNicify(nameof(Checkpoint.CurveTimeStart));
                writer.WriteNextColNicify(nameof(Checkpoint.CurveTimeEnd));
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.distance));
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.normal) + ".x");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.normal) + ".y");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.normal) + ".z");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.origin) + ".x");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.origin) + ".y");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneStart.origin) + ".z");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.distance));
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.normal) + ".x");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.normal) + ".y");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.normal) + ".z");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.origin) + ".x");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.origin) + ".y");
                writer.WriteNextColNicify(nameof(Checkpoint.PlaneEnd.origin) + ".z");
                writer.WriteNextColNicify(nameof(Checkpoint.StartDistance));
                writer.WriteNextColNicify(nameof(Checkpoint.EndDistance));
                writer.WriteNextColNicify(nameof(Checkpoint.TrackWidth));
                writer.WriteNextColNicify(nameof(Checkpoint.ConnectToTrackIn));
                writer.WriteNextColNicify(nameof(Checkpoint.ConnectToTrackOut));
                //writer.WriteNextColNicify(nameof(Checkpoint.zero_0x4E));
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int nodeLength = scene.trackNodes.Length;
                    int nodeIndex = 0;
                    foreach (var trackNode in scene.trackNodes)
                    {
                        int pointLength = trackNode.Checkpoints.Length;
                        int pointIndex = 0;
                        foreach (var trackPoint in trackNode.Checkpoints)
                        {
                            writer.WriteNextCol($"COLI_COURSE{scene.CourseIndex:d2}");
                            writer.WriteNextCol($"[{nodeIndex}/{nodeLength}]");
                            writer.WriteNextCol($"[{pointIndex}/{pointLength}]");

                            writer.WriteNextCol(trackPoint.CurveTimeStart);
                            writer.WriteNextCol(trackPoint.CurveTimeEnd);
                            writer.WriteNextCol(trackPoint.PlaneStart.distance);
                            writer.WriteNextCol(trackPoint.PlaneStart.normal.x);
                            writer.WriteNextCol(trackPoint.PlaneStart.normal.y);
                            writer.WriteNextCol(trackPoint.PlaneStart.normal.z);
                            writer.WriteNextCol(trackPoint.PlaneStart.origin.x);
                            writer.WriteNextCol(trackPoint.PlaneStart.origin.y);
                            writer.WriteNextCol(trackPoint.PlaneStart.origin.z);
                            writer.WriteNextCol(trackPoint.PlaneEnd.distance);
                            writer.WriteNextCol(trackPoint.PlaneEnd.normal.x);
                            writer.WriteNextCol(trackPoint.PlaneEnd.normal.y);
                            writer.WriteNextCol(trackPoint.PlaneEnd.normal.z);
                            writer.WriteNextCol(trackPoint.PlaneEnd.origin.x);
                            writer.WriteNextCol(trackPoint.PlaneEnd.origin.y);
                            writer.WriteNextCol(trackPoint.PlaneEnd.origin.z);
                            writer.WriteNextCol(trackPoint.StartDistance);
                            writer.WriteNextCol(trackPoint.EndDistance);
                            writer.WriteNextCol(trackPoint.TrackWidth);
                            writer.WriteNextCol(trackPoint.ConnectToTrackIn);
                            writer.WriteNextCol(trackPoint.ConnectToTrackOut);
                            //writer.WriteNextCol(trackPoint.zero_0x4E);
                            writer.WriteNextRow();

                            pointIndex++;
                        }
                        nodeIndex++;
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeStaticColliderMeshes(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
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
                writer.WriteNextColNicify(nameof(BoundingSphere.origin) + ".x");
                writer.WriteNextColNicify(nameof(BoundingSphere.origin) + ".x");
                writer.WriteNextColNicify(nameof(BoundingSphere.origin) + ".x");
                writer.WriteNextColNicify(nameof(BoundingSphere.radius));
                writer.WriteNextRow();

                int index = 0;
                foreach (var scene in scenes)
                {
                    var staticColliderMeshes = scene.staticColliderMeshManager;

                    writer.WriteNextCol($"COLI_COURSE{scene.CourseIndex:d2}");
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
                    writer.WriteNextCol(staticColliderMeshes.BoundingSphere.origin.x);
                    writer.WriteNextCol(staticColliderMeshes.BoundingSphere.origin.y);
                    writer.WriteNextCol(staticColliderMeshes.BoundingSphere.origin.z);
                    writer.WriteNextCol(staticColliderMeshes.BoundingSphere.radius);
                    writer.WriteNextRow();
                }
                writer.Flush();
            }
        }

        public static void AnalyzeSceneObjectLODs(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol("name");
                writer.WriteNextCol("Object Type");
                writer.WriteNextCol("Addr");
                //writer.WriteNextCol(nameof(SceneObjectLOD.zero_0x00));
                writer.WriteNextCol(nameof(SceneObjectLOD.LodNamePtr));
                //writer.WriteNextCol(nameof(SceneObjectLOD.zero_0x08));
                writer.WriteNextCol(nameof(SceneObjectLOD.LodDistance));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    // Get all the scene object references
                    var sceneObjectLODs = new List<SceneObjectLOD>();
                    foreach (var templateSceneObject in scene.sceneObjects)
                    {
                        var sceneObjects = templateSceneObject.LODs;
                        foreach (var sceneObject in sceneObjects)
                            sceneObjectLODs.Add(sceneObject);
                    }
                    //foreach (var staticSceneObject in scene.staticSceneObjects)
                    //{
                    //    var sceneObjects = staticSceneObject.templateSceneObject.sceneObjects;
                    //    foreach (var sceneObject in sceneObjects)
                    //        objectsList.Add((sceneObject, "Instance"));
                    //}

                    // iterate
                    foreach (var sceneObjectLOD in sceneObjectLODs)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        writer.WriteNextCol(sceneObjectLOD.Name);
                        writer.WriteNextCol(sceneObjectLOD.AddressRange.PrintStartAddress());
                        //writer.WriteNextCol(sceneObjectReference.zero_0x00);
                        writer.WriteNextCol(sceneObjectLOD.LodNamePtr);
                        //writer.WriteNextCol(sceneObjectReference.zero_0x08);
                        writer.WriteNextCol(sceneObjectLOD.LodDistance);
                        //
                        writer.WriteNextRow();
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeSceneObjects(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol("name");
                writer.WriteNextCol("Object Type");
                writer.WriteNextCol("Addr");
                writer.WriteNextCol(nameof(SceneObject.LodRenderFlags));
                writer.WriteNextCol(nameof(SceneObject.LodsPtr));
                writer.WriteNextCol(nameof(SceneObject.ColliderMeshPtr));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    // Get all the scene object references
                    var sceneObjectsList = new List<(SceneObject sir, string category)>();
                    foreach (var sceneInstance in scene.sceneObjects)
                    {
                        sceneObjectsList.Add((sceneInstance, "Instance"));
                    }
                    foreach (var sceneOriginObject in scene.staticSceneObjects)
                    {
                        var sceneInstance = sceneOriginObject.SceneObject;
                        sceneObjectsList.Add((sceneInstance, "Origin"));
                    }

                    // iterate
                    foreach (var sceneObject in sceneObjectsList)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        writer.WriteNextCol(sceneObject.sir.PrimaryLOD.Name);
                        writer.WriteNextCol(sceneObject.category);
                        writer.WriteNextCol(sceneObject.sir.AddressRange.PrintStartAddress());
                        writer.WriteNextCol(sceneObject.sir.LodRenderFlags);
                        writer.WriteNextCol(sceneObject.sir.LodsPtr);
                        writer.WriteNextCol(sceneObject.sir.ColliderMeshPtr);
                        //
                        writer.WriteNextRow();
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeSceneObjectsAndLODs(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
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

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    foreach (var template in scene.sceneObjects)
                    {
                        var index = 0;
                        var length = template.LODs.Length;
                        foreach (var sceneObject in template.LODs)
                        {
                            writer.WriteNextCol(scene.FileName);
                            writer.WriteNextCol(scene.CourseIndex);
                            writer.WriteNextCol(venueID);
                            writer.WriteNextCol(courseID);
                            writer.WriteNextCol(isAxGx);
                            //
                            writer.WriteNextCol(template.Name);
                            writer.WriteNextCol(template.LodRenderFlags);
                            writer.WriteNextCol(template.LodsPtr.length);
                            writer.WriteNextCol(template.LodsPtr.PrintAddress);
                            writer.WriteNextCol(template.ColliderMeshPtr);
                            writer.WriteNextCol($"[{++index}/{length}]");
                            //writer.WriteNextCol(sceneObject.zero_0x00);
                            writer.WriteNextCol(sceneObject.LodNamePtr);
                            //writer.WriteNextCol(sceneObject.zero_0x08);
                            writer.WriteNextCol(sceneObject.LodDistance);
                            writer.WriteNextCol(sceneObject.Name);
                            writer.WriteNextRow();
                        }
                    }
                }
                writer.Flush();
            }
        }


        public static void AnalyzeGeneralData(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol(nameof(ViewRange) + "." + nameof(ViewRange.near));
                writer.WriteNextCol(nameof(ViewRange) + "." + nameof(ViewRange.far));
                writer.WriteNextCol(nameof(Scene.trackMinHeight));
                writer.WriteNextCol(nameof(Scene.trackLength));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    writer.WriteNextCol(scene.FileName);
                    writer.WriteNextCol(scene.CourseIndex);
                    writer.WriteNextCol(venueID);
                    writer.WriteNextCol(courseID);
                    writer.WriteNextCol(isAxGx);
                    //
                    writer.WriteNextCol(scene.UnkRange0x00.near);
                    writer.WriteNextCol(scene.UnkRange0x00.far);
                    writer.WriteNextCol(scene.trackMinHeight.Value);
                    writer.WriteNextCol(scene.trackLength.Value);
                    writer.WriteNextRow();
                }
                writer.Flush();
            }
        }

        public static void AnalyzeSurfaceAttributeAreas(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.LengthFrom));
                writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.LengthTo));
                writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.WidthLeft));
                writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.WidthRight));
                writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.PropertyType));
                writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.TrackBranchID));
                //writer.WriteNextCol(nameof(EmbeddedTrackPropertyArea.zero_0x12));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    foreach (var surfaceAttributeArea in scene.embeddedPropertyAreas)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        writer.WriteNextCol(surfaceAttributeArea.LengthFrom);
                        writer.WriteNextCol(surfaceAttributeArea.LengthTo);
                        writer.WriteNextCol(surfaceAttributeArea.WidthLeft);
                        writer.WriteNextCol(surfaceAttributeArea.WidthRight);
                        writer.WriteNextCol(surfaceAttributeArea.PropertyType);
                        writer.WriteNextCol(surfaceAttributeArea.TrackBranchID);
                        //writer.WriteNextCol(surfaceAttributeArea.zero_0x12);
                        //
                        writer.WriteNextRow();
                    }
                }
                writer.Flush();
            }
        }


        public static void AnalyzeUnknownCollider(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Index");
                writer.WriteNextCol("Venue");
                writer.WriteNextCol("Course");
                writer.WriteNextCol("AX/GX");
                //
                writer.WriteNextCol(nameof(UnknownCollider.SceneObjectPtr));
                writer.WriteNextCol(nameof(UnknownCollider.Transform.Position));
                writer.WriteNextCol(nameof(UnknownCollider.Transform.RotationEuler));
                writer.WriteNextCol(nameof(UnknownCollider.Transform.Scale));
                writer.WriteNextCol(nameof(UnknownCollider.Transform.UnknownOption));
                //
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    var venueID = CourseUtility.GetVenueID(scene.CourseIndex).GetDescription();
                    var courseID = ((CourseIndexAX)scene.CourseIndex).GetDescription();
                    var isAxGx = scene.IsFileGX ? "GX" : "AX";

                    foreach (var unkSols in scene.unknownColliders)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteNextCol(scene.CourseIndex);
                        writer.WriteNextCol(venueID);
                        writer.WriteNextCol(courseID);
                        writer.WriteNextCol(isAxGx);
                        //
                        writer.WriteNextCol(unkSols.SceneObjectPtr);
                        writer.WriteNextCol(unkSols.Transform.Position);
                        writer.WriteNextCol(unkSols.Transform.RotationEuler);
                        writer.WriteNextCol(unkSols.Transform.Scale);
                        writer.WriteNextCol(unkSols.Transform.UnknownOption);
                        //
                        writer.WriteNextRow();
                    }
                    writer.Flush();
                }
            }
        }



        public static void AnalyzeStaticColliderTriangles(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Addr");
                writer.WriteNextCol("Tri Index");
                writer.WriteNextColNicify(nameof(ColliderTriangle.PlaneDistance));
                writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Normal) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.Vertex2) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderTriangle.EdgeNormal2) + ".z");
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int triIndex = 0;
                    foreach (var tri in scene.staticColliderMeshManager.ColliderTris)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteStartAddress(tri);
                        writer.WriteNextCol(triIndex++);
                        writer.WriteNextCol(tri.PlaneDistance);
                        writer.WriteNextCol(tri.Normal.x);
                        writer.WriteNextCol(tri.Normal.y);
                        writer.WriteNextCol(tri.Normal.z);
                        writer.WriteNextCol(tri.Vertex0.x);
                        writer.WriteNextCol(tri.Vertex0.y);
                        writer.WriteNextCol(tri.Vertex0.z);
                        writer.WriteNextCol(tri.Vertex1.x);
                        writer.WriteNextCol(tri.Vertex1.y);
                        writer.WriteNextCol(tri.Vertex1.z);
                        writer.WriteNextCol(tri.Vertex2.x);
                        writer.WriteNextCol(tri.Vertex2.y);
                        writer.WriteNextCol(tri.Vertex2.z);
                        writer.WriteNextCol(tri.EdgeNormal0.x);
                        writer.WriteNextCol(tri.EdgeNormal0.y);
                        writer.WriteNextCol(tri.EdgeNormal0.z);
                        writer.WriteNextCol(tri.EdgeNormal1.x);
                        writer.WriteNextCol(tri.EdgeNormal1.y);
                        writer.WriteNextCol(tri.EdgeNormal1.z);
                        writer.WriteNextCol(tri.EdgeNormal2.x);
                        writer.WriteNextCol(tri.EdgeNormal2.y);
                        writer.WriteNextCol(tri.EdgeNormal2.z);
                        writer.WriteNextRow();
                    }
                }
                writer.Flush();
            }
        }

        public static void AnalyzeStaticColliderQuads(Scene[] scenes, string fileName)
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                // Write header
                writer.WriteNextCol("File");
                writer.WriteNextCol("Addr");
                writer.WriteNextCol("Quad Index");
                writer.WriteNextColNicify(nameof(ColliderQuad.PlaneDistance));
                writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Normal) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex2) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.Vertex3) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal0) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal1) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal2) + ".z");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".x");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".y");
                writer.WriteNextColNicify(nameof(ColliderQuad.EdgeNormal3) + ".z");
                writer.WriteNextRow();

                foreach (var scene in scenes)
                {
                    int quadIndex = 0;
                    foreach (var quad in scene.staticColliderMeshManager.ColliderQuads)
                    {
                        writer.WriteNextCol(scene.FileName);
                        writer.WriteStartAddress(quad);
                        writer.WriteNextCol(quadIndex++);
                        writer.WriteNextCol(quad.PlaneDistance);
                        writer.WriteNextCol(quad.Normal.x);
                        writer.WriteNextCol(quad.Normal.y);
                        writer.WriteNextCol(quad.Normal.z);
                        writer.WriteNextCol(quad.Vertex0.x);
                        writer.WriteNextCol(quad.Vertex0.y);
                        writer.WriteNextCol(quad.Vertex0.z);
                        writer.WriteNextCol(quad.Vertex1.x);
                        writer.WriteNextCol(quad.Vertex1.y);
                        writer.WriteNextCol(quad.Vertex1.z);
                        writer.WriteNextCol(quad.Vertex2.x);
                        writer.WriteNextCol(quad.Vertex2.y);
                        writer.WriteNextCol(quad.Vertex2.z);
                        writer.WriteNextCol(quad.Vertex3.x);
                        writer.WriteNextCol(quad.Vertex3.y);
                        writer.WriteNextCol(quad.Vertex3.z);
                        writer.WriteNextCol(quad.EdgeNormal0.x);
                        writer.WriteNextCol(quad.EdgeNormal0.y);
                        writer.WriteNextCol(quad.EdgeNormal0.z);
                        writer.WriteNextCol(quad.EdgeNormal1.x);
                        writer.WriteNextCol(quad.EdgeNormal1.y);
                        writer.WriteNextCol(quad.EdgeNormal1.z);
                        writer.WriteNextCol(quad.EdgeNormal2.x);
                        writer.WriteNextCol(quad.EdgeNormal2.y);
                        writer.WriteNextCol(quad.EdgeNormal2.z);
                        writer.WriteNextCol(quad.EdgeNormal3.x);
                        writer.WriteNextCol(quad.EdgeNormal3.y);
                        writer.WriteNextCol(quad.EdgeNormal3.z);
                        writer.WriteNextRow();
                    }
                }
                writer.Flush();
            }
        }

    }
}
