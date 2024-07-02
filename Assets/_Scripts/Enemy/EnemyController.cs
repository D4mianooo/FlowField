using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour{
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _speed = 2f;
    private Rigidbody _rigidbody;
    private FlowField _flowField;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        Enemy enemy = GetComponent<EnemyComponent>().Enemy;
        _attackRange = enemy.AttackRange;
        _speed = enemy.Speed;
    }
    
    private void OnEnable() {
        FlowFieldGenerator.OnFlowFieldDrawed += SetFlowField;
    }
    
    private void OnDisable() {
        FlowFieldGenerator.OnFlowFieldDrawed -= SetFlowField;

    }
    private void Update() {
        transform.LookAt(_flowField.DestinationCell.WorldPosition);
    }
    private void FixedUpdate() {
        if (IsWithinStoppingDistance()) {
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        
        Cell cell = _flowField.GetCellFromWorldPosition(transform.position);
        Vector3 direction = new Vector3(cell.BestDirection.Vector.x, 0f, cell.BestDirection.Vector.y);
        _rigidbody.velocity = direction * _speed;
      
    }
    private bool IsWithinStoppingDistance() {
        float distance = Vector3.Distance(transform.position, _flowField.DestinationCell.WorldPosition);

        return distance <= _attackRange;
    }
    
    private void SetFlowField(object sender, FlowField flowField) {
        _flowField = flowField;
    }
}


