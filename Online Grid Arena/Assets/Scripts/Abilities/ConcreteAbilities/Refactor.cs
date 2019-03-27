using System.Collections.Generic;
using UnityEngine;

public sealed class Refactor : AbstractTargetedAbility
{
    public Refactor(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/Refactor"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/eaglesound"),
        activeCharacter,
        4,
        3,
        AbilityType.TARGET_ENEMY,
        "Refactor - Special Attack \nTA Eagle scrambles up an enemy in a 3 tile range causing them to be stunned for 1 turn.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Refactor. Primary action being called.");
        targetTiles[0].OccupantCharacter.StatusEffectState = StatusEffectState.STUNNED;
        EventBus.Publish(new StatusEffectEvent("stun", true, targetTiles[0].OccupantCharacter));
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
