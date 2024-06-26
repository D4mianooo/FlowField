using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamianoUtils;

public class UnitSelection : MonoBehaviour {
    private Collider[] colliders;
    private Vector3 start;
    private Vector3 end;
    private float width;
    private float height;
    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            start = Mouse3D.GetMouseWorldPosition();
            foreach (Collider collider in colliders) {
                if (collider.TryGetComponent(out ISelectable selectable)) {
                    selectable.Unselect();
                }
            }
        }
        if (Input.GetMouseButtonUp(1)) {
            end = Mouse3D.GetMouseWorldPosition();
            width = Math.Abs(end.x - start.x);
            height = Math.Abs(end.z - start.z);
            
            colliders = Physics.OverlapBox(Vector3.Lerp(start, end, .5f), new Vector3(width, 1f, height));
            
            foreach (Collider collider in colliders) {
                if (collider.TryGetComponent(out ISelectable selectable)) {
                    selectable.Select();
                }
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.Lerp(start, end, .5f), new Vector3(width, 1f, height));
    }
}
