using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Represents a complex scene object that can have various properties.
    /// </summary>
    [Serializable]
    public sealed class SceneObjectDynamic :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // FIELDS
        private ObjectRenderFlags0x00 unk0x00;
        private ObjectRenderFlags0x04 unk0x04;
        private Pointer sceneObjectPtr;
        private TransformTRXS transformTRXS = new();
        private int zero_0x2C; // null ptr?
        private Pointer animationClipPtr;
        private Pointer textureScrollPtr;
        private Pointer skeletalAnimatorPtr;
        private Pointer transformMatrix3x4Ptr;
        // FIELDS (deserialized from pointers)
        private SceneObject sceneObject;
        private AnimationClip animationClip;
        private TextureScroll textureScroll;
        private SkeletalAnimator skeletalAnimator;
        private TransformMatrix3x4 transformMatrix3x4;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public ShiftJisCString Name => SceneObject.Name;

        public ObjectRenderFlags0x00 Unk0x00 { get => unk0x00; set => unk0x00 = value; }
        public ObjectRenderFlags0x04 Unk0x04 { get => unk0x04; set => unk0x04 = value; }
        public Pointer SceneObjectPtr { get => sceneObjectPtr; set => sceneObjectPtr = value; }
        public TransformTRXS TransformTRXS { get => transformTRXS; set => transformTRXS = value; }
        public Pointer AnimationClipPtr { get => animationClipPtr; set => animationClipPtr = value; }
        public Pointer TextureScrollPtr { get => textureScrollPtr; set => textureScrollPtr = value; }
        public Pointer SkeletalAnimatorPtr { get => skeletalAnimatorPtr; set => skeletalAnimatorPtr = value; }
        public Pointer TransformMatrix3x4Ptr { get => transformMatrix3x4Ptr; set => transformMatrix3x4Ptr = value; }
        public SceneObject SceneObject { get => sceneObject; set => sceneObject = value; }
        public AnimationClip AnimationClip { get => animationClip; set => animationClip = value; }
        public TextureScroll TextureScroll { get => textureScroll; set => textureScroll = value; }
        public SkeletalAnimator SkeletalAnimator { get => skeletalAnimator; set => skeletalAnimator = value; }
        public TransformMatrix3x4 TransformMatrix3x4 { get => transformMatrix3x4; set => transformMatrix3x4 = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref unk0x00);
                reader.Read(ref unk0x04);
                reader.Read(ref sceneObjectPtr);
                reader.Read(ref transformTRXS);
                reader.Read(ref zero_0x2C);
                reader.Read(ref animationClipPtr);
                reader.Read(ref textureScrollPtr);
                reader.Read(ref skeletalAnimatorPtr);
                reader.Read(ref transformMatrix3x4Ptr);
            }
            this.RecordEndAddress(reader);
            {
                //
                reader.JumpToAddress(SceneObjectPtr);
                reader.Read(ref sceneObject);

                if (AnimationClipPtr.IsNotNull)
                {
                    reader.JumpToAddress(AnimationClipPtr);
                    reader.Read(ref animationClip);
                }

                if (TextureScrollPtr.IsNotNull)
                {
                    reader.JumpToAddress(TextureScrollPtr);
                    reader.Read(ref textureScroll);
                }

                if (SkeletalAnimatorPtr.IsNotNull)
                {
                    reader.JumpToAddress(SkeletalAnimatorPtr);
                    reader.Read(ref skeletalAnimator);
                }

                // 1518 objects without a transform
                // They appear to use animation, so the matrix is null
                // They do have a "normal" transform, though
                if (TransformMatrix3x4Ptr.IsNotNull)
                {
                    reader.JumpToAddress(TransformMatrix3x4Ptr);
                    reader.Read(ref transformMatrix3x4);
                }

                // Assert pointer and the like
                ValidateReferences();
            }
            // After deserializing sub-structures, return to end position
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                // Get pointers from refered instances
                sceneObjectPtr = sceneObject.GetPointer();
                animationClipPtr = animationClip.GetPointer();
                textureScrollPtr = textureScroll.GetPointer();
                skeletalAnimatorPtr = skeletalAnimator.GetPointer();
                transformMatrix3x4Ptr = transformMatrix3x4.GetPointer();
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(unk0x00);
                writer.Write(unk0x04);
                writer.Write(sceneObjectPtr);
                writer.Write(transformTRXS);
                writer.Write(zero_0x2C);
                writer.Write(animationClipPtr);
                writer.Write(textureScrollPtr);
                writer.Write(skeletalAnimatorPtr);
                writer.Write(transformMatrix3x4Ptr);
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            // This pointer CANNOT be null and must refer to an object.
            Assert.IsTrue(SceneObject != null);
            Assert.IsTrue(SceneObjectPtr.IsNotNull, $"{Name}: {sceneObjectPtr.PrintAddress}");
            Assert.ReferencePointer(SceneObject, SceneObjectPtr);
            // This should always exist
            Assert.IsTrue(TransformTRXS != null);

            // Optional data
            Assert.ReferencePointer(AnimationClip, AnimationClipPtr);
            Assert.ReferencePointer(TextureScroll, TextureScrollPtr);
            Assert.ReferencePointer(SkeletalAnimator, SkeletalAnimatorPtr);
            Assert.ReferencePointer(TransformMatrix3x4, TransformMatrix3x4Ptr);

            // Constants 
            Assert.IsTrue(zero_0x2C == 0);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(SceneObjectDynamic));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Name)}: {Name}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Unk0x00)}: {Unk0x00}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Unk0x04)}: {Unk0x04}");
            builder.AppendMultiLineIndented(indent, indentLevel, transformTRXS);
            builder.AppendMultiLineIndented(indent, indentLevel, sceneObject);
            builder.AppendMultiLineIndented(indent, indentLevel, animationClip);
            builder.AppendMultiLineIndented(indent, indentLevel, textureScroll);
            builder.AppendMultiLineIndented(indent, indentLevel, skeletalAnimator);
            builder.AppendMultiLineIndented(indent, indentLevel, transformMatrix3x4);
        }

        public string PrintSingleLine()
        {
            return $"{nameof(SceneObjectDynamic)}({Name})";
        }

        public override string ToString() => PrintSingleLine();

    }
}