using System.Collections.Generic;
using UnityEngine;

public sealed class ProfessorOwl : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();
        IAbility placeholder = new Placeholder(this);
        IAbility placeholder1 = new Placeholder(this);
        IAbility accusation = new Accusation(this);
        IAbility flakyTests = new FlakyTests(this);

        var abilities = new List<IAbility>() { placeholder, placeholder1, accusation, flakyTests };

        var characterStats = InitializeStats(100, 50, 20, 100);

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
