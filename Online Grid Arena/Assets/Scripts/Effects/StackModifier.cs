﻿using System.Collections.Generic;

public abstract class StackModifier : AbstractEffect
{
    protected int MaxStacks { get; }
    public int Stacks { get; set; }
    protected Dictionary<string, float> StatModifier { get; }

    protected StackModifier(EffectType type, int duration, int maxStacks, Dictionary<string, float> statModifier) : base(type, duration)
    {
        this.Type = type;
        this.MaxStacks = maxStacks;
        this.StatModifier = statModifier;
        this.duration = duration;
        this.Stacks = 1;
        Name = "stack modifier";
    }

    public override void DecrementStack()
    {
        Stacks--;
    }

    public override bool StacksRanOut()
    {
        return Stacks <= 0;
    }

    public override void DecrementDuration()
    {
        DurationRemaining--;
        if (DurationRemaining < 0)
        {
            DurationRemaining = 0;
        }
    }

    public override void Refresh()
    {
        if (Stacks < MaxStacks)
        {
            Stacks++;
        }
        DurationRemaining = duration;
    }
}
