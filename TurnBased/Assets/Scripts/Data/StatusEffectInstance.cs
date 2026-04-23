using UnityEngine;

public class StatusEffectInstance
{
    public StatusEffectSO statusEffect;
    public int remainingTurns;
    public TickMoment tickMoment;
    public StatusType statusType;
    public int powerPerTick;

    public StatusEffectInstance(StatusEffectSO effect)
    {
        statusEffect = effect;
        remainingTurns = effect.duration;
        tickMoment = effect.tickMoment;
        statusType = effect.statusType;
        powerPerTick = effect.powerTick;
    }
}
