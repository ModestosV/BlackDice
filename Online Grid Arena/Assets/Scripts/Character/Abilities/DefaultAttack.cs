using UnityEngine;

public sealed class DefaultAttack : AbstractAbility
{
    public DefaultAttack()
    {
        AbilityType = AbilityType.TARGET_ENEMY;

        power = 20;
        range = 5;
        cooldown = 1;
        cooldownRemaining = cooldown;

        abilityAnimationPrefab = Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation");
        soundEffect = Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker");
    }

    public override void Execute(IHexTileController targetTile)
    {
        targetTile.Damage(power);
        PlaySoundEffect();
        PlayAnimation(targetTile);
        cooldownRemaining += cooldown;
    }
}