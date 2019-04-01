using UnityEngine;
using System.Collections.Generic;

public class Silence : AbstractTargetedAbility
{
    public Silence(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/slide"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlideHitAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/owlSh"),
        activeCharacter,
        7,
        100,
        AbilityType.TARGET_CHARACTER_LINE,
        "Silence - Special Ability \nProfessor Owl Silences an enemy in a straight line for 1 turn. Deals 50% of his attack stat.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Tongue Pull. Primary action being called.");
        PlaySoundEffect();
        //silence here
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
