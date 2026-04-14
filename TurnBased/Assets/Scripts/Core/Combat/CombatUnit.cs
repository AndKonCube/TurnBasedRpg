using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    CharacterDataSO data;
    private int currentHP;
    private int currentMP;
    private List<StatusEffectInstance> statusEffectInstances;
    private bool isAlive;
    private bool isPlayer;
    private GameEventSO onUnitDied;
    private GameEventSO onHPChanged;
    private GameEventSO onMPChanged;

    private void Initialize(CharacterDataSO characterData)
    {
        data = characterData;
        currentHP = characterData.baseHP;
        currentMP = characterData.baseMP;
        isAlive = true;
        statusEffectInstances = new List<StatusEffectInstance>();
    }

    public void TakeDamage(int amount)
    {
        currentHP= currentHP - amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
            onUnitDied.Raise();
        }
        onHPChanged.Raise();
    }

    public void HealHP(int amount)
    {
        currentHP = Mathf.Clamp(currentHP+amount, 0, data.baseHP);
        onHPChanged.Raise();
    }

        public void SpendMP(int amount)
    {
        currentMP = Mathf.Clamp(currentMP+amount, 0, data.baseMP);
        onMPChanged.Raise();
    }

    private bool HasEnouhgMana(int amount)
    {
        return currentMP >= amount;
    }
}
