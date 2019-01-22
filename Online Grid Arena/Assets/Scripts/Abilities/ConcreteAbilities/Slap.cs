using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class Slap : AbstractTargetedAbility
{
    public Slap(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/PengwinSlap"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/slap"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY)
    {
        Description = "basic Ability \nPengwin slaps adjacent enemy target and deals damage equal to his attack. Has a 75% chance of re-casting, with diminishing odds each time (maximum number of hits: 4).";
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        ChanceToTrigger(75, targetTiles);
    }

    private void ChanceToTrigger(int chance, List<IHexTileController> targetTiles)
    {
        targetTiles[0].Damage(character.Controller.CharacterStats["attack"].Value);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);

        System.Random randomizer = new System.Random();
        int rand = randomizer.Next(0,100);
        if (rand < chance)
        {
            ChanceToTrigger(chance-25, targetTiles);
        }

    }
}
