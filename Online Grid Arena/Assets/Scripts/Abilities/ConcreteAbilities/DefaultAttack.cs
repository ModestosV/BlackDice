using UnityEngine;
using System.Collections.Generic;

public sealed class DefaultAttack : AbstractTargetedAbility
{
    public DefaultAttack(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY)
    { 
        Description = "This is the default attack. It is used to test attacking.";
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].Damage(character.Controller.CharacterStats["attack"].Value);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
