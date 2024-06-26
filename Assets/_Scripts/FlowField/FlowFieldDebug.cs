using UnityEditor;
using UnityEngine;



public class FlowFieldDebug : MonoBehaviour
{
	[SerializeField] private FlowFieldGenerator _flowFieldGenerator;
	[SerializeField] private bool _displayGrid;
	[SerializeField] private FlowFieldDisplayType _curDisplayType;

	private Vector2Int _gridSize;
	private float _cellRadius;
	private FlowField _curFlowField;
	private Sprite[] _ffIcons;

	private void Start() {
		_ffIcons = Resources.LoadAll<Sprite>("Sprites/FFicons");
	}

	public void SetFlowField(FlowField newFlowField) {
		_curFlowField = newFlowField;
		_cellRadius = newFlowField.CellRadius;
		_gridSize = newFlowField.Size;
	}
	
	public void DrawFlowField() {
		ClearCellDisplay();

		switch (_curDisplayType)
		{
			case FlowFieldDisplayType.AllIcons:
				DisplayAllCells();
				break;

			case FlowFieldDisplayType.DestinationIcon:
				DisplayDestinationCell();
				break;
		}
	}

	private void DisplayAllCells() {
		if (_curFlowField == null) { return; }
		foreach (Cell curCell in _curFlowField.Grid)
		{
			DisplayCell(curCell);
		}
	}

	private void DisplayDestinationCell() {
		if (_curFlowField == null) { return; }
		DisplayCell(_curFlowField.DestinationCell);
	}

	private void DisplayCell(Cell cell) {
		GameObject iconGO = new GameObject();
		SpriteRenderer iconSR = iconGO.AddComponent<SpriteRenderer>();
		iconGO.transform.parent = transform;
		iconGO.transform.position = cell.WorldPosition + Vector3.up * 0.01f;

		if (cell.BestCost == 0)
		{
			iconSR.sprite = _ffIcons[3];
			Quaternion newRot = Quaternion.Euler(90, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.Cost == byte.MaxValue)
		{
			iconSR.sprite = _ffIcons[2];
			Quaternion newRot = Quaternion.Euler(90, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.North)
		{
			iconSR.sprite = _ffIcons[0];
			Quaternion newRot = Quaternion.Euler(90, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.South)
		{
			iconSR.sprite = _ffIcons[0];
			Quaternion newRot = Quaternion.Euler(90, 180, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.East)
		{
			iconSR.sprite = _ffIcons[0];
			Quaternion newRot = Quaternion.Euler(90, 90, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.West)
		{
			iconSR.sprite = _ffIcons[0];
			Quaternion newRot = Quaternion.Euler(90, 270, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.NorthEast)
		{
			iconSR.sprite = _ffIcons[1];
			Quaternion newRot = Quaternion.Euler(90, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.NorthWest)
		{
			iconSR.sprite = _ffIcons[1];
			Quaternion newRot = Quaternion.Euler(90, 270, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.SouthEast)
		{
			iconSR.sprite = _ffIcons[1];
			Quaternion newRot = Quaternion.Euler(90, 90, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.BestDirection == GridDirection.SouthWest)
		{
			iconSR.sprite = _ffIcons[1];
			Quaternion newRot = Quaternion.Euler(90, 180, 0);
			iconGO.transform.rotation = newRot;
		}
		else
		{
			iconSR.sprite = _ffIcons[0];
		}
	}

	public void ClearCellDisplay() {
		foreach (Transform t in transform)
		{
			Destroy(t.gameObject);
		}
	}
	
	private void OnDrawGizmos() {
		if (_displayGrid)
		{
			if (_curFlowField == null)
			{
				DrawGrid(_flowFieldGenerator.Size, Color.yellow, _flowFieldGenerator.CellRadius);
			}
			else
			{
				DrawGrid(_gridSize, Color.green, _cellRadius);
			}
		}
		
		if (_curFlowField == null) { return; }

		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.alignment = TextAnchor.MiddleCenter;

		switch (_curDisplayType)
		{
			case FlowFieldDisplayType.CostField:

				foreach (Cell curCell in _curFlowField.Grid)
				{
					Handles.Label(curCell.WorldPosition, curCell.Cost.ToString(), style);
				}
				break;
				
			case FlowFieldDisplayType.IntegrationField:

				foreach (Cell curCell in _curFlowField.Grid)
				{
					Handles.Label(curCell.WorldPosition, curCell.BestCost.ToString(), style);
				}
				break;
		}
	}

	private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius) {
		Gizmos.color = drawColor;
		for (int x = 0; x < drawGridSize.x; x++)
		{
			for (int y = 0; y < drawGridSize.y; y++)
			{
				Vector3 center = new Vector3(drawCellRadius * 2 * x + drawCellRadius, 0, drawCellRadius * 2 * y + drawCellRadius);
				Vector3 size = new Vector3(1f, 0f, 1f) * drawCellRadius * 2;
				Gizmos.DrawWireCube(center, size);
			}
		}
	}
}
