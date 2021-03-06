﻿using System.Collections.Generic;
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
    protected string Name { private get; set; }
    public string Description { get; protected set; }
    public int DurationRemaining { get; protected set; }
    protected int duration;

    protected AbstractEffect(EffectType type, int duration)
    {
        Type = type;
        this.duration = duration;
        DurationRemaining = duration;
        Name = "abstract effect";
        Description = "Default ability description. If you're seeing this, somebody didn't do their job right";
    }

    public abstract bool IsMaxStacks();

    public bool IsDurationOver()
    {
        return DurationRemaining <= 0;
    }

    public abstract Dictionary<string, float> GetEffects();
    public abstract void DecrementStack();
    public abstract bool StacksRanOut();
    public virtual void Refresh()
    {
        DurationRemaining = duration;
    }

    public virtual void DecrementDuration()
    {
        DurationRemaining--;
    }

    public virtual string GetName()
    {
        return Name;
    }
}