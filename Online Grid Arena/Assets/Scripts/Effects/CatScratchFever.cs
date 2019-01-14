using System.Collections.Generic;
using UnityEngine;

public sealed class CatScratchFever : StackModifier
{
    public CatScratchFever(Sprite icon) : base(
        EffectType.STACK,
        6,
        5,
        new Dictionary<string, float>()
        {
            {"0", 5.0f }
        }
        )
    {
        Name = "CatScratchFever";
        EffectIcon = icon;
    }

    public override Dictionary<string, float> GetEffects()
    {
        return StatModifier;
    }

    public override bool IsMaxStacks()
    {
        return Stacks == MaxStacks;
    }
}