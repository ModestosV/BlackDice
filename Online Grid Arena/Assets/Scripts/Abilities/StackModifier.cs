using UnityEngine;

public abstract class StackModifier : AbstractEffect
{
    protected float buffPerStack;
    protected int maxStacks;

    protected StackModifier(EffectType type, int duration, int buffPerStack, int maxStacks) : base(type, duration)
    {
        this.Type = type;
        this.buffPerStack = buffPerStack;
        this.maxStacks = maxStacks;
    }
}
