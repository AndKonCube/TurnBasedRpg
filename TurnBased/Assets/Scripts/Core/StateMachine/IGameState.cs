using UnityEngine;

public interface IGameState
{
    public void Enter();

    public void Tick();

    public void Exit();
    
}
