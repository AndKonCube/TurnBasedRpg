using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : ActionCommand
{
    public SkillDataSO skill;
    public SkillCommand(CombatUnit source, List<CombatUnit> targets, SkillDataSO skill) : base(source, targets)
    {
        this.skill = skill;
    }

    public override void Execute()
    {
        if (!source.HasEnoughMP(skill.costAmount))
        {
            return;
        }

        source.SpendMP(skill.costAmount);
        foreach (CombatUnit target in targets)
        {
            if (target.isAlive)
            {
                switch (skill.skillCategory)
                {
                    case SkillCategory.Damage:
                        int damage = DamageCalculator.Calculate(source, target, skill);
                        target.TakeDamage(damage);
                        if (skill.statusToApply != null)
                        {
                            if (Random.Range(0.0f, 1.0f) < skill.applicationChance)
                            {
                                StatusEffectHandler.Apply(target, skill.statusToApply);
                            }
                        }
                        break;
                    case SkillCategory.Heal:
                        target.HealHP(skill.basePower);
                        break;
                    case SkillCategory.StatusApply:
                        if (Random.Range(0.0f, 1.0f) < skill.applicationChance)
                        {
                            Debug.Log("statusToApply: " + skill.statusToApply);
                            StatusEffectHandler.Apply(target, skill.statusToApply);
                        }
                        break;
                    case SkillCategory.Buff:
                        //TODO: Add logic later 
                        break;
                    case SkillCategory.Debuff:
                        //TODO: Add logic later
                        break;
                }
            }
        }
    }
}
