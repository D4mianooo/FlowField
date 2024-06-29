using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamianoUtils;

public class UnitSelection : MonoBehaviour {
    [SerializeField] public List<UnitMover> SelectedUnits;
    private Vector3 _start;
    private Vector3 _end;
    private float _width;
    private float _height;

    private void Start() {
        SelectedUnits = new List<UnitMover>();
    }
    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            _start = Mouse3D.GetMouseWorldPosition();
            
            List<UnitMover> copy = new List<UnitMover>(SelectedUnits);
            
            SelectedUnits.Clear();
            
            foreach (UnitMover unit in copy) {
                ISelectable selectable = unit.GetComponentInChildren<ISelectable>();
                selectable.Unselect();
            }
        }
        if (Input.GetMouseButtonUp(1)) {
            _end = Mouse3D.GetMouseWorldPosition();
            _width = Math.Abs(_end.x - _start.x);
            _height = Math.Abs(_end.z - _start.z);
            
            Collider[] colliders = Physics.OverlapBox(Vector3.Lerp(_start, _end, .5f), new Vector3(_width/2, 1f, _height/2));
            
            foreach (Collider collider in colliders) {
                if (collider.TryGetComponent(out UnitMover unit)) {
                    SelectedUnits.Add(unit);
                    unit.GetComponentInChildren<ISelectable>().Select();
                }
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(_start, Vector3.Lerp(_start, _end, .5f));
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.Lerp(_start, _end, .5f), new Vector3(_width, 1f, _height));
    }
}
    