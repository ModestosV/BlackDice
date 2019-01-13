using System.Collections.Generic;

public enum EffectType //stack, buff, and debuff, are all constant. the rest are only applied at the end of the turn
{
    CONSTANT,
    END_OF_TURN,
    START_OF_TURN,
    STACK
}

public abstract class AbstractEffect : IEffect
{
    public EffectType Type { get; set; }
    public string Name { get; set; }
    protected int duration;
    protected int durationRemaining;

    protected AbstractEffect(EffectType type, int duration)
    {
        Type = type;
        this.duration = duration;
        durationRemaining = duration;
        Name = "abstract effect";
    }

    public abstract bool MaxStacks();

    public bool HasRunOut()
    {
        return durationRemaining <= 0;
    }

    public abstract Dictionary<string, float> GetEffects();
    public abstract void DecrementStack();
    public abstract bool StacksRanOut();
    public abstract string Print();
    public abstract void Reset();
    public virtual void Refresh()
    {
        durationRemaining = duration;
    }

    public virtual void DecrementDuration()
    {
        durationRemaining--;
    }

    public virtual string GetName()
    {
        return Name;
    }
}