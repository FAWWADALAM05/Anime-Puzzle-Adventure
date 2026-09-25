using UnityEngine;

public class TimeScore : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int startingScore = 1000;
    [SerializeField] private int pointsLostPerSecond = 10;

    private int finalScore = 0;


    // ====================================================
    // CALCULATE SCORE
    // ====================================================

    public int CalculateScore(float completionTime)
    {
        int seconds = Mathf.FloorToInt(completionTime);

        int scoreLost = seconds * pointsLostPerSecond;

        finalScore = Mathf.Max(
            0,
            startingScore - scoreLost
        );

        Debug.Log(
            "🏆 TIME SCORE: " +
            finalScore +
            " points"
        );

        return finalScore;
    }


    // ====================================================
    // GET FINAL SCORE
    // ====================================================

    public int GetFinalScore()
    {
        return finalScore;
    }
}