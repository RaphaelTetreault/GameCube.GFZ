using System.Text;

namespace GameCube.GFZ
{
    public class TextEncoding
    {
        // TODO: These encogins are true of GameCube in general. Move to GameCube library.
        public static readonly Encoding Windows1252 = Encoding.GetEncoding(codepage: 1252);
        public static readonly Encoding ShiftJIS = Encoding.GetEncoding(codepage: 932);
    }
}
