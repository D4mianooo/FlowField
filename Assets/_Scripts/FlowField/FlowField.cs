using System;
using System.Collections.Generic;
using UnityEngine;

public class FlowField {
    public Cell[,] _grid { get; private set; }
    public Vector2Int _size { get; private set; }
    public float _cellRadius { get; private set; }
    public float _cellDiameter { get; private set; }

    public Cell _destinationCell;
    
    private static readonly Vector3[] dirs = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    public FlowField(Vector2Int size, float cellRadius) {
        _size = size;
        _cellDiameter = cellRadius * 2f;
        _cellRadius = cellRadius;
        _grid = new Cell[size.x, size.y];
    }
    
    public void CreateGrid() {
        for (int x = 0; x < _size.x; x++) {
            for (int y = 0; y < _size.y; y++) {
                _grid[x, y] = new Cell(new Vector3(x * _cellDiameter + _cellRadius, 0f, y * _cellDiameter  + _cellRadius), new Vector2Int(x, y));
            }   
        }
    }
    public void CreateCostField() {
        int layer = LayerMask.GetMask("Impassible", "Mud");
        foreach (Cell cell in _grid) {
            bool hasIncreased = false;
            Collider[] colliders = Physics.OverlapBox(cell.GetWorldPosition(), Vector3.one * _cellRadius, Quaternion.identity, layer);
            foreach (Collider collider in colliders) {
                if (collider.gameObject.layer == 6) {
                    cell.IncreaseCost(Byte.MaxValue);
                } else if(!hasIncreased && collider.gameObject.layer == 7) {
                    cell.IncreaseCost(3);
                    hasIncreased = true;

                }
            }
        }
    }
    public void CreateIntegrationField(Cell cell) {
        ClearIntegrationField();
        
        _destinationCell = cell;
        if(_destinationCell.GetCost() >= byte.MaxValue) return;
        _destinationCell.SetBestCost(0);

        Queue<Cell> q = new Queue<Cell>();
        q.Enqueue(_destinationCell);
        
        while (q.Count > 0) {
            Cell current = q.Dequeue();
            
            List<Cell> neighbours = GetNeighboursFromCell(current, GridDirection.AllDirections);
            foreach (Cell neighbour in neighbours) {
                if (neighbour.GetCost() < byte.MaxValue && neighbour.GetBestCost() == ushort.MaxValue) {
                    int neighbourCost = current.GetBestCost() + neighbour.GetCost();
                    if (neighbourCost < neighbour.GetBestCost()) {
                        neighbour.SetBestCost((ushort) neighbourCost);
                    }
                    q.Enqueue(neighbour);
                }
            }
        }
    }

    public void CreateFlowField() {
        foreach (Cell current in _grid) {
            List<Cell> neighbours = GetNeighboursFromCell(current, GridDirection.AllDirections);
            int bestCost = current.GetBestCost();
            foreach (Cell neighbour in neighbours) {
                if (neighbour.GetBestCost() < bestCost) {
                    bestCost = neighbour.GetBestCost();
                    current.SetBestDirection(GridDirection.GetDirectionFromV2I(neighbour.GetCoordinate() - current.GetCoordinate()));
                }
            }
        }
    }
    
    public Cell GetCellFromWorldPosition(Vector3 worldPos) {
        float percentX = worldPos.x / (_size.x * _cellDiameter);
        float percentY = worldPos.z / (_size.y * _cellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt(_size.x * percentX), 0, _size.x - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt(_size.y * percentY), 0, _size.y - 1);
        
        return _grid[x, y];
    }
    
    public Cell GetCellFromCoordinates(Vector2Int coordinates) {
        if (coordinates.x < 0 || coordinates.x >= _size.x || coordinates.y < 0 || coordinates.y >= _size.y) return null;    
        return _grid[coordinates.x, coordinates.y];
    }
    
    private List<Cell> GetNeighboursFromCell(Cell cell, List<GridDirection> directions) {
        List<Cell> neighbours = new List<Cell>();
        foreach (GridDirection dir in directions) {
            Cell neighbour = GetCellFromCoordinates(cell.GetCoordinate() + dir.Vector);
            if (neighbour != null) {
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }
    private void ClearIntegrationField() {
        foreach (Cell cell in _grid) {
            cell.SetBestCost(ushort.MaxValue);
        }
    }

}
