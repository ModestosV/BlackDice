using UnityEngine;

public abstract class StackModifier : AbstractEffect
{
    protected float buffPerStack;
    protected int maxStacks;
    protected string statModifier;
    protected int stacks;

    protected StackModifier(EffectType type, int duration, int buffPerStack, int maxStacks, string statModifier) : base(type, duration)
    {
        this.Type = type;
        this.buffPerStack = buffPerStack;
        this.maxStacks = maxStacks;
        this.statModifier = statModifier;
        this.stacks = 1;
    }
}
