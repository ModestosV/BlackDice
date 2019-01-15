﻿using UnityEngine;
using System.Collections.Generic;

public class BlastOff : ActiveAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public BlastOff(RocketCat activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/jetpack-png-3"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        1,
        1
        )
    {

    }

    // Move Rocket Cat to new location
    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        ActiveCharacter.Controller.OccupiedTile.OccupantCharacter = null;
        ActiveCharacter.Controller.OccupiedTile.Deselect();

        // Movement animation

        ActiveCharacter.MoveToTile(targetTiles[0].HexTile);
        ActiveCharacter.Controller.OccupiedTile = targetTiles[0];

        targetTiles[0].OccupantCharacter = ActiveCharacter.Controller;
        targetTiles[0].Select();
    }

    // Damage all tiles around target location
    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        // damage all characters at target location            
        foreach (IHexTileController target in targetTiles[0].GetNeighbors())
        {
            target.Damage(15);
            PlayAnimation(target);
        }

        PlaySoundEffect();

    }
}
