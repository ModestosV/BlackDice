﻿using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractActiveAbility : AbstractAbility, IActiveAbility
{
    public int Cooldown { get; protected set; }
    public int cooldownRemaining { get; protected set; }
    protected readonly AudioClip soundEffect;
    protected readonly GameObject animationPrefab;

    protected AbstractActiveAbility(
        Sprite abilityIcon,
        GameObject animationPrefab,
        AudioClip soundEffect,
        ICharacter character,
        int cooldown,
        string description) : base(abilityIcon, character, description)
    {
        this.Cooldown = cooldown;
        this.animationPrefab = animationPrefab;
        this.soundEffect = soundEffect;
        cooldownRemaining = 0;
    }

    public override void Execute(List<IHexTileController> targetTiles)
    {
        PrimaryAction(targetTiles);

        SecondaryAction(targetTiles);

        cooldownRemaining = Cooldown;
    }

    protected override abstract void PrimaryAction(List<IHexTileController> targetTiles);

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

    public bool IsOnCooldown()
    {
        return cooldownRemaining > 0;
    }

    public void UpdateCooldown()
    {
        if (cooldownRemaining > 0)
        {
            cooldownRemaining--;
        }
    }
}
