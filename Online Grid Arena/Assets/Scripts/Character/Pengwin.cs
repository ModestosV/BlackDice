using System.Collections.Generic;
using UnityEngine;

public sealed class Pengwin : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        ICharacterStat health = new CharacterStat(140.0f);
        ICharacterStat moves = new CharacterStat(4.0f);
        ICharacterStat attack = new CharacterStat(15.0f);
        ICharacterStat defense = new CharacterStat(100.0f);

        var characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "health", health },
            { "moves", moves },
            { "attack", attack },
            { "defense", defense }
        };

        var effects = new List<IEffect>() { };

        IAbility slap = new Slap(this);
        IAbility slide = new Slide(this);
        IAbility huddle = new Huddle(this);
        IAbility arcticFury = new ArcticFury(this);

        var abilities = new List<IAbility>() { slap, slide, huddle, arcticFury };

        characterController = new CharacterController(this)
        {
            CharacterOwner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = healthBar.GetComponent<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            ActiveCircle = activeCircle.GetComponent<SpriteRenderer>()
        };
    }
}
