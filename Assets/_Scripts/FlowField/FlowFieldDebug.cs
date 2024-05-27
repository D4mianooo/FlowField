using UnityEditor;
using UnityEngine;

public enum FlowFieldDisplayType { None, AllIcons, DestinationIcon, CostField, IntegrationField, Coordinates };
public class FlowFieldDebug : MonoBehaviour {
    [SerializeField] private FlowFieldDisplayType _flowFieldDisplayType;
    [SerializeField] private bool _displayGrid;
    private FlowField _flowField;
    private GUIStyle style;

    private void OnDrawGizmos() {
        if(_flowField == null) return;
        if(!_displayGrid) return; 
        DrawGrid(_flowField);
        
        style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        
        if (_flowFieldDisplayType == FlowFieldDisplayType.CostField) {
            DrawCell(_flowField);
        }
        if (_flowFieldDisplayType == FlowFieldDisplayType.IntegrationField) {
            DrawIntegrationField(_flowField);
        }
        if (_flowFieldDisplayType == FlowFieldDisplayType.Coordinates) {
            DrawCoordinates(_flowField);
        }
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
    private void DrawCell(FlowField flowField) {
        foreach (Cell cell in flowField._grid) {
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

}

