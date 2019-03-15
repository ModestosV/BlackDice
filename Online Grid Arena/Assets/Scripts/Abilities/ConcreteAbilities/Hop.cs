﻿using UnityEngine;
using System.Collections.Generic;

public class Hop : AbstractTargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public Hop(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/leaping"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/frog sound effect"),
        activeCharacter,
        3,
        8,
        AbilityType.TARGET_TILE,
        "Hop - Combo Ability \nAgent Frog hops up to 8 tiles. *COMBO* Can use another ability after casting this.")
    { }

    // Move Rocket Cat to new location
    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Hop. Primary action being called.");
        character.Controller.OccupiedTile.OccupantCharacter = null;
        character.MoveToTile(targetTiles[0].HexTile);
        character.Controller.OccupiedTile = targetTiles[0];
        targetTiles[0].OccupantCharacter = character.Controller;
    }

    // Damage all tiles around target location
    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        character.Controller.IncrementAbilitiesRemaining();
        PlaySoundEffect();
    }
}
