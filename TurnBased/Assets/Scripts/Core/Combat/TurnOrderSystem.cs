using System.Collections.Generic;
using System.Linq;

public static class TurnOrderSystem
{
    public static Queue<CombatUnit> Sort(List<CombatUnit> allUnits)
    {
        List<CombatUnit> sorted = allUnits
            .OrderByDescending(static unit => unit.data.baseSpeed)
            .ThenBy(unit => unit.isPlayer ? 0 : 1)
            .ToList();

        return new Queue<CombatUnit>(sorted);
    }
}