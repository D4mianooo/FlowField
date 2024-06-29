using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour {
    [SerializeField] private FlowFieldGenerator _flowFieldGenerator;
    [SerializeField] private List<TreeScriptableObject> _treeScriptableObjects;
    [SerializeField] private int treesToSpawn;
    private TreeFactory _treeFactory;
    
    void Start() {
        _treeFactory = new TreeFactory(_treeScriptableObjects);
        List <Tree> trees = _treeFactory.Manufacture(treesToSpawn);
        
        foreach (Tree tree in trees) {
            Vector3 randomPosition = GetRandomPosition();
            SpawnAt(tree, randomPosition);
        }
    }
    private void SpawnAt(in Tree tree, Vector3 position) {
        GameObject t = Instantiate(tree.GetModel(), position, Quaternion.identity);
        
        TreeHealthComponent treeHealthComponent = t.AddComponent<TreeHealthComponent>();
        treeHealthComponent.SetTreeHealth(tree.GetTreeHealth());
    }
    private Vector3 GetRandomPosition() {
        float x = Randomizer.Singleton().Next((int)(_flowFieldGenerator.Size.x * _flowFieldGenerator.CellRadius * 2f));
        float z = Randomizer.Singleton().Next((int)(_flowFieldGenerator.Size.y * _flowFieldGenerator.CellRadius * 2f));
        
        return new Vector3(x, 0f, z);
    }
}
