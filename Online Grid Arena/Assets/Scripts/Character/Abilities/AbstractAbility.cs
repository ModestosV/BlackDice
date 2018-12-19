using UnityEngine;

public enum AbilityType
{
    TARGET_ENEMY,
    TARGET_ALLY
}

public abstract class AbstractAbility : IAbility
{
    public AbilityType AbilityType { get; set; }
    protected float power;
    protected int range;
    protected int cooldown;
    protected int cooldownRemaining;
    protected GameObject abilityAnimationPrefab;
    protected AudioClip soundEffect;

    public abstract void Execute(IHexTileController targetTile);

    public bool IsInRange(int range)
    {
        return this.range >= range;
    }

    public bool IsOnCooldown()
    {
        return cooldownRemaining > 0;
    }

    public void Refresh()
    {
        cooldownRemaining = Mathf.Clamp(cooldownRemaining - 1, 0, int.MaxValue);
    }

    protected void PlaySoundEffect()
    {
        if (soundEffect != null)
            EventBus.Publish(new AbilitySoundEvent(soundEffect));
    }

    protected void PlayAnimation(IHexTileController targetTile)
    {
        if (abilityAnimationPrefab != null)
            targetTile.PlayAbilityAnimation(abilityAnimationPrefab);
    }
}