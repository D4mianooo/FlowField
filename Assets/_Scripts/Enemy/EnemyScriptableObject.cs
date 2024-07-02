using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject {
    public GameObject model;
    public EnemyType type;
    public float attackRange;
    public float health;
    public float damage;
    public float speed;
}
