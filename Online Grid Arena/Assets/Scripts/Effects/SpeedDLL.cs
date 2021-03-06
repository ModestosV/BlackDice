﻿using System.Collections.Generic;
using UnityEngine;

public sealed class SpeedDLL : StackModifier
{
    public SpeedDLL() : base(
        EffectType.STACK,
        100,
        100,
        new Dictionary<string, float>()
        {
            {"moves", 1 },
        }
        )
    {
        Name = "Speed DLL";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/speedDLL");
        Description = "Increases Moves by 1 per stack";
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