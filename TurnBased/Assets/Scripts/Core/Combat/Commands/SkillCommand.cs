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

    public override int Execute()
    {
        if (!source.HasEnoughMP(skill.costAmount))
            return 0;

        source.SpendMP(skill.costAmount);

        int totalDamage = 0;
        foreach (CombatUnit target in targets)
        {
            if (target.isAlive)
            {
                switch (skill.skillCategory)
                {
                    case SkillCategory.Damage:
                        int damage = DamageCalculator.Calculate(source, target, skill);
                        target.TakeDamage(damage);
                        totalDamage += damage;
                        if (skill.statusToApply != null &&
                            Random.Range(0f, 1f) < skill.applicationChance)
                            StatusEffectHandler.Apply(target, skill.statusToApply);
                        break;
                    case SkillCategory.Heal:
                        target.HealHP(skill.basePower);
                        break;
                    case SkillCategory.StatusApply:
                        if (skill.statusToApply != null &&
                            Random.Range(0f, 1f) < skill.applicationChance)
                            StatusEffectHandler.Apply(target, skill.statusToApply);
                        break;
                    case SkillCategory.Buff:
                    case SkillCategory.Debuff:
                        break;
                }
            }
        }
        return totalDamage;
    }
}
