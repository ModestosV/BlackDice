using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : AbstractTargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public Kamikaze(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/rocketcat_kamikaze"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/rocket"),
        activeCharacter,
        10,
        100,
        AbilityType.TARGET_LINE_AOE,
        "Ultimate Ability \nRocket Cat flies in a straight line and deals 250% her attack in an AOE. Damages allies and herself.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        character.Controller.OccupiedTile.OccupantCharacter = null;
        EventBus.Publish(new DeselectSelectedTileEvent());
        character.MoveToTile(targetTiles[0].HexTile);
        character.Controller.OccupiedTile = targetTiles[0];

        targetTiles[0].OccupantCharacter = character.Controller;
        EventBus.Publish(new SelectTileEvent(targetTiles[0]));
        PlaySoundEffect();
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        foreach (IHexTileController neighbor in targetTiles[0].GetNeighbors())
        {
            neighbor.Damage(2.50f * character.Controller.CharacterStats["attack"].CurrentValue);
            PlayAnimation(neighbor);
        }
        targetTiles[0].Damage(2.50f * character.Controller.CharacterStats["attack"].CurrentValue);
    }
}