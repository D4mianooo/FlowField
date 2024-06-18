using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TreeHealthComponent : MonoBehaviour, IDamageable{
    private TreeHealth _treeHealth;

    public void SetTreeHealth(TreeHealth treeHealth) {
        _treeHealth = treeHealth;
    }
    public void GetDamage(int hitPoints) {
        _treeHealth.GetDamage(hitPoints);
    }
}
