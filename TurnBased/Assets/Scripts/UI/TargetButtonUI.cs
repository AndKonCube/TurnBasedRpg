using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetButtonUI : MonoBehaviour
{
    [SerializeField] private CombatUnit targetUnit;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text targetNameText;
    [SerializeField] private TMP_Text targetHPText;

    public void SetUp(CombatUnit unit, TargetSelectionUI selectionUI)
    {
        targetUnit = unit;
        targetNameText.text = unit.data.characterName;
        targetHPText.text = $"{unit.GetCurrentHP()} / {unit.data.baseHP}";
        button.onClick.AddListener(()=> selectionUI.OnTargetSelected(targetUnit));
    }
}
