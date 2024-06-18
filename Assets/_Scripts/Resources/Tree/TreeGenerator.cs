using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour {
    [SerializeField] private List<TreeScriptableObject> _treeScriptableObjects;
    private TreeFactory _treeFactory;
    void Start() {
        _treeFactory = new TreeFactory(_treeScriptableObjects);
        
        List <Tree> trees = _treeFactory.Manufacture(10);
        Vector3 startPosition = Vector3.zero;
        foreach (Tree tree in trees) {
            SpawnAt(tree, startPosition);
            startPosition += Vector3.right;
        }
    }
    private void SpawnAt(Tree tree, Vector3 position) {
        GameObject t = Instantiate(tree.GetModel(), position, Quaternion.identity);
        TreeHealthComponent treeHealthComponent = t.AddComponent<TreeHealthComponent>();
        treeHealthComponent.SetTreeHealth(tree.GetTreeHealth());
    }
}
