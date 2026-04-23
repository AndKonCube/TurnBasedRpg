using System.Collections.Generic;
using UnityEngine;

public static class StatusEffectHandler
{
    public static void Apply(CombatUnit unit, StatusEffectSO effect)
    {
        Debug.Log("Applying effect: " + effect);
        Debug.Log("Effect name: " + effect?.effectName);
        foreach (StatusEffectInstance existing in unit.activeEffects)
        {
            if (existing.statusType == effect.statusType)
            {
                existing.remainingTurns = effect.duration;
                return;
            }
        }

        unit.activeEffects.Add(new StatusEffectInstance(effect));
    }

    public static void Tick(CombatUnit unit, TickMoment moment, BattleLogUI log = null)
    {
        foreach (StatusEffectInstance effect in unit.activeEffects)
        {
            if (effect.tickMoment == moment)
            {
                ApplyTickEffect(unit, effect,log);
                effect.remainingTurns--;
            }
        }
        unit.activeEffects.RemoveAll(effect => effect.remainingTurns <= 0);
    }

    private static void ApplyTickEffect(CombatUnit unit, StatusEffectInstance effect, BattleLogUI logUI)
    {
        switch (effect.statusType)
        {
            case StatusType.Poison:
                unit.TakeDamage(effect.powerPerTick);
                logUI.Log($"{unit.data.characterName} takes {effect.powerPerTick} {effect.statusType} damage!");
                break;
            case StatusType.Burn:
                unit.TakeDamage(effect.powerPerTick);
                logUI.Log($"{unit.data.characterName} takes {effect.powerPerTick} {effect.statusType} damage!");
                break;
            case StatusType.Bleed:
                unit.TakeDamage(effect.powerPerTick);
                logUI.Log($"{unit.data.characterName} takes {effect.powerPerTick} {effect.statusType} damage!");
                break;
            case StatusType.AttackUp:
                unit.attackModifier += effect.powerPerTick;
                break;
            case StatusType.DefenseDown:
                unit.defenseModifier -= effect.powerPerTick;
                break;
            case StatusType.Stun:
                unit.isStunned = true;
                logUI.Log($"{unit.data.characterName} is Stunned!");
                break;
        }

    }

    public static void RemoveAll(CombatUnit unit)
    {
        unit.activeEffects.Clear();
        unit.attackModifier = 0;
        unit.defenseModifier = 0;
        unit.isStunned = false;
    }

    public static void Remove(CombatUnit unit, StatusType type)
    {
        unit.activeEffects.RemoveAll(effect => effect.statusType == type);
        unit.attackModifier = 0;
        unit.defenseModifier = 0;
        unit.isStunned = false;
    }
}