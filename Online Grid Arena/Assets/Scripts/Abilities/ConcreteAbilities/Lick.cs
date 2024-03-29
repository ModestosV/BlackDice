﻿using System.Collections.Generic;
using UnityEngine;

public sealed class Lick : AbstractTargetedAbility
{
    public Lick(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/lick"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/kouaks"),
        activeCharacter,
        1,
        2,
        AbilityType.TARGET_ENEMY,
        "Lick - Basic Attack \nAgent Frog licks an opponent, causing their speed to drop by 2 for 1 turn. Range: 2")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Lick. Primary action being called.");
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        targetTiles[0].OccupantCharacter.ApplyEffect(new ViscousSaliva());
    }
}
