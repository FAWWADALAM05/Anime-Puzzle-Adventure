using UnityEngine;

public class PuzzlePiecePlacement : MonoBehaviour
{
    [Header("Correct Position")]
    public Vector3 correctPosition;

    [Header("Placement Settings")]
    public float snapDistance = 20f;

    private RectTransform rectTransform;

    private PuzzlePieceMovement movement;

    private bool isPlacedCorrectly = false;

    private PuzzleCompletion puzzleCompletion;

    private void Awake()
    {
        rectTransform =
            GetComponent<RectTransform>();

        movement =
            GetComponent<PuzzlePieceMovement>();

        puzzleCompletion =
            GetComponentInParent<PuzzleCompletion>();
    }

    public bool CheckPlacement()
    {
        if (rectTransform == null)
            return false;

        if (isPlacedCorrectly)
            return true;

        Vector2 targetPosition =
            new Vector2(
                correctPosition.x,
                correctPosition.y
            );

        float distance =
            Vector2.Distance(
                rectTransform.anchoredPosition,
                targetPosition
            );

        if (distance <= snapDistance)
        {
            SnapToCorrectPosition();

            return true;
        }

        return false;
    }

    private void SnapToCorrectPosition()
    {
        // Move exactly to correct position
        rectTransform.anchoredPosition =
            new Vector2(
                correctPosition.x,
                correctPosition.y
            );

        // Mark as correctly placed
        isPlacedCorrectly = true;

        // Lock piece
        if (movement != null)
        {
            movement.LockPiece();
        }

        Debug.Log(
            gameObject.name +
            " snapped to correct position and locked!"
        );

        // Tell PuzzleCompletion
        if (puzzleCompletion != null)
        {
            puzzleCompletion.PiecePlacedCorrectly();
        }
    }

    public bool IsPlacedCorrectly()
    {
        return isPlacedCorrectly;
    }
}