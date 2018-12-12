using System.Collections.Generic;

public enum AbilityType
{
    TARGET_ENEMY,
    TARGET_ALLY
}

public abstract class Ability : IAbility
{
    public AbilityType Type { get; set; }
    public Dictionary<string, float> Values { get; set; }

    public abstract void Execute(IHexTileController targetTile);
    public bool IsInRange(int range)
    {
        return Values["range"] >= range;
    }
}
