using UnityEngine;

public enum Difficulty
{
    Beginner,
    Easy,
    Normal,
    Hard,
    Expert,
    Master,
    Extreme
}

public static class DifficultySystem
{
    public static int GetPieceCount(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Beginner:
                return 9;

            case Difficulty.Easy:
                return 16;

            case Difficulty.Normal:
                return 32;

            case Difficulty.Hard:
                return 64;

            case Difficulty.Expert:
                return 124;

            case Difficulty.Master:
                return 256;

            case Difficulty.Extreme:
                return 324;

            default:
                return 9;
        }
    }

    public static int GetGridSize(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Beginner:
                return 3;

            case Difficulty.Easy:
                return 4;

            case Difficulty.Normal:
                return 4;

            case Difficulty.Hard:
                return 8;

            case Difficulty.Expert:
                return 0;

            case Difficulty.Master:
                return 16;

            case Difficulty.Extreme:
                return 18;

            default:
                return 3;
        }
    }
}