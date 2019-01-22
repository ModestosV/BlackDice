using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    TARGET_ENEMY,
    TARGET_ALLY,
    TARGET_TILE,
    INVALID,
    TARGET_LINE
}

public abstract class AbstractTargetedAbility : AbstractActiveAbility, ITargetedAbility
{
    public AbilityType Type { get; set; }

    protected int range;

    protected AbstractTargetedAbility(
        Sprite abilityIcon,
        GameObject animationPrefab,
        AudioClip soundEffect,
        ICharacter character,
        int cooldown,
        int range,
        AbilityType type) : base(abilityIcon, animationPrefab, soundEffect, character, cooldown)
    {
        this.range = range;
        Type = type;
    }

    protected override abstract void PrimaryAction(List<IHexTileController> targetTiles);

    public bool IsInRange(int range)
    {
        return this.range >= range;
    }
}