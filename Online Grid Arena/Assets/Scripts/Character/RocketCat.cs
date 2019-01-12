﻿using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCat : AbstractCharacter
{
    void Awake()
    {
        // Init abilities
        IAbility catScratchFeverAbility = new CatScratchFeverAbility(this);
        IAbility scratch = new Scratch(catScratchFeverAbility);
        IEffect passive = new CatScratchFever();
        var abilities = new List<IAbility>() { scratch, catScratchFeverAbility };
        var effects = new List<IEffect>() { };

        // Init stats

        ICharacterStat health = new CharacterStat(120.0f);
        ICharacterStat moves = new CharacterStat(6.0f);

        var characterStats = new Dictionary<string, ICharacterStat>()
        {
            {"health", health},
            {"moves", moves}
        };

        characterController = new CharacterController()
        {
            Character = this,
            OwnedByPlayer = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = GetComponentInChildren<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects
        };
        EventBus.Subscribe<StartNewTurnEvent>((IEventSubscriber)this.Controller);
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
    }


}
