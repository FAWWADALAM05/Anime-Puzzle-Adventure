using UnityEngine;
using TMPro;

public class MoveCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text moveText;

    private int moves = 0;

    public int Moves => moves;

    public void AddMove()
    {
        moves++;
        UpdateMoveText();
    }

    public void ResetMoves()
    {
        moves = 0;
        UpdateMoveText();
    }

    private void UpdateMoveText()
    {
        if (moveText != null)
        {
            moveText.text = "Moves: " + moves;
        }
    }
}