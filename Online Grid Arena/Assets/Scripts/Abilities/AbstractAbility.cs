using UnityEngine;

public enum AbilityType
{
    TARGET_ENEMY,
    TARGET_ALLY,
    ACTIVATED,
    PASSIVE,
    TRIGGERED
}

public abstract class AbstractAbility : IAbility
{
    public AbilityType Type { get; set; }
    protected int cooldown;
    protected int cooldownRemaining;

    protected AbstractAbility(AbilityType type, int cooldown)
    {
        Type = type;
        this.cooldown = cooldown;
        cooldownRemaining = cooldown;
    }

    public abstract void Execute(IHexTileController targetTile);
    
    public bool IsOnCooldown()
    {
        return cooldownRemaining > 0;
    }

    public void Refresh()
    {
        cooldownRemaining = Mathf.Clamp(cooldownRemaining - 1, 0, int.MaxValue);
    }

}