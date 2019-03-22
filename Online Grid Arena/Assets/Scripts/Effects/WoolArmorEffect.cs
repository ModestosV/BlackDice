using System.Collections.Generic;
using UnityEngine;

public sealed class WoolArmorEffect : StackModifier
{
    public WoolArmorEffect() : base(
        EffectType.STACK,
        5,
        10,
        new Dictionary<string, float>()
        {
            {"defense", 10.0f },
        }
        )
    {
        Name = "Wool Armor";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/claw-marks");
        Description = "Increases Sheepadin's defense by 10 per stack";
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