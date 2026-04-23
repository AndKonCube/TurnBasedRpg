using System.Collections.Generic;
using UnityEngine;

public static class ExperienceSystem
{
    public static int CalculateXP(List<CombatUnit> defeatedEnemies)
    {
        int totalXP = 0;
        foreach (CombatUnit enemy in defeatedEnemies)
        {
            int baseXP = enemy.data.expirienceReward;
            int statBonus = (enemy.data.baseHP + enemy.data.baseAttack + enemy.data.baseDefense) / 10;
            totalXP += baseXP + statBonus;
        }
        return totalXP;
    }

    public static int CalculateGold(List<CombatUnit> defeatedEnemies)
    {
        int totalGold = 0;
        int enemyCount = defeatedEnemies.Count;
        foreach (CombatUnit enemy in defeatedEnemies)
        {
            totalGold += enemy.data.goldReward * enemyCount;
        }
        return totalGold;
    }

    public static int XPRequiredForLevel(int level)
    {
        int baseXP = 100;
        return (int)(baseXP * Mathf.Pow(level, 3) / 50);
    }

    public static int GetLevelFromXP(int totalXP)
    {
        int level = 1;
        while (totalXP >= XPRequiredForLevel(level + 1))
        {
            level++;
        }
        return level;
    }
}