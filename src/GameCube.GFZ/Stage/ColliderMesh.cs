using Manifold;
using Manifold.IO;
using System;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// Represent a "dynamic" collider object. Static collision is stored in a
    /// separate table, while colliders attached to object (which may animate)
    /// is stored in this structure.
    /// </summary>
    /// <remarks>
    /// Example: rotary collider in Port Town [Long Pipe]
    /// </remarks>
    [Serializable]
    public sealed class ColliderMesh :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // CONSTANTS
        public const int kTriIndex = 0;
        public const int kQuadIndex = 1;
        public const int kTotalIndices = 2;

        // FIELDS
        private ColliderMeshType colliderType;
        private BoundingSphere boundingSphere;
        private ArrayPointer2D collisionArrayPtr2D = new ArrayPointer2D(kTotalIndices);
        // REFERENCE FIELDS
        private ColliderTriangle[] tris;
        private ColliderQuad[] quads;

        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public ArrayPointer2D CollisionArrayPtr2D { get => collisionArrayPtr2D; set => collisionArrayPtr2D = value; }
        public ColliderTriangle[] Tris { get => tris; set => tris = value; }
        public ArrayPointer TrisPtr { get => collisionArrayPtr2D[kTriIndex]; set => collisionArrayPtr2D[kTriIndex] = value; }
        public ColliderQuad[] Quads { get => quads; set => quads = value; }
        public ArrayPointer QuadsPtr { get => collisionArrayPtr2D[kQuadIndex]; set => collisionArrayPtr2D[kQuadIndex] = value; }
        public ColliderMeshType ColliderType { get => colliderType; set => colliderType = value; }
        public BoundingSphere BoundingSphere { get => boundingSphere; set => boundingSphere = value; }


        // METHODS
        public void ValidateReferences()
        {
            Assert.ReferencePointer(Tris, TrisPtr);
            Assert.ReferencePointer(Quads, QuadsPtr);

            // SANITY CHECK
            // Make sure counts line up
            if (Tris != null)
            {
                if (Tris.Length > 0)
                {
                    Assert.IsTrue(TrisPtr.length == Tris.Length);
                    Assert.IsTrue(TrisPtr.IsNotNull);

                    foreach (var tri in Tris)
                    {
                        Assert.IsTrue(tri != null);
                    }
                }
            }

            if (Quads != null)
            {
                if (Quads.Length > 0)
                {
                    Assert.IsTrue(QuadsPtr.length == Quads.Length);
                    Assert.IsTrue(QuadsPtr.IsNotNull);

                    foreach (var quad in Quads)
                    {
                        Assert.IsTrue(quad != null);
                    }
                }
            }
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            {
                // Initialize ArrayPointer2D with constant size, manually deserialized later
                collisionArrayPtr2D = new ArrayPointer2D(kTotalIndices);
            }
            this.RecordStartAddress(reader);
            {
                reader.Read(ref colliderType);
                reader.Read(ref boundingSphere);
                collisionArrayPtr2D.Deserialize(reader);
            }
            this.RecordEndAddress(reader);
            {
                if (TrisPtr.IsNotNull)
                {
                    reader.JumpToAddress(TrisPtr);
                    reader.Read(ref tris, TrisPtr.length);
                }

                if (QuadsPtr.IsNotNull)
                {
                    reader.JumpToAddress(QuadsPtr);
                    reader.Read(ref quads, QuadsPtr.length);
                }
            }
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                Assert.IsTrue(collisionArrayPtr2D.Length == kTotalIndices);
                //
                TrisPtr = tris.GetArrayPointer();
                QuadsPtr = quads.GetArrayPointer();
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(colliderType);
                writer.Write(boundingSphere);
                writer.Write(collisionArrayPtr2D);
            }
            this.RecordEndAddress(writer);

        }

        public override string ToString() => PrintSingleLine();

        public string PrintSingleLine()
        {
            return $"{nameof(ColliderMesh)}({nameof(tris)}: {tris.Length}, {nameof(quads)}: {quads.Length})";
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(ColliderMesh));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(colliderType)}: {colliderType}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(boundingSphere)}: {boundingSphere}");
            //builder.AppendLineIndented(indent, indentLevel, $"{nameof(TrisPtr)}: {TrisPtr}");
            //builder.AppendLineIndented(indent, indentLevel, $"{nameof(QuadsPtr)}: {QuadsPtr}");
            int trisLength = tris.IsNullOrEmpty() ? 0 : tris.Length;
            int quadsLength = quads.IsNullOrEmpty() ? 0 : quads.Length;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Tris)}: {trisLength} triangles");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Quads)}: {quadsLength} quads");
        }
    }
}