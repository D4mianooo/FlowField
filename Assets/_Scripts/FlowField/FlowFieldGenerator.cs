using System;
using UnityEngine;
using DamianoUtils;
using Unity.VisualScripting;

public class FlowFieldGenerator : MonoBehaviour {
    [SerializeField] private FlowFieldDebug _flowFieldDebug;
    public FlowField FlowField { get; private set; }
    public Vector2Int Size;
    public float CellRadius;
    public event EventHandler<FlowField> OnFlowFieldDrawed;

    
    private void Start() {
        CreateFlowField();
    }
    
    private void Update() {
        Vector3 mouseWorldPos = Mouse3D.GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0)) {
            Cell cellAtMousePosition = FlowField.GetCellFromWorldPosition(mouseWorldPos);
            FlowField.CreateIntegrationField(cellAtMousePosition);
            FlowField.CreateFlowField();
            if (OnFlowFieldDrawed != null) {
                OnFlowFieldDrawed(this, FlowField);
            }
            _flowFieldDebug.DrawFlowField();
        }   
    }
    public void CreateFlowField() {
        FlowField = new FlowField(Size, CellRadius);
        FlowField.CreateGrid();
        FlowField.CreateCostField();
        _flowFieldDebug.SetFlowField(FlowField);
    }
}
