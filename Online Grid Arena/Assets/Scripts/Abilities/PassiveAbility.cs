
using UnityEngine;

public abstract class PassiveAbility : AbstractAbility
{
    protected AbstractEffect effect;

    protected PassiveAbility(AbilityType type,AbstractEffect effect) : base(type, 0)
    {
        this.effect = effect;
    }
}
