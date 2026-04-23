using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUDCard : MonoBehaviour
{
    [SerializeField] TMP_Text unitNameText;
    [SerializeField] Image unitSprite;
    [SerializeField] Image hpBarFill;
    [SerializeField] Slider hpBar;
    [SerializeField] TMP_Text hpText;

    private CombatUnit unit;

    public void SetUp(CombatUnit combatUnit)
    {
        unit = combatUnit;
        unitNameText.text = unit.data.characterName;
        hpBar.maxValue = unit.data.baseHP;
        hpBar.value = unit.GetCurrentHP();
        hpText.text = $"{unit.GetCurrentHP()} / {unit.data.baseHP}";
        hpBarFill.color = Color.green;

        if(unit.data.sprite != null)
        unitSprite.sprite = unit.data.sprite;
    }

    public void UpdateHP()
    {
        hpBar.value = unit.GetCurrentHP();
        hpText.text = $"{unit.GetCurrentHP()} / {unit.data.baseHP}";

        float hpPercent = (float)unit.GetCurrentHP() / unit.data.baseHP;

        if (hpPercent > 0.5f)
            hpBarFill.color = Color.green;
        else if (hpPercent > 0.25f)
            hpBarFill.color = Color.yellow;
        else
            hpBarFill.color = Color.red;
    }
}