using UnityEngine;
public enum TickMoment
{   
    StartOfTurn,
    EndOfTurn
}
public enum StatusType
{
    Poison,
    Burn,
    Bleed,
    Stun,
    DefenseDown,
    AttackUp
};

[CreateAssetMenu(fileName = "StatusEffects", menuName = "StatusEffectsData")]
public class StatusEffectSO : ScriptableObject
{
    public string effectName;
    public Sprite icon;
    public StatusType statusType;
    public int duration;
    public TickMoment tickMoment;
    public int powerTick;
    public string applicationMessage;
    public string tickMessage;
}
