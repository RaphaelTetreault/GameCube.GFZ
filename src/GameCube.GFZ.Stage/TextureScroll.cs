// NOTES:
// Indexes 0-3
//  idx0: uv.Xy scrolling (or, at least, on some models)
//  idx1: ?
//  idx2: ?
//  idx3: ?
// Indexes 4-11
// ...are assigned dynamically at runtime for race.gma models (start line)
// ...and perhaps some other things

using Manifold;
using Manifold.IO;

namespace GameCube.GFZ.Stage;

/// <summary>
///     Texture metadata. In some instances defines how a texture scrolls.
/// </summary>
public sealed class TextureScroll :
    IBinaryAddressable,
    IBinarySerializable,
    IHasReference,
    ITextPrintable
{
    // CONSTANTS
    public const int kCount = 12;

    // FIELDS
    private Pointer[] fieldPtrs = new Pointer[kCount];
    // REFERENCE FIELDS
    private TextureScrollField[]? fields; // could you not init to size 12?


    // PROPERTIES
    public AddressRange AddressRange { get; set; }
    public TextureScrollField[]? Fields { get => fields; set => fields = value; }
    public Pointer[] FieldPtrs { get => fieldPtrs; set => fieldPtrs = value; }


    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        this.RecordStartAddress(reader);
        {
            reader.Read(ref fieldPtrs, kCount);
        }
        this.RecordEndAddress(reader);
        {
            fields = new TextureScrollField[kCount];
            for (int i = 0; i < kCount; i++)
            {
                var pointer = fieldPtrs[i];
                if (pointer.IsNotNull)
                {
                    reader.JumpToAddress(pointer);
                    reader.Read(ref fields[i]);
                }
            }
        }
        this.SetReaderToEndAddress(reader);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        {
            fieldPtrs = fields.GetPointers();
        }
        this.RecordStartAddress(writer);
        {
            writer.Write(fieldPtrs);
        }
        this.RecordEndAddress(writer);
    }

    public void ValidateReferences()
    {
        // Validate each field/pointer
        for (int i = 0; i < kCount; i++)
        {
            // reference can be to Vector2(0, 0)
            Assert.ReferencePointer(fields[i], fieldPtrs[i]);

            //if (fields[i] != null)
            //    Assert.IsTrue(fields[i].X != 0 && fields[i].Y != 0);
        }
    }

    public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
    {
        builder.AppendLineIndented(indent, indentLevel, nameof(TextureScroll));
        indentLevel++;
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i] == null)
                continue;
            builder.AppendLineIndented(indent, indentLevel, $"[{i}] {fields[i]}");
        }
    }

    public string PrintSingleLine()
    {
        return nameof(TextureScroll);
    }

    public override string ToString() => PrintSingleLine();

}