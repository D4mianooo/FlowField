using UnityEngine;

public class TreeHealth {
    private int _hitPoints;
    public TreeHealth(int maxHitPoints) {
        _hitPoints = maxHitPoints;
    }
    public void GetDamage(int hitPoints) {
        if (_hitPoints <= 0) {
            Debug.Log("I'm dead, leave me alone!");
            return;
        }
        Debug.Log($"{_hitPoints}");
        _hitPoints -= hitPoints;
    }
}
