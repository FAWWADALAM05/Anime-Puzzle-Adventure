using UnityEngine;

public class PuzzleCompletion : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private int totalPieces = 16;

    private int completedPieces = 0;

    private bool puzzleCompleted = false;

    private ImageSlicer imageSlicer;


    // ====================================================
    // START
    // ====================================================

    private void Start()
    {
        // Find ImageSlicer on the same GameObject
        imageSlicer =
            GetComponent<ImageSlicer>();

        if (imageSlicer != null)
        {
            // Get total pieces from current difficulty
            totalPieces =
                imageSlicer.GetPieceCount();

            Debug.Log(
                "Puzzle Completion Settings: " +
                imageSlicer.GetDifficulty() +
                " = " +
                totalPieces +
                " pieces"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: ImageSlicer component not found!"
            );
        }
    }


    // ====================================================
    // PIECE PLACED CORRECTLY
    // ====================================================

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


    // ====================================================
    // COMPLETE PUZZLE
    // ====================================================

    private void CompletePuzzle()
    {
        puzzleCompleted = true;

        Debug.Log(
            "🎉 PUZZLE COMPLETE!"
        );
    }


    // ====================================================
    // GET COMPLETED PIECES
    // ====================================================

    public int GetCompletedPieces()
    {
        return completedPieces;
    }


    // ====================================================
    // GET TOTAL PIECES
    // ====================================================

    public int GetTotalPieces()
    {
        return totalPieces;
    }


    // ====================================================
    // GET PUZZLE COMPLETION STATUS
    // ====================================================

    public bool IsPuzzleCompleted()
    {
        return puzzleCompleted;
    }
}