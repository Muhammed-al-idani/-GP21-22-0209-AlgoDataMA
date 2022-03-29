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
}
