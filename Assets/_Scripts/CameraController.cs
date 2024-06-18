using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform _floor;
    [SerializeField] private float _sensitivity;
    void Update() {
        Vector3 direction = transform.position;
        if (Input.GetKey(KeyCode.W)) {
            direction.z += Time.deltaTime * _sensitivity;
        }
        if (Input.GetKey(KeyCode.S)) {
            direction.z += -(Time.deltaTime * _sensitivity);
        }
        if (Input.GetKey(KeyCode.A)) {
            direction.x += -(Time.deltaTime * _sensitivity);
        }
        if (Input.GetKey(KeyCode.D)) {
            direction.x += Time.deltaTime * _sensitivity;
        }
        Vector3 localForward = GetLocalMatrix(Camera.main.transform.forward);
        
        Camera.main.transform.localPosition += localForward * Input.mouseScrollDelta.y;
        transform.position = direction;
    }
    public Vector3 GetLocalMatrix(Vector3 position) {
        return transform.worldToLocalMatrix.MultiplyVector(position);
    }
}