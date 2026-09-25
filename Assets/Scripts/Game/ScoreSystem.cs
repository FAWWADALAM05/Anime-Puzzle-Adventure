using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Move Score")]
    [SerializeField] private int pointsPerMove = 10;

    private int score = 0;

    public int Score => score;

    private void Start()
    {
        ResetScore();
    }

    // ====================================================
    // ADD MOVE SCORE
    // ====================================================

    public void AddMove()
    {
        score += pointsPerMove;

        UpdateScoreText();
    }

    // ====================================================
    // ADD TIME BONUS
    // ====================================================

    public void AddTimeBonus(int timeBonus)
    {
        score += timeBonus;

        UpdateScoreText();
    }

    // ====================================================
    // RESET SCORE
    // ====================================================

    public void ResetScore()
    {
        score = 0;

        UpdateScoreText();
    }

    // ====================================================
    // UPDATE UI
    // ====================================================

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}