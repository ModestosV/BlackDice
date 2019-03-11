﻿using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCat : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        IEffect catScratchFever = new CatScratchFever();

        IAbility catScratchFeverAbility = new CatScratchFeverAbility(this, catScratchFever);
        IAbility scratch = new Scratch(this, catScratchFeverAbility);
        IAbility blastoff = new BlastOff(this);
        IAbility kamikaze = new Kamikaze(this);

        var abilities = new List<IAbility>() { scratch, blastoff, catScratchFeverAbility, kamikaze };
        var effects = new List<IEffect>() { };
        
        ICharacterStat health = new CharacterStat(120.0f);
        ICharacterStat moves = new CharacterStat(6.0f);
        ICharacterStat attack = new CharacterStat(25.0f);
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
            CharacterOwner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = healthBar.GetComponent<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            ActiveCircle = activeCircle.GetComponent<SpriteRenderer>(),
            Shield = shield.GetComponent<SpriteRenderer>()
        };
    }
}
