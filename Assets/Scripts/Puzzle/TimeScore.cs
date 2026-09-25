using UnityEngine;

public class TimeScore : MonoBehaviour
{
    [Header("Time Bonus Settings")]
    [SerializeField] private int startingTimeBonus = 1000;
    [SerializeField] private int pointsLostPerSecond = 10;

    private int finalTimeBonus = 0;

    // ====================================================
    // CALCULATE TIME BONUS
    // ====================================================

    public int CalculateScore(float completionTime)
    {
        int seconds = Mathf.FloorToInt(completionTime);

        int scoreLost =
            seconds * pointsLostPerSecond;

        finalTimeBonus = Mathf.Max(
            0,
            startingTimeBonus - scoreLost
        );

        Debug.Log(
            "⏱ TIME BONUS: " +
            finalTimeBonus +
            " POINTS"
        );

        return finalTimeBonus;
    }

    // ====================================================
    // GET FINAL TIME BONUS
    // ====================================================

    public int GetFinalScore()
    {
        return finalTimeBonus;
    }
}