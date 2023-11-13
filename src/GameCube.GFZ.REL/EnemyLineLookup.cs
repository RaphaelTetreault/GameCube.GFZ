using System.IO;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// Main address lookup directory
    /// </summary>
    public static class EnemyLineLookup
    {
        //public enum GameCode
        //{
        //    GX_J,
        //    GX_E,
        //    GX_P,
        //    AX,
        //}

        public static readonly EnemyLineDataBlocks GFZE01 = new EnemyLineDataBlocksGfze01();
        public static readonly EnemyLineDataBlocks GFZJ01 = new EnemyLineDataBlocksGfzj01();
        public static readonly EnemyLineDataBlocks GFZP01 = new EnemyLineDataBlocksGfzp01();
        public static readonly EnemyLineDataBlocks GFZJ8P = new MainDolDataBlocksGfzj8p();

        public static EnemyLineDataBlocks GetInfo(GameCode gameCode)
        {
            switch (gameCode)
            {
                case GameCode.GFZJ01: return GFZJ01;
                case GameCode.GFZE01: return GFZE01;
                case GameCode.GFZP01: return GFZP01;
                case GameCode.GFZJ8P: return GFZJ8P;
                default:
                    throw new System.ArgumentException($"Invalid game code {gameCode}");
            }
        }
    }
}
