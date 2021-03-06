﻿using System.Collections.Generic;
using UnityEngine;

public sealed class HuddleEffect : StackModifier
{
    public HuddleEffect(float amount) : base(
        EffectType.STACK,
        3,
        1,
        new Dictionary<string, float>()
        {
            {"defense", amount },
        }
        )
    {
        Name = "Huddle";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/huddle");
        Description = "Increases Defense by 20";
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