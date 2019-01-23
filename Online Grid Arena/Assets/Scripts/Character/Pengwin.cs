﻿using System.Collections.Generic;
using UnityEngine;

public sealed class Pengwin : AbstractCharacter
{
    void Awake()
    {
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

        ActiveCircle = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/ActiveCircle"), this.transform);
        ActiveCircle.transform.parent = this.transform;

        HealthBar = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/HealthBar"), this.transform);
        HealthBar.transform.parent = this.transform;

        characterController = new CharacterController()
        {
            Character = this,
            OwnedByPlayer = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = GetComponentInChildren<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            ActiveCircle = GetComponentInChildren<ActiveCircle>().GetComponentInChildren<SpriteRenderer>()
        };

        TeamColorIndicator = Resources.Load<GameObject>("Prefabs/Characters/CharColorMarker");
        TeamColorIndicator = Instantiate(TeamColorIndicator, this.transform);
        TeamColorIndicator.transform.parent = this.transform;
        TeamColorIndicator.GetComponent<SpriteRenderer>().color = borderColor;
    }
}
