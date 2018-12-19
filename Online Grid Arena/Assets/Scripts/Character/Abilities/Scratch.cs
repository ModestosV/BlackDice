using UnityEngine;

public sealed class Scratch : AbstractAbility
{
    public Scratch()
    {
        AbilityType = AbilityType.TARGET_ENEMY;

        power = 25;
        range = 1;
        cooldown = 1;
        cooldownRemaining = cooldown;
        
        abilityAnimationPrefab = Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation");
        soundEffect = Resources.Load<AudioClip>("Audio/Ability/CottonRip");
    }

    public override void Execute(IHexTileController targetTile)
    {
        targetTile.Damage(power);
        PlaySoundEffect();
        PlayAnimation(targetTile);
        cooldownRemaining += cooldown;
    }
}
