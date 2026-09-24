using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("Correct Position")]
    public Vector3 correctPosition;

    // Set the correct position for this puzzle piece
    public void SetCorrectPosition(Vector3 position)
    {
        correctPosition = position;
    }
}