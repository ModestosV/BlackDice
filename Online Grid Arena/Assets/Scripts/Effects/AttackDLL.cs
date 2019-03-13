using System.Collections.Generic;
using UnityEngine;

public sealed class AttackDLL : StackModifier
{
    public AttackDLL() : base(
        EffectType.STACK,
        100,
        100,
        new Dictionary<string, float>()
        {
            {"attack", 5 },
        }
        )
    {
        Name = "Attack DLL";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/attackDLL");
        Description = "Increases Attack by 5";
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