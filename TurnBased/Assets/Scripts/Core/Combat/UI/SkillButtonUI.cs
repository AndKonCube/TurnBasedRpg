using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] SkillDataSO skillData;
    [SerializeField] ActionsMenuUI actionsMenuUI;
    [SerializeField] TMP_Text buttonLabel;
    [SerializeField] Button button;


    public void SetUp(SkillDataSO skill, ActionsMenuUI menu)
    {
        actionsMenuUI = GetComponent<ActionsMenuUI>();
        skillData = skill;
        actionsMenuUI = menu;
        buttonLabel.text = skill.skillName;
        button.onClick.AddListener(()=> actionsMenuUI.OnSkillSelected(skillData));
    }

    public void OnSkillSelected()
    {
        actionsMenuUI.OnSkillSelected(skillData);
    }
}
