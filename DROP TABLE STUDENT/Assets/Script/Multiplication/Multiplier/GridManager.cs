using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    [SerializeField] private NumberData _numData;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private GameObject _gridEasy;
    [SerializeField] private GameObject _gridMedium;
    [SerializeField] private GameObject _gridHard;

    private List<string> _targets = new List<string>();
    private int[,] _board;
    private int[] _minVals = {1, 2, 3}, _maxVals = {6, 8, 9};
    private int _minVal = 1, _maxVal = 9;

    public static int answered = 0;
    public static int size;
    public static bool isSelecting = false;
    public static GameObject sourceCell = null;
    public static List<GameObject> pairCells = new List<GameObject>();

    void Start()
    {
        _numData = GetComponent<NumberData>();
        InitBoard();
        DrawTargets();
    }


    /// <summary>
    /// Initialises game board with values
    /// </summary>
    private void InitBoard()
    {
        _minVal = _minVals[MultiplierGame.difficulty];
        _maxVal = _maxVals[MultiplierGame.difficulty] + 1;

        size = MultiplierGame.difficulty + 4;
        _board = new int[size, size];

        if (MultiplierGame.difficulty == 0)
            _gridEasy.SetActive(true);
        if (MultiplierGame.difficulty == 1)
            _gridMedium.SetActive(true);
        if (MultiplierGame.difficulty == 2)
            _gridHard.SetActive(true);

        SetBoardValues();
    }


    /// <summary>
    /// Sets numeric values for all tiles
    /// </summary>
    private void SetBoardValues()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                GameObject cell = GameObject.Find($"Cell{y}{x}");
                SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();
                _board[y, x] = Random.Range(_minVal, _maxVal);
                spriteRenderer.sprite = _numData.numberData[_board[y, x] - 1].image;

                Tile tile = cell.GetComponent<Tile>();
                tile.SetCoordinates(x, y);
                tile.SetValue(_board[y, x]);
            }
        }
    }


    /// <summary>
    /// Sets numeric values for the target cells
    /// </summary>
    private void DrawTargets()
    {
        // resets targets
        _targets.Clear();

        for (int i = 1; i <= 3; i++)
        {
            GameObject targetText = GameObject.Find($"Target{i}Text");
            TMP_Text textObj = targetText.GetComponent<TMP_Text>();
            string targetValue = GenerateTargetString();

            while (_targets.Contains(targetValue))
                targetValue = GenerateTargetString();

            _targets.Add(targetValue);
            textObj.text = targetValue;
            textObj.name = $"TargetTxt{targetValue}";
        }
    }


    /// <summary>
    /// Creates a target value based on game board
    /// </summary>
    /// <returns></returns>
    private string GenerateTargetString()
    {
        // choose a random cell as first number
        int randX = Random.Range(0, size);
        int randY = Random.Range(0, size);
        int sourceNum = _board[randX, randY];

        int[] adj = GetAdjacent(randX, randY);
        int pairNum = _board[adj[0], adj[1]];

        // Debug.Log($"({randY}, {randX}, {sourceNum}) ({adj[1]}, {adj[0]}, {pairNum})");

        int mult = sourceNum * pairNum;
        return mult.ToString();
    }


    /// <summary>
    /// Retrieves a random adjacent cell from the x y coordinate passed through
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int[] GetAdjacent(int x, int y)
    {
        int[] coord = new int[2];
        int offsetX = 0, offsetY = 0;

        // our choices for the neighbours
        List<int> offsetListX = new List<int>() { -1, 0, 1 };
        List<int> offsetListY = new List<int>() { -1, 0, 1 };

        // prevent going out of bounds
        if (x == 0)
            offsetListX.Remove(-1);
        if (x == (size - 1))
            offsetListX.Remove(1);

        if (y == 0)
            offsetListY.Remove(-1);
        if (y == (size - 1))
            offsetListY.Remove(1);

        // prevent adjacent cell from being the same cell
        while (offsetX == 0 && offsetY == 0)
        {
            offsetX = (offsetListX.Count > 1) ? offsetListX[Random.Range(0, offsetListX.Count)] : 0;
            offsetY = (offsetListY.Count > 1) ? offsetListY[Random.Range(0, offsetListY.Count)] : 0;
        }

        coord[0] = x + offsetX;
        coord[1] = y + offsetY;
        return coord;
    }
}
