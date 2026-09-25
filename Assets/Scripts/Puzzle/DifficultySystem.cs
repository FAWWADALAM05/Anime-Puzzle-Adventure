using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Expert
}

public static class DifficultySystem
{
    public static int GetGridSize(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return 3;

            case Difficulty.Normal:
                return 4;

            case Difficulty.Hard:
                return 5;

            case Difficulty.Expert:
                return 6;

            default:
                return 3;
        }
    }

    public static int GetPieceCount(Difficulty difficulty)
    {
        int gridSize = GetGridSize(difficulty);
        return gridSize * gridSize;
    }
}