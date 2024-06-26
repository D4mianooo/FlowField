using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
    [SerializeField] private FlowFieldGenerator _flowFieldGenerator;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private Unit _unit;
    private Rigidbody _rigidbody;
    private Transform _destination;
    private FlowField _lastFlowField;
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _unit = GetComponent<Unit>();
    }
    private void FixedUpdate() {
        if(_flowFieldGenerator.FlowField == null) return;
        if (_unit.IsSelected()) {
            _lastFlowField = _flowFieldGenerator.FlowField;
        }
        if(_lastFlowField == null) return; 
        Cell cell = _lastFlowField.GetCellFromWorldPosition(transform.position);
        Vector3 direction = new Vector3(cell.BestDirection.Vector.x, 0f, cell.BestDirection.Vector.y);
        if (IsWithinStoppingDistance()) {
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        _rigidbody.velocity = direction * 10f;
      
    }
    private bool IsWithinStoppingDistance() {
        float distance = Vector3.Distance(transform.position, _lastFlowField.DestinationCell.WorldPosition);

        return distance <= _stoppingDistance;
    }
}

