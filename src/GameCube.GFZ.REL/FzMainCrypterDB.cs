namespace GameCube.GFZ.REL;

/// <summary>
///     DataBase for <see cref="FzMainCrypter"/>.
/// </summary>
public static class FzMainCrypterDB
{
    /// <summary>
    ///     Get <see cref="FzMainCrypter"/> data related to <paramref name="gameCode"/>.
    /// </summary>
    /// <param name="gameCode"></param>
    /// <returns>
    ///     <see cref="FzMainCrypter"/> data related to <paramref name="gameCode"/>.
    /// </returns>
    /// <exception cref="System.ArgumentException">
    ///     Raised if unable to map <paramref name="gameCode"/>.
    /// </exception>
    public static FzMainCrypter Get(GameCode gameCode)
    {
        return gameCode switch
        {
            GameCode.GFZJ01                    => Japanese,
            GameCode.GFZE01 or GameCode.GFZP01 => Latin,
            GameCode.GFZJ8P or GameCode.GGGE6E => None,
            _ => throw new System.ArgumentException($"Invalid game code {gameCode}"),
        };
    }

    /// <summary>
    ///     Encryption/decryption parameters for <see cref="GameCode.GFZJ01"/>.
    /// </summary>
    public static readonly FzMainCrypter Japanese = new()
    {
        Salt = unchecked((short)0x0ce0),
        Key0 = unchecked((int)0x0004f107),
        Key1 = unchecked((int)0xb5fb6483),
        Key2 = unchecked((int)0xdeaddead),
        BlockKey0 = unchecked((int)0xb5fb0000),
        BlockKey1 = unchecked((short)0x6483),
        BlockKey2 = unchecked((short)0xf107),
    };

    /// <summary>
    ///     Encryption/decryption parameters for <see cref="GameCode.GFZE01"/>
    ///     and <see cref="GameCode.GFZP01"/>.
    /// </summary>
    public static readonly FzMainCrypter Latin = new()
    {
        Salt = unchecked((short)0x180a),
        Key0 = unchecked((int)0x000cd8f3),
        Key1 = unchecked((int)0x9b36bb94),
        Key2 = unchecked((int)0xaf8910be),
        BlockKey0 = unchecked((int)0x9b370000),
        BlockKey1 = unchecked((short)0xbb94),
        BlockKey2 = unchecked((short)0xd8f3),
    };

    public static readonly FzMainCrypter None = new();

}
