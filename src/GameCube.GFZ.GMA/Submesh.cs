using GameCube.GX;
using Manifold;
using Manifold.IO;
using System.Collections.Generic;
using System.Numerics;

namespace GameCube.GFZ.GMA;

/// <summary>
///     
/// </summary>
public class Submesh :
    IBinaryAddressable,
    IBinarySerializable
{
    // METADATA
    private Gcmf gcmf = new();

    // FIELDS
    private RenderFlags renderFlags;
    private GXColor materialColor = new(0xFFFFFFFF, GXComponentType.GX_RGBA8);
    private GXColor ambientColor = new(0xFFFFFFFF, GXComponentType.GX_RGBA8);
    private GXColor specularColor = new(0x00000000, GXComponentType.GX_RGBA8);
    private MatFlags0x10 unk0x10 = 0; // 2022/07/08: maybe tells GPU how to use color? Fails on non-textured objects. (TEV?)
    private byte alpha = 255;
    private byte tevLayerCount = 0;
    private MaterialDestination materialDestination = 0;
    private sbyte unkAlpha0x14 = -1; // 0xFF. Mainly -1 (3/4 of data). Index => previous material's index for same model.
    private MatFlags0x15 unk0x15 = 0;
    private short tevLayerIndex0 = -1; // 0xFFFF
    private short tevLayerIndex1 = -1; // 0xFFFF
    private short tevLayerIndex2 = -1; // 0xFFFF
    private GXAttributeFlags vertexAttributes;
    private DisplayListDescriptor primaryDisplayListDescriptor = new();
    private Vector3 blendDepthSortOrigin; //
    private float blendUnkFloat; // if GCMF flags at 0x00 are set with bit 9, this value exists. All values: 0f, 1f.
    private BlendFactors blendFactors; // 0xF bitmask for src blend factor, 0xF0 for dst blend factor
    private GXDisplayList[] primaryDisplayListsOpaque = [];
    private GXDisplayList[] primaryDisplayListsTranslucid = [];
    private DisplayListDescriptor secondaryDisplayListDescriptor = new();
    private GXDisplayList[] secondaryDisplayListsOpaque = [];
    private GXDisplayList[] secondaryDisplayListsTranslucid = [];


    // PROPERTIES
    public AddressRange AddressRange { get; set; }
    /// <summary>
    ///     A copy of the GCMF attributes of the parent GCMF class.
    /// </summary>
    public Gcmf GCMF { get => gcmf; set => gcmf = value; }
    public bool Is16bitModel => gcmf.Is16bitModel;
    public bool IsPhysicsDrivenModel => gcmf.IsPhysicsDrivenModel;
    public bool IsSkinnedModel => gcmf.IsSkinnedModel;
    public bool IsStitchingModel => gcmf.IsStitchingModel;
    //
    public RenderFlags RenderFlags { get => renderFlags; set => renderFlags = value; }
    public GXColor MaterialColor { get => materialColor; set => materialColor = value; }
    public GXColor AmbientColor { get => ambientColor; set => ambientColor = value; }
    public GXColor SpecularColor { get => specularColor; set => specularColor = value; }
    public MatFlags0x10 Unk0x10 { get => unk0x10; set => unk0x10 = value; }
    public byte Alpha { get => alpha; set => alpha = value; }
    public byte TevLayerCount { get => tevLayerCount; set => tevLayerCount = value; }
    public MaterialDestination MaterialDestination { get => materialDestination; set => materialDestination = value; }
    public sbyte UnkAlpha0x14 { get => unkAlpha0x14; set => unkAlpha0x14 = value; }
    public MatFlags0x15 Unk0x15 { get => unk0x15; set => unk0x15 = value; }
    public short TevLayerIndex0 { get => tevLayerIndex0; set => tevLayerIndex0 = value; }
    public short TevLayerIndex1 { get => tevLayerIndex1; set => tevLayerIndex1 = value; }
    public short TevLayerIndex2 { get => tevLayerIndex2; set => tevLayerIndex2 = value; }
    public GXAttributeFlags VertexAttributes { get => vertexAttributes; set => vertexAttributes = value; }
    public DisplayListDescriptor PrimaryDisplayListDescriptor { get => primaryDisplayListDescriptor; set => primaryDisplayListDescriptor = value; }
    public Vector3 BlendDepthSortOrigin { get => blendDepthSortOrigin; set => blendDepthSortOrigin = value; }
    public float BlendUnkFloat { get => blendUnkFloat; set => blendUnkFloat = value; }
    public BlendFactors BlendFactors { get => blendFactors; set => blendFactors = value; }
    public GXDisplayList[] PrimaryBackFacing { get => primaryDisplayListsOpaque; set => primaryDisplayListsOpaque = value; }
    public GXDisplayList[] PrimaryFrontFacing { get => primaryDisplayListsTranslucid; set => primaryDisplayListsTranslucid = value; }
    public bool RenderPrimaryFrontFaceCull => MaterialDestination.HasFlag(MaterialDestination.PrimaryFrontCull);
    public bool RenderPrimaryBackFaceCull => MaterialDestination.HasFlag(MaterialDestination.PrimaryBackCull);
    public bool RenderSecondary => RenderSecondaryFrontFaceCull || RenderSecondaryBackFaceCull;
    public bool RenderSecondaryFrontFaceCull => MaterialDestination.HasFlag(MaterialDestination.SecondaryFrontCull);
    public bool RenderSecondaryBackFaceCull => MaterialDestination.HasFlag(MaterialDestination.SecondaryBackCull);
    public DisplayListDescriptor SecondaryDisplayListDescriptor { get => secondaryDisplayListDescriptor; set => secondaryDisplayListDescriptor = value; }
    public GXDisplayList[] SecondaryBackFacing { get => secondaryDisplayListsOpaque; set => secondaryDisplayListsOpaque = value; }
    public GXDisplayList[] SecondaryFrontFacing { get => secondaryDisplayListsTranslucid; set => secondaryDisplayListsTranslucid = value; }

    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        this.RecordStartAddress(reader);
        {
            //Read
            reader.Read(ref renderFlags);
            materialColor.Deserialize(reader);
            ambientColor.Deserialize(reader);
            specularColor.Deserialize(reader);
            reader.Read(ref unk0x10);
            reader.Read(ref alpha);
            reader.Read(ref tevLayerCount);
            reader.Read(ref materialDestination);
            reader.Read(ref unkAlpha0x14);
            reader.Read(ref unk0x15);
            reader.Read(ref tevLayerIndex0);
            reader.Read(ref tevLayerIndex1);
            reader.Read(ref tevLayerIndex2);
            reader.Read(ref vertexAttributes);
            reader.Read(ref primaryDisplayListDescriptor);
            reader.Read(ref blendDepthSortOrigin);
            reader.Read(ref blendUnkFloat);
            reader.Read(ref blendFactors);
            reader.AlignTo(GXUtility.GX_FIFO_ALIGN);

            int endAddress = reader.GetPositionAsPointer().address;

            // If the GCMF this submesh is a part of has either of these attributes,
            // then it does not actually store any display lists (at least, not in
            // in a GX/GPU useable format). While some of the data /should/ be placed
            // in a DisplayList, it is easier to manage in their own container classes.
            // These "skinned" containers reside in the GCMF structure.
            if (IsSkinnedModel || IsPhysicsDrivenModel)
            {
                this.RecordEndAddress(reader);
                return;
            }

            if (RenderPrimaryFrontFaceCull)
            {
                endAddress += primaryDisplayListDescriptor.FrontFaceCullingDisplayListSize;
                primaryDisplayListsOpaque = ReadDisplayLists(reader, endAddress);
            }

            if (RenderPrimaryBackFaceCull)
            {
                endAddress += primaryDisplayListDescriptor.BackFaceCullingDisplayListSize;
                primaryDisplayListsTranslucid = ReadDisplayLists(reader, endAddress);
            }

            if (RenderSecondary)
            {
                reader.Read(ref secondaryDisplayListDescriptor);
                reader.AlignTo(GXUtility.GX_FIFO_ALIGN);
                endAddress = new Pointer(reader.BaseStream.Position).address;

                if (RenderSecondaryFrontFaceCull)
                {
                    endAddress += secondaryDisplayListDescriptor.FrontFaceCullingDisplayListSize;
                    secondaryDisplayListsOpaque = ReadDisplayLists(reader, endAddress);
                }

                if (RenderSecondaryBackFaceCull)
                {
                    endAddress += secondaryDisplayListDescriptor.BackFaceCullingDisplayListSize;
                    secondaryDisplayListsTranslucid = ReadDisplayLists(reader, endAddress);
                }
            }
        }
        this.RecordEndAddress(reader);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        Assert.IsTrue(materialColor.ComponentType == GXComponentType.GX_RGBA8);
        Assert.IsTrue(ambientColor.ComponentType == GXComponentType.GX_RGBA8);
        Assert.IsTrue(specularColor.ComponentType == GXComponentType.GX_RGBA8);

        // Reset the render flags based on instance data
        MaterialDestination =
            (primaryDisplayListsOpaque.IsNullOrEmpty() ? 0 : MaterialDestination.PrimaryFrontCull) |
            (primaryDisplayListsTranslucid.IsNullOrEmpty() ? 0 : MaterialDestination.PrimaryBackCull) |
            (secondaryDisplayListsOpaque.IsNullOrEmpty() ? 0 : MaterialDestination.SecondaryFrontCull) |
            (secondaryDisplayListsTranslucid.IsNullOrEmpty() ? 0 : MaterialDestination.SecondaryBackCull);

        // Temp variables to store ranges that display lists are serialized at, used to get size on disk
        var pdloRange = new AddressRange();
        var pdltRange = new AddressRange();
        var sdloRange = new AddressRange();
        var sdltRange = new AddressRange();

        this.RecordStartAddress(writer);
        {
            writer.Write(renderFlags);
            writer.Write(materialColor);
            writer.Write(ambientColor);
            writer.Write(specularColor);
            writer.Write(unk0x10);
            writer.Write(alpha);
            writer.Write(tevLayerCount);
            writer.Write(materialDestination);
            writer.Write(unkAlpha0x14);
            writer.Write(unk0x15);
            writer.Write(tevLayerIndex0);
            writer.Write(tevLayerIndex1);
            writer.Write(tevLayerIndex2);
            writer.Write(vertexAttributes);
            writer.Write(primaryDisplayListDescriptor);
            writer.Write(blendDepthSortOrigin);
            writer.Write(blendUnkFloat);
            writer.Write(blendFactors);
            writer.AlignTo(GXUtility.GX_FIFO_ALIGN);

            if (RenderPrimaryFrontFaceCull)
                WriteDisplayLists(writer, primaryDisplayListsOpaque, out pdloRange);

            if (RenderPrimaryBackFaceCull)
                WriteDisplayLists(writer, primaryDisplayListsTranslucid, out pdltRange);


            if (RenderSecondary)
            {
                writer.Write(secondaryDisplayListDescriptor);
                writer.AlignTo(GXUtility.GX_FIFO_ALIGN);

                if (RenderSecondaryFrontFaceCull)
                    WriteDisplayLists(writer, secondaryDisplayListsOpaque, out sdloRange);

                if (RenderSecondaryBackFaceCull)
                    WriteDisplayLists(writer, secondaryDisplayListsTranslucid, out sdltRange);
            }

        }
        this.RecordEndAddress(writer);
        {
            // Now that we know the size of the display lists, update values and reserialize
            primaryDisplayListDescriptor.FrontFaceCullingDisplayListSize = pdloRange.Size;
            primaryDisplayListDescriptor.BackFaceCullingDisplayListSize = pdltRange.Size;
            writer.JumpToAddress(primaryDisplayListDescriptor.AddressRange.startAddress);
            writer.Write(primaryDisplayListDescriptor);

            if (RenderSecondary)
            {
                secondaryDisplayListDescriptor.FrontFaceCullingDisplayListSize = sdloRange.Size;
                secondaryDisplayListDescriptor.BackFaceCullingDisplayListSize = sdltRange.Size;
                writer.JumpToAddress(secondaryDisplayListDescriptor.AddressRange.startAddress);
                writer.Write(secondaryDisplayListDescriptor);
            }
        }
        this.SetWriterToEndAddress(writer);
    }

    private GXDisplayList[] ReadDisplayLists(EndianBinaryReader reader, int endAddress)
    {
        var displayLists = new List<GXDisplayList>();

        var gxNOP = reader.ReadByte();
        Assert.IsTrue(gxNOP == GXUtility.GX_NOP);

        while (!reader.IsAtEndOfStream())
        {
            // Reasons to stop reading display list data
            bool isAtEnd = reader.BaseStream.Position >= endAddress;
            bool isFifoPadding = reader.PeekUInt8() == 0;
            if (isAtEnd || isFifoPadding)
                break;

            var displayList = new GXDisplayList(vertexAttributes, GfzGX.VAT);
            displayList.Deserialize(reader);
            displayLists.Add(displayList);
        }
        reader.AlignTo(GXUtility.GX_FIFO_ALIGN);

        return displayLists.ToArray();
    }

    private static void WriteDisplayLists(EndianBinaryWriter writer, GXDisplayList[] displayLists, out AddressRange addressRange)
    {
        addressRange = new AddressRange();
        addressRange.RecordStartAddress(writer);
        {
            writer.Write(GXUtility.GX_NOP);
            writer.Write(displayLists);
            writer.AlignTo(GXUtility.GX_FIFO_ALIGN);
        }
        addressRange.RecordEndAddress(writer);
    }

    private static int DisplayListsSizeOnDisk(GXDisplayList[] displayLists)
    {
        int size = 0;

        if (displayLists is null || displayLists.Length == 0)
            return size;

        // Get GX properties about display list
        var gxAttributes = displayLists[0].Attributes;
        var formatIndex = displayLists[0].GxCommand.VertexFormatIndex;
        var format = displayLists[0].Vat[formatIndex];

        // Compute size of all display lists
        int sizeOfVertex = GXUtility.GetGxVertexSize(gxAttributes, format);
        foreach (var displayList in displayLists)
        {
            // Every display list should have the same properties
            Assert.IsTrue(gxAttributes == displayList.Attributes);
            Assert.IsTrue(formatIndex == displayList.GxCommand.VertexFormatIndex);

            // Add +1 for size of GXCommand
            // Add +2 for size of count (uint16)
            size += 3;
            size += sizeOfVertex * displayList.VertexCount;
        }

        // Add +1 for size of GX_NOP
        size += 1;

        // Add padding size if necessary
        var remainder = size % GXUtility.GX_FIFO_ALIGN;
        if (remainder > 0)
            size += GXUtility.GX_FIFO_ALIGN - remainder;

        return size;
    }

}