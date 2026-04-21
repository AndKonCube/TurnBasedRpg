using System.Collections.Generic;
using UnityEngine;

public static class EnemyAI
{
    public static ActionCommand DecideAction(CombatUnit self, List<CombatUnit> playerUnits)
    {
        switch (self.data.aiProfile.behaviorType)
        {
            case AIBehaviorType.Aggressive:
                return BuildAttackCommand(self, playerUnits);
            case AIBehaviorType.Random:
                return BuildSkillCommand(self, playerUnits);
            case AIBehaviorType.Defensive:
                //TODO: Later defensive option addition
                return BuildAttackCommand(self, playerUnits);
            default:
                return BuildAttackCommand(self, playerUnits);

        }
    }

    public static ActionCommand BuildAttackCommand(CombatUnit self, List<CombatUnit> playerUnits)
    {
        CombatUnit target = GetRandomAliveTarget(playerUnits);
        if (target == null) return null;
        return new AttackCommand(self,new List<CombatUnit>{target});
    }

    public static ActionCommand BuildSkillCommand(CombatUnit self, List<CombatUnit> playerUnits)
    {
        CombatUnit target = GetRandomAliveTarget(playerUnits);
        if(target == null) return null;

        return new AttackCommand(self, new List<CombatUnit>{target});
    }

    public static CombatUnit GetRandomAliveTarget(List<CombatUnit> playerUnits)
    {
        List<CombatUnit> aliveTargets = playerUnits.FindAll(unit=> unit.isAlive);
        if(aliveTargets.Count == 0) return null;

        return aliveTargets[Random.Range(0,aliveTargets.Count)];
    }
}
