using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUDCard : MonoBehaviour
{
    TMP_Text unitNameText;
    [SerializeField] Image hpBarFill;
    Slider hpBar;
    TMP_Text hpText;
    CombatUnit unit;

    public void SetUp(CombatUnit combatUnit)
    {
        unit = combatUnit;
        unitNameText.text = unit.data.characterName;
        hpBar.maxValue = unit.data.baseHP;
        hpBar.value = unit.GetCurrentHP();
        hpText.text = $"{unit.GetCurrentHP().ToString()} / {unit.data.baseHP}";
    }

    public void UpdateHP()
    {
        hpBar.value = unit.GetCurrentHP();
        hpText.text = $"{unit.GetCurrentHP().ToString()} / {unit.data.baseHP}";
        float hpPercent = (float)unit.GetCurrentHP() / unit.data.baseHP;

        if (unit.GetCurrentHP() > 0.5)
        {
            hpBarFill.color = Color.green;
        }
        if (unit.GetCurrentHP() <=  0.5)
        {
            hpBarFill.color = Color.yellow;
        }
        if (unit.GetCurrentHP() < 0.25)
        {
            Color color = Color.red;
        }
    }
}
