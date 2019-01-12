using UnityEngine;

public sealed class Scratch : TargetedAbility
{
    public IAbility passive;
    public Scratch(IAbility passiveTrigger) : base(
        AbilityType.TARGET_ENEMY,
        1,
        25.0f,
        1,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip")
        )
    {
        passive = passiveTrigger;
    }

    public override void Execute(IHexTileController targetTile)
    {
        targetTile.Damage(power);
        PlaySoundEffect();
        PlayAnimation(targetTile);
        cooldownRemaining += cooldown;
        Debug.LogWarning("SCRATCH");
        Debug.LogWarning("calling passive now");
        passive.Execute(targetTile);
    }
}
