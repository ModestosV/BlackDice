using UnityEngine;

public sealed class DefaultAttack : TargetedAbility
{
    public DefaultAttack(ICharacter activeCharacter) : base(
        AbilityType.TARGET_ENEMY,
        1,
        20.0f,
        5,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        activeCharacter
        )
    {

    }
}