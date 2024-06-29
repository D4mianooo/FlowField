using UnityEngine;

public class Cell {
    public Vector3 WorldPosition { get; private set; }
    public Vector2Int Coordinate { get; private set; }
    public byte Cost { get; set; }
    public ushort BestCost { get; set; }
    public GridDirection BestDirection { get; private set; }
    
    public Cell(Vector3 worldPosition, Vector2Int coordinate) {
        WorldPosition = worldPosition;
        Coordinate = coordinate;
        Cost = 1;
        BestCost = ushort.MaxValue;
        BestDirection = null;
    }
    
    public void IncreaseCost(byte value) {
        if (value + Cost >= byte.MaxValue) {
            Cost = byte.MaxValue;
            return;
        }
        Cost += value;
    }
    
    public void SetBestCost(ushort value) {
        BestCost = value;
    }
    
    public void SetBestDirection(GridDirection direction) {
        BestDirection = direction;
    }
}
