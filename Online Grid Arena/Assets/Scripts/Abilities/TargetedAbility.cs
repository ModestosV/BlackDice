using UnityEngine;
using UnityEngine.UI;

public abstract class TargetedAbility : AbstractAbility
{
    protected float power;
    protected int range;
    protected GameObject animationPrefab;
    protected AudioClip soundEffect;

    protected readonly ICharacter activeCharacter;

    protected TargetedAbility(AbilityType type, int cooldown, float power, int range, GameObject animationPrefab, AudioClip soundEffect, Sprite abilityIcon, ICharacter activeCharacter) : base(type, cooldown, abilityIcon)
    {
        this.power = power;
        this.range = range;
        this.animationPrefab = animationPrefab;
        this.soundEffect = soundEffect;
        this.activeCharacter = activeCharacter;
    }

    public bool IsInRange(int range)
    {
        return this.range >= range;
    }

    protected void PlaySoundEffect()
    {
        if (soundEffect != null)
            EventBus.Publish(new AbilitySoundEvent(soundEffect));
    }

    protected void PlayAnimation(IHexTileController targetTile)
    {
        if (animationPrefab != null)
            targetTile.PlayAbilityAnimation(animationPrefab);
    }

    protected void ExecuteMove(IHexTileController targetTile)
    {
        activeCharacter.Controller.OccupiedTile.OccupantCharacter = null;
        activeCharacter.Controller.OccupiedTile.Deselect();

        // Movement animation

        activeCharacter.MoveToTile(targetTile.HexTile);
        activeCharacter.Controller.OccupiedTile = targetTile;

        targetTile.OccupantCharacter = activeCharacter.Controller;
        targetTile.Select();
    }
}
