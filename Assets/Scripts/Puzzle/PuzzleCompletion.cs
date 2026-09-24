using UnityEngine;

public class PuzzleCompletion : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private int totalPieces = 16;

    private int completedPieces = 0;

    private bool puzzleCompleted = false;

    public void PiecePlacedCorrectly()
    {
        if (puzzleCompleted)
            return;

        completedPieces++;

        Debug.Log(
            "Puzzle Progress: " +
            completedPieces +
            " / " +
            totalPieces
        );

        if (completedPieces >= totalPieces)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        puzzleCompleted = true;

        Debug.Log(
            "🎉 PUZZLE COMPLETE!"
        );
    }

    public int GetCompletedPieces()
    {
        return completedPieces;
    }

    public bool IsPuzzleCompleted()
    {
        return puzzleCompleted;
    }
}