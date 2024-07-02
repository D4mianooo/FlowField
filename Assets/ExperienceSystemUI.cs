using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystemUI : MonoBehaviour {
    [SerializeField] private Slider _slider;
    private ExperienceSystem _experienceSystem;
    private void Awake() {
        _experienceSystem = new ExperienceSystem(1.5f);
    }
    private void OnEnable() {
        _experienceSystem.OnExperienceGained += SetSliderValue;
    }
    private void OnDisable() {
        _experienceSystem.OnExperienceGained -= SetSliderValue;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            _experienceSystem.AddExperience(5f);
            
        }
    }

    private void SetSliderValue(object obj, float experienceNormalized) {
        _slider.value = experienceNormalized;
    }
}
