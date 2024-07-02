using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassSystemUI : MonoBehaviour {
    [SerializeField] private ClassSystemComponent _classSystemComponent;
    [SerializeField] private TMP_Text _text;
    
    void Start() {
        _classSystemComponent.GetClassSystem().Speed.OnStatValueChanged += (sender, newValue) =>
        {
            _text.text = newValue.ToString();
        };
    }
}
