using UnityEngine;

public enum EffectType
{
    BUFF,
    DEBUFF,
    DAMAGE_OVER_TIME,
    HEAL_OVER_TIME
}

public abstract class AbstractEffect : IEffect
{
    public EffectType Type { get; set; }
    protected int duration;
    protected int durationRemaining;

    protected AbstractEffect(EffectType type, int duration)
    {
        Type = type;
        this.duration = duration;
        durationRemaining = duration;
    }

    public abstract void Execute(IHexTileController targetTile);

    public bool HasRunOut()
    {
        return durationRemaining > 0;
    }

    public abstract void Apply(IHexTileController targetTile);
}