using UnityEngine;
using UnityEngine.UI;

public class PuzzleImageSelector : MonoBehaviour
{
    [SerializeField] private Sprite[] puzzleImages;
    [SerializeField] private int selectedImageIndex = 0;
    [SerializeField] private Image imageContainer;

    public void LoadSelectedImage()
    {
        if (puzzleImages == null || puzzleImages.Length == 0)
        {
            Debug.LogWarning("No puzzle images assigned.");
            return;
        }

        if (selectedImageIndex < 0 || selectedImageIndex >= puzzleImages.Length)
        {
            Debug.LogWarning("Invalid puzzle image index.");
            return;
        }

        if (imageContainer == null)
        {
            Debug.LogWarning("Image Container is not assigned.");
            return;
        }

        imageContainer.sprite = puzzleImages[selectedImageIndex];
        imageContainer.preserveAspect = true;
    }
}