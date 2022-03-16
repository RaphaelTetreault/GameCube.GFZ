using Manifold;
using Manifold.IO;
using System;
using System.IO;
using Unity.Mathematics;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// A trigger for special Story Mode objects.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    ///     <listheader>This structure is used for the following missions:</listheader>
    ///     <description></description>
    ///     <item>
    ///         <term>Story 1: Captain Falcon Trains</term>
    ///         <description>
    ///             (F-Zero AX only!) Represents collectable capsule's trigger.
    ///             Story 1 capsule data was moved to the MiscellaneousTrigger type for F-Zero GX.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>Story 2: Goroh the Vengeful Samurai</term>
    ///         <description>Represents boulders, their trigger, and their animation paths.</description>
    ///     </item>
    ///     <item>
    ///         <term>Story 5: Save Jody!</term>
    ///         <description>Represents energy capsule's trigger.</description>
    ///     </item>
    /// </list>
    /// </remarks>
    [Serializable]
    public sealed class StoryObjectTrigger :
        IBinaryAddressable,
        IBinarySerializable,
        IHasReference,
        ITextPrintable
    {
        // FIELDS
        private ushort zero_0x00;
        private byte boulderGroupOrderIndex;
        private byte boulderGroupAndDifficulty; // split lower/upper 4 bits, see properties
        private float3 story2BoulderScale;
        private Pointer story2BoulderPathPtr;
        private float3 scale;    // trigger scale
        private float3 rotation; // trigger rotation
        private float3 position; // trigger position
        // FIELDS (deserialized from pointers)
        private StoryObjectPath storyObjectPath;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public StoryDifficulty Difficulty
        {
            get
            {
                // Lower 4 bits are for difficulty
                return (StoryDifficulty)(BoulderGroupAndDifficulty & 0b00001111);
            }
        }
        public byte BoulderGroup
        {
            get
            {
                // Upper 4 bits are for group of boulders which fall
                return (byte)(BoulderGroupAndDifficulty >> 4);
            }
        }
        public byte BoulderGroupOrderIndex { get => boulderGroupOrderIndex; set => boulderGroupOrderIndex = value; }
        public byte BoulderGroupAndDifficulty { get => boulderGroupAndDifficulty; set => boulderGroupAndDifficulty = value; }
        public float3 Story2BoulderScale { get => story2BoulderScale; set => story2BoulderScale = value; }
        public Pointer Story2BoulderPathPtr { get => story2BoulderPathPtr; set => story2BoulderPathPtr = value; }
        public float3 Scale { get => scale; set => scale = value; }
        public float3 Rotation { get => rotation; set => rotation = value; }
        public float3 Position { get => position; set => position = value; }
        public StoryObjectPath StoryObjectPath { get => storyObjectPath; set => storyObjectPath = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref zero_0x00);
                reader.Read(ref boulderGroupOrderIndex);
                reader.Read(ref boulderGroupAndDifficulty);
                reader.Read(ref story2BoulderScale);
                reader.Read(ref story2BoulderPathPtr);
                reader.Read(ref scale);
                reader.Read(ref rotation);
                reader.Read(ref position);
            }
            this.RecordEndAddress(reader);
            {
                if (story2BoulderPathPtr.IsNotNull)
                {
                    // Read array pointer
                    reader.JumpToAddress(story2BoulderPathPtr);
                    reader.Read(ref storyObjectPath);
                }
            }
            this.SetReaderToEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            {
                story2BoulderPathPtr = storyObjectPath.GetPointer();
            }
            this.RecordStartAddress(writer);
            {
                writer.Write(zero_0x00);
                writer.Write(boulderGroupOrderIndex);
                writer.Write(boulderGroupAndDifficulty);
                writer.Write(story2BoulderScale);
                writer.Write(story2BoulderPathPtr);
                writer.Write(scale);
                writer.Write(rotation);
                writer.Write(position);
            }
            this.RecordEndAddress(writer);
        }

        public void ValidateReferences()
        {
            Assert.ReferencePointer(StoryObjectPath, Story2BoulderPathPtr);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(StoryObjectTrigger));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Position)}: {Position}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Rotation)}: {rotation}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Scale)}: {Scale}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(BoulderGroupOrderIndex)}: {BoulderGroupOrderIndex}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(BoulderGroup)}: {BoulderGroup}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Difficulty)}: {Difficulty}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Story2BoulderScale)}: {Story2BoulderScale}");
            builder.AppendLineIndented(indent, indentLevel, StoryObjectPath);
        }

        public string PrintSingleLine()
        {
            return nameof(StoryObjectTrigger);
        }

        public override string ToString() => PrintSingleLine();

    }
}
