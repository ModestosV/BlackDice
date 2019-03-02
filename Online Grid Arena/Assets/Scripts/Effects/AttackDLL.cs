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
            {"attack", 10 },
        }
        )
    {
        Name = "Attack DLL";
        EffectIcon = Resources.Load<Sprite>("Resources/Sprites/cursorSword_gold");
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