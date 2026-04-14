using UnityEngine;

public class StatusEffectInstance : MonoBehaviour
{
    private StatusEffectSO statusEffect;
    private int remainingTurns;
    private TickMoment tickMoment;
    private StatusType statusType;
    private int powerPerTick;

    public StatusEffectInstance(StatusEffectSO effect)
    {
        statusEffect = effect;
        remainingTurns = effect.duration;
        tickMoment = effect.tickMoment;
        statusType = effect.statusType;
        powerPerTick = effect.powerTick;
    }
}
