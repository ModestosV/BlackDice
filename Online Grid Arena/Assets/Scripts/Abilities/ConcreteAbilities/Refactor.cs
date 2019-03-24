﻿using System.Collections.Generic;
using UnityEngine;

public sealed class Refactor : AbstractTargetedAbility
{
    public Refactor(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        1,
        3,
        AbilityType.TARGET_ENEMY,
        "Refactor - Special Attack \nTA Eagle scrambles up an enemy in a 3 tile range causing them to be stunned for 1 turn.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Refactor. Primary action being called.");
        targetTiles[0].OccupantCharacter.StatusEffectState = StatusEffectState.STUNNED;
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
