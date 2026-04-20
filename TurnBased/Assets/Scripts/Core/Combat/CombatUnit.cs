using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    public CharacterDataSO data;
    private int currentHP;
    private int currentMP;
    public List<StatusEffectInstance> activeEffects;
    public bool isAlive;
    public bool isPlayer;
    public int attackModifier;
    public int defenseModifier;
    public bool isStunned;

    [SerializeField] private GameEventSO onUnitDied;
    [SerializeField] private GameEventSO onHPChanged;
    [SerializeField] private GameEventSO onMPChanged;

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public void Initialize(CharacterDataSO characterData)
    {
        data = characterData;
        currentHP = characterData.baseHP;
        currentMP = characterData.baseMP;
        isAlive = true;
        activeEffects = new List<StatusEffectInstance>();
    }

    public void TakeDamage(int amount)
    {
        currentHP = currentHP - amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
            if (onUnitDied != null) onUnitDied.Raise();
        }
        if (onHPChanged != null) onHPChanged.Raise();
    }

    public void HealHP(int amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 0, data.baseHP);
        if (onHPChanged != null) onHPChanged.Raise();
    }

    public void RestoreMP(int amount)
    {
        currentMP = Mathf.Clamp(currentMP + amount, 0, data.baseMP);
        if (onMPChanged != null) onMPChanged.Raise();
    }

    public void SpendMP(int amount)
    {
        currentMP = Mathf.Clamp(currentMP - amount, 0, data.baseMP);
        if (onMPChanged != null) onMPChanged.Raise();
    }

    public bool HasEnoughMP(int amount)
    {
        return currentMP >= amount;
    }
}