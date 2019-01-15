using UnityEngine;
using System.Collections.Generic;

public sealed class DefaultAttack : ActiveAbility
{
    public DefaultAttack(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        1,
        1)
    {

    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].Damage(25);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}