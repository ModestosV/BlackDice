using UnityEngine;

public sealed class DefaultAttack : TargetedAbility
{
    public DefaultAttack() : base(
        AbilityType.TARGET_ENEMY,
        1,
        20.0f,
        5,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        Resources.Load<Sprite>("Sprites/cursorSword_gold")
        )
    {

    }

    public override void Execute(IHexTileController targetTile)
    {
        targetTile.Damage(power);
        PlaySoundEffect();
        PlayAnimation(targetTile);
        cooldownRemaining += cooldown;
    }
}