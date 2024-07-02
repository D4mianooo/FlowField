using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;


public class ClassSystemComponent : MonoBehaviour {
    [SerializeField] private ClassScriptableObject _class;
    private ClassSystem _classSystem;
    private HealthSystem _healthSystem;
    
    void Start() {
        _classSystem = new ClassSystem(_class);
        _healthSystem = new HealthSystem(_classSystem.Health.Value);
    }
    
    public ClassSystem GetClassSystem() {
        return _classSystem;
    }
}
