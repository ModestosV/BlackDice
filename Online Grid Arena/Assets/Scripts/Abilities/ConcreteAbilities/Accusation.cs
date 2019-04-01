using System.Collections.Generic;
using UnityEngine;

public class Accusation : AbstractTargetedAbility
{
    public Accusation(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/importDLL"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/grunt"),
        character,
        3,
        6,
        AbilityType.TARGET_ENEMY,
        "Accusation - Special Ability \nProfessor Owl accuses an enemy of plagiarism, dealing 50% of his attack damage and applying the \"Probation\" effect on them. This effect reduces their defense by 50 for 2 turns.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Accusation. Primary action being called.");
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value * 0.50f, targetTiles[0].OccupantCharacter);
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].OccupantCharacter.ApplyEffect(new ProbationEffect());
    }
}