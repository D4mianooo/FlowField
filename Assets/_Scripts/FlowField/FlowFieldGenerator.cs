using UnityEngine;
[ExecuteInEditMode]
public class FlowFieldGenerator : MonoBehaviour{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private float _cellRadius = 0.5f;
    [SerializeField] private FlowFieldDebug _flowFieldDebug;
    [SerializeField] private bool generate;
    private FlowField _flowField;
    private float _cellDiameter;
    
    private void Update() {
        if (generate) {
            CreateFlowField();
        }
        Vector3 mouseWorldPos = Mouse3D.GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0)) {
            Cell cellAtMousePosition = _flowField.GetCellFromWorldPosition(mouseWorldPos);
            _flowField.CreateIntegrationField(cellAtMousePosition);
            _flowField.CreateFlowField();
        }   
    }
    public void CreateFlowField() {
        _flowField = new FlowField(_size, _cellRadius);
        _flowField.CreateGrid();
        _flowField.CreateCostField();
        _flowFieldDebug.SetFlowField(_flowField);
    }
}
