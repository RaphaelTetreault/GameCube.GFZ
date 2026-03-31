namespace GameCube.GFZ;

public class GameCodeUtility
{
    public static GameCode SetRegion(GameCode gameCode, GameCodeFlags newRegion)
    {
        // Validate incoming region
        if ((newRegion & GameCodeFlags.MaskRegion) == 0)
        {
            string msg = $"{nameof(GameCodeFlags)} {newRegion} is not a region!";
            throw new System.ArgumentException(msg);
        }

        // Strip old region and apply new region
        GameCodeFlags value = (GameCodeFlags)gameCode;
        value &= ~GameCodeFlags.MaskRegion;
        value |= newRegion;
        return (GameCode)value;
    }

    public static GameCode SetGame(GameCode gameCode, GameCodeFlags newGame)
    {
        // Validate incoming game
        if ((newGame & GameCodeFlags.MaskRegion) == 0)
        {
            string msg = $"{nameof(GameCodeFlags)} {newGame} is not a game!";
            throw new System.ArgumentException(msg);
        }

        // Strip old region and apply new region
        GameCodeFlags value = (GameCodeFlags)gameCode;
        value &= ~GameCodeFlags.MaskGame;
        value |= newGame;
        return (GameCode)value;
    }

    public static GameCodeFlags GetRegion(GameCode gameCode)
    {
        GameCodeFlags fields = (GameCodeFlags)gameCode & GameCodeFlags.MaskRegion;
        return fields;
    }

    public static GameCodeFlags GetGame(GameCode gameCode)
    {
        GameCodeFlags fields = (GameCodeFlags)gameCode & GameCodeFlags.MaskGame;
        return fields;
    }
}
