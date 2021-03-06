using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Game : MonoBehaviour
{
    public State currentState;
    public GameView view;


    void OnEnable()
    {
        view.updateView(currentState);
    }

    State CurrentState
    {
        set
        {
            currentState = value;
            view.updateView(currentState);
        }
    }

    void Update()
    {
        if(currentState.IsEnemyWinState()|| currentState.IsPlayerWinState())
            return;
        if (currentState.playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentState.player.x > 0)
            {
                CurrentState = new State
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.left,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }

            if (Input.GetKeyDown(KeyCode.S) && currentState.player.y < currentState.gridWidth - 1)
            {
                CurrentState = new State
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.down,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }

            if (Input.GetKeyDown(KeyCode.D) && currentState.player.x < currentState.GridHeight - 1)
            {
                CurrentState = new State
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.right,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }

            if (Input.GetKeyDown(KeyCode.W) && currentState.player.y < currentState.GridHeight - 1)
            {
                CurrentState = new State
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.up,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }
        }
        else
        {
            var enemyWinPath = find_path(currentState);
            currentState = enemyWinPath[1];
        }
    }

    State[] find_path(State start_node)
    {
        int depth = 1;
        while (depth < 10000)
        {
            State[] result = find_path(start_node, depth++);
            if (result != null)
                return result;
        }

        throw new Exception("found no path!");
    }

    State[] find_path(State start_node, int depth)
        {
            HashSet<State> visited_nodes = new HashSet<State>();
            visited_nodes.Add(start_node);

            Stack<State> path = new Stack<State>();
            path.Push(start_node);

            while (path.Count > 0)
            {
                bool found_next_node = false;
                foreach (var neighbor in path.Peek().GetAdjacent())
                {
                    if (visited_nodes.Contains(neighbor))
                        continue;
                    if (depth > 0)
                    {
                        visited_nodes.Add(neighbor);
                        path.Push(neighbor);
                        depth--;
                        if (neighbor.IsEnemyWinState())
                        {
                            return path.Reverse().ToArray();
                        }

                        found_next_node = true;
                        break;
                    }
                }

                if (!found_next_node)
                {
                    path.Pop();
                    depth++;
                }
            }

            return null;

        }
    }

