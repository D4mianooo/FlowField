using Unity.Mathematics;
using UnityEngine;

public class Tree {
    private TreeHealth _treeHealth;
    private GameObject _model;
    public Tree(TreeScriptableObject treeScriptableObject) {
        _treeHealth = new TreeHealth(treeScriptableObject.HitPoints);
        _model = treeScriptableObject.Model;
    }
    public GameObject GetModel() {
        return _model;
    }
    public TreeHealth GetTreeHealth() {
        return _treeHealth;
    }
}
