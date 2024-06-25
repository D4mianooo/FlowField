using System;
using System.Collections.Generic;
using UnityEngine;

public class FlowField {
    public Cell[,] Grid { get; private set; }
    public Vector2Int Size { get; private set; }
    public float CellRadius { get; private set; }
    public float CellDiameter { get; private set; }
    public Cell DestinationCell { get; private set; }
    
    public FlowField(Vector2Int size, float cellRadius) {
        Size = size;
        CellDiameter = cellRadius * 2f;
        CellRadius = cellRadius;
        Grid = new Cell[size.x, size.y];
    }
    
    public void CreateGrid() {
        for (int x = 0; x < Size.x; x++) {
            for (int y = 0; y < Size.y; y++) {
                Grid[x, y] = new Cell(new Vector3(x * CellDiameter + CellRadius, 0f, y * CellDiameter  + CellRadius), new Vector2Int(x, y));
            }   
        }
    }
    
    public void CreateCostField() {
        int layer = LayerMask.GetMask("Impassible", "Mud");
        foreach (Cell cell in Grid) {
            bool hasIncreased = false;
            Collider[] colliders = Physics.OverlapBox(cell.WorldPosition, Vector3.one * CellRadius, Quaternion.identity, layer);
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
        
        DestinationCell = cell;
        if(DestinationCell.Cost >= byte.MaxValue) return;
        
        DestinationCell.SetBestCost(0);

        Queue<Cell> q = new Queue<Cell>();
        q.Enqueue(DestinationCell);
        
        while (q.Count > 0) {
            Cell current = q.Dequeue();
            List<Cell> neighbours = GetNeighboursFromCell(current.Coordinate, GridDirection.AllDirections);
            
            foreach (Cell neighbour in neighbours) {
                if (neighbour.Cost < byte.MaxValue && neighbour.BestCost == ushort.MaxValue) {
                    int neighbourCost = current.BestCost + neighbour.Cost;
                    if (neighbourCost < neighbour.BestCost) {
                        neighbour.SetBestCost((ushort) neighbourCost);
                    }
                    q.Enqueue(neighbour);
                }
            }
        }
    }

    public void CreateFlowField() {
        foreach (Cell current in Grid) {
            List<Cell> neighbours = GetNeighboursFromCell(current.Coordinate, GridDirection.AllDirections);
            int bestCost = current.BestCost;
            foreach (Cell neighbour in neighbours) {
                if (neighbour.BestCost < bestCost) {
                    bestCost = neighbour.BestCost;
                    current.SetBestDirection(GridDirection.GetDirectionFromV2I(neighbour.Coordinate - current.Coordinate));
                }
            }
        }
    }
    
    public Cell GetCellFromWorldPosition(Vector3 worldPos) {
        float percentX = worldPos.x / (Size.x * CellDiameter);
        float percentY = worldPos.z / (Size.y * CellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt(Size.x * percentX), 0, Size.x - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt(Size.y * percentY), 0, Size.y - 1);
        
        return Grid[x, y];
    }
    
    
    private List<Cell> GetNeighboursFromCell(Vector2Int coordinates, List<GridDirection> directions) {
        List<Cell> neighbours = new List<Cell>();
        foreach (Vector2Int dir in directions) {
            Cell neighbour = GetCellFromCoordinates(coordinates + dir);
            if (neighbour != null) {
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }
    
    public Cell GetCellFromCoordinates(Vector2Int coordinates) {
        if (coordinates.x < 0 || coordinates.x >= Size.x || coordinates.y < 0 || coordinates.y >= Size.y) return null;    
        return Grid[coordinates.x, coordinates.y];
    }
    
    private void ClearIntegrationField() {
        foreach (Cell cell in Grid) {
            cell.SetBestCost(ushort.MaxValue);
        }
    }
}
