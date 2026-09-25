using UnityEngine;

public class PuzzleCompletion : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private int totalPieces = 16;

    [Header("Timer")]
    [SerializeField] private PuzzleStopwatch puzzleStopwatch;

    [Header("Time Score")]
    [SerializeField] private TimeScore timeScore;

    private int completedPieces = 0;
    private bool puzzleCompleted = false;

    [SerializeField] private ImageSlicer imageSlicer;


    // ====================================================
    // START
    // ====================================================

    private void Start()
    {
        // Find ImageSlicer automatically if not assigned
        if (imageSlicer == null)
        {
            imageSlicer = FindFirstObjectByType<ImageSlicer>();
        }

        if (imageSlicer != null)
        {
            totalPieces = imageSlicer.GetPieceCount();

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
                "PuzzleCompletion: ImageSlicer not found!"
            );
        }


        // Find PuzzleStopwatch automatically if not assigned
        if (puzzleStopwatch == null)
        {
            puzzleStopwatch = FindFirstObjectByType<PuzzleStopwatch>();
        }

        if (puzzleStopwatch != null)
        {
            Debug.Log(
                "PuzzleCompletion: PuzzleStopwatch connected!"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: PuzzleStopwatch not found!"
            );
        }


        // Find TimeScore automatically if not assigned
        if (timeScore == null)
        {
            timeScore = FindFirstObjectByType<TimeScore>();
        }

        if (timeScore != null)
        {
            Debug.Log(
                "PuzzleCompletion: TimeScore connected!"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: TimeScore not found!"
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
        if (puzzleCompleted)
            return;

        puzzleCompleted = true;

        Debug.Log("🎉 PUZZLE COMPLETE!");


        // ====================================================
        // STOP TIMER
        // ====================================================

        if (puzzleStopwatch != null)
        {
            puzzleStopwatch.StopTimer();

            Debug.Log(
                "⏱ NEW Puzzle Stopwatch STOP command sent!"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: PuzzleStopwatch is NULL!"
            );

            return;
        }


        // ====================================================
        // GET COMPLETION TIME
        // ====================================================

        float completionTime =
            puzzleStopwatch.GetCompletionTime();

        Debug.Log(
            "⏱ COMPLETION TIME: " +
            completionTime.ToString("F2") +
            " seconds"
        );


        // ====================================================
        // CALCULATE TIME SCORE
        // ====================================================

        if (timeScore != null)
        {
            int score =
                timeScore.CalculateScore(completionTime);

            Debug.Log(
                "🏆 FINAL TIME SCORE: " +
                score +
                " POINTS"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: TimeScore is NULL!"
            );
        }
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