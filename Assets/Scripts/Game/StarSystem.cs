using UnityEngine;
using TMPro;

public class StarSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text starText;

    [Header("Star Requirements")]
    [SerializeField] private int threeStarMoves = 20;
    [SerializeField] private int twoStarMoves = 40;

    private int stars = 0;

    public int Stars => stars;

    public void CalculateStars(int moves)
    {
        if (moves <= threeStarMoves)
        {
            stars = 3;
        }
        else if (moves <= twoStarMoves)
        {
            stars = 2;
        }
        else
        {
            stars = 1;
        }

        UpdateStarText();
    }

    private void UpdateStarText()
    {
        if (starText != null)
        {
            starText.text = "Stars: " + stars;
        }
    }

    public void ResetStars()
    {
        stars = 0;
        UpdateStarText();
    }
}