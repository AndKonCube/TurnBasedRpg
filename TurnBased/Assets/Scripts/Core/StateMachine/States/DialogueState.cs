using UnityEngine;

public class DialogueState : IGameState
{
    GameEventSO onDialogueStarted;
    StateMachine stateMachine;
    //TODO: DialogueManager dialogueManager;

    public void Enter()
    {
        onDialogueStarted.Raise();
        //TODO: Pause Player Movement
    }

    public void Exit()
    {
        //TODO: Hide dialogue
        //TODO: Resume Player Move
    }

    public void Tick()
    {
        //if(dialogueManager.IsFinished(true)){
        //stateMachine.ChangeState(ExplorationState)}
    }
}
