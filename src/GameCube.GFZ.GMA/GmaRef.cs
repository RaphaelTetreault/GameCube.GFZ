using Manifold;
using Manifold.IO;
using System.Text;

namespace GameCube.GFZ.GMA;

/// <summary>
///     An assetized version of <see cref="Gma"/> with loose text references
///     to its required models.
/// </summary>
/// <remarks>
///     TODO: Convert this to BinaryFileWrapper(Gcmf)?
/// </remarks>
public class GmaRef :
    IPlainTextSerializable
{
    // READONLY
    public const string Extension = "gmaref";
    public static readonly Encoding Encoding = Encoding.Unicode;

    // FIELDS
    private string[] gcmfModels = [];

    // PROPERTIES
    public string[] GcmfModels { get => gcmfModels; set => gcmfModels = value; }

    // METHODS
    public void Deserialize(PlainTextReader reader)
    {
        // Create new array
        gcmfModels = new string[reader.Lines.Length];
        for (int i = 0; i < gcmfModels.Length; i++)
        {
            // PARSE: split at colons, grab last element, and trim whitespace
            string value = reader.Lines[i].Split(":")[^1].Trim();
            gcmfModels[i] = value;
        }
    }

    public void Serialize(PlainTextWriter writer)
    {
        // Get width of max digit
        int padWidth = gcmfModels.Length.ToString().Length;
        // Write entries
        for (int i = 0; i < gcmfModels.Length; i++)
        {
            string digits = i.PadLeft(padWidth);
            string gcfmModel = gcmfModels[i];
            // Write "number: modelname"
            writer.Write($"{digits}:\t{gcfmModel}\n");
        }
    }
}
