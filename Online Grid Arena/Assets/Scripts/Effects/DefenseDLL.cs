using System.Collections.Generic;
using UnityEngine;

public sealed class DefenseDLL : StackModifier
{
    public DefenseDLL() : base(
        EffectType.STACK,
        100,
        100,
        new Dictionary<string, float>()
        {
            {"defense", 10 },
        }
        )
    {
        Name = "Defense DLL";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/defenseDLL.png");
        Description = "Increases Defense by 10";
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