using System.Collections.Generic;
using UnityEngine;

public sealed class TAEagle : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        // Init abilities
        IAbility swoopDown = new SwoopDown(this);
        IAbility refactor = new Placeholder(this);
        IAbility importDLL = new ImportDLLs(this);
        IAbility codeReview = new CodeReview(this);

        var abilities = new List<IAbility>() { swoopDown, refactor, importDLL, codeReview };

        var effects = new List<IEffect>() { };

        // Init stats
        var characterStats = InitializeStats(100, 5, 20, 100);

        characterController = new CharacterController(this)
        {
            Owner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = healthBar.GetComponent<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            Shield = shield.GetComponent<MeshRenderer>()
        };
    }
}
