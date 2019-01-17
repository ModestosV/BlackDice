﻿using System.Collections.Generic;
using UnityEngine;

public sealed class CatScratchFever : StackModifier
{
    public CatScratchFever() : base(
        EffectType.STACK,
        6,
        5,
        new Dictionary<string, float>()
        {
            {"attack", 5.0f },
        }
        )
    {
        Name = "CatScratchFever";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/claw-marks");
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