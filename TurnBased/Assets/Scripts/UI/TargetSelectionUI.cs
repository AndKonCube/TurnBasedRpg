using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject targetButtonPrefab;
    [SerializeField] private Transform targetContainer;
    [SerializeField] private BattleManager battleManager;
    private SkillDataSO pendingSkill;
    private CombatUnit sourceUnit;

    public void ShowForSkill(CombatUnit source, List<CombatUnit> enemies, SkillDataSO skill)
    {
        sourceUnit = source;
        pendingSkill = skill;
        Show(enemies);
    }

    public void ShowForAttack(CombatUnit source, List<CombatUnit> enemies)
    {
        sourceUnit = source;
        pendingSkill = null;
        Show(enemies);
    }

    private void Show(List<CombatUnit> target)
    {
        ClearButtons();

        List<CombatUnit> aliveTargets = target.FindAll(unit => unit.isAlive);
        if (aliveTargets.Count == 1)
        {
            OnTargetSelected(aliveTargets[0]);
            return;
        }

        gameObject.SetActive(true);
        foreach (CombatUnit t in aliveTargets)
        {
            GameObject button = Instantiate(targetButtonPrefab, targetContainer);
            TargetButtonUI buttonUI = button.GetComponent<TargetButtonUI>();
            buttonUI.SetUp(t, this);
        }
    }

    public void OnTargetSelected(CombatUnit target)
    {
        gameObject.SetActive(false);
        ClearButtons();

        List<CombatUnit> targets = new List<CombatUnit> { target };
        ActionCommand command;

        if (pendingSkill == null)
        {
            command = new AttackCommand(sourceUnit, targets);
        }
        else
        {
            command = new SkillCommand(sourceUnit, targets, pendingSkill);
        }

        battleManager.SubmitActions(command);
    }
    private void ClearButtons()
    {
        for (int i = targetContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(targetContainer.GetChild(i).gameObject);
        }
    }
}
