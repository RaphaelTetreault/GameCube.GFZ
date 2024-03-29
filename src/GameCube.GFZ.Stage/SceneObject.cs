﻿using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Stage
{
    // Regarding LODs: LODs appear to be ordered using 2 criteria. First, they are sorted alphabetically by name.
    // Then, they are sorted by LOD distance ASCENDING. So an example sorting looks like the following. However,
    // if this is a hard fact, I do not know. The only thing that is consistent is using a null name after to denote
    // reusing the same model. In such a case, as noted above, the distance is ascending.
    //
    // SAMPLE 1 (ST01)
    // 30	PODHOUSE02_A_LOD
    // 10	PODHOUSE02_B_LOD
    // 5	PODHOUSE02_C_LOD
    // 10	[string.Empty]
    //
    // SAMPLE 2 (confusing sample from ST01, consider WTF)
    // 0	STADIUM_CEILING_LOD
    // 351	STADIUM_CEILING_A_LOD
    // 61	STADIUM_CEILING_B_LOD
    // 82	[string.Empty]

    /// <summary>
    /// An object to display in a scene. Refers to LODs.
    /// </summary>
    [Serializable]
    public sealed class SceneObject :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // STRUCTURE
        private LodRenderFlags lodRenderFlags;
        private ArrayPointer lodsPtr;
        private Pointer colliderGeometryPtr;
        // FIELDS (deserialized from pointers)
        private SceneObjectLOD[] lods;
        private ColliderMesh colliderMesh;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public ShiftJisCString Name => lods[0].Name;
        public SceneObjectLOD PrimaryLOD => lods[0];
        public SceneObjectLOD[] LODs { get => lods; set => lods = value; }
        public ColliderMesh ColliderMesh { get => colliderMesh; set => colliderMesh = value; }
        public Pointer ColliderMeshPtr { get => colliderGeometryPtr; set => colliderGeometryPtr = value; }
        public ArrayPointer LodsPtr { get => lodsPtr; set => lodsPtr = value; }
        public LodRenderFlags LodRenderFlags { get => lodRenderFlags; set => lodRenderFlags = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref lodRenderFlags);
                reader.Read(ref lodsPtr);
                reader.Read(ref colliderGeometryPtr);
            }
            this.RecordEndAddress(reader);
            {
                Assert.IsTrue(lodsPtr.IsNotNull);
                reader.JumpToAddress(lodsPtr);
                reader.Read(ref lods, lodsPtr.length);

                // Collision is not required, load only if pointer is not null
                if (colliderGeometryPtr.IsNotNull)
                {
                    reader.JumpToAddress(colliderGeometryPtr);
                    reader.Read(ref colliderMesh);
                }
            }
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                lodsPtr = LODs.GetArrayPointer();
                colliderGeometryPtr = colliderMesh.GetPointer();
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(lodRenderFlags);
                writer.Write(lodsPtr);
                writer.Write(colliderGeometryPtr);
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            // This pointer CANNOT be null and must refer to an object.
            Assert.IsTrue(LODs != null);
            Assert.IsTrue(lodsPtr.IsNotNull);
            // Assert that instance/pointer is correct
            Assert.ReferencePointer(LODs, lodsPtr);
            Assert.ReferencePointer(colliderMesh, colliderGeometryPtr);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(SceneObject));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(LodRenderFlags)}: {LodRenderFlags}");
            builder.AppendLineIndented(indent, indentLevel, $"Has {nameof(ColliderMesh)}: {ColliderMesh is not null}");
            if (colliderMesh is not null)
            {
                builder.AppendMultiLineIndented(indent, indentLevel, ColliderMesh);
            }
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(LODs)}[{LODs.Length}]");
            indentLevel++;
            int index = 0;
            foreach (var lod in LODs)
            {
                builder.AppendLineIndented(indent, indentLevel,
                    $"[{index}]\t" +
                    $"{nameof(lod.Name)}: {lod.Name}, " +
                    $"{nameof(lod.LodDistance)}: {lod.LodDistance}");
            }
        }

        public string PrintSingleLine()
        {
            return $"{nameof(SceneObject)}({Name})";
        }

        public override string ToString() => PrintSingleLine();

    }
}