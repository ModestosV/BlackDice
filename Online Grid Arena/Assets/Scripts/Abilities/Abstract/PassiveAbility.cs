using UnityEngine;

public abstract class PassiveAbility : AbstractAbility, IPassiveAbility
{
    protected PassiveAbility(Sprite abilityIcon, ICharacter character, string description, bool isEndOfTurn) : base(abilityIcon, character, description)
    {
        IsEndOfTurnPassive = isEndOfTurn;
    }

    public bool IsEndOfTurnPassive { get;}
}
