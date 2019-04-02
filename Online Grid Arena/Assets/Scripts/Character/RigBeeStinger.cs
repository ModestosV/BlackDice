using System.Collections.Generic;
using UnityEngine;

public sealed class RigBeeStinger : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();
        IAbility sting = new Sting(this);
        IAbility silence = new Silence(this);
        IAbility accusation = new Accusation(this);
        IAbility flakyTests = new FlakyTests(this);

        var abilities = new List<IAbility>() { sting, silence, accusation, flakyTests };

        var characterStats = InitializeStats(100, 8, 20, 80);

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
