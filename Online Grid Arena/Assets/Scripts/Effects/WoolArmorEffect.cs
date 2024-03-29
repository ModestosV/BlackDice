﻿using System.Collections.Generic;
using UnityEngine;

public sealed class WoolArmorEffect : StackModifier, IWoolArmorEffect
{
    public WoolArmorEffect() : base(
        EffectType.STACK,
        5,
        10,
        new Dictionary<string, float>()
        {
            {"defense", 5.0f },
        }
        )
    {
        Name = "Wool Armor";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/wool");
        Description = "Increases Defense by 5 per stack";
    }

    public override Dictionary<string, float> GetEffects()
    {
        return StatModifier;
    }

    public override bool IsMaxStacks()
    {
        return Stacks == MaxStacks;
    }

    public int GetHalfOfStacks()
    {
        return Stacks/2;
    }
}