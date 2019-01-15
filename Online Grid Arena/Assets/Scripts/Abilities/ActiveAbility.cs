using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : AbstractAbility
{
    protected int cooldown;
    protected int cooldownRemaining;
    protected int range;
    protected GameObject animationPrefab;
    protected AudioClip soundEffect;

    protected ActiveAbility(Sprite abilityIcon, GameObject animationPrefab, AudioClip soundEffect, ICharacter character, int cooldown, int range) : base(abilityIcon, character)
    {
        this.cooldown = cooldown;
        this.range = range;
        this.animationPrefab = animationPrefab;
        this.soundEffect = soundEffect;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {

    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {

    }

    public bool IsInRange(int range)
    {
        return this.range >= range;
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
        if (animationPrefab != null)
            targetTile.PlayAbilityAnimation(animationPrefab);
    }
}
