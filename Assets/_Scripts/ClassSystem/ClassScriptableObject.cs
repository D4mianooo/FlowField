using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "Classes/Class", order = 1)]
public class ClassScriptableObject : ScriptableObject {
    public float _health;
    public float _attackDamage;
    public float _speed;
}
