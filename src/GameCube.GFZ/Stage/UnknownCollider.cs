using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Collider volume only available in Sand Ocean [Lateral Shift]. It is known to
    /// be a collider since has a reference stored in StaticColliderMeshManager.
    /// </summary>
    /// <remarks>
    /// GX: 6 instances, AX: 9 instances
    /// </remarks>
    [Serializable]
    public sealed class UnknownCollider :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // FIELDS
        private Pointer sceneObjectPtr;
        private TransformTRXS transform;
        // REFERENCE FIELDS
        private SceneObject sceneObject;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public SceneObject SceneObject { get => sceneObject; set => sceneObject = value; }
        public Pointer SceneObjectPtr { get => sceneObjectPtr; set => sceneObjectPtr = value; }
        public TransformTRXS Transform { get => transform; set => transform = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref sceneObjectPtr);
                reader.Read(ref transform);
            }
            this.RecordEndAddress(reader);
            {
                if (sceneObjectPtr.IsNotNull)
                {
                    reader.JumpToAddress(sceneObjectPtr);
                    reader.Read(ref sceneObject);
                }
            }
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                sceneObjectPtr = sceneObject.GetPointer();
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(sceneObjectPtr);
                writer.Write(transform);
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            Assert.ReferencePointer(sceneObject, sceneObjectPtr);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(UnknownCollider));
            indentLevel++;
            builder.AppendMultiLineIndented(indent, indentLevel, Transform);
        }

        public string PrintSingleLine()
        {
            return nameof(UnknownCollider);
        }

        public override string ToString() => PrintSingleLine();

    }
}
