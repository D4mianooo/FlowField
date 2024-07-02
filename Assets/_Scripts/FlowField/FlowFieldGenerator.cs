using System;
using UnityEngine;
using DamianoUtils;
using Unity.VisualScripting;

public class FlowFieldGenerator : MonoBehaviour {
    [SerializeField] private FlowFieldDebug _flowFieldDebug;
    [SerializeField] private Transform _player;
    public FlowField FlowField { get; private set; }
    public Vector2Int Size;
    public float CellRadius;
    public static event EventHandler<FlowField> OnFlowFieldDrawed;

    private void Start() {
        CreateFlowField();
    }
    
    private void Update() {
        Cell cellAtMousePosition = FlowField.GetCellFromWorldPosition(_player.position);
        FlowField.CreateIntegrationField(cellAtMousePosition);
        FlowField.CreateFlowField();
        if (OnFlowFieldDrawed != null) {
            OnFlowFieldDrawed(this, FlowField);
        }
        // _flowFieldDebug.DrawFlowField();
    }
    public void CreateFlowField() {
        FlowField = new FlowField(Size, CellRadius);
        FlowField.CreateGrid();
        FlowField.CreateCostField();
        // _flowFieldDebug.SetFlowField(FlowField);
    }
    public Vector3 GetRandomPosition() {
        int min = (int)Math.Ceiling(CellRadius);
        int max = (int)Math.Ceiling(Size.x - CellRadius);
        int x = Randomizer.Singleton().Next(min, max);
        int z = Randomizer.Singleton().Next(min, max);

        return new Vector3(x, 0f, z);
    }
}
