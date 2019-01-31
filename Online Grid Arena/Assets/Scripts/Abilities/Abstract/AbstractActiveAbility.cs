using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractActiveAbility : AbstractAbility, IActiveAbility
{

    protected readonly AudioClip soundEffect;
    protected readonly GameObject animationPrefab;

    protected readonly int cooldown;
    protected int cooldownRemaining;

    public int CooldownRemaining { get { return cooldownRemaining; } }

    protected AbstractActiveAbility(
        Sprite abilityIcon,
        GameObject animationPrefab,
        AudioClip soundEffect,
        ICharacter character,
        int cooldown,
        string description) : base(abilityIcon, character, description)
    {
        this.cooldown = cooldown;
        this.animationPrefab = animationPrefab;
        this.soundEffect = soundEffect;
        cooldownRemaining = 0;
    }

    public override void Execute(List<IHexTileController> targetTiles)
    {
        PrimaryAction(targetTiles);

        SecondaryAction(targetTiles);

        EventBus.Publish(new SelectTileEvent(character.Controller.OccupiedTile));

        cooldownRemaining = cooldown;

    }

    protected override abstract void PrimaryAction(List<IHexTileController> targetTiles);

    protected void PlaySoundEffect()
    {
        if (soundEffect != null)
        {
            EventBus.Publish(new AbilitySoundEvent(soundEffect));
        }
    }

    protected void PlayAnimation(IHexTileController targetTile)
    {
        if (animationPrefab != null)
        {
            targetTile.PlayAbilityAnimation(animationPrefab);
        }
    }

    public bool IsOnCooldown()
    {
        return CooldownRemaining > 0;
    }

    public void UpdateCooldown()
    {
        if (CooldownRemaining > 0)
        {
            cooldownRemaining--;
        }
    }
}
