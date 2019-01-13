using UnityEngine;

public sealed class DefaultHeal : TargetedAbility
{
    public DefaultHeal(ICharacter activeCharacter) : base(
        AbilityType.TARGET_ALLY,
        1,
        35.0f,
        5,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultHealAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/SmallSplash"),
        Resources.Load<Sprite>("Sprites/Heal_Icon"),
        activeCharacter
        )
    {

    }

    public override void Execute(IHexTileController targetTile)
    {
        targetTile.Heal(power);
        PlaySoundEffect();
        PlayAnimation(targetTile);
        cooldownRemaining += cooldown;
    }
}
