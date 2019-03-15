using UnityEngine;

public abstract class AbstractPassiveAbility : AbstractAbility, IPassiveAbility
{
    protected AbstractPassiveAbility(Sprite abilityIcon, ICharacter character, string description) : base(abilityIcon, character, description) { }

    public abstract bool IsEndOfTurnPassive();
}
