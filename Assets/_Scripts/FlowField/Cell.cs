using UnityEngine;

public class Cell {
    private Vector3 _worldPosition;
    private Vector2Int _coordinate;
    private byte _cost;
    private ushort _bestCost;
    private GridDirection _bestDirection;
    public Cell(Vector3 worldPosition, Vector2Int coordinate) {
        _worldPosition = worldPosition;
        _coordinate = coordinate;
        _cost = 1;
        _bestCost = ushort.MaxValue;
    }

    public Vector3 GetWorldPosition() {
        return _worldPosition;
    }

    public Vector2Int GetCoordinate() {
        return _coordinate;
    }
    public byte GetCost() {
        return _cost;
    }
    public void IncreaseCost(byte value) {
        if (value + _cost >= byte.MaxValue) {
            _cost = byte.MaxValue;
            return;
        }
        _cost += value;
    }
    public ushort GetBestCost() {
        return _bestCost;
    }
    public void SetBestCost(ushort value) {
        _bestCost = value;
    }
    public void SetBestDirection(GridDirection direction) {
        _bestDirection = direction;
    }
}
