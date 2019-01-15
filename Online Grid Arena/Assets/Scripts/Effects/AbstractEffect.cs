using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    CONSTANT,
    END_OF_TURN,
    START_OF_TURN,
    STACK
}

public abstract class AbstractEffect : IEffect
{
    public EffectType Type { get; set; }
    public Sprite EffectIcon { get; set; }
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

    public abstract bool IsMaxStacks();

    public bool IsDurationOver()
    {
        return durationRemaining <= 0;
    }

    public abstract Dictionary<string, float> GetEffects();
    public abstract void DecrementStack();
    public abstract bool StacksRanOut();
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

    // TODO: Implement this
    public void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }
}