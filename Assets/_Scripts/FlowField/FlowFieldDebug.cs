using UnityEditor;
using UnityEngine;

public enum FlowFieldDisplayType { None, AllIcons, DestinationIcon, CostField, IntegrationField, Coordinates };
public class FlowFieldDebug : MonoBehaviour {
    [SerializeField] private FlowFieldDisplayType _flowFieldDisplayType;
    [SerializeField] private Color _color = Color.black;
    [SerializeField] private int fontSize = 11;
    [SerializeField] private bool _displayGrid;

    private FlowField _flowField;
    private GUIStyle style;

    private void OnDrawGizmos() {
  
        if (_displayGrid) {
            if (_flowField == null) {
                
            }
            else {
                DrawGrid(_flowField);
            }
        }
        
        SetLabelStyle();
        switch (_flowFieldDisplayType) {
            case FlowFieldDisplayType.CostField:
                DrawCells(_flowField);
                break;
            case FlowFieldDisplayType.IntegrationField:
                DrawIntegrationField(_flowField);
                break;
            case FlowFieldDisplayType.Coordinates:
                DrawCoordinates(_flowField);
                break;
            case FlowFieldDisplayType.AllIcons:
                DrawGridDirections(_flowField);
                if (_flowField._destinationCell != null) {
                    DrawDestinationIcon(_flowField);
                }
                break;
            case FlowFieldDisplayType.DestinationIcon:
                DrawDestinationIcon(_flowField);
                break;
        }
            

    }

    private void SetLabelStyle() {
        style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = _color;
        style.fontSize = fontSize;
        style.alignment = TextAnchor.MiddleCenter;
    }
    public void SetFlowField(FlowField flowField) {
        _flowField = flowField;
    }
    private void DrawGrid(FlowField flowField) {
        Gizmos.color = Color.yellow;
        foreach (Cell cell in flowField._grid) {
            Gizmos.DrawWireCube(cell.GetWorldPosition(), new Vector3(1f, 0, 1f) * flowField._cellDiameter);
        }
    }
    private void DrawCells(FlowField flowField) {
        foreach (Cell cell in flowField._grid) {
            Gizmos.color = Color.black;
            Handles.Label(cell.GetWorldPosition(), cell.GetCost().ToString(), style);
        }
    }
    private void DrawIntegrationField(FlowField flowField) {
        foreach (Cell cell in flowField._grid) {
            Handles.Label(cell.GetWorldPosition(), cell.GetBestCost().ToString(), style);
        }
    }
    private void DrawCoordinates(FlowField flowField) {
        foreach (Cell cell in flowField._grid) {
            Handles.Label(cell.GetWorldPosition(), cell.GetCoordinate().ToString(), style);
        }
    }
    private void DrawGridDirections(FlowField flowField) {
        foreach (Cell cell in flowField._grid) {
            Gizmos.DrawIcon(cell.GetWorldPosition() + (Vector3.up * 0.02f), "East", false);
        }
    }
    private void DrawDestinationIcon(FlowField flowField) {
        Gizmos.DrawIcon(flowField._destinationCell.GetWorldPosition() + (Vector3.up * 0.02f), "None", false);
    }

}

