using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UnitMover : MonoBehaviour{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _stoppingDistance;
    private FlowField _unitFlowField;

    private void FixedUpdate() {
        if(_unitFlowField == null) return;
        
        if (IsWithinStoppingDistance()) {
            _rigidbody.velocity = Vector3.zero;
            _unitFlowField = null;
            return;
        }
        
        Cell cell = _unitFlowField.GetCellFromWorldPosition(transform.position);
        Vector3 direction = new Vector3(cell.BestDirection.Vector.x, 0f, cell.BestDirection.Vector.y);
        _rigidbody.velocity = direction * 10f;
      
    }
    private bool IsWithinStoppingDistance() {
        float distance = Vector3.Distance(transform.position, _unitFlowField.DestinationCell.WorldPosition);

        return distance <= _stoppingDistance;
    }
    public void SetFlowField(FlowField flowField) {
        _unitFlowField = new FlowField(flowField.Size, flowField.CellRadius);
        _unitFlowField.CreateGrid();
        _unitFlowField.CreateCostField();
        _unitFlowField.CreateIntegrationField(flowField.DestinationCell);
        _unitFlowField.CreateFlowField();
    }
}


