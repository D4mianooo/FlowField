using System;
using UnityEngine;

public class FlowFieldGenerator : MonoBehaviour {
    [SerializeField] private FlowFieldDebug _flowFieldDebug;

    public Vector2Int Size;
    public float CellRadius;
    
    public FlowField _flowField;
    private float _cellDiameter;
    

    private void Start() {
        CreateFlowField();
    }
    private void Update() {
        Vector3 mouseWorldPos = Mouse3D.GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0)) {
            Cell cellAtMousePosition = _flowField.GetCellFromWorldPosition(mouseWorldPos);
            _flowField.CreateIntegrationField(cellAtMousePosition);
            _flowField.CreateFlowField();
            _flowFieldDebug.DrawFlowField();
        }   
    }
    public void CreateFlowField() {
        _flowField = new FlowField(Size, CellRadius);
        _flowField.CreateGrid();
        _flowField.CreateCostField();
        _flowFieldDebug.SetFlowField(_flowField);
    }
}
