using UnityEngine;

public abstract class AbstractPassiveAbility : AbstractAbility, IPassiveAbility
{
    protected AbstractPassiveAbility(Sprite abilityIcon, ICharacter character, string description, bool isEndOfTurn) : base(abilityIcon, character, description) { }

    public bool IsEndOfTurnPassive { get;}
}
