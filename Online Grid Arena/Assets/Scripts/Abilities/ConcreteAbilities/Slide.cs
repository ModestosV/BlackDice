using UnityEngine;
using System.Collections.Generic;

public class Slide : AbstractTargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;
    private int distanceTravelled;

    public Slide(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/slide"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlideHitAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/woosh"),
        activeCharacter,
        7,
        100,
        AbilityType.TARGET_LINE,
        "Slide - Special Ability \nPengwin slides in a straight line. Deals 6 * (number of tiles moved) damage to the target tile.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        character.Controller.OccupiedTile.OccupantCharacter = null;
        distanceTravelled = character.Controller.OccupiedTile.GetAbsoluteDistance(targetTiles[0]);
        character.MoveToTile(targetTiles[0].HexTile);
        character.Controller.OccupiedTile = targetTiles[0];

        targetTiles[0].OccupantCharacter = character.Controller;

        PlaySoundEffect();
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        if (targetTiles.Count > 1)
        {
            DamageLastTile(targetTiles);
        }
    }

    private void DamageLastTile(List<IHexTileController> targetTiles)
    {
        if (!targetTiles[1].OccupantCharacter.IsAlly(character.Controller))
        {
            actionHandler.Damage(6.0f * distanceTravelled, targetTiles[1].OccupantCharacter);
            PlayAnimation(targetTiles[1]);
        }
    }
}
