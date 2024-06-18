using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour, ISelectable {
    [SerializeField] private GameObject _circle;
    public void Select() {
        _circle.SetActive(true);
    }
    public void Unselect() {
        _circle.SetActive(false);

    }
}

public interface ISelectable {
    public void Select();
    public void Unselect();
}
