namespace GameCube.GFZ.LineREL
{
    /// <summary>
    ///     Main address lookup directory
    /// </summary>
    public static class LineLookup
    {
        public static LineInformation GFZE01 => new LineInformationGfze01();
        public static LineInformation GFZJ01 => new LineInformationGfzj01();
        public static LineInformation GFZP01 => new LineInformationGfzp01();
        public static LineInformation GFZJ8P => new MainDolDataBlocksGfzj8p();

        public static LineInformation GetInfo(GameCode gameCode)
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
