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
    protected int Cooldown { get; set; }

    public abstract void Execute(IHexTileController targetTile);

    public bool IsInRange(int range)
    {
        return Values["range"] >= range;
    }

    public bool IsOnCooldown()
    {
        return Cooldown > 0;
    }

    public void Refresh()
    {
        Cooldown = Mathf.Clamp(Cooldown - 1, 0, int.MaxValue);
    }
}
