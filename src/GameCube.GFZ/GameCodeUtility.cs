namespace GameCube.GFZ;

public class GameCodeUtility
{
    public static GameCode SetRegion(GameCode gameCode, GameCodeFields newRegion)
    {
        // Validate incoming region
        if ((newRegion & GameCodeFields.MaskRegion) == 0)
        {
            string msg = $"{nameof(GameCodeFields)} {newRegion} is not a region!";
            throw new System.ArgumentException(msg);
        }

        // Strip old region and apply new region
        GameCodeFields value = (GameCodeFields)gameCode;
        value &= ~GameCodeFields.MaskRegion;
        value |= newRegion;
        return (GameCode)value;
    }

    public static GameCode SetGame(GameCode gameCode, GameCodeFields newGame)
    {
        // Validate incoming game
        if ((newGame & GameCodeFields.MaskRegion) == 0)
        {
            string msg = $"{nameof(GameCodeFields)} {newGame} is not a game!";
            throw new System.ArgumentException(msg);
        }

        // Strip old region and apply new region
        GameCodeFields value = (GameCodeFields)gameCode;
        value &= ~GameCodeFields.MaskGame;
        value |= newGame;
        return (GameCode)value;
    }

    public static GameCodeFields GetRegion(GameCode gameCode)
    {
        GameCodeFields fields = (GameCodeFields)gameCode & GameCodeFields.MaskRegion;
        return fields;
    }

    public static GameCodeFields GetGame(GameCode gameCode)
    {
        GameCodeFields fields = (GameCodeFields)gameCode & GameCodeFields.MaskGame;
        return fields;
    }
}
