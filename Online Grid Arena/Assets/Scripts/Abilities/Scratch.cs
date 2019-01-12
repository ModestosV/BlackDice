using UnityEngine;

public sealed class Scratch : TargetedAbility
{
    public Scratch() : base(
        AbilityType.TARGET_ENEMY,
        1,
        25.0f,
        1,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip"),
        Resources.Load<Sprite>("Sprites/cursorSword_gold")
        )
    {

    }

    public override void Execute(IHexTileController targetTile)
    {
        targetTile.Damage(power);
        PlaySoundEffect();
        PlayAnimation(targetTile);
        cooldownRemaining += cooldown;
    }
}
