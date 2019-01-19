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

        IAbility placeholder1 = new Placeholder(this);
        IAbility placeholder2 = new Placeholder(this);
        IAbility placeholder3 = new Placeholder(this);
        IAbility placeholder4 = new Placeholder(this);

        var abilities = new List<IAbility>() { placeholder1, placeholder2, placeholder3, placeholder4 };
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
    }
}
