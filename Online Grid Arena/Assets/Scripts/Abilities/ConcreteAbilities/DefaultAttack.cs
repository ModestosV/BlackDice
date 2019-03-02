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
        AbilityType.TARGET_ENEMY,
        "This is the default attack. It is used to test attacking.",
        false)
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
