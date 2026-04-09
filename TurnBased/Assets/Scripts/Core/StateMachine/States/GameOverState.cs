using UnityEngine;

public class GameOverState : IGameState
{
    GameEventSO onGameOver;

    public void Enter()
    {
        onGameOver.Raise();
        //TODO: Show a GameOver screen. Stop all other systems
    }

    public void Exit()
    {
        //TODO: Hide GameOver Screen.
    }

    public void Tick()
    {
        //if(playermanager.IsRestart(true)){ playerManager.Restart();}
    }

}
