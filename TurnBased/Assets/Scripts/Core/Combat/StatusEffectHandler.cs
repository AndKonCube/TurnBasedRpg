using System.Collections.Generic;
using UnityEngine;

public static class StatusEffectHandler
{
    public static void Apply(CombatUnit unit, StatusEffectSO effect)
    {
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

    public static void Tick(CombatUnit unit, TickMoment moment)
    {
        foreach (StatusEffectInstance effect in unit.activeEffects)
        {
            if (effect.tickMoment == moment)
            {
                ApplyTickEffect(unit, effect);
                effect.remainingTurns--;
            }
        }
        unit.activeEffects.RemoveAll(effect => effect.remainingTurns <= 0);
    }

    private static void ApplyTickEffect(CombatUnit unit, StatusEffectInstance effect)
    {
        switch (effect.statusType)
        {
            case StatusType.Poison:
                unit.TakeDamage(effect.powerPerTick);
                break;
            case StatusType.Burn:
                unit.TakeDamage(effect.powerPerTick);
                break;
            case StatusType.Bleed:
                unit.TakeDamage(effect.powerPerTick);
                break;
            case StatusType.AttackUp:
                unit.attackModifier += effect.powerPerTick;
                break;
            case StatusType.DefenseDown:
                unit.defenseModifier -= effect.powerPerTick;
                break;
            case StatusType.Stun:
                unit.isStunned = true;
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