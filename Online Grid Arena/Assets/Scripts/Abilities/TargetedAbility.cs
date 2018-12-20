using UnityEngine;

public abstract class TargetedAbility : AbstractAbility
{
    protected float power;
    protected int range;
    protected GameObject animationPrefab;
    protected AudioClip soundEffect;
    
    protected TargetedAbility(AbilityType type, int cooldown, float power, int range, GameObject animationPrefab, AudioClip soundEffect) : base(type, cooldown)
    {
        this.power = power;
        this.range = range;
        this.animationPrefab = animationPrefab;
        this.soundEffect = soundEffect;
    }

    public bool IsInRange(int range)
    {
        return this.range >= range;
    }

    protected void PlaySoundEffect()
    {
        if (soundEffect != null)
            EventBus.Publish(new AbilitySoundEvent(soundEffect));
    }

    protected void PlayAnimation(IHexTileController targetTile)
    {
        if (animationPrefab != null)
            targetTile.PlayAbilityAnimation(animationPrefab);
    }
}
