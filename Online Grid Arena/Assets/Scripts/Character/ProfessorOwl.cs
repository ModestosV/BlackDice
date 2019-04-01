using System.Collections.Generic;
using UnityEngine;

public sealed class ProfessorOwl : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();
        IAbility swoopDown = new SwoopDown(this);
        IAbility refactor = new Placeholder(this);
        IAbility accusation = new Accusation(this);
        IAbility codeReview = new CodeReview(this);

        var abilities = new List<IAbility>() { swoopDown, refactor, accusation, codeReview };

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
