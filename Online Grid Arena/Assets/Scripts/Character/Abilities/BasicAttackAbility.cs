using UnityEngine;

public sealed class BasicAttackAbility : AbstractAbility
{
    public BasicAttackAbility(float power, int range, int cooldown, GameObject abilityAnimationPrefab, AudioClip abilitySound)
    {
        Type = AbilityType.TARGET_ENEMY;

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

        targetCharacter.Damage(power);

        if (abilityAnimationPrefab != null)
            targetCharacter.InstantiateAbilityAnimation(abilityAnimationPrefab);

        if (abilitySound != null)
            targetCharacter.PlayAbilitySound(abilitySound);

        cooldownRemaining += cooldown;
    }
}