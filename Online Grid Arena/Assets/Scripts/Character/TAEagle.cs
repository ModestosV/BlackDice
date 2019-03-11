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

        ICharacterStat health = new CharacterStat(100.0f);
        ICharacterStat moves = new CharacterStat(5.0f);
        ICharacterStat attack = new CharacterStat(20.0f);
        ICharacterStat defense = new CharacterStat(100.0f);

        var characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "health", health },
            { "moves", moves },
            { "attack", attack },
            { "defense", defense }
        };

        characterController = new CharacterController(this)
        {
            Owner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = healthBar.GetComponent<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            ActiveCircle = activeCircle.GetComponent<SpriteRenderer>(),
            Shield = shield.GetComponent<MeshRenderer>()
        };
    }
}
