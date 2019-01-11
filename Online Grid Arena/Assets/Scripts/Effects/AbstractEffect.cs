using System.Collections.Generic;

public enum EffectType //stack, buff, and debuff, are all constant. the rest are only applied at the end of the turn
{
    BUFF,
    DEBUFF,
    DAMAGE_OVER_TIME,
    HEAL_OVER_TIME,
    STACK,
    STACK_BUFF,
    STACK_DAMAGE,
    STACK_HEAL
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

    public bool HasRunOut()
    {
        return durationRemaining > 0;
    }

    public abstract void Apply(IHexTileController targetTile);
    public abstract Dictionary<string, float> GetEffects();
    public abstract void RemoveStack();
    public abstract bool StacksRanOut();

    public virtual void Refresh()
    {
        durationRemaining = duration;
    }

    public virtual void Decrement()
    {
        durationRemaining--;
    }
}