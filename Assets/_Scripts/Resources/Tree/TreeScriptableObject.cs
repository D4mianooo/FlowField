using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeData", menuName = "Resources/Tree", order = 1)]
public class TreeScriptableObject : ScriptableObject {
    public GameObject Model;
    public int HitPoints;
    
}
