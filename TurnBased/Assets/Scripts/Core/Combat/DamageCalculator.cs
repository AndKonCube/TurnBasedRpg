using UnityEngine;

public static class DamageCalculator
{
    public static int Calculate(CombatUnit attacker, CombatUnit defender, SkillDataSO skill)
    {
        Debug.Log($"Attacker: {attacker.data.characterName} ATK:{attacker.data.baseAttack} MAG:{attacker.data.baseMagicPower} MOD:{attacker.attackModifier}");
        Debug.Log($"Defender: {defender.data.characterName} DEF:{defender.data.baseDefense}");
        if (skill != null)
            Debug.Log($"Skill: {skill.skillName} Power:{skill.basePower} Scaling:{skill.scalingStat}");
        int rawDamage = attacker.data.baseAttack + attacker.attackModifier;

        if (skill != null)
        {
            if (skill.scalingStat == "Attack")
                rawDamage = attacker.data.baseAttack + attacker.attackModifier + skill.basePower;
            else if (skill.scalingStat == "MagicPower")
                rawDamage = attacker.data.baseMagicPower + attacker.attackModifier + skill.basePower;
        }

        int reduction = Mathf.RoundToInt(defender.data.baseDefense * 0.3f + defender.defenseModifier);
        int finalDamage = Mathf.Max(1, rawDamage - reduction);

        if (IsCritical())
            finalDamage = finalDamage * 2;

        return finalDamage;
    }

    public static bool IsCritical()
    {
        return UnityEngine.Random.Range(0.0f, 1.0f) < 0.1f;
    }
}