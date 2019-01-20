using UnityEngine;

public abstract class AbstractPassiveAbility : AbstractAbility, IAbility
{
    protected AbstractPassiveAbility(Sprite abilityIcon, ICharacter character) : base(abilityIcon, character) { }
}
