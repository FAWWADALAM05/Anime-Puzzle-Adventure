using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintSystem : MonoBehaviour
{
    [Header("Hint Settings")]
    [SerializeField] private int maxHighlightUses = 3;
    [SerializeField] private int maxRevealUses = 3;

    [Header("Puzzle Board")]
    [SerializeField] private RectTransform puzzleBoard;

    [Header("Highlight Appearance")]
    [SerializeField] private float borderThickness = 6f;

    // Exact #8B0000
    private readonly Color highlightColor =
        new Color32(139, 0, 0, 255);

    [Header("Highlight UI")]
    [SerializeField] private Button highlightButton;
    [SerializeField] private TMP_Text highlightCountText;

    [Header("Reveal UI")]
    [SerializeField] private Button revealButton;
    [SerializeField] private TMP_Text revealCountText;

    private int highlightUses;
    private int revealUses;

    private GameObject currentHighlight;
    private Outline currentPieceOutline;

    private void Start()
    {
        highlightUses = maxHighlightUses;
        revealUses = maxRevealUses;

        UpdateUI();

        if (highlightButton != null)
        {
            highlightButton.onClick.AddListener(UseHighlight);
        }

        if (revealButton != null)
        {
            revealButton.onClick.AddListener(UseReveal);
        }
    }

    // =========================================================
    // HIGHLIGHT BUTTON
    // =========================================================

    public void UseHighlight()
    {
        if (highlightUses <= 0)
        {
            Debug.Log("No Highlight hints remaining.");
            return;
        }

        PuzzlePiecePlacement unsolvedPiece =
            FindUnsolvedPiece();

        if (unsolvedPiece == null)
        {
            Debug.Log("No unsolved puzzle piece found.");
            return;
        }

        highlightUses--;

        UpdateUI();

        HighlightCorrectLocation(unsolvedPiece);

        Debug.Log(
            "Highlight used. Piece: " +
            unsolvedPiece.gameObject.name +
            ". Remaining highlights: " +
            highlightUses
        );
    }

    // =========================================================
    // REVEAL BUTTON
    // =========================================================

    public void UseReveal()
    {
        if (revealUses <= 0)
        {
            Debug.Log("No Reveal hints remaining.");
            return;
        }

        PuzzlePiecePlacement unsolvedPiece =
            FindUnsolvedPiece();

        if (unsolvedPiece == null)
        {
            Debug.Log("No unsolved puzzle piece found.");
            return;
        }

        revealUses--;

        UpdateUI();

        RevealPiece(unsolvedPiece);

        Debug.Log(
            "Reveal used. Piece: " +
            unsolvedPiece.gameObject.name +
            ". Remaining reveals: " +
            revealUses
        );
    }

    // =========================================================
    // FIND UNSOLVED PIECE
    // =========================================================

    private PuzzlePiecePlacement FindUnsolvedPiece()
    {
        if (puzzleBoard == null)
        {
            Debug.LogWarning("Puzzle Board is not assigned.");
            return null;
        }

        PuzzlePiecePlacement[] pieces =
            puzzleBoard.GetComponentsInChildren<PuzzlePiecePlacement>(true);

        foreach (PuzzlePiecePlacement piece in pieces)
        {
            if (!piece.IsPlacedCorrectly())
            {
                return piece;
            }
        }

        return null;
    }

    // =========================================================
    // HIGHLIGHT CORRECT LOCATION
    // =========================================================

    private void HighlightCorrectLocation(
        PuzzlePiecePlacement piece)
    {
        RemoveCurrentHighlight();
        RemovePieceOutline();

        RectTransform pieceRect =
            piece.GetComponent<RectTransform>();

        if (pieceRect == null)
        {
            Debug.LogError(
                "Puzzle piece has no RectTransform!"
            );

            return;
        }

        // -----------------------------------------------------
        // CREATE RIGHT-PLACE HIGHLIGHT
        // -----------------------------------------------------

        currentHighlight =
            new GameObject("HintHighlight");

        RectTransform highlightRect =
            currentHighlight.AddComponent<RectTransform>();

        // Same parent as puzzle piece
        highlightRect.SetParent(
            pieceRect.parent,
            false
        );

        // Same size as puzzle piece
        highlightRect.anchorMin =
            pieceRect.anchorMin;

        highlightRect.anchorMax =
            pieceRect.anchorMax;

        highlightRect.pivot =
            pieceRect.pivot;

        highlightRect.sizeDelta =
            pieceRect.sizeDelta;

        // Correct puzzle position
        highlightRect.anchoredPosition =
            new Vector2(
                piece.correctPosition.x,
                piece.correctPosition.y
            );

        // -----------------------------------------------------
        // PUT HIGHLIGHT DIRECTLY BEHIND PUZZLE PIECE
        // -----------------------------------------------------

        int pieceIndex =
            piece.transform.GetSiblingIndex();

        highlightRect.SetSiblingIndex(
            pieceIndex
        );

        // -----------------------------------------------------
        // CREATE BORDER
        // -----------------------------------------------------

        CreateBorder(
            currentHighlight.transform,
            "Top"
        );

        CreateBorder(
            currentHighlight.transform,
            "Bottom"
        );

        CreateBorder(
            currentHighlight.transform,
            "Left"
        );

        CreateBorder(
            currentHighlight.transform,
            "Right"
        );

        // -----------------------------------------------------
        // NON-BLOCKING
        // -----------------------------------------------------

        CanvasGroup canvasGroup =
            currentHighlight.AddComponent<CanvasGroup>();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        // -----------------------------------------------------
        // HIGHLIGHT ACTUAL PUZZLE PIECE
        // -----------------------------------------------------

        HighlightPuzzlePiece(piece);

        Debug.Log(
            "RIGHT PLACE HIGHLIGHT CREATED: " +
            piece.gameObject.name +
            " Position: " +
            piece.correctPosition
        );
    }

    // =========================================================
    // PUZZLE PIECE OUTLINE
    // =========================================================

    private void HighlightPuzzlePiece(
        PuzzlePiecePlacement piece)
    {
        currentPieceOutline =
            piece.GetComponent<Outline>();

        if (currentPieceOutline == null)
        {
            currentPieceOutline =
                piece.gameObject.AddComponent<Outline>();
        }

        // Exact #8B0000
        currentPieceOutline.effectColor =
            highlightColor;

        currentPieceOutline.effectDistance =
            new Vector2(
                borderThickness,
                borderThickness
            );

        currentPieceOutline.useGraphicAlpha = true;

        // Actual puzzle piece stays above
        // the correct-place highlight
        piece.transform.SetAsLastSibling();
    }

    // =========================================================
    // CREATE BORDER
    // =========================================================

    private void CreateBorder(
        Transform parent,
        string borderName)
    {
        GameObject border =
            new GameObject(borderName);

        Image image =
            border.AddComponent<Image>();

        // Exact #8B0000
        image.color = highlightColor;

        RectTransform rect =
            border.GetComponent<RectTransform>();

        rect.SetParent(parent, false);

        // -----------------------------------------------------
        // TOP
        // -----------------------------------------------------

        if (borderName == "Top")
        {
            rect.anchorMin =
                new Vector2(0f, 1f);

            rect.anchorMax =
                new Vector2(1f, 1f);

            rect.pivot =
                new Vector2(0.5f, 1f);

            rect.sizeDelta =
                new Vector2(
                    0f,
                    borderThickness
                );

            rect.anchoredPosition =
                Vector2.zero;
        }

        // -----------------------------------------------------
        // BOTTOM
        // -----------------------------------------------------

        else if (borderName == "Bottom")
        {
            rect.anchorMin =
                new Vector2(0f, 0f);

            rect.anchorMax =
                new Vector2(1f, 0f);

            rect.pivot =
                new Vector2(0.5f, 0f);

            rect.sizeDelta =
                new Vector2(
                    0f,
                    borderThickness
                );

            rect.anchoredPosition =
                Vector2.zero;
        }

        // -----------------------------------------------------
        // LEFT
        // -----------------------------------------------------

        else if (borderName == "Left")
        {
            rect.anchorMin =
                new Vector2(0f, 0f);

            rect.anchorMax =
                new Vector2(0f, 1f);

            rect.pivot =
                new Vector2(0f, 0.5f);

            rect.sizeDelta =
                new Vector2(
                    borderThickness,
                    0f
                );

            rect.anchoredPosition =
                Vector2.zero;
        }

        // -----------------------------------------------------
        // RIGHT
        // -----------------------------------------------------

        else if (borderName == "Right")
        {
            rect.anchorMin =
                new Vector2(1f, 0f);

            rect.anchorMax =
                new Vector2(1f, 1f);

            rect.pivot =
                new Vector2(1f, 0.5f);

            rect.sizeDelta =
                new Vector2(
                    borderThickness,
                    0f
                );

            rect.anchoredPosition =
                Vector2.zero;
        }
    }

    // =========================================================
    // REVEAL PIECE
    // =========================================================

    private void RevealPiece(
        PuzzlePiecePlacement piece)
    {
        RemoveCurrentHighlight();
        RemovePieceOutline();

        piece.RevealAndLockPiece();

        Debug.Log(
            piece.gameObject.name +
            " was revealed by Hint and locked!"
        );
    }

    // =========================================================
    // REMOVE CURRENT HIGHLIGHT
    // =========================================================

    private void RemoveCurrentHighlight()
    {
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
            currentHighlight = null;
        }
    }

    // =========================================================
    // REMOVE PUZZLE PIECE OUTLINE
    // =========================================================

    private void RemovePieceOutline()
    {
        if (currentPieceOutline != null)
        {
            Destroy(currentPieceOutline);
            currentPieceOutline = null;
        }
    }

    // =========================================================
    // UPDATE UI
    // =========================================================

    private void UpdateUI()
    {
        if (highlightCountText != null)
        {
            highlightCountText.text =
                highlightUses.ToString();
        }

        if (revealCountText != null)
        {
            revealCountText.text =
                revealUses.ToString();
        }

        if (highlightButton != null)
        {
            highlightButton.interactable =
                highlightUses > 0;
        }

        if (revealButton != null)
        {
            revealButton.interactable =
                revealUses > 0;
        }
    }

    // =========================================================
    // GETTERS
    // =========================================================

    public int GetRemainingHighlightUses()
    {
        return highlightUses;
    }

    public int GetRemainingRevealUses()
    {
        return revealUses;
    }
}