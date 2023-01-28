using GameCube.GX;
using GameCube.GX.Texture;
using Manifold;
using Manifold.IO;

namespace GameCube.GFZ.Emblem
{
    public class Emblem : FixedSizeTextureRGB5A3
    {
        public override int Width => 64;
        public override int Height => 64;
        public static int Size => 64 * 64 * sizeof(ushort);
    }
}
