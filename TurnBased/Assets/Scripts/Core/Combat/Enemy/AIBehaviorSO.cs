using System.Collections.Generic;
using UnityEngine;

public enum AIBehaviorType
{
    Aggressive,
    Random,
    Defensive
}

public class AIBehaviorSO : MonoBehaviour
{
    public AIBehaviorType behaviorType;
    public List<SkillDataSO> skills;
}
