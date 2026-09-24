using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePieceMovement : MonoBehaviour,
    IPointerDownHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private RectTransform rectTransform;

    [Header("Puzzle Area")]
    [SerializeField] private RectTransform puzzleArea;

    [Header("Puzzle Playground")]
    [SerializeField] private float playgroundWidth = 306f;
    [SerializeField] private float playgroundHeight = 306f;

    private Vector2 pointerOffset;

    private PuzzlePiecePlacement placement;

    private bool isLocked = false;

    private void Awake()
    {
        rectTransform =
            GetComponent<RectTransform>();

        placement =
            GetComponent<PuzzlePiecePlacement>();

        if (puzzleArea == null)
        {
            puzzleArea =
                rectTransform.parent as RectTransform;
        }
    }

    public void SetPuzzleArea(RectTransform area)
    {
        puzzleArea = area;
    }

    // ====================================================
    // LOCK PIECE
    // ====================================================

    public void LockPiece()
    {
        isLocked = true;

        // Move locked piece UNDER all unlocked pieces
        transform.SetAsFirstSibling();

        Debug.Log(
            gameObject.name +
            " movement locked and moved underneath unlocked pieces!"
        );
    }

    // ====================================================
    // POINTER DOWN
    // ====================================================

    public void OnPointerDown(
        PointerEventData eventData)
    {
        if (isLocked)
            return;

        if (placement != null &&
            placement.IsPlacedCorrectly())
        {
            isLocked = true;
            return;
        }

        // Bring unlocked piece to the top
        transform.SetAsLastSibling();

        if (puzzleArea == null)
            return;

        if (RectTransformUtility
            .ScreenPointToLocalPointInRectangle(
                puzzleArea,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint))
        {
            pointerOffset =
                rectTransform.anchoredPosition -
                localPoint;
        }
    }

    // ====================================================
    // BEGIN DRAG
    // ====================================================

    public void OnBeginDrag(
        PointerEventData eventData)
    {
        if (isLocked)
            return;

        if (placement != null &&
            placement.IsPlacedCorrectly())
        {
            isLocked = true;
            return;
        }

        // Always put dragged piece on top
        transform.SetAsLastSibling();
    }

    // ====================================================
    // DRAG
    // ====================================================

    public void OnDrag(
        PointerEventData eventData)
    {
        if (isLocked)
            return;

        if (placement != null &&
            placement.IsPlacedCorrectly())
        {
            isLocked = true;
            return;
        }

        if (puzzleArea == null)
            return;

        if (!RectTransformUtility
            .ScreenPointToLocalPointInRectangle(
                puzzleArea,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint))
        {
            return;
        }

        Vector2 targetPosition =
            localPoint + pointerOffset;

        float halfWidth =
            rectTransform.rect.width * 0.5f;

        float halfHeight =
            rectTransform.rect.height * 0.5f;

        float minX =
            -(playgroundWidth * 0.5f) +
            halfWidth;

        float maxX =
            (playgroundWidth * 0.5f) -
            halfWidth;

        float minY =
            -(playgroundHeight * 0.5f) +
            halfHeight;

        float maxY =
            (playgroundHeight * 0.5f) -
            halfHeight;

        targetPosition.x =
            Mathf.Clamp(
                targetPosition.x,
                minX,
                maxX
            );

        targetPosition.y =
            Mathf.Clamp(
                targetPosition.y,
                minY,
                maxY
            );

        rectTransform.anchoredPosition =
            targetPosition;

        if (placement != null)
        {
            placement.CheckPlacement();
        }
    }

    // ====================================================
    // END DRAG
    // ====================================================

    public void OnEndDrag(
        PointerEventData eventData)
    {
        if (isLocked)
            return;

        if (placement != null)
        {
            placement.CheckPlacement();
        }
    }
}