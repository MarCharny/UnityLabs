using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class CellView : MonoBehaviour
{
    [SerializeField] private TextMeshPro valueText;

    private Cell cell;
    private RectTransform rectTransform;
    private GameField gameField;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameField = GetComponentInParent<GameField>();
    }

    public void Init(Cell cell)
    {
        this.cell = cell;

        cell.OnValueChanged += UpdateValue;
        cell.OnPositionChanged += UpdatePosition;

        UpdateValue(cell.Value);
        UpdatePosition(cell.GridPosition);
    }

    private void UpdateValue(int newValue)
    {
        int displayValue = (int)Mathf.Pow(2, newValue);
        valueText.text = displayValue.ToString();
    }

    private void UpdatePosition(Vector2Int newPosition)
    {
        Vector2 worldPosition = gameField.GetWorldPosition(newPosition.x, newPosition.y);
        rectTransform.anchoredPosition = worldPosition;
    }

    private void OnDestroy()
    {
        if (cell != null)
        {
            cell.OnValueChanged -= UpdateValue;
            cell.OnPositionChanged -= UpdatePosition;
        }
    }
}