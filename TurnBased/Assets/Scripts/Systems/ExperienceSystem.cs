using System.Collections.Generic;



public static class ExperienceSystem
{
    public static int CalculateXP(List<CombatUnit> defeatedEnemies)
    {
        int totalXP = 0;
        foreach (CombatUnit enemy in defeatedEnemies)
        {
            int baseXP = enemy.data.expirienceReward;
            int stabonus = (enemy.data.baseHP + enemy.data.baseAttack + enemy.data.baseDefense) / 10;

            totalXP += baseXP + stabonus;
        }
        return totalXP;
    }

    public static int CalculateGold(List<CombatUnit> defeatedEnemies)
    {
        int totalGold = 0;

        int enemyCount = defeatedEnemies.Count;
        foreach(CombatUnit enemy in defeatedEnemies)
        {
            totalGold += enemy.data.goldReward *enemyCount;
        }

        return totalGold;
    }
}
