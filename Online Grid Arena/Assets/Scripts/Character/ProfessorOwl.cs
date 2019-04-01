using System.Collections.Generic;
using UnityEngine;

public sealed class ProfessorOwl : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();
        IAbility lecture = new Lecture(this);
        IAbility silence = new Silence(this);
        IAbility accusation = new Accusation(this);
        IAbility flakyTests = new FlakyTests(this);

        var abilities = new List<IAbility>() { lecture, silence, accusation, flakyTests };

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
