using UnityEngine;

public class PuzzleCompletion : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private int totalPieces = 16;

    [Header("Timer")]
    [SerializeField] private PuzzleStopwatch puzzleStopwatch;

    [Header("Time Score")]
    [SerializeField] private TimeScore timeScore;

    [Header("Moves & Score")]
    [SerializeField] private MoveCounter moveCounter;
    [SerializeField] private ScoreSystem scoreSystem;

    [Header("Star System")]
    [SerializeField] private StarSystem starSystem;

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


        // Find PuzzleStopwatch automatically
        if (puzzleStopwatch == null)
        {
            puzzleStopwatch =
                FindFirstObjectByType<PuzzleStopwatch>();
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


        // Find TimeScore automatically
        if (timeScore == null)
        {
            timeScore =
                FindFirstObjectByType<TimeScore>();
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


        // Find MoveCounter automatically
        if (moveCounter == null)
        {
            moveCounter =
                FindFirstObjectByType<MoveCounter>();
        }

        if (moveCounter != null)
        {
            Debug.Log(
                "PuzzleCompletion: MoveCounter connected!"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: MoveCounter not found!"
            );
        }


        // Find ScoreSystem automatically
        if (scoreSystem == null)
        {
            scoreSystem =
                FindFirstObjectByType<ScoreSystem>();
        }

        if (scoreSystem != null)
        {
            Debug.Log(
                "PuzzleCompletion: ScoreSystem connected!"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: ScoreSystem not found!"
            );
        }


        // Find StarSystem automatically
        if (starSystem == null)
        {
            starSystem =
                FindFirstObjectByType<StarSystem>();
        }

        if (starSystem != null)
        {
            Debug.Log(
                "PuzzleCompletion: StarSystem connected!"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: StarSystem not found!"
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


        // ====================================================
        // ADD MOVE
        // ====================================================

        if (moveCounter != null)
        {
            moveCounter.AddMove();
        }


        // ====================================================
        // ADD MOVE SCORE
        // ====================================================

        if (scoreSystem != null)
        {
            scoreSystem.AddMove();
        }


        Debug.Log(
            "Puzzle Progress: " +
            completedPieces +
            " / " +
            totalPieces
        );


        // ====================================================
        // CHECK COMPLETION
        // ====================================================

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
                "⏱ Puzzle Stopwatch STOP command sent!"
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
        // CALCULATE TIME BONUS
        // ====================================================

        int timeBonus = 0;

        if (timeScore != null)
        {
            timeBonus =
                timeScore.CalculateScore(
                    completionTime
                );

            Debug.Log(
                "⏱ TIME BONUS: " +
                timeBonus +
                " POINTS"
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: TimeScore is NULL!"
            );
        }


        // ====================================================
        // ADD TIME BONUS TO FINAL SCORE
        // ====================================================

        if (scoreSystem != null)
        {
            scoreSystem.AddTimeBonus(timeBonus);

            Debug.Log(
                "🏆 FINAL SCORE: " +
                scoreSystem.Score +
                " POINTS"
            );
        }


        // ====================================================
        // CALCULATE STARS
        // ====================================================

        if (starSystem != null && moveCounter != null)
        {
            starSystem.CalculateStars(
                moveCounter.Moves
            );

            Debug.Log(
                "⭐ FINAL STARS: " +
                starSystem.Stars
            );
        }
        else
        {
            Debug.LogError(
                "PuzzleCompletion: StarSystem or MoveCounter is NULL!"
            );
        }


        // ====================================================
        // FINAL MOVE LOG
        // ====================================================

        if (moveCounter != null)
        {
            Debug.Log(
                "👣 FINAL MOVES: " +
                moveCounter.Moves
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