using UnityEngine;

public class BattleState : IGameState
{
    StateMachine stateMachine;
    GameEventSO onBattleStarted;
    //TODO: BattleManager battleManager;

    public void Enter()
    {
        onBattleStarted.Raise();
        //TODO: battleManager.StartBattle();
        //TODO: Show battle UI
    }

    public void Exit()
    {
        //TODO:
        //battleManager.CleanUp()
        //hide battle UI
    }

    public void Tick()
    {
        //TODO: battle runs itself via BattleManager
        /*
                // battle runs itself via BattleManager
        // we only listen for a result here

        IF battleManager.BattleIsOver THEN
            IF battleManager.PlayerWon THEN
                stateMachine.ChangeState(ExplorationState)
            ELSE
                stateMachine.ChangeState(GameOverState)
            END IF
        END IF
    END METHOD
        */
    }
}
