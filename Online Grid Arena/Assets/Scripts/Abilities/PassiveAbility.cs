
using UnityEngine;

public abstract class PassiveAbility : AbstractAbility
{
    protected AbstractEffect effect;

    protected PassiveAbility(AbilityType type,AbstractEffect effect) : base(type, cooldown)
    {
        this.effect = effect;
    }
}
