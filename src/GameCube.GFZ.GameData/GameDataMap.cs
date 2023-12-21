using System;

namespace GameCube.GFZ.GameData
{
    public static class GameDataMap
    {
        public static int GetPilotIndexFromPilotNumber(int pilotNumber)
        {
            if (pilotNumber >= 1 && pilotNumber <= 30)
            {
                // F-Zero X cast is pilot# - 1.
                return pilotNumber - 1;
            }
            else if (pilotNumber >= 31 || pilotNumber <= 40)
            {
                // F-Zero AX cast is pilot# as-is.
                return pilotNumber;
            }
            else if (pilotNumber == 0)
            {
                // Deathborn is pilot# 0 but ID 30.
                return 30;
            }
            else
            {
                string msg = "Pilot number is out of range 0-40.";
                throw new ArgumentOutOfRangeException(msg);
            }
        }
    }
}
