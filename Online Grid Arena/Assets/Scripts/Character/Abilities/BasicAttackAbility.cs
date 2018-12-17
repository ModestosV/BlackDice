using UnityEngine;

public sealed class BasicAttackAbility : AbstractAbility
{
    public BasicAttackAbility(float power, int range, int cooldown, GameObject abilityAnimationPrefab, AudioClip abilitySound)
    {
        AbilityType = AbilityType.TARGET_ENEMY;

        this.power = power;
        this.range = range;
        this.cooldown = cooldown;

        this.abilityAnimationPrefab = abilityAnimationPrefab;
        this.soundEffect = abilitySound;

        cooldownRemaining = cooldown;
    }

    public override void Execute(IHexTileController targetTile)
    {
        ICharacterController targetCharacter = targetTile.OccupantCharacter;

        targetCharacter.Damage(power);

        PlaySoundEffect();

        PlayAnimation(targetTile);

        cooldownRemaining += cooldown;
    }
}