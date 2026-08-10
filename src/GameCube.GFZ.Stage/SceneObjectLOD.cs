using Manifold;
using Manifold.IO;

namespace GameCube.GFZ.Stage;

/// <summary>
///     Binds an object name to a loadable display model.
/// </summary>
public sealed class SceneObjectLOD :
    IBinaryAddressable,
    IBinarySerializable,
    IHasReference,
    ITextPrintable
{
    // FIELDS
    private uint flags_0x00; // some runtime flag, visibility of object?
    private Pointer lodNamePtr;
    private Pointer ptr_0x08; // some runtime pointer
    private float inverseLodDistance; // Frustum range 250,000m / inverse LOD distance
    // REFERENCE FIELDS
    private ShiftJisCString name;


    // PROPERTIES
    public AddressRange AddressRange { get; set; }
    public Pointer LodNamePtr { get => lodNamePtr; set => lodNamePtr = value; }
    public float LodDistance { get => inverseLodDistance; set => inverseLodDistance = value; }
    public ShiftJisCString Name { get => name; set => name = value; }


    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        this.RecordStartAddress(reader);
        {
            reader.Read(ref flags_0x00);
            reader.Read(ref lodNamePtr);
            reader.Read(ref ptr_0x08);
            reader.Read(ref inverseLodDistance);
        }
        this.RecordEndAddress(reader);
        {
            Assert.IsTrue(flags_0x00 == 0);
            Assert.IsTrue(ptr_0x08 == 0);

            reader.JumpToAddress(lodNamePtr);
            reader.Read(ref name);
        }
        this.SetReaderToEndAddress(reader);
    }

    public void Serialize(EndianBinaryWriter writer)
    {
        {
            Assert.IsTrue(flags_0x00 == 0);
            Assert.IsTrue(ptr_0x08 == 0);

            lodNamePtr = name.GetPointer();
        }
        this.RecordStartAddress(writer);
        {
            writer.Write(flags_0x00);
            writer.Write(lodNamePtr);
            writer.Write(ptr_0x08);
            writer.Write(inverseLodDistance);
        }
        this.RecordEndAddress(writer);
    }

    public void ValidateReferences()
    {
        // This pointer CANNOT be null and must refer to an object name.

        Assert.IsTrue(name != null); // true null only, name can be string.Empty
        Assert.ReferencePointer(name, lodNamePtr); // 2022/01/25: must always have instance pointer!

        // Constants
        Assert.IsTrue(flags_0x00 == 0);
        Assert.IsTrue(ptr_0x08 == 0);
    }

    public void PrintMultiLine(System.Text.StringBuilder builder, int indentLevel = 0, string indent = "\t")
    {
        builder.AppendLineIndented(indent, indentLevel, PrintSingleLine());
    }

    public string PrintSingleLine()
    {
        return $"{nameof(SceneObjectLOD)}({name}, {nameof(LodDistance)}: {LodDistance})";
    }

    public override string ToString() => PrintSingleLine();

}