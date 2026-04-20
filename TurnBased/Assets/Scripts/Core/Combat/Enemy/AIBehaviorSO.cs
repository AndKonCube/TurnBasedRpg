using System.Collections.Generic;
using UnityEngine;

public enum AIBehaviorType
{
    Aggressive,
    Random,
    Defensive
}

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyBehavior")]
public class AIBehaviorSO : ScriptableObject
{
    public AIBehaviorType behaviorType;
    public List<SkillDataSO> skills;
}
