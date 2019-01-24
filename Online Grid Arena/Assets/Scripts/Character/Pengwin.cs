﻿using System.Collections.Generic;
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
        IAbility placeholder3 = new Placeholder(this);
        IAbility placeholder4 = new Placeholder(this);

        var abilities = new List<IAbility>() { slap, slide, placeholder3, placeholder4 };

        characterController = new CharacterController()
        {
            Character = this,
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
