using UnityEngine;

public static class DamageCalculator
{
    public static int Calculate(CombatUnit attacker, CombatUnit defender, SkillDataSO skill)
    {
        int rawDamage = attacker.data.baseAttack = attacker.attackModifier;
        int reduction = defender.data.baseDefense + defender.defenseModifier;

        if (skill != null)
        {
            if (skill.scalingStat == "Attack")
            {
                rawDamage = attacker.data.baseAttack + skill.basePower;
            }
            else if (skill.scalingStat == "MagicPower")
            {
                rawDamage = attacker.data.baseMagicPower + skill.basePower;
            }
        }

        int finalDamage = Mathf.Max(1, rawDamage - reduction);

        if (IsCritical())
        {
            finalDamage = finalDamage * 2;
        }

        return finalDamage;
    }

    public static bool IsCritical()
    {
        return UnityEngine.Random.Range(0.0f, 1.0f) < 0.1f;
    }
}