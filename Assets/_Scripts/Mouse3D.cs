using UnityEngine;

public static class Mouse3D {
    public static Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) {
            return Vector3.zero;
        }

        return hit.point;
    }
}