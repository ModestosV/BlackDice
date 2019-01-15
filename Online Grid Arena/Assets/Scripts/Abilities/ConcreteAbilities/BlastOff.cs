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
        1,
        100,
        AbilityType.TARGET_TILE)
    { }

    // Move Rocket Cat to new location
    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        character.Controller.OccupiedTile.OccupantCharacter = null;
        character.Controller.OccupiedTile.Deselect();

        // Movement animation

        character.MoveToTile(targetTiles[0].HexTile);
        character.Controller.OccupiedTile = targetTiles[0];

        targetTiles[0].OccupantCharacter = character.Controller;
        targetTiles[0].Select();
    }

    // Damage all tiles around target location
    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        // damage all characters at target location            
        foreach (IHexTileController target in targetTiles[0].GetNeighbors())
        {
            target.Damage(character.Controller.CharacterStats["attack"].Value*0.75f);
            PlayAnimation(target);
        }

        PlaySoundEffect();

    }
}
