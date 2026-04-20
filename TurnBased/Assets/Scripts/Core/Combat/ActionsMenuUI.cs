using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsMenuUI : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] GameObject skillButtonPrefab;
    [SerializeField] Transform skillButtonContainer;
    CombatUnit currentUnit;

    public void Show(CombatUnit unit)
    {
        Debug.Log("Unit: " + unit);
        Debug.Log("Unit data: " + unit.data);
        Debug.Log("Unit skills: " + unit.data.skills);
        currentUnit = unit;
        ClearSkillButtons();
        foreach (SkillDataSO skill in unit.data.skills)
        {
            Debug.Log("SkillButtonPrefab: " + skillButtonPrefab);
            GameObject button = Instantiate(skillButtonPrefab, skillButtonContainer);
            Debug.Log("ButtonUI component: " + button.GetComponent<SkillButtonUI>());
            SkillButtonUI buttonUI = button.GetComponent<SkillButtonUI>();
            buttonUI.SetUp(skill, this);
        }
    }

    public void ClearSkillButtons()
    {
        for (int i = skillButtonContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(skillButtonContainer.GetChild(i).gameObject);
        }
    }

    public void OnAttackClicked()
    {
        List<CombatUnit> target = new List<CombatUnit> { battleManager.GetFirstAliveEnemy() };
        ActionCommand command = new AttackCommand(currentUnit, target);
        battleManager.SubmitActions(command);
    }

    public void OnSkillSelected(SkillDataSO skill)
    {
        List<CombatUnit> target = new List<CombatUnit> { battleManager.GetFirstAliveEnemy() };
        ActionCommand command = new SkillCommand(currentUnit, target, skill);
        battleManager.SubmitActions(command);
    }
}
