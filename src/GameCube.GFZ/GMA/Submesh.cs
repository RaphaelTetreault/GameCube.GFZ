using GameCube.GX;
using Manifold;
using Manifold.IO;
using System.Collections.Generic;

namespace GameCube.GFZ.GMA
{
    public class Submesh :
        IBinaryAddressable,
        IBinarySerializable
    {
        // CONSTANTS
        /// <summary>
        /// GameCube GPU No-Operation opcode
        /// </summary>
        public const byte GX_NOP = 0x00;

        // METADATA
        private GcmfAttributes attributes;

        // FIELDS
        private RenderFlags renderFlags;
        private Material material = new Material();
        private AttributeFlags vertexAttributes;
        private DisplayListDescriptor primaryDisplayListDescriptor = new DisplayListDescriptor();
        private UnkAlphaOptions unknownAlphaOptions = new UnkAlphaOptions();
        private DisplayList[] primaryDisplayListsOpaque;
        private DisplayList[] primaryDisplayListsTranslucid;
        private DisplayListDescriptor secondaryDisplayListDescriptor;
        private DisplayList[] secondaryDisplayListsOpaque;
        private DisplayList[] secondaryDisplayListsTranslucid;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        /// <summary>
        /// A copy of the GCMF attributes of the parent GCMF class.
        /// </summary>
        public GcmfAttributes Attributes { get => attributes; set => attributes = value; }
        public bool Is16bitModel => attributes.HasFlag(GcmfAttributes.is16Bit);
        public bool IsPhysicsDrivenModel => attributes.HasFlag(GcmfAttributes.isEffectiveModel);
        public bool IsSkinnedModel => attributes.HasFlag(GcmfAttributes.isSkinModel);
        public bool IsStitchingModel => attributes.HasFlag(GcmfAttributes.isStitchingModel);
        //
        public RenderFlags RenderFlags { get => renderFlags; set => renderFlags = value; }
        public Material Material { get => material; set => material = value; }
        public AttributeFlags VertexAttributes { get => vertexAttributes; set => vertexAttributes = value; }
        public DisplayListDescriptor PrimaryDisplayListDescriptor { get => primaryDisplayListDescriptor; set => primaryDisplayListDescriptor = value; }
        public DisplayList[] PrimaryDisplayListsOpaque { get => primaryDisplayListsOpaque; set => primaryDisplayListsOpaque = value; }
        public DisplayList[] PrimaryDisplayListsTranslucid { get => primaryDisplayListsTranslucid; set => primaryDisplayListsTranslucid = value; }
        public bool RenderPrimaryFrontFaceCull => material.DisplayListFlags.HasFlag(DisplayListFlags.PrimaryFrontCull);
        public bool RenderPrimaryBackFaceCull => material.DisplayListFlags.HasFlag(DisplayListFlags.PrimaryBackCull);
        public bool RenderSecondary => RenderSecondaryFrontFaceCull || RenderSecondaryBackFaceCull;
        public bool RenderSecondaryFrontFaceCull => material.DisplayListFlags.HasFlag(DisplayListFlags.SecondaryFrontCull);
        public bool RenderSecondaryBackFaceCull => material.DisplayListFlags.HasFlag(DisplayListFlags.SecondaryBackCull);
        public DisplayListDescriptor SecondaryDisplayListDescriptor { get => secondaryDisplayListDescriptor; set => secondaryDisplayListDescriptor = value; }
        public DisplayList[] SecondaryDisplayListsOpaque { get => secondaryDisplayListsOpaque; set => secondaryDisplayListsOpaque = value; }
        public DisplayList[] SecondaryDisplayListsTranslucid { get => secondaryDisplayListsTranslucid; set => secondaryDisplayListsTranslucid = value; }
        public UnkAlphaOptions Unknown { get => unknownAlphaOptions; set => unknownAlphaOptions = value; }

        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                //Read
                reader.Read(ref renderFlags);
                reader.Read(ref material);
                reader.Read(ref vertexAttributes);
                reader.Read(ref primaryDisplayListDescriptor);
                reader.Read(ref unknownAlphaOptions);
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
            // Reset the render flags based on instance data
            material.DisplayListFlags =
                (primaryDisplayListsOpaque.IsNullOrEmpty() ? 0 : DisplayListFlags.PrimaryFrontCull) |
                (primaryDisplayListsTranslucid.IsNullOrEmpty() ? 0 : DisplayListFlags.PrimaryBackCull) |
                (secondaryDisplayListsOpaque.IsNullOrEmpty() ? 0 : DisplayListFlags.SecondaryFrontCull) |
                (secondaryDisplayListsTranslucid.IsNullOrEmpty() ? 0 : DisplayListFlags.SecondaryBackCull);

            // Temp variables to store ranges that display lists are serialized at, used to get size on disk
            var pdlffc = new AddressRange();
            var pdlbfc = new AddressRange();
            var sdlffc = new AddressRange();
            var sdlbfc = new AddressRange();

            this.RecordStartAddress(writer);
            {
                writer.Write(renderFlags);
                writer.Write(material);
                writer.Write(vertexAttributes);
                writer.Write(primaryDisplayListDescriptor);
                writer.Write(unknownAlphaOptions);
                writer.WriteAlignment(GXUtility.GX_FIFO_ALIGN);

                if (RenderPrimaryFrontFaceCull)
                    WriteDisplayLists(writer, primaryDisplayListsOpaque, out pdlffc);

                if (RenderPrimaryBackFaceCull)
                    WriteDisplayLists(writer, primaryDisplayListsTranslucid, out pdlbfc);


                if (RenderSecondary)
                {
                    writer.Write(secondaryDisplayListDescriptor);
                    writer.WriteAlignment(GXUtility.GX_FIFO_ALIGN);

                    if (RenderSecondaryFrontFaceCull)
                        WriteDisplayLists(writer, secondaryDisplayListsOpaque, out sdlffc);

                    if (RenderSecondaryBackFaceCull)
                        WriteDisplayLists(writer, secondaryDisplayListsTranslucid, out sdlbfc);
                }

            }
            this.RecordEndAddress(writer);
            {
                // Now that we know the size of the display lists, update values and reserialize
                primaryDisplayListDescriptor.FrontFaceCullingDisplayListSize = pdlffc.Size;
                primaryDisplayListDescriptor.BackFaceCullingDisplayListSize = pdlbfc.Size;
                writer.JumpToAddress(primaryDisplayListDescriptor.AddressRange.startAddress);
                writer.Write(primaryDisplayListDescriptor);

                if (RenderSecondary)
                {
                    secondaryDisplayListDescriptor.FrontFaceCullingDisplayListSize = sdlffc.Size;
                    secondaryDisplayListDescriptor.BackFaceCullingDisplayListSize = sdlbfc.Size;
                    writer.JumpToAddress(secondaryDisplayListDescriptor.AddressRange.startAddress);
                    writer.Write(secondaryDisplayListDescriptor);
                }
            }
            this.SetWriterToEndAddress(writer);
        }

        private DisplayList[] ReadDisplayLists(EndianBinaryReader reader, int endAddress)
        {
            var displayLists = new List<DisplayList>();

            var gxNOP = reader.ReadByte();
            Assert.IsTrue(gxNOP == GX_NOP);

            while (!reader.IsAtEndOfStream())
            {
                // Reasons to stop reading display list data
                bool isAtEnd = reader.BaseStream.Position >= endAddress;
                bool isFifoPadding = reader.PeekUInt8() == 0;
                if (isAtEnd || isFifoPadding)
                    break;

                var displayList = new DisplayList(vertexAttributes, GfzGX.VAT);
                displayList.Deserialize(reader);
                displayLists.Add(displayList);
            }
            reader.AlignTo(GXUtility.GX_FIFO_ALIGN);

            return displayLists.ToArray();
        }

        private void WriteDisplayLists(EndianBinaryWriter writer, DisplayList[] displayLists, out AddressRange addressRange)
        {
            addressRange = new AddressRange();
            addressRange.RecordStartAddress(writer);
            {
                writer.Write(GX_NOP);
                writer.Write(displayLists);
                writer.WriteAlignment(GXUtility.GX_FIFO_ALIGN);
            }
            addressRange.RecordEndAddress(writer);
        }

        private int DisplayListsSizeOnDisk(DisplayList[] displayLists)
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
}