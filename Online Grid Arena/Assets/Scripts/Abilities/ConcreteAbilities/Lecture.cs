using UnityEngine;
using System.Collections.Generic;

public sealed class Lecture : AbstractTargetedAbility
{
    public Lecture(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/swoopDown"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/owlHootSound"),
        activeCharacter,
        1,
        5,
        AbilityType.TARGET_ENEMY,
        "lecture - Basic Attack \nProfessor Owl lectures an enemy from up to 5 tiles away, dealing his attack stat in damage to them, and reducing the current cooldown of all his other abilities by 1.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Lecture. primary action being called.");
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Lecture. secondary action being called, reducing cooldown of other abilities.");
        foreach (AbstractActiveAbility ability in character.Controller.Abilities)
        {
            if (ability.IsOnCooldown())
            {
                ability.UpdateCooldown();
            }
        }
    }
}
