using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.Stage
{
    /// <summary>
    /// A volume which triggers some visial effect.
    /// NOTE: assumed mesh scale for trigger is 10xyz
    /// </summary>
    [Serializable]
    public sealed class VisualEffectTrigger :
        IBinaryAddressable,
        IBinarySerializable,
        ITextPrintable
    {
        // FIELDS
        private TransformTRXS transform;
        private TriggerableAnimation animation;
        private TriggerableVisualEffect visualEffect;


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public TriggerableAnimation Animation { get => animation; set => animation = value; }
        public TransformTRXS Transform { get => transform; set => transform = value; }
        public TriggerableVisualEffect VisualEffect { get => visualEffect; set => visualEffect = value; }


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                reader.Read(ref transform);
                reader.Read(ref animation);
                reader.Read(ref visualEffect);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                writer.Write(transform);
                writer.Write(animation);
                writer.Write(visualEffect);
            }
            this.RecordEndAddress(writer);
        }

        public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
        {
            builder.AppendLineIndented(indent, indentLevel, nameof(VisualEffectTrigger));
            indentLevel++;
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(Animation)}: {Animation}");
            builder.AppendLineIndented(indent, indentLevel, $"{nameof(VisualEffect)}: {VisualEffect}");
            builder.AppendLineIndented(indent, indentLevel, Transform);
        }

        public string PrintSingleLine()
        {
            return nameof(VisualEffectTrigger);
        }

        public override string ToString() => PrintSingleLine();

    }
}
