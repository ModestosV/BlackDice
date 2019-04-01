using System.Collections.Generic;
using UnityEngine;

public sealed class DefaultCharacter : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        IAbility defaultAttack = new DefaultAttack(this);
        IAbility defaultHeal = new DefaultHeal(this);
        IAbility placeholder = new Placeholder(this);

        var abilities = new List<IAbility>() { defaultAttack, defaultHeal, placeholder, placeholder };

        var characterStats = InitializeStats(100, 5, 20, 100);

        characterController = new CharacterController(this)
        {
            Owner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            Shield = shield.GetComponent<MeshRenderer>()
        };
    }
}
