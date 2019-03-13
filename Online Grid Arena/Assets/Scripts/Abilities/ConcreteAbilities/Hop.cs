using UnityEngine;
using System.Collections.Generic;

public class Hop : AbstractTargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public Hop(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/jetpack-png-3"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        3,
        8,
        AbilityType.TARGET_TILE,
        "Hop - Combo Ability \nAgent Frog hops up to 8 tiles. *COMBO* Can use another ability after casting this.",
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
        character.Controller.Combo();
        PlaySoundEffect();
    }
}
