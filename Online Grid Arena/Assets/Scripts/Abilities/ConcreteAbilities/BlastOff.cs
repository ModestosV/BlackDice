using UnityEngine;
using System.Collections.Generic;

public class BlastOff : AbstractTargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public BlastOff(RocketCat activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/jetpack-png-3"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        5,
        100,
        AbilityType.TARGET_TILE,
        "Blast Off - Special Ability \nRocket Cat flies to any open tile on the map, and deals 75% of her attack power as damage to all tiles next to her landing point",
        false)
    { }

    // Move Rocket Cat to new location
    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        character.Controller.OccupiedTile.OccupantCharacter = null;
        
        character.MoveToTile(targetTiles[0].HexTile);
        character.Controller.OccupiedTile = targetTiles[0];

        targetTiles[0].OccupantCharacter = character.Controller;
    }

    // Damage all tiles around target location
    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {        
        foreach (IHexTileController target in targetTiles[0].GetNeighbors())
        {
            actionHandler.Damage(character.Controller.CharacterStats["attack"].Value*0.75f, target.OccupantCharacter);
            PlayAnimation(target);
        }

        PlaySoundEffect();
    }
}
