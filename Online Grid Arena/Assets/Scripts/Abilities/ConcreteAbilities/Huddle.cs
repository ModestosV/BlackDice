﻿using System.Collections.Generic;
using UnityEngine;

public class Huddle : AbstractActiveAbility
{
    private const float DEFENSE_BONUS_AMOUNT = 20.0f;

    public Huddle(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/huddle"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/huddle"),
        character,
        3,
        "Huddle - Special Ability \nPengwin gives 20 bonus defense to itself and all adjacent allies.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        foreach (IHexTileController neighbor in targetTiles[0].GetNeighbors())
        {
            if (neighbor.OccupantCharacter != null)
            {
                if (neighbor.OccupantCharacter.IsAlly(character.Controller))
                {
                    neighbor.OccupantCharacter.ApplyEffect(new HuddleEffect(DEFENSE_BONUS_AMOUNT));
                    PlayAnimation(neighbor);
                }
            }
        }
        PlaySoundEffect();
       
        character.Controller.ApplyEffect(new HuddleEffect(DEFENSE_BONUS_AMOUNT));
        PlayAnimation(targetTiles[0]);
    }
}