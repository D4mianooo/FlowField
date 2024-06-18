using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out RaycastHit hit, 100)) {
                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                damageable.GetDamage(1);
            }

        }
    }
}
