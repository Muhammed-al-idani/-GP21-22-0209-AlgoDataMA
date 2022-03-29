using System;
using UnityEngine;

[Serializable]
public class State
{
    public Celltype[] grid;
    public int gridWidth;
    public int GridHeight => grid.Length / gridWidth;
    public Vector2Int player;
    public Vector2Int enemy;
    public Vector2Int goal;
    public bool playerTurn;


    public Celltype GetCell(Vector2Int index)
    {
        return grid[index.x + index.y * gridWidth];
    }
}
