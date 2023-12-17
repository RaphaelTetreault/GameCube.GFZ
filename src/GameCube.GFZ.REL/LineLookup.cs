namespace GameCube.GFZ.LineREL
{
    /// <summary>
    ///     Main address lookup directory
    /// </summary>
    public static class LineLookup
    {
        public static LineRelInfo GFZE01 => new LineInfoGfze01();
        public static LineRelInfo GFZJ01 => new LineRelInfoGfzj01();
        public static LineRelInfo GFZP01 => new LineRelInfoGfzp01();
        public static LineRelInfo GFZJ8P => new LineRelInfoGfzj8p();

        public static LineRelInfo GetInfo(GameCode gameCode)
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
