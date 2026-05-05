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
    [SerializeField] GameObject levelUpPanel;
    [SerializeField] GameObject fleeButton;
    [SerializeField] private float unitSpacing = 2.5f;
    [SerializeField] private float playerZPos = 3f;
    [SerializeField] private float enemyZPos = -3f;

    public bool IsBattleOver() => isBattleOver;

    void Awake()
    {
        playerUnits = new List<CombatUnit>();
        enemyUnits  = new List<CombatUnit>();
    }

    void Start()
    {
        if (playerPanel == null) return;
        if (levelUpPanel != null) levelUpPanel.SetActive(false);
    }

    public CombatUnit GetFirstAliveEnemy()
    {
        return enemyUnits.Find(unit => unit.isAlive);
    }

    public List<CombatUnit> GetAliveEnemies()
    {
        return enemyUnits.FindAll(unit => unit.isAlive);
    }

    public void StartBattle(List<CharacterDataSO> playerData, List<CharacterDataSO> enemyData)
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            GameObject obj  = Instantiate(combatUnitPrefab);
            CombatUnit unit = obj.GetComponent<CombatUnit>();
            unit.Initialize(playerData[i]);
            unit.isPlayer          = true;
            obj.transform.position = CalculateSpawnPosition(i, playerData.Count, true);
            playerUnits.Add(unit);
        }

        for (int i = 0; i < enemyData.Count; i++)
        {
            GameObject obj  = Instantiate(combatUnitPrefab);
            CombatUnit unit = obj.GetComponent<CombatUnit>();
            unit.Initialize(enemyData[i]);
            unit.isPlayer          = false;
            obj.transform.position = CalculateSpawnPosition(i, enemyData.Count, false);
            enemyUnits.Add(unit);
        }

        battleHUD.Initialize(playerUnits, enemyUnits);
        battleLog.Clear();
        battleFsm.Start();
    }

    public void BuildTurnOrder()
    {
        List<CombatUnit> allUnits = playerUnits.Concat(enemyUnits).ToList();
        turnOrder   = TurnOrderSystem.Sort(allUnits);
        currentUnit = turnOrder.Dequeue();
        battleLog.NextTurn();
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

        int damage = command.Execute();
        battleHUD.RefreshAllCards();

        if (command is AttackCommand)
        {
            if (damage > 0)
                battleLog.LogDamage(
                    actingUnit.data.characterName,
                    command.targets[0].data.characterName,
                    damage);
        }
        else if (command is SkillCommand skillCmd)
        {
            battleLog.Log(
                actingUnit.data.characterName + " uses " + skillCmd.skill.skillName + "!",
                BattleLogUI.LogType.System);

            if (damage > 0)
                battleLog.LogDamage(
                    actingUnit.data.characterName,
                    command.targets[0].data.characterName,
                    damage);
        }

        foreach (CombatUnit unit in playerUnits)
            battleLog.Log($"{unit.data.characterName} HP: {unit.GetCurrentHP()}");
        foreach (CombatUnit unit in enemyUnits)
            battleLog.Log($"{unit.data.characterName} HP: {unit.GetCurrentHP()}");

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
                yield return StartCoroutine(ExecuteAction(command, false));

            if (turnOrder.Count == 0) break;

            currentUnit = turnOrder.Dequeue();
            if (currentUnit.isPlayer) break;
        }
        battleFsm.ChangePhase(BattlePhase.EndOfRound);
    }

    public void TickStatusEffects()
    {
        foreach (CombatUnit unit in Enumerable.Concat<CombatUnit>(playerUnits, enemyUnits))
            StatusEffectHandler.Tick(unit, TickMoment.EndOfTurn, battleLog);

        battleHUD.RefreshAllCards();

        foreach (CombatUnit unit in playerUnits)
            battleLog.Log($"{unit.data.characterName} HP: {unit.GetCurrentHP()}");
        foreach (CombatUnit unit in enemyUnits)
            battleLog.Log($"{unit.data.characterName} HP: {unit.GetCurrentHP()}");
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
            playerWon    = true;
            isBattleOver = true;
            battleFsm.ChangePhase(BattlePhase.BattleOver);
        }
        else if (playerUnits.All(unit => !unit.isAlive))
        {
            playerWon    = false;
            isBattleOver = true;
            battleFsm.ChangePhase(BattlePhase.BattleOver);
        }
    }

    public void DeclareBattleResult()
    {
        if (OnBattleEnded != null) OnBattleEnded.Raise();

        if (playerWon)
        {
            int xp   = ExperienceSystem.CalculateXP(enemyUnits);
            int gold = ExperienceSystem.CalculateGold(enemyUnits);
            PartyManager.Instance.AddXP(xp);
            battleResultUI.ShowVictory(xp, gold);
        }
        else
        {
            battleResultUI.ShowDefeat();
        }

        fleeButton.SetActive(false);
    }

    public void Flee()
    {
        isBattleOver = true;
        playerPanel.SetActive(false);
        fleeButton.SetActive(false);
        battleResultUI.ShowDefeat();
    }

    private Vector3 CalculateSpawnPosition(int index, int totalUnits, bool isPlayer)
    {
        float totalWidth = (totalUnits - 1) * unitSpacing;
        float startX     = -totalWidth / 2f;
        float xPos       = startX + index * unitSpacing;
        float zPos       = isPlayer ? playerZPos : enemyZPos;

        return new Vector3(xPos, 0f, zPos);
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