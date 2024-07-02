using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using DamianoUtils;
using UnityEngine;

public class WeaponRotation : MonoBehaviour {
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _distance;


    void Update() {
        Vector3 mousePosition = Mouse3D.GetMouseWorldPositionWithoutY();
        if (Vector3.Distance(transform.position, mousePosition) >= 1f) {
            Vector3 direction = mousePosition - _playerController.transform.position;
            transform.position = _playerController.transform.position + direction.normalized * _distance;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out IGetHealthSystem getHealthSystem)) {
            Debug.Log(other.transform.name);
            HealthSystem healthSystem = getHealthSystem.GetHealthSystem();
            healthSystem.Damage(10f);
            if (healthSystem.GetHealthNormalized() <= 0f) {
                healthSystem.Die();
            }
        }
    }
}
