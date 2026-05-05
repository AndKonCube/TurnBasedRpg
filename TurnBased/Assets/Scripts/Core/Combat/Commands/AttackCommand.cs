using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : ActionCommand
{
    public AttackCommand(CombatUnit source, List<CombatUnit> targets) : base(source, targets)
    {

    }

    public override int Execute()
    {
        int totalDamage = 0;
        foreach (CombatUnit target in targets)
        {
            if (target.isAlive)
            {
                int damage = DamageCalculator.Calculate(source, target, null);
                target.TakeDamage(damage);
                totalDamage += damage;
            }
        }
        return totalDamage;
    }
}
