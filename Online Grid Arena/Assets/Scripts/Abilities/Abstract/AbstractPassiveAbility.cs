using UnityEngine;

public abstract class AbstractPassiveAbility : AbstractAbility, IPassiveAbility
{
    protected AbstractPassiveAbility(Sprite abilityIcon, ICharacter character, string description, bool isEndOfTurn) : base(abilityIcon, character, description)
    {
        IsEndOfTurnPassive = isEndOfTurn;
    }

    public bool IsEndOfTurnPassive { get;}
}
