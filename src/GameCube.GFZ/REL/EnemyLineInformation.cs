using System.IO;

namespace GameCube.GFZ.REL
{
    /// <summary>
    /// Main address lookup directory
    /// </summary>
    public static class EnemyLineInformation
    {
        public enum GameCode
        {
            GX_J,
            GX_E,
            GX_P,
            AX,
        }

        public static readonly EnemyLineInformationLookup GFZE01 = new EnemyLineInformationLookupGfze01();
        public static readonly EnemyLineInformationLookup GFZJ01 = new EnemyLineInformationLookupGfzj01();
        public static readonly EnemyLineInformationLookup GFZP01 = new EnemyLineInformationLookupGfzp01();
        public static readonly EnemyLineInformationLookup GFZJ8P = new MainDolInformationLookupGfzj8p();

        public static EnemyLineInformationLookup GetInfo(GameCode gameCode)
        {
            switch (gameCode)
            {
                case GameCode.GX_J: return GFZJ01;
                case GameCode.GX_E: return GFZE01;
                case GameCode.GX_P: return GFZP01;
                case GameCode.AX: return GFZJ8P;
                default:
                    throw new System.ArgumentException($"Invalid game code {gameCode}");
            }
        }
    }
}
