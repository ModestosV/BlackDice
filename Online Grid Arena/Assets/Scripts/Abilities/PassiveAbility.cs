using UnityEngine;

public abstract class PassiveAbility : AbstractAbility
{
    protected IEffect effect;

    protected PassiveAbility(IEffect effect, Sprite abilityIcon, ICharacter character) : base(abilityIcon, character)
    {
        this.effect = effect;
    }

    protected abstract void ActivatePassive();
}
