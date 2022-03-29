using System;
using UnityEngine;

[Serializable]
public class Game : MonoBehaviour
{
    public State currentState;
    public GameView view;


    private void OnEnable()
    {
        view.updateView(currentState);
    }

    public bool IsEnemyWinState(State state)
    {
        return state.player == state.enemy;
    }
}
