namespace GameCube.GFZ.GCI
{
    public enum GfzGciFileType : uint
    {
        Emblem  = 0xaf85d074, // "Emblem".GetHashCode().ToString("x8")
        Garage  = 0x875b9557, // "Garage".GetHashCode().ToString("x8")
        Ghost   = 0xd675dc6f, // "Ghost".GetHashCode().ToString("x8")
        Replay  = 0xfe40216f, // "Replay".GetHashCode().ToString("x8")
        Save    = 0x8ac5d2c5, // "Save".GetHashCode().ToString("x8")
    }
}
