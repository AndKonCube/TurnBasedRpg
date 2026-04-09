using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    IGameState currentState;

    public void ChangeState( IGameState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.Tick();
        }
    }
}
