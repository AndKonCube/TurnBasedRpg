using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    List<CombatUnit> playerUnits;
    List<CombatUnit> enemyUnits;
    Queue<CombatUnit> turnOrder;
    BattleStateMachine battleFsm;
    CombatUnit currentUnit;
    bool playerWon;
    bool isBattleOver;
    [SerializeField] GameObject playerPanel;

    [SerializeField] GameEventSO OnBattleEnded;
    [SerializeField] GameEventSO OnTurnStarted;
    [SerializeField] private GameObject combatUnitPrefab;


    void Awake()
    {
        playerUnits = new List<CombatUnit>();
        enemyUnits = new List<CombatUnit>();
    }
    void Start()
    {
        if (playerPanel == null) return;

        playerPanel.SetActive(false);
    }
    public void StartBattle(List<CharacterDataSO> playerData, List<CharacterDataSO> enemyData)
    {
        foreach (CharacterDataSO data in playerData)
        {
            GameObject obj = Instantiate(combatUnitPrefab);
            CombatUnit unit = obj.GetComponent<CombatUnit>();
            unit.Initialize(data);
            unit.isPlayer = true;
            playerUnits.Add(unit);
        }

        foreach (CharacterDataSO data in enemyData)
        {
            GameObject obj = Instantiate(combatUnitPrefab);
            CombatUnit unit = obj.GetComponent<CombatUnit>();
            unit.Initialize(data);
            unit.isPlayer = false;
            enemyUnits.Add(unit);
        }

        battleFsm.Start();
    }

    public void BuildTurnOrder()
    {
        List<CombatUnit> allUnits = playerUnits.Concat(enemyUnits).ToList();
        turnOrder = TurnOrderSystem.Sort(allUnits);
        currentUnit = turnOrder.Dequeue();
        OnTurnStarted.Raise();
    }

    public void PromptPlayerAction()
    {
        if (currentUnit.isPlayer)
        {
            playerPanel.SetActive(true);
        }
        else
        {
            battleFsm.ChangePhase(BattlePhase.EnemyTurn);
        }
    }
    private void SubmitActions(ActionCommand command)
    {
        StartCoroutine(ExecuteAction(command));
    }
    private IEnumerator ExecuteAction(ActionCommand command)
    {
        command.Execute();
        yield return new WaitForSeconds(2);
        CheckBattleOver();
        AdvanceTurn();
    }
    public void RunEnemyTurns()
    {
        StartCoroutine(ProcessEnemyTurns());
    }

    private IEnumerator ProcessEnemyTurns()
    {
        while (currentUnit != currentUnit.isPlayer && turnOrder != null)
        {
            command = currentUnit.aiProfile.DecideAction(currentUnit, playerUnits);
            yield return ExecuteAction(command);
            currentUnit = turnOrder.Dequeue();
        }
        battleFsm.ChangePhase(BattlePhase.EndOfRound);
    }

    public void TickStatusEffects()
    {
        foreach (CombatUnit unit in Enumerable.Concat<CombatUnit>(playerUnits, enemyUnits))
        {
            StatusEffectHandler.Tick(unit,TickMoment.EndOfTurn);
        }
    }

    private void AdvanceTurn()
    {
        if (turnOrder.Count == 0)
        {
            battleFsm.ChangePhase(BattlePhase.EndOfRound);
            return;
        }
        currentUnit = turnOrder.Dequeue();
        OnTurnStarted.Raise();
        PromptPlayerAction();
    }   

    public void CheckBattleOver()
    {
        if (enemyUnits.All(unit => !unit.isAlive))
        {
            playerWon = true;
            isBattleOver = true;
            battleFsm.ChangePhase(BattlePhase.BattleOver);
        }
        else if (playerUnits.All(unit => !unit.isAlive))
        {
            playerWon = false;
            isBattleOver = true;
            battleFsm.ChangePhase(BattlePhase.BattleOver);
        }
    }
    public void DeclareBattleResult()
    {
        OnBattleEnded.Raise();

    }
    private void CleanUp()
    {
        Destroy(combatUnitPrefab);
        playerUnits.Clear();
        enemyUnits.Clear();
    }
}
