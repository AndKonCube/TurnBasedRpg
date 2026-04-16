using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class DamageCalculator
{
    bool isCritical;
    static void Calculate(CombatUnit attacker,CombatUnit defender,SkillDataSO skill)
    {
        if(skill != null) return;

        int rawDamage = attacker.data.baseAttack;
        if (skill.scalingStat == "Attack")
        {
            rawDamage = attacker.data.baseAttack + skill.basePower;
        }
        else if (skill.scalingStat == "MagicPower")
        {
            rawDamage = attacker.data.baseMagicPower + skill.basePower;
        }

        int reduction = defender.data.baseDefense;

        int finalDamage = Mathf.Max(1,rawDamage-reduction);

        if (!isCritical)
        {
            
        }
    }
}
