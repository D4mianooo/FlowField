using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitController : MonoBehaviour {
    [SerializeField] private FlowFieldGenerator _flowFieldGenerator;
    private Rigidbody _rigidbody;
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        if(_flowFieldGenerator._flowField == null) return;
        if(Vector3.Distance(_flowFieldGenerator._flowField._destinationCell.GetWorldPosition(), transform.position) < 1) return;
        Cell cell = _flowFieldGenerator._flowField.GetCellFromWorldPosition(transform.position);
        
        Vector3 direction = new Vector3(cell.GetBestDirection().Vector.x, 0f, cell.GetBestDirection().Vector.y);
        
        transform.LookAt(direction);
        _rigidbody.velocity = direction * 10f;
    }
}
