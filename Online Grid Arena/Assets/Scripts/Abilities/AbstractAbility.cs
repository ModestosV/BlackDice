using UnityEngine;
using UnityEngine.UI;

public enum AbilityType
{
    TARGET_ENEMY,
    TARGET_ALLY,
    TARGET_TILE,
    ACTIVATED,
    PASSIVE,
    TRIGGERED
}

public abstract class AbstractAbility : IAbility
{
    public AbilityType Type { get; set; }
    public Sprite AbilityIcon { get; set; }
    protected int cooldown;
    protected int cooldownRemaining;

    protected AbstractAbility(AbilityType type, int cooldown, Sprite abilityIcon)
    {
        Type = type;
        this.cooldown = cooldown;
        cooldownRemaining = cooldown;
        AbilityIcon = abilityIcon;
    }

    public abstract void Execute(IHexTileController targetTile);
    public abstract void ModifyPower(float amount);

    protected abstract void PrimaryAction(IHexTileController targetTile);

    public void Execute(IHexTileController targetTile)
    {
        PrimaryAction(targetTile);

        cooldownRemaining += cooldown;
    }
    
    public bool IsOnCooldown()
    {
        return cooldownRemaining > 0;
    }

    public void Refresh()
    {
        cooldownRemaining = Mathf.Clamp(cooldownRemaining - 1, 0, int.MaxValue);
    }

}