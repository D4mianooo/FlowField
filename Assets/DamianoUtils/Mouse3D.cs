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
        public static Vector3 GetMouseWorldPositionWithoutY() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) {
                throw new NotImplementedException();
            }
            Vector3 position = hit.point;
            position.y = 0f;


            return position;
        }
    }
}
