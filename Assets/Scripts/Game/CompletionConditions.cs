using UnityEngine;
using TMPro;

public class CompletionConditions : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text resultText;

    [Header("Systems")]
    [SerializeField] private MoveCounter moveCounter;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private StarSystem starSystem;

    private bool completed = false;

    public void CompletePuzzle()
    {
        if (completed)
            return;

        completed = true;

        int moves = moveCounter != null ? moveCounter.Moves : 0;

        if (scoreSystem != null)
        {
            scoreSystem.AddMove();
        }

        if (starSystem != null)
        {
            starSystem.CalculateStars(moves);
        }

        ShowResult(moves);
    }

    private void ShowResult(int moves)
    {
        if (resultText != null)
        {
            int score = scoreSystem != null ? scoreSystem.Score : 0;
            int stars = starSystem != null ? starSystem.Stars : 0;

            resultText.text =
                "Puzzle Complete!\n" +
                "Moves: " + moves + "\n" +
                "Score: " + score + "\n" +
                "Stars: " + stars;
        }
    }

    public void ResetCompletion()
    {
        completed = false;
    }
}