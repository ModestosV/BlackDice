﻿using UnityEngine;

public sealed class BasicHealAbility : Ability
{
    public BasicHealAbility(float power, int range, int cooldown, GameObject abilityAnimationPrefab, AudioClip abilitySound)
    {
        Type = AbilityType.TARGET_ALLY;

        this.power = power;
        this.range = range;
        this.cooldown = cooldown;

        this.abilityAnimationPrefab = abilityAnimationPrefab;
        this.abilitySound = abilitySound;

        cooldownRemaining = cooldown;
    }

    public override void Execute(IHexTileController targetTile)
    {
        ICharacterController targetCharacter = targetTile.OccupantCharacter;

        targetCharacter.Heal(power);

        if (abilityAnimationPrefab != null)
            targetCharacter.InstantiateAbilityAnimation(abilityAnimationPrefab);

        if (abilitySound != null)
            targetCharacter.PlayAbilitySound(abilitySound);

        cooldownRemaining += cooldown;
    }
}
