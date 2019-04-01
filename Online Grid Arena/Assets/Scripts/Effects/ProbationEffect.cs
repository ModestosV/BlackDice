using System.Collections.Generic;
using UnityEngine;

public sealed class ProbationEffect : StackModifier
{
    public ProbationEffect() : base(
        EffectType.STACK,
        3,
        1,
        new Dictionary<string, float>()
        {
            {"defense", -50.0f },
        }
        )
    {
        Name = "Probation";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/defenseDLL");
        Description = "Reduces Defense by 50";
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