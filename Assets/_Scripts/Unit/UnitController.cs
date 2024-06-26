using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
    [SerializeField] private FlowFieldGenerator _flowFieldGenerator;
    private UnitSelection _unitSelection;
    private void Awake() {
        _unitSelection = GetComponent<UnitSelection>();
    }
    private void OnEnable() {
        _flowFieldGenerator.OnFlowFieldDrawed += SetSelectedUnitsFlowField;

    }
    private void SetSelectedUnitsFlowField(object sender, FlowField flowField) {
        if (_unitSelection.SelectedUnits != null) {
            if (_unitSelection.SelectedUnits.Count > 0) {
                foreach (Unit unit in _unitSelection.SelectedUnits) {
                    unit.SetFlowField(flowField);
                }
            }
        }
    }
}

