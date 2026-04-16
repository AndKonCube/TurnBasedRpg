using System;
using Unity.VisualScripting;
using UnityEngine;


public enum TargetType
{
    SingleEnemy,
    AllEnemies,
    SingleAlly,
    AllAllies,
    Self
}
public enum SkillCategory
{
    Damage,
    Heal,
    StatusApply,
    Buff,
    Debuff
}
public enum CostType
{
    MP,
    HP,
    None
}

[CreateAssetMenu(fileName = "Skills", menuName = "SkillData")]
public class SkillDataSO : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;
    public SkillCategory skillCategory;
    public TargetType targetType;
    public CostType costType;
    public int costAmount;
    public int basePower;
    public string scalingStat;
    public StatusEffectSO statusToApply;
    public float applicationChance;
    public string animationTrigger;
    public AudioClip soundEffect;
}
