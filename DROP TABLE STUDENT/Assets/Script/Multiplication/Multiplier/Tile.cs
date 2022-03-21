using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private int x;
    private int y;
    private int value;

    public bool isPair = false;
    public Color col = Color.white;


    /// <summary>
    /// Sets sprite of the tile to the number image
    /// </summary>
    /// <param name="sprite"></param>
    public void SetSprite(Sprite sprite)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = sprite;
    }


    /// <summary>
    /// Sets coordinates of the tile
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


    /// <summary>
    /// Sets the numeric value of the tile
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(int value)
    {
        this.value = value;
    }


    /// <summary>
    /// When tile is selected, it will highlight all adjacent tiles
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void SelectTile(int x, int y)
    {
        // our choices for the neighbours
        List<int> offsetListX = new List<int>() { -1, 0, 1 };
        List<int> offsetListY = new List<int>() { -1, 0, 1 };

        // prevent going out of bounds
        if (x == 0)
            offsetListX.Remove(-1);
        if (x == (GridManager.size - 1))
            offsetListX.Remove(1);

        if (y == 0)
            offsetListY.Remove(-1);
        if (y == (GridManager.size - 1))
            offsetListY.Remove(1);

        ActivateAdjacent(offsetListX, offsetListY);
    }


    /// <summary>
    /// Activtes adjacent tiles according to clicked tile
    /// </summary>
    /// <param name="offsetListX"></param>
    /// <param name="offsetListY"></param>
    private void ActivateAdjacent(List<int> offsetListX, List<int> offsetListY)
    {
        for (int i = 0; i < offsetListX.Count; i++)
        {
            for (int j = 0; j < offsetListY.Count; j++)
            {
                int offsetX = offsetListX[i];
                int offsetY = offsetListY[j];

                if (offsetX == 0 && offsetY == 0)
                    continue;

                ActivateTile(x + offsetX, y + offsetY);
            }
        }
    }


    /// <summary>
    /// Activates individual tile by x y coordinate
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void ActivateTile(int x, int y)
    {
        GameObject adjCell = GameObject.Find($"Cell{y}{x}");

        SpriteRenderer spriteRenderer = adjCell.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.cyan;

        Tile tile = adjCell.GetComponent<Tile>();
        tile.isPair = true;

        GridManager.pairCells.Add(adjCell);
    }

    /// <summary>
    /// Handles clicking of a highlighted tile
    /// </summary>
    private void PairClick()
    {
        int sourceValue = GridManager.sourceCell.GetComponent<Tile>().value;
        int mult = sourceValue * this.value;

        GameObject targetTxt = GameObject.Find($"TargetTxt{mult}");

        if (targetTxt != null)
            DrawSolved(targetTxt);

        ClearActivated();
    }


    /// <summary>
    /// Updates UI when target number is found
    /// </summary>
    /// <param name="targetTxt"></param>
    private void DrawSolved(GameObject targetTxt)
    {
        // set target text colour to green
        targetTxt.name = "TargetTxtSolved";
        TMP_Text textObj = targetTxt.GetComponent<TMP_Text>();
        textObj.fontStyle = FontStyles.Strikethrough;
        textObj.color = Color.green;

        // set source tile colour to green
        SpriteRenderer spriteRenderer = GridManager.sourceCell.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        Tile tile = GridManager.sourceCell.GetComponent<Tile>();
        tile.col = Color.green;

        // set destination tile colour to green
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        this.col = Color.green;

        // draws line between pair of correct tiles
        LineRenderer ansLine = new GameObject($"Line{GridManager.answered++}").AddComponent<LineRenderer>();
        ansLine.startColor = Color.green;
        ansLine.endColor = Color.green;
        ansLine.material.SetColor("_Color", Color.green);
        ansLine.startWidth = 0.5f;
        ansLine.endWidth = 0.5f;
        ansLine.positionCount = 2;
        ansLine.useWorldSpace = true;

        Vector3 sourcePos = GridManager.sourceCell.transform.position;
        Vector3 destPos = this.transform.position;
        ansLine.SetPosition(0, sourcePos);
        ansLine.SetPosition(1, destPos);

        if (GridManager.answered == 3)
        {
            GameObject canvas = GameObject.Find("Canvas");
            MultiplierGame game = canvas.GetComponent<MultiplierGame>();
            game.EndGame();
        }
    }


    /// <summary>
    /// Resets currently highlighted tiles back to their previous colour
    /// </summary>
    private void ClearActivated()
    {
        for (int i = 0; i < GridManager.pairCells.Count; i++)
        {
            GameObject cell = GridManager.pairCells[i];

            Tile tile = cell.GetComponent<Tile>();
            tile.isPair = false;

            SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();
            spriteRenderer.color = tile.col;
        }
    }


    /// <summary>
    /// Handles mouse click events for all tiles
    /// </summary>
    private void OnMouseDown()
    {
        // only triggers click when game is running
        if (MultiplierGame.running)
        {
            // if selecting first number
            if (!GridManager.isSelecting)
            {
                GridManager.isSelecting = true;
                GridManager.sourceCell = this.gameObject;
                GridManager.pairCells.Clear();
                SelectTile(this.x, this.y);
            }

            // if select second number
            if (isPair)
            {
                GridManager.isSelecting = false;
                PairClick();
            }
        }
    }
}
