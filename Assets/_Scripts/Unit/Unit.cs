using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour, ISelectable {
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _circle;
    [SerializeField] private float _stoppingDistance;
    private FlowField _unitFlowField;

    private void FixedUpdate() {
        if(_unitFlowField == null) return;
        
        Cell cell = _unitFlowField.GetCellFromWorldPosition(transform.position);
        Vector3 direction = new Vector3(cell.BestDirection.Vector.x, 0f, cell.BestDirection.Vector.y);
        
        if (IsWithinStoppingDistance()) {
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        _rigidbody.velocity = direction * 10f;
      
    }
    private bool IsWithinStoppingDistance() {
        float distance = Vector3.Distance(transform.position, _unitFlowField.DestinationCell.WorldPosition);

        return distance <= _stoppingDistance;
    }
    public void SetFlowField(FlowField flowField) {
        _unitFlowField = flowField;
    }
    public void Select() {
        _circle.SetActive(true);
    }
    public void Unselect() {
        _circle.SetActive(false);
    }
}


