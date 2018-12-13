using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    TARGET_ENEMY,
    TARGET_ALLY
}

public abstract class Ability : IAbility
{
    public AbilityType Type { get; set; }
    protected Dictionary<string, float> Values { get; set; }
    protected GameObject AbilityAnimationPrefab { get; set; }
    protected AudioClip AbilitySound { get; set; }

    public abstract void Execute(IHexTileController targetTile);
    public bool IsInRange(int range)
    {
        return Values["range"] >= range;
    }
}
