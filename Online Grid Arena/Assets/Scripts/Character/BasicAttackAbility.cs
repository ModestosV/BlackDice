using System.Collections.Generic;
using UnityEngine;

public sealed class BasicAttackAbility : Ability
{
    public BasicAttackAbility(float power, float range) : this(power, range, null, null)
    {

    }

    public BasicAttackAbility(float power, float range, GameObject abilityAnimationPrefab, AudioClip abilitySound)
    {
        Type = AbilityType.TARGET_ENEMY;
        Values = new Dictionary<string, float>() {
            {"power", power },
            {"range", range }
        };
        AbilityAnimationPrefab = abilityAnimationPrefab;
        AbilitySound = abilitySound;
    }

    public override void Execute(IHexTileController targetTile)
    {
        ICharacterController targetCharacter = targetTile.OccupantCharacter;
        float damageToDeal = Values["power"];
        targetCharacter.Damage(damageToDeal);
        if (AbilityAnimationPrefab != null)
            targetCharacter.InstantiateAbilityAnimation(AbilityAnimationPrefab);
        if (AbilitySound != null)
        {
            targetCharacter.PlayAbilitySound(AbilitySound);
        }
            
    }
}
