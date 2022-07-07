using Manifold;
using Manifold.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// The structure representing the model mesh, submeshes, and materials.
    /// </summary>
    public class Gcmf :
        IBinaryAddressable,
        IBinarySerializable
    {
        // CONSTANTS
        /// <summary>
        /// Equivilent to ASCII/Shift-JIS "GCMF"
        /// </summary>
        public const uint kMagic = 0x47434D46;


        // FIELDS
        private uint magic;
        private GcmfAttributes attributes;
        private BoundingSphere boundingSphere;
        private ushort textureCount;
        private ushort opaqueMaterialCount;
        private ushort translucidMaterialCount;
        private byte boneCount;
        private byte zero0x1F;
        private Offset submeshOffsetPtr;
        private uint zero0x24;
        private BoneIndexes8 boneIndices = new BoneIndexes8();
        private TevLayer[] tevLayers = new TevLayer[0];
        private TransformMatrix3x4[] bones = new TransformMatrix3x4[0];
        private SkinnedVertexDescriptor skinnedVertexDescriptor;
        private Submesh[] submeshes = new Submesh[0];
        private SkinnedVertexA[] skinnedVerticesA = new SkinnedVertexA[0];
        private SkinnedVertexB[] skinnedVerticesB = new SkinnedVertexB[0];
        private SkinBoneBinding[] skinBoneBindings = new SkinBoneBinding[0];
        private short[] unkBoneIndices = new short[0];


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public bool IsSkinnedModel
        {
            get => attributes.HasFlag(GcmfAttributes.isSkinModel);
        }
        public bool IsPhysicsDrivenModel
        {
            get => attributes.HasFlag(GcmfAttributes.isEffectiveModel);
        }
        public bool IsStitchingModel
        {
            get => attributes.HasFlag(GcmfAttributes.isStitchingModel);
        }
        public bool Is16bitModel
        {
            get => attributes.HasFlag(GcmfAttributes.is16Bit);
        }

        public int TotalSubmeshCount
        {
            get => opaqueMaterialCount + translucidMaterialCount;
        }
        public Pointer SkinnedDataBaseAddress { get; private set; }
        public Pointer SkinBoneBindingBaseAddress { get; private set; }

        public GcmfAttributes Attributes { get => attributes; set => attributes = value; }
        public BoundingSphere BoundingSphere { get => boundingSphere; set => boundingSphere = value; }
        public ushort TextureConfigsCount { get => textureCount; set => textureCount = value; }
        public ushort OpaqueMaterialCount { get => opaqueMaterialCount; set => opaqueMaterialCount = value; }
        public ushort TranslucidMaterialCount { get => translucidMaterialCount; set => translucidMaterialCount = value; }
        public byte BoneCount { get => boneCount; set => boneCount = value; }
        public Offset SubmeshOffsetPtr { get => submeshOffsetPtr; set => submeshOffsetPtr = value; }
        public BoneIndexes8 BoneIndices { get => boneIndices; set => boneIndices = value; }
        public TransformMatrix3x4[] Bones { get => bones; set => bones = value; }
        public TevLayer[] TevLayers { get => tevLayers; set => tevLayers = value; }
        public SkinnedVertexDescriptor SkinnedVertexDescriptor { get => skinnedVertexDescriptor; set => skinnedVertexDescriptor = value; }
        public Submesh[] Submeshes { get => submeshes; set => submeshes = value; }
        public SkinnedVertexA[] SkinnedVerticesA { get => skinnedVerticesA; set => skinnedVerticesA = value; }
        public SkinnedVertexB[] SkinnedVerticesB { get => skinnedVerticesB; set => skinnedVerticesB = value; }
        public SkinBoneBinding[] SkinBoneBindings { get => skinBoneBindings; set => skinBoneBindings = value; }
        public short[] UnkBoneIndices { get => unkBoneIndices; set => unkBoneIndices = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref magic);
                reader.Read(ref attributes);
                reader.Read(ref boundingSphere);
                reader.Read(ref textureCount);
                reader.Read(ref opaqueMaterialCount);
                reader.Read(ref translucidMaterialCount);
                reader.Read(ref boneCount);
                reader.Read(ref zero0x1F);
                reader.Read(ref submeshOffsetPtr);
                reader.Read(ref zero0x24);
                reader.Read(ref boneIndices);
            }
            this.RecordEndAddress(reader);
            {
                // Align from after main deserialization
                reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);

                // Assert some of the data
                Assert.IsTrue(magic == kMagic);
                Assert.IsTrue(zero0x1F == 0);
                Assert.IsTrue(zero0x24 == 0);

                var submeshStart = AddressRange.startAddress + submeshOffsetPtr;
                if (!Submeshes.IsNullOrEmpty())
                    Assert.IsTrue(submeshStart == Submeshes[0].AddressRange.startAddress);
            }
            // Deserialize other structures
            {
                // Read in texture configs. Between each, align for GX FIFO
                tevLayers = new TevLayer[textureCount];
                for (int i = 0; i < tevLayers.Length; i++)
                {
                    reader.Read(ref tevLayers[i]);
                    reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);
                }

                // Read in bones. If 'transformMatrixCount' is 0, nothing happens
                reader.Read(ref bones, boneCount);
                reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);

                // Check GCMF attributes; some types has special data embedded
                if (IsSkinnedModel || IsPhysicsDrivenModel)
                {
                    reader.Read(ref skinnedVertexDescriptor);
                    reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);

                    // Get base pointer for skinned data offsets
                    SkinnedDataBaseAddress = skinnedVertexDescriptor.AddressRange.startAddress;
                }

                // Read in submeshes. Each submesh contains a single material to be applied
                // to one or more triangle strips. If some special data, may not have any
                // vertices, though.
                submeshes = new Submesh[TotalSubmeshCount];
                for (int i = 0; i < submeshes.Length; i++)
                {
                    submeshes[i] = new Submesh();
                    submeshes[i].Attributes = attributes;
                    submeshes[i].Deserialize(reader);
                }
                //reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);

                // If GCMF is a skinned, we contain the following data.
                if (IsSkinnedModel)
                {
                    //
                    {
                        var address = SkinnedDataBaseAddress + skinnedVertexDescriptor.SkinnedVerticesAPtrOffset;
                        reader.JumpToAddress(address);
                        reader.Read(ref skinnedVerticesA, skinnedVertexDescriptor.SkinnedVerticesACount);
                        // Redundant since the FIFO alignment is the same size as the structure (0x20)
                        //reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);
                    }

                    // 
                    {
                        var address = SkinnedDataBaseAddress + skinnedVertexDescriptor.UnkBoneIndicesPtrOffset;
                        reader.JumpToAddress(address);
                        reader.Read(ref unkBoneIndices, BoneCount);
                        reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);
                    }
                }

                // If GCMF is skinned or physics-driven, we contain the following data.
                if (IsSkinnedModel || IsPhysicsDrivenModel)
                {
                    // Skinned Vertices
                    {
                        var address = SkinnedDataBaseAddress + skinnedVertexDescriptor.SkinnedVerticesBPtrOffset;
                        reader.JumpToAddress(address);
                        reader.Read(ref skinnedVerticesB, skinnedVertexDescriptor.SkinnedVerticesBCount);
                        reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);
                    }

                    // Skin Bone Binding
                    {
                        var address = SkinnedDataBaseAddress + skinnedVertexDescriptor.SkinBoneBindingsPtrOffset;
                        reader.JumpToAddress(address);
                        var bindings = new List<SkinBoneBinding>();

                        // Kinda hacky, but it works. Read so long as the first int of the type is 
                        // non-zero AND fits in a byte (matrix indexes are a single byte). This has been 
                        // checked to work (no non-zero data unread from any file) since that first int of
                        // the type (which is the count) must be none zero to declare the array's size.
                        //
                        // Basically, what we want is to read so long as the 32bit value is not zero but
                        // also between 0-256 (8-bit non-zero).
                        //
                        // To do this with a single Peek call, we have to handle the case of 0 correctly.
                        // If we subtract by 1, 0 underflows and becomes an invalid index. To check if a max
                        // index of 255 is valid (which is now 254 due to the -1), we check for less than 255.
                        while (unchecked(reader.PeekUInt() - 1) < byte.MaxValue)
                        {
                            var skinBoneBinding = new SkinBoneBinding();
                            skinBoneBinding.Deserialize(reader);
                            bindings.Add(skinBoneBinding);
                        }
                        skinBoneBindings = bindings.ToArray();
                        reader.AlignTo(GX.GXUtility.GX_FIFO_ALIGN);
                    }
                }
            }
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            //
            Pointer submeshOffsetPtrPatchAddress;

            {
                // Update counts
                boneCount = (byte)Bones.Length;
                textureCount = (ushort)tevLayers.Length;

                //
                opaqueMaterialCount = 0; // (ushort)submeshes.Length;
                translucidMaterialCount = 0;
                for (int i = 0; i < submeshes.Length; i++)
                {
                    var submesh = submeshes[i];

                    // Seems to be actually really close!
                    var isTranslucid =
                        submesh.Material.UnkAlpha0x14 != -1 ||
                        submesh.Material.Alpha < 255;

                    if (isTranslucid)
                        translucidMaterialCount++;
                    else
                        opaqueMaterialCount++;
                }

                // See start and end of function for submeshOffsetPtr
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(kMagic);
                writer.Write(attributes);
                writer.Write(boundingSphere);
                writer.Write(textureCount);
                writer.Write(opaqueMaterialCount);
                writer.Write(translucidMaterialCount);
                writer.Write(boneCount);
                writer.Write(zero0x1F);
                submeshOffsetPtrPatchAddress = writer.GetPositionAsPointer();
                writer.Write(submeshOffsetPtr);
                writer.Write(zero0x24);
                writer.Write(boneIndices);
            }
            this.RecordEndAddress(writer);
            {
                writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);

                //
                foreach (var textureConfig in tevLayers)
                {
                    writer.Write(textureConfig);
                    writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);
                }

                //
                writer.Write(bones);
                writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);

                // Write garbage/blank on first pass
                if (IsSkinnedModel || IsPhysicsDrivenModel)
                {
                    writer.Write(skinnedVertexDescriptor);
                    writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);
                }

                //
                writer.Write(submeshes);
                writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);


                if (IsSkinnedModel || IsPhysicsDrivenModel)
                {
                    // Order here matters since current code uses pointers to solve for many of these
                    // counts / deserializing data. If more is known, could be arbitrarily ordered.

                    // Get where the descriptor is serialized in the stream. 
                    Pointer basePtr = skinnedVertexDescriptor.GetPointer();

                    // (#1) Write this data first (always has lowest offset)
                    Pointer SkinnedVerticesBPtr = writer.GetPositionAsPointer();
                    writer.Write(skinnedVerticesB);
                    writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);

                    // (#2) This data will be empty / [0] when 'IsPhysicsDrivenModel'
                    // However, that's a good thing (we still want this address/offset).
                    Pointer SkinnedVerticesAPtr = writer.GetPositionAsPointer();
                    if (skinnedVerticesA is not null)
                    {
                        writer.Write(skinnedVerticesA);
                        writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN); // redundant
                    }

                    // (#3) Like the previous type, this too will be null when 'IsPhysicsDrivenModel'
                    Pointer unkBoneIndicesPtr = writer.GetPositionAsPointer();
                    if (UnkBoneIndices is not null)
                    {
                        writer.Write(UnkBoneIndices);
                        writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);
                    }

                    // (#4) Lastly, write the skin-bone bindings
                    Pointer SkinBoneBindingsPtr = writer.GetPositionAsPointer();
                    writer.Write(skinBoneBindings);
                    writer.WriteAlignment(GX.GXUtility.GX_FIFO_ALIGN);

                    // Second pass: rewrite data, getting correct count and offsets
                    skinnedVertexDescriptor = new SkinnedVertexDescriptor()
                    {
                        SkinnedVerticesBCount = skinnedVerticesB.Length,
                        SkinnedVerticesAPtrOffset = SkinnedVerticesAPtr - basePtr,
                        SkinnedVerticesBPtrOffset = SkinnedVerticesBPtr - basePtr,
                        SkinBoneBindingsPtrOffset = SkinBoneBindingsPtr - basePtr,
                        UnkBoneIndicesPtrOffset = unkBoneIndicesPtr - basePtr,
                    };

                    // Jump to previous address, overwrite
                    writer.JumpToAddress(basePtr);
                    writer.Write(skinnedVertexDescriptor);
                }

                //
                if (!submeshes.IsNullOrEmpty())
                {
                    var endAddress = writer.GetPositionAsPointer();
                    submeshOffsetPtr = submeshes[0].AddressRange.startAddress - AddressRange.startAddress;
                    writer.JumpToAddress(submeshOffsetPtrPatchAddress);
                    writer.Write(submeshOffsetPtr);
                    writer.JumpToAddress(endAddress);
                }


            }
        }


        //

        public void PatchTevLayerIndexes()
        {
            for (ushort i = 0; i < tevLayers.Length; i++)
            {
                tevLayers[i].TevLayerIndex = i;
            }
        }
    }
}