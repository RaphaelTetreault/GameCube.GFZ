﻿using Manifold;
using Manifold.IO;
using System;
using System.IO;


namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Defines the tracks position, rotation, and scale using 9 animation curves,
    /// each defining the X, Y, and Z properties. Their values are (assummed to be)
    /// multiplied with an associated Transform. Hierarchies of these exists, each
    /// being multiplied with it's parent up a tree structure.
    /// </summary>
    [Serializable]
    public sealed class AnimationCurveTRS :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // CONSTANTS
        public const int kCurveCount = 9;


        // FIELDS
        private ArrayPointer2D animationCurvesPtr2D = new ArrayPointer2D(kCurveCount);
        // REFERENCE FIELDS
        private AnimationCurve[] animationCurves = new AnimationCurve[kCurveCount]
        {
            new(), new(), new(), new(), new(), new(), new(), new(), new(),
        };


        // INDEXERS
        public AnimationCurve this[int index]
        {
            get => animationCurves[index];
            set => animationCurves[index] = value;
        }

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public AnimationCurve[] AnimationCurves { get => animationCurves; set => animationCurves = value; }
        public AnimationCurve[] AnimationCurvesOrderedPRS
        {
            get => new AnimationCurve[]{
                PositionX, PositionY, PositionZ,
                RotationX, RotationY, RotationZ,
                ScaleX, ScaleY, ScaleZ,
            };
        }
        public ArrayPointer2D AnimationCurvesPtr2D { get => animationCurvesPtr2D; set => animationCurvesPtr2D = value; }
        public AnimationCurve PositionX { get => AnimationCurves[6]; set => AnimationCurves[6] = value; }
        public AnimationCurve PositionY { get => AnimationCurves[7]; set => AnimationCurves[7] = value; }
        public AnimationCurve PositionZ { get => AnimationCurves[8]; set => AnimationCurves[8] = value; }
        public AnimationCurve RotationX { get => AnimationCurves[3]; set => AnimationCurves[3] = value; }
        public AnimationCurve RotationY { get => AnimationCurves[4]; set => AnimationCurves[4] = value; }
        public AnimationCurve RotationZ { get => AnimationCurves[5]; set => AnimationCurves[5] = value; }
        public AnimationCurve ScaleX { get => AnimationCurves[0]; set => AnimationCurves[0] = value; }
        public AnimationCurve ScaleY { get => AnimationCurves[1]; set => AnimationCurves[1] = value; }
        public AnimationCurve ScaleZ { get => AnimationCurves[2]; set => AnimationCurves[2] = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                animationCurvesPtr2D.Deserialize(reader);
            }
            this.RecordEndAddress(reader);
            {
                // Init array
                AnimationCurves = new AnimationCurve[kCurveCount];

                for (int i = 0; i < AnimationCurves.Length; i++)
                {
                    var arrayPointer = animationCurvesPtr2D.ArrayPointers[i];
                    if (arrayPointer.IsNotNull)
                    {
                        // Deserialization is done to instance with properties set through constructor.
                        reader.JumpToAddress(arrayPointer);
                        var animationCurve = new AnimationCurve(arrayPointer.length);
                        animationCurve.Deserialize(reader);

                        // Assign curve to array
                        AnimationCurves[i] = animationCurve;
                    }
                    else
                    {
                        AnimationCurves[i] = new AnimationCurve(0);
                    }
                }
            }
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                // Ensure we have the correct amount of animation curves before indexing
                Assert.IsTrue(AnimationCurves.Length == kCurveCount);

                // Construct ArrayPointer2D for animation curves
                var pointers = new ArrayPointer[kCurveCount];
                for (int i = 0; i < pointers.Length; i++)
                    pointers[i] = AnimationCurves[i].GetArrayPointer();

                animationCurvesPtr2D = new ArrayPointer2D(pointers);
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(animationCurvesPtr2D);
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            // Assert array information
            Assert.IsTrue(AnimationCurves != null);
            Assert.IsTrue(AnimationCurves.Length == kCurveCount);
            Assert.IsTrue(AnimationCurvesPtr2D.Length == kCurveCount);

            // Assert each animation curve
            for (int i = 0; i < kCurveCount; i++)
            {
                var animCurve = AnimationCurves[i];
                var pointer = AnimationCurvesPtr2D.ArrayPointers[i];

                // Only assert if there are keyables
                if (animCurve.Length != 0)
                    Assert.ReferencePointer(animCurve, pointer);
            }
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(AnimationCurveTRS));
            indentLevel++;
            WriteKeyables(builder, PositionX, nameof(PositionX), indentLevel, indent);
            WriteKeyables(builder, PositionY, nameof(PositionY), indentLevel, indent);
            WriteKeyables(builder, PositionZ, nameof(PositionZ), indentLevel, indent);
            WriteKeyables(builder, RotationX, nameof(RotationX), indentLevel, indent);
            WriteKeyables(builder, RotationY, nameof(RotationY), indentLevel, indent);
            WriteKeyables(builder, RotationZ, nameof(RotationZ), indentLevel, indent);
            WriteKeyables(builder, ScaleX, nameof(ScaleX), indentLevel, indent);
            WriteKeyables(builder, ScaleY, nameof(ScaleY), indentLevel, indent);
            WriteKeyables(builder, ScaleZ, nameof(ScaleZ), indentLevel, indent);
        }

        public string PrintSingleLine()
        {
            return nameof(AnimationCurveTRS);
        }

        public override string ToString() => PrintSingleLine();

        private void WriteKeyables(System.Text.StringBuilder builder, AnimationCurve curve, string heading, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, $"{heading}[{curve.Length}]");
            int index = 0;
            foreach (var keyableAttribute in curve.KeyableAttributes)
            {
                var keyableInfoCondensed = $"[{index++,3}] {keyableAttribute.PrintKeyableCondensed()}";
                builder.AppendLineIndented(indent, indentLevel + 1, keyableInfoCondensed);
            }
        }
    }
}
