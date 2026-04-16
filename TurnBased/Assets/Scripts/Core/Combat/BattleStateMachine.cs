using UnityEngine;
public enum BattlePhase
{
    StartOfRound,
    PlayerTurn,
    EnemyTurn,
    EndOfRound,
    BattleOver
}


public class BattleStateMachine : MonoBehaviour
{
    BattlePhase currentPhase;
    BattleManager battleManager;

   public void Start()
    {
        ChangePhase(BattlePhase.StartOfRound);
    }
    public void ChangePhase(BattlePhase newPhase)
    {
        currentPhase = newPhase;

        switch (newPhase)
        {
            case BattlePhase.StartOfRound:
            battleManager.BuildTurnOrder();
            ChangePhase(BattlePhase.PlayerTurn);
            break;
            case BattlePhase.PlayerTurn:
            battleManager.PromptPlayerAction();
            break;
            case BattlePhase.EnemyTurn:
            battleManager.RunEnemyTurns();
            break;
            case BattlePhase.EndOfRound:
            battleManager.TickStatusEffects();
            battleManager.CheckBattleOver();
            break;
            case BattlePhase.BattleOver:
            battleManager.DeclareBattleResult();
            break;
        }
    }
}
