using System;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    public int gridSize = 4;

    private Cell[,] cells;
    private float cellSize = 1.3f;
    private float spacing = 0.14f;

    private void Start()
    {
        cells = new Cell[gridSize, gridSize];
        InitializeField();
    }

    private void InitializeField()
    {
        float startX = -((gridSize * cellSize + (gridSize - 1) * spacing) / 2) + (cellSize / 2);
        float startY = -((gridSize * cellSize + (gridSize - 1) * spacing) / 2) + (cellSize / 2);

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                cells[x, y] = null;
            }
        }

        CreateCell();
        CreateCell();
    }

    public Vector2Int GetEmptyPosition()
    {
        List<Vector2Int> emptyCells = new List<Vector2Int>();

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (cells[x, y] == null)
                    emptyCells.Add(new Vector2Int(x, y));
            }
        }

        if (emptyCells.Count == 0) return new Vector2Int(-1, -1);

        return emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
    }

    public void CreateCell()
    {
        Vector2Int emptyPos = GetEmptyPosition();
        if (emptyPos.x == -1) return;

        Vector2 worldPos = GetWorldPosition(emptyPos.x, emptyPos.y);

        GameObject cellObject = Instantiate(cellPrefab, new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity, transform);

        int value = UnityEngine.Random.value < 0.9f ? 1 : 2;
        Cell cell = new Cell(emptyPos, value);
        cells[emptyPos.x, emptyPos.y] = cell;

        CellView cellView = cellObject.GetComponent<CellView>();
        if (cellView != null)
        {
            cellView.Init(cell);
        }
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        float startX = -((gridSize * cellSize + (gridSize - 1) * spacing) / 2) + (cellSize / 2);
        float startY = -((gridSize * cellSize + (gridSize - 1) * spacing) / 2) + (cellSize / 2);
        return new Vector2(startX + x * (cellSize + spacing), startY + y * (cellSize + spacing));
    }
}
