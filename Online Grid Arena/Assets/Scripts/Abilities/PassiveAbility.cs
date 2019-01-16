using UnityEngine;

public abstract class PassiveAbility : AbstractAbility
{
    protected IEffect effect;

    protected PassiveAbility(AbilityType type, IEffect effect, Sprite abilityIcon) : base(type, 0, abilityIcon)
    {
        this.effect = effect;
    }

    protected abstract void ActivatePassive();
}
