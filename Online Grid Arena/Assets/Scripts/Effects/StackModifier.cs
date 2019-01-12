using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class StackModifier : AbstractEffect
{
    protected int maxStacks;
    public int stacks;
    public Dictionary<string, float> statModifier;

    protected StackModifier(EffectType type, int duration, int maxStacks, Dictionary<string, float> statModifier) : base(type, duration)
    {
        this.Type = type;
        this.maxStacks = maxStacks;
        this.statModifier = statModifier;
        this.duration = duration;
        this.stacks = 1;
        Name = "stack modifier";
    }

    public override void RemoveStack()
    {
        stacks--;
    }

    public override bool StacksRanOut()
    {
        return stacks <= 0;
    }

    public override void Decrement()
    {
        durationRemaining--;
        if (HasRunOut())
        {
            RemoveStack();
        }
    }

    public override void Refresh()
    {
        if (stacks < maxStacks)
        {
            stacks++;
            durationRemaining = duration;
        }
        else
        {
            durationRemaining = duration;
        }
    }
}
