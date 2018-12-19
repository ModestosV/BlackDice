using UnityEngine;

public sealed class DefaultHeal : AbstractAbility
{
    public DefaultHeal()
    {
        AbilityType = AbilityType.TARGET_ALLY;

        power = 35;
        range = 5;
        cooldown = 1;
        cooldownRemaining = cooldown;

        abilityAnimationPrefab = Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultHealAnimation");
        soundEffect = Resources.Load<AudioClip>("Audio/Ability/SmallSplash");
    }

    public override void Execute(IHexTileController targetTile)
    {
        targetTile.Heal(power);
        PlaySoundEffect();
        PlayAnimation(targetTile);
        cooldownRemaining += cooldown;
    }
}
