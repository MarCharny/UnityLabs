using System;
using UnityEngine;

public class Cell
{
    public System.Action<int> OnValueChanged;
    public System.Action<Vector2Int> OnPositionChanged;

    private Vector2Int gridPosition;
    private int value;

    public Vector2Int GridPosition
    {
        get => gridPosition;
        set
        {
            if (gridPosition != value)
            {
                gridPosition = value;
                OnPositionChanged?.Invoke(gridPosition);
            }
        }
    }

    public int Value
    {
        get => value;
        set
        {
            if (this.value != value)
            {
                this.value = value;
                OnValueChanged?.Invoke(this.value);
            }
        }
    }

    public Cell(Vector2Int gridPosition, int value)
    {
        this.gridPosition = gridPosition;
        this.value = value;
    }

    public void MoveTo(Vector2Int newPosition)
    {
        GridPosition = newPosition;
    }

    public void Merge()
    {
        Value *= 2;
    }
}