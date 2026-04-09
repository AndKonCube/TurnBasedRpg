using UnityEngine;

public class ExplorationState : IGameState
{
    StateMachine stateMachine;
    GameEventSO onExplorationStarted;

    public void Enter()
    {
        onExplorationStarted.Raise();
        //TODO: Unlock Player Movement
        //TODO: Show UI
    }

    public void Exit()
    {
        //TODO: Disable PlayerMovement
        //TODO: Hide Exploration UI
    }

    public void Tick()
    {
        //TODO: Check for random encounter
        //TODO: Check for NPC interaction Input
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
