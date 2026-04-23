using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    List<CombatUnit> playerUnits;
    List<CombatUnit> enemyUnits;
    Queue<CombatUnit> turnOrder;
    CombatUnit currentUnit;
    bool playerWon;
    bool isBattleOver;

    [SerializeField] BattleResultUI battleResultUI;
    [SerializeField] BattleStateMachine battleFsm;
    [SerializeField] GameObject playerPanel;
    [SerializeField] ActionsMenuUI actionMenuUI;
    [SerializeField] GameEventSO OnBattleEnded;
    [SerializeField] GameEventSO OnTurnStarted;
    [SerializeField] private GameObject combatUnitPrefab;
    [SerializeField] private BattleLogUI battleLog;
    [SerializeField] BattleHUD battleHUD;
    [SerializeField] LevelUpUI levelUpUI;

    public bool IsBattleOver()
    {
        return isBattleOver;
    }

    void Awake()
    {
        playerUnits = new List<CombatUnit>();
        enemyUnits = new List<CombatUnit>();
    }

    void Start()
    {
        if (playerPanel == null) return;
    }

    public CombatUnit GetFirstAliveEnemy()
    {
        return enemyUnits.Find(unit => unit.isAlive);
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

        battleHUD.Initialize(playerUnits, enemyUnits);
        battleFsm.Start();
    }

    public void BuildTurnOrder()
    {
        List<CombatUnit> allUnits = playerUnits.Concat(enemyUnits).ToList();
        turnOrder = TurnOrderSystem.Sort(allUnits);
        currentUnit = turnOrder.Dequeue();
        if (OnTurnStarted != null) OnTurnStarted.Raise();
        PromptPlayerAction();
    }

    public void PromptPlayerAction()
    {
        if (currentUnit.isPlayer)
        {
            playerPanel.SetActive(true);
            actionMenuUI.Show(currentUnit);
        }
        else
        {
            playerPanel.SetActive(false);
            battleFsm.ChangePhase(BattlePhase.EnemyTurn);
        }
    }

    public void SubmitActions(ActionCommand command)
    {
        playerPanel.SetActive(false);
        StartCoroutine(ExecuteAction(command));
    }

    private IEnumerator ExecuteAction(ActionCommand command, bool autoAdvance = true)
    {
        CombatUnit actingUnit = command.source;

        if (command is AttackCommand)
            battleLog.Log(actingUnit.data.characterName + " attacks!");
        else if (command is SkillCommand skillCmd)
            battleLog.Log(actingUnit.data.characterName + " uses " + skillCmd.skill.skillName + "!");

        command.Execute();
        battleHUD.RefreshAllCards();

        foreach (CombatUnit unit in playerUnits)
            battleLog.Log(unit.data.characterName + " HP: " + unit.GetCurrentHP());
        foreach (CombatUnit unit in enemyUnits)
            battleLog.Log(unit.data.characterName + " HP: " + unit.GetCurrentHP());

        yield return new WaitForSeconds(2);
        CheckBattleOver();
        if (!isBattleOver && autoAdvance)
            AdvanceTurn();
    }

    public void RunEnemyTurns()
    {
        StartCoroutine(ProcessEnemyTurns());
    }

    private IEnumerator ProcessEnemyTurns()
    {
        while (!currentUnit.isPlayer)
        {
            ActionCommand command = EnemyAI.DecideAction(currentUnit, playerUnits);
            if (command != null)
            {
                yield return StartCoroutine(ExecuteAction(command, false));
            }

            if (turnOrder.Count == 0) break;

            currentUnit = turnOrder.Dequeue();
            if (currentUnit.isPlayer) break;
        }
        battleFsm.ChangePhase(BattlePhase.EndOfRound);
    }

    public void TickStatusEffects()
    {
        foreach (CombatUnit unit in Enumerable.Concat<CombatUnit>(playerUnits, enemyUnits))
        {
            StatusEffectHandler.Tick(unit, TickMoment.EndOfTurn, battleLog);
        }
        battleHUD.RefreshAllCards();

        battleHUD.RefreshAllCards();

        foreach (CombatUnit unit in playerUnits)
        {
            battleLog.Log($"{unit.data.characterName} HP: {unit.GetCurrentHP()}");
        }
        foreach (CombatUnit unit in enemyUnits)
        {
            battleLog.Log($"{unit.data.characterName} HP: {unit.GetCurrentHP()}");
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
        if (OnTurnStarted != null) OnTurnStarted.Raise();
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
        if (playerWon)
        {
            int xp = ExperienceSystem.CalculateXP(enemyUnits);
            int gold = ExperienceSystem.CalculateGold(enemyUnits);
            PartyManager.Instance.AddXP(xp);
            battleResultUI.ShowVictory(xp, gold);
        }
        else
        {
            battleResultUI.ShowDefeat();
        }
    }

    private void CleanUp()
    {
        foreach (CombatUnit unit in playerUnits)
            Destroy(unit.gameObject);
        foreach (CombatUnit unit in enemyUnits)
            Destroy(unit.gameObject);

        playerUnits.Clear();
        enemyUnits.Clear();
    }
}