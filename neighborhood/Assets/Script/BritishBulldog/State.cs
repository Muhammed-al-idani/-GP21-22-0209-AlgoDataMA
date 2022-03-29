using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public bool IsEnemyWinState()
    {
        return player == enemy;
    }
    public bool IsPlayerWinState()
    {
        return player == goal;
    }

    public IEnumerator<State> GetAdjacent()
    {
        if (playerTurn)
        {
            if (player.x > 0)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    player = this.player + Vector2Int.left,
                    enemy = this.enemy,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
            
            if (player.y > 0)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    player = this.player + Vector2Int.down,
                    enemy = this.enemy,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
            
            if (player.x < gridWidth-1)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    player = this.player + Vector2Int.right,
                    enemy = this.enemy,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
            if (player.x < GridHeight-1)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    player = this.player + Vector2Int.up,
                    enemy = this.enemy,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
        }
        else
        {
            if (enemy.x > 0)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    enemy = this.enemy + Vector2Int.left,
                    player = this.player,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
            
            if (enemy.y > 0)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    enemy = this.enemy + Vector2Int.down,
                    player = this.player,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
            
            if (enemy.x < gridWidth-1)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    enemy = this.enemy + Vector2Int.right,
                    player = this.player,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
            if (enemy.x < GridHeight-1)
            {
                yield return new State
                {
                    grid = this.grid,
                    gridWidth = this.gridWidth,
                    enemy = this.enemy + Vector2Int.up,
                    player = this.player,
                    goal = this.goal,
                    playerTurn = !playerTurn
                };
            }
        }
    }
    
}
