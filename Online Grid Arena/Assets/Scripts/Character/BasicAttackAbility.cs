using System.Collections.Generic;
using UnityEngine;

public sealed class BasicAttackAbility : Ability
{
    public BasicAttackAbility(float power, int range) : this(power, range, 0, null, null)
    {

    }

    public BasicAttackAbility(float power, int range, int cooldown, GameObject abilityAnimationPrefab, AudioClip abilitySound)
    {
        Type = AbilityType.TARGET_ENEMY;
        Values = new Dictionary<string, float>() {
            { "power", power },
            { "range", range },
            { "cooldown", cooldown }
        };
        AbilityAnimationPrefab = abilityAnimationPrefab;
        AbilitySound = abilitySound;
        Cooldown = cooldown;
    }

    public override void Execute(IHexTileController targetTile)
    {
        ICharacterController targetCharacter = targetTile.OccupantCharacter;
        float damageToDeal = Values["power"];
        targetCharacter.Damage(damageToDeal);
        if (AbilityAnimationPrefab != null)
            targetCharacter.InstantiateAbilityAnimation(AbilityAnimationPrefab);
        if (AbilitySound != null)
            targetCharacter.PlayAbilitySound(AbilitySound);
        Cooldown += (int)Values["cooldown"];
    }
}
