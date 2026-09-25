using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageSlicer : MonoBehaviour
{
    [Header("Difficulty")]
    [SerializeField] private Difficulty difficulty = Difficulty.Easy;

    [Header("Puzzle Settings")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 3;

    [Header("Puzzle Playground")]
    [SerializeField] private float playgroundWidth = 306f;
    [SerializeField] private float playgroundHeight = 306f;

    [Header("Puzzle Piece")]
    [SerializeField] private GameObject puzzlePiecePrefab;

    [Header("Puzzle Image")]
    [SerializeField] private Sprite puzzleImage;

    private RectTransform puzzleArea;


    // ====================================================
    // START
    // ====================================================

    private void Start()
    {
        puzzleArea = GetComponent<RectTransform>();

        GeneratePuzzle();
    }


    // ====================================================
    // GENERATE PUZZLE
    // ====================================================

    public void GeneratePuzzle()
    {
        if (puzzleArea == null)
        {
            Debug.LogError(
                "ImageSlicer must be attached to ImageContainer!"
            );

            return;
        }

        if (puzzlePiecePrefab == null)
        {
            Debug.LogError(
                "Puzzle Piece Prefab is not assigned!"
            );

            return;
        }

        if (puzzleImage == null)
        {
            Debug.LogError(
                "Puzzle Image is not assigned!"
            );

            return;
        }


        // ------------------------------------------------
        // GET GRID SIZE FROM DIFFICULTY
        // ------------------------------------------------

        rows = DifficultySystem.GetGridSize(difficulty);
        columns = DifficultySystem.GetGridSize(difficulty);

        int totalPieces =
            DifficultySystem.GetPieceCount(difficulty);

        Debug.Log(
            "Difficulty: " + difficulty +
            " | Grid: " + rows + "x" + columns +
            " | Pieces: " + totalPieces
        );


        // ------------------------------------------------
        // REMOVE OLD PUZZLE PIECES
        // ------------------------------------------------

        for (int i = puzzleArea.childCount - 1; i >= 0; i--)
        {
            Transform child =
                puzzleArea.GetChild(i);

            if (child.GetComponent<PuzzlePiecePlacement>() != null)
            {
                Destroy(child.gameObject);
            }
        }


        // ------------------------------------------------
        // DISABLE GRID LAYOUT GROUP
        // ------------------------------------------------

        GridLayoutGroup grid =
            puzzleArea.GetComponent<GridLayoutGroup>();

        if (grid != null)
        {
            grid.enabled = false;
        }


        // ------------------------------------------------
        // IMAGE SIZE
        // ------------------------------------------------

        Texture2D texture =
            puzzleImage.texture;

        Rect sourceRect =
            puzzleImage.textureRect;

        float sourcePieceWidth =
            sourceRect.width / columns;

        float sourcePieceHeight =
            sourceRect.height / rows;


        // ------------------------------------------------
        // PLAYGROUND PIECE SIZE
        // ------------------------------------------------

        float pieceWidth =
            playgroundWidth / columns;

        float pieceHeight =
            playgroundHeight / rows;


        // ------------------------------------------------
        // CORRECT POSITIONS
        // ------------------------------------------------

        List<Vector2> correctPositions =
            new List<Vector2>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x =
                    -playgroundWidth / 2f +
                    pieceWidth / 2f +
                    col * pieceWidth;

                float y =
                    playgroundHeight / 2f -
                    pieceHeight / 2f -
                    row * pieceHeight;

                correctPositions.Add(
                    new Vector2(x, y)
                );
            }
        }


        // ------------------------------------------------
        // CREATE PUZZLE PIECES
        // ------------------------------------------------

        List<GameObject> pieces =
            new List<GameObject>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int index =
                    row * columns + col;


                // ----------------------------------------
                // CREATE PIECE
                // ----------------------------------------

                GameObject piece =
                    Instantiate(
                        puzzlePiecePrefab,
                        puzzleArea
                    );

                piece.name =
                    "PuzzlePiece_" +
                    (index + 1);


                // ----------------------------------------
                // RECT TRANSFORM
                // ----------------------------------------

                RectTransform rect =
                    piece.GetComponent<RectTransform>();

                rect.sizeDelta =
                    new Vector2(
                        pieceWidth,
                        pieceHeight
                    );


                // ----------------------------------------
                // CREATE DIFFERENT IMAGE SECTION
                // ----------------------------------------

                float spriteX =
                    sourceRect.x +
                    col * sourcePieceWidth;


                // Unity texture coordinates start
                // from the BOTTOM, while our puzzle rows
                // start from the TOP.

                float spriteY =
                    sourceRect.y +
                    sourceRect.height -
                    (row + 1) *
                    sourcePieceHeight;


                Rect pieceRect =
                    new Rect(
                        spriteX,
                        spriteY,
                        sourcePieceWidth,
                        sourcePieceHeight
                    );


                // ----------------------------------------
                // CREATE SPRITE
                // ----------------------------------------

                Sprite pieceSprite =
                    Sprite.Create(
                        texture,
                        pieceRect,
                        new Vector2(0.5f, 0.5f),
                        puzzleImage.pixelsPerUnit
                    );

                pieceSprite.name =
                    "PuzzlePieceSprite_" +
                    (index + 1);


                // ----------------------------------------
                // SET IMAGE
                // ----------------------------------------

                Image image =
                    piece.GetComponent<Image>();

                if (image != null)
                {
                    image.sprite =
                        pieceSprite;

                    image.preserveAspect =
                        false;
                }


                // ----------------------------------------
                // SET CORRECT POSITION
                // ----------------------------------------

                PuzzlePiecePlacement placement =
                    piece.GetComponent<PuzzlePiecePlacement>();

                if (placement != null)
                {
                    placement.correctPosition =
                        correctPositions[index];
                }


                pieces.Add(piece);
            }
        }


        // ------------------------------------------------
        // RANDOMIZE PIECES
        // ------------------------------------------------

        ShufflePieces(pieces);


        // ------------------------------------------------
        // RANDOM STARTING POSITIONS
        // ------------------------------------------------

        for (int i = 0; i < pieces.Count; i++)
        {
            RectTransform rect =
                pieces[i].GetComponent<RectTransform>();

            Vector2 randomPosition =
                GetRandomPosition(
                    correctPositions[i]
                );

            rect.anchoredPosition =
                randomPosition;
        }


        Debug.Log(
            totalPieces +
            " puzzle pieces generated with RANDOM positions and DIFFERENT image sections."
        );
    }


    // ====================================================
    // RANDOM POSITION
    // ====================================================

    private Vector2 GetRandomPosition(
        Vector2 correctPosition)
    {
        float pieceWidth =
            playgroundWidth / columns;

        float pieceHeight =
            playgroundHeight / rows;


        float halfWidth =
            pieceWidth / 2f;

        float halfHeight =
            pieceHeight / 2f;


        float minX =
            -playgroundWidth / 2f +
            halfWidth;

        float maxX =
            playgroundWidth / 2f -
            halfWidth;


        float minY =
            -playgroundHeight / 2f +
            halfHeight;

        float maxY =
            playgroundHeight / 2f -
            halfHeight;


        Vector2 randomPosition;

        int attempts = 0;


        do
        {
            randomPosition =
                new Vector2(
                    Random.Range(minX, maxX),
                    Random.Range(minY, maxY)
                );

            attempts++;

        }
        while
        (
            Vector2.Distance(
                randomPosition,
                correctPosition
            ) < 60f
            &&
            attempts < 100
        );


        return randomPosition;
    }


    // ====================================================
    // SHUFFLE
    // ====================================================

    private void ShufflePieces(
        List<GameObject> pieces)
    {
        for (int i = pieces.Count - 1; i > 0; i--)
        {
            int randomIndex =
                Random.Range(
                    0,
                    i + 1
                );


            GameObject temp =
                pieces[i];

            pieces[i] =
                pieces[randomIndex];

            pieces[randomIndex] =
                temp;
        }
    }


    // ====================================================
    // SET DIFFICULTY
    // ====================================================

    public void SetDifficulty(
        Difficulty newDifficulty)
    {
        difficulty =
            newDifficulty;

        Debug.Log(
            "Difficulty changed to: " +
            difficulty
        );
    }


    // ====================================================
    // GET CURRENT DIFFICULTY
    // ====================================================

    public Difficulty GetDifficulty()
    {
        return difficulty;
    }


    // ====================================================
    // GET CURRENT GRID SIZE
    // ====================================================

    public int GetGridSize()
    {
        return DifficultySystem.GetGridSize(
            difficulty
        );
    }


    // ====================================================
    // GET CURRENT PIECE COUNT
    // ====================================================

    public int GetPieceCount()
    {
        return rows * columns;
    }
}