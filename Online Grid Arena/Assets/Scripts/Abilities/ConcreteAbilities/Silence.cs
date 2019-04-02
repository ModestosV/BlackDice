using UnityEngine;
using System.Collections.Generic;

public class Silence : AbstractTargetedAbility
{
    public Silence(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/silence"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlideHitAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/owlSh"),
        activeCharacter,
        7,
        100,
        AbilityType.TARGET_CHARACTER_LINE,
        "Silence - Special Ability \nProfessor Rig-Bee Silences an enemy in a straight line for 1 turn. Deals 50% of his attack stat.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Silence. Primary action being called.");
        PlaySoundEffect();
        if (targetTiles.Count > 1)
        {
            targetTiles[1].OccupantCharacter.StatusEffectState = StatusEffectState.SILENCED;
            EventBus.Publish(new StatusEffectEvent("silence", true, targetTiles[1].OccupantCharacter));
            PlayAnimation(targetTiles[1]);
        }
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Silence. Secondary action being called.");

        if (targetTiles.Count > 1)
        {
            actionHandler.Damage(character.Controller.CharacterStats["attack"].Value*0.5f, targetTiles[1].OccupantCharacter);
            PlayAnimation(targetTiles[1]);
        }
    }
}
