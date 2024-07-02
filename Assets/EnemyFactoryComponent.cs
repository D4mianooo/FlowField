using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {
    public GameObject Model { get; private set; }
    public EnemyType Type { get; private set; }
    public float Health { get; private set; }
    public float Damage { get; private set; }
    public float Speed { get; private set; }
    public float AttackRange { get; private set; }
    
    public Enemy(EnemyScriptableObject enemyScriptableObject) {
        Model = enemyScriptableObject.model;
        Type = enemyScriptableObject.type;
        Health = enemyScriptableObject.health;
        Damage = enemyScriptableObject.damage;
        Speed = enemyScriptableObject.speed;
        AttackRange = enemyScriptableObject.attackRange;
    }
    
}
public class EnemyFactory {
    private List<EnemyScriptableObject> _enemiesSO;
    
    public EnemyFactory(List<EnemyScriptableObject> enemiesSO) {
        _enemiesSO = enemiesSO;
    }
    
    public void SetEnemiesSO(List<EnemyScriptableObject> enemiesSO) {
        _enemiesSO = enemiesSO;
    }
    
    public List<Enemy> Manufacture(int count) {
        List<Enemy> enemies = new List<Enemy>();

        for (int i = 0; i < count; i++) {
            Enemy enemy = new Enemy(_enemiesSO[Randomizer.Singleton().Next(0, _enemiesSO.Count - 1)]);
            enemies.Add(enemy);
        }

        return enemies;
    }
}

public class EnemyFactoryComponent : MonoBehaviour {
    [SerializeField] private FlowFieldGenerator _flowFieldGenerator;
    [SerializeField] private List<EnemyScriptableObject> _enemyScriptableObjects;
    [SerializeField] private int _enemiesToSpawn;
    private EnemyFactory _enemyFactory;
    
    private void Awake() {
        _flowFieldGenerator = FindObjectOfType<FlowFieldGenerator>();
    }
    
    void Start() {
        _enemyFactory = new EnemyFactory(_enemyScriptableObjects);
        List<Enemy> enemies = _enemyFactory.Manufacture(_enemiesToSpawn);
        foreach (Enemy enemy in enemies) {
            GameObject enemyGameObject = SpawnAt(enemy);
            enemyGameObject.GetComponent<EnemyComponent>().Enemy = enemy;
        }
    }

    private GameObject SpawnAt(Enemy enemy) {
        GameObject instance = Instantiate(enemy.Model, enemy.Model.transform.position + _flowFieldGenerator.GetRandomPosition(), Quaternion.identity, transform);
        
        return instance;
    }
}
