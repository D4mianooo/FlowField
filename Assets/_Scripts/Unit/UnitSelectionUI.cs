using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionUI : MonoBehaviour {
    [SerializeField] private RectTransform _selectBox;
    private Vector3 start;
    private Vector3 end;
    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            start = Input.mousePosition;
        }
        if (Input.GetMouseButton(1)) {
            float width = Input.mousePosition.x - start.x;
            float height = Input.mousePosition.y - start.y;
            
            _selectBox.sizeDelta = new Vector2(Math.Abs(width), Math.Abs(height));
            _selectBox.anchoredPosition = (start + Input.mousePosition)/2;
        }
    }
}
