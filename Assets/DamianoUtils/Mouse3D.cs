using System;
using UnityEngine;

namespace DamianoUtils {
    public static class Mouse3D {
        public static Vector3 GetMouseWorldPosition() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) {
                throw new NotImplementedException();
            }
            return hit.point;
        }
    }
}
