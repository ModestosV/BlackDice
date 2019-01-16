﻿using System.Collections.Generic;
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
        cooldownRemaining = 0;
    }

    public override void Execute(List<IHexTileController> targetTiles)
    {
        PrimaryAction(targetTiles);

        SecondaryAction(targetTiles);

        cooldownRemaining = cooldown;
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
        if (cooldownRemaining > 0) cooldownRemaining--;
    }
}