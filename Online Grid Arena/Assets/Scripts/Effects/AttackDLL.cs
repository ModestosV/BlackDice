﻿using System.Collections.Generic;
using UnityEngine;

public sealed class AttackDLL : StackModifier
{
    public AttackDLL() : base(
        EffectType.STACK,
        100,
        5,
        new Dictionary<string, float>()
        {
            {"attack", 10 },
        }
        )
    {
        Name = "Attack DLL";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/attackDLL");
        Description = "Increases Attack by 10";
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