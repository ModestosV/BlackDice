using UnityEngine;
using System.Collections.Generic;
using System;

public sealed class Sting : AbstractTargetedAbility
{
    public Sting(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/sting"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/StingAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/bee"),
        activeCharacter,
        1,
        5,
        AbilityType.TARGET_ENEMY,
        "Sting - Basic Attack \nProfessor Rig-Bee stings an enemy from up to 5 tiles away, dealing his attack stat in damage to them. Reducing the current cooldown of all his other abilities by 1.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Sting. primary action being called.");
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Sting. secondary action being called, reducing cooldown of other abilities.");
        foreach (IAbility ability in character.Controller.Abilities)
        {
            try
            {
                IActiveAbility active = (IActiveAbility)ability;
                if (active.IsOnCooldown())
                {
                    active.UpdateCooldown();
                }
            }
            catch(InvalidCastException)
            {

            }
        }
    }
}
