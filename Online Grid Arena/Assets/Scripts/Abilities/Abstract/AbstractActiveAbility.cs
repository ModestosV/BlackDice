using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractActiveAbility : AbstractAbility, IActiveAbility
{
    protected int cooldown;
    protected int cooldownRemaining;
    protected GameObject animationPrefab;
    protected AudioClip soundEffect;

    protected AbstractActiveAbility(Sprite abilityIcon, GameObject animationPrefab, AudioClip soundEffect, ICharacter character, int cooldown) : base(abilityIcon, character)
    {
        this.cooldown = cooldown;
        this.animationPrefab = animationPrefab;
        this.soundEffect = soundEffect;
    }

    protected override abstract void PrimaryAction(List<IHexTileController> targetTiles);
    
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
        if (animationPrefab != null)
            targetTile.PlayAbilityAnimation(animationPrefab);
    }

    // TODO: Fix this and possible remove Refresh(). Each Action should reset cooldown and then each turn should decrement counter
    public bool IsOnCooldown()
    {
        return false;
    }
}
