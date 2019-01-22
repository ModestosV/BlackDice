using UnityEngine;
using System.Collections.Generic;

public class Slide : AbstractTargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public Slide(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/slide"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/woosh"),
        activeCharacter,
        5,
        100,
        AbilityType.TARGET_LINE)
    {
        Description = "Special Ability \nPengwin slides in a straight line and stops right before the target tile. Deals 4 * (number of tiles moved) damage.";
    }

    // Move Rocket Cat to new location
    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        character.Controller.OccupiedTile.OccupantCharacter = null;
        character.Controller.OccupiedTile.Deselect();

        character.MoveToTile(targetTiles[0].HexTile);
        character.Controller.OccupiedTile = targetTiles[0];

        targetTiles[0].OccupantCharacter = character.Controller;
        targetTiles[0].Select();
        PlaySoundEffect();
    }
}
