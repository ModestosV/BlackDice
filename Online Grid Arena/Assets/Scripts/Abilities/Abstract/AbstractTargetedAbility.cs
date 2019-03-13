using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    TARGET_ENEMY,
    TARGET_ALLY,
    TARGET_TILE,
    INVALID,
    TARGET_LINE,
    TARGET_LINE_AOE,
    TARGET_TILE_AOE
}

public abstract class AbstractTargetedAbility : AbstractActiveAbility, ITargetedAbility
{
    public AbilityType Type { get; }

    protected readonly int range;

    protected AbstractTargetedAbility(
        Sprite abilityIcon,
        GameObject animationPrefab,
        AudioClip soundEffect,
        ICharacter character,
        int cooldown,
        int range,
        AbilityType type,
        string description,
        bool usedAbility) : base(abilityIcon, animationPrefab, soundEffect, character, cooldown, description, usedAbility)
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