using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : AbstractAbility, IActiveAbility
{
    public int Cooldown { get; }
    public int CooldownRemaining => cooldownRemaining;

    private readonly AudioClip soundEffect;
    private readonly GameObject animationPrefab;
    private int cooldownRemaining;
    
    protected ActiveAbility(
        Sprite abilityIcon,
        GameObject animationPrefab,
        AudioClip soundEffect,
        ICharacter character,
        int cooldown,
        string description) : base(abilityIcon, character, description)
    {
        Cooldown = cooldown;
        this.animationPrefab = animationPrefab;
        this.soundEffect = soundEffect;
        cooldownRemaining = 0;
    }

    public override void Execute(List<IHexTileController> targetTiles)
    {
        Debug.Log(ToString() + " Ability has been executed");

        PrimaryAction(targetTiles);

        SecondaryAction(targetTiles);
        cooldownRemaining = Cooldown;
        EventBus.Publish(new AbilityUsedEvent());
        EventBus.Publish(new ActiveCharacterEvent());
    }

    protected abstract override void PrimaryAction(List<IHexTileController> targetTiles);

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
