using System.Collections.Generic;
using UnityEngine;

public sealed class AngryEffect : StackModifier
{
    public AngryEffect() : base(
        EffectType.STACK,
        10,
        10,
        new Dictionary<string, float>()
        {
            {"attack", 20 },
        }
        )
    {
        Name = "Angry";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/angry");
        Description = "Increases Attack by 20 per stack";
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