using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;

    private ClassSystem _classSystem;
    
    private void Start() {
        _classSystem = GetComponent<ClassSystemComponent>().GetClassSystem();
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            _classSystem.Speed.Increase(.5f);
        }
    }
    
    private void FixedUpdate() {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W)) {
            direction.z = 1f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            direction.z = -1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            direction.x = -1f;
        }
        if (Input.GetKey(KeyCode.D)) {
            direction.x = 1f;
        }
        _moveSpeed = _classSystem.Speed.Value;
        _rigidbody.velocity = direction.normalized * _moveSpeed;
    }
}
