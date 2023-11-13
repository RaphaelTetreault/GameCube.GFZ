namespace GameCube.GFZ
{
    [System.Flags]
    public enum GameCodeFields
    {
        // Games
        GX = 1 << 0,
        AX = 1 << 1,

        // Regions
        Japan = 1 << 2,
        NorthAmerica = 1 << 3,
        Europe = 1 << 4,
    }
}
