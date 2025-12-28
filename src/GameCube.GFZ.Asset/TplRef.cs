using GameCube.GFZ.TPL;
using Manifold;
using Manifold.IO;
using System.Text;

namespace GameCube.GFZ.Asset;

/// <summary>
///     An assetized version of <see cref="Tpl"/> with loose text references
///     to its required textures.
/// </summary>
public class TplRef :
    IPlainTextSerializable
{
    // READONLY
    public const string Extension = "tplref";
    public static readonly Encoding Encoding = Encoding.Unicode;
    private const string splitChar = ":";

    // FIELDS
    public string[] Textures = [];

    // METHODS
    public void Deserialize(PlainTextReader reader)
    {
        // Figure out how many entries in TPL
        string lastLine = reader.Lines[^1];
        int lastIndex = ReadIndex(lastLine) + 1;
        // Create new array
        Textures = new string[lastIndex];
        for (int i = 0; i < reader.LineCount; i++)
        {
            string line = reader.ReadLine();
            int texIndex = ReadIndex(line);
            string texValue = ReadValue(line);
            Textures[texIndex] = texValue;
        }
    }

    public void Serialize(PlainTextWriter writer)
    {
        // Get width of max digit
        int padWidth = Textures.Length.ToString().Length;
        // Write entries
        for (int i = 0; i < Textures.Length; i++)
        {
            // Skip empty strings
            if (string.IsNullOrWhiteSpace(Textures[i]))
                continue;
            // Otherwise write texture name with index
            string digits = i.PadLeft(padWidth);
            string texture = Textures[i];
            writer.Write($"{digits}{splitChar}\t{texture}\n");
        }
    }


    private static int ReadIndex(string line)
    {
        string lastIndexStr = line.Split(splitChar)[0].Trim();
        bool isValid = int.TryParse(lastIndexStr, out int lastIndex);
        if (!isValid)
        {
            string msg = "";
            throw new System.Exception(msg);
        }
        return lastIndex;
    }

    private static string ReadValue(string line)
    {
        string value = line.Split(splitChar)[^1].Trim();
        return value;
    }
}
