using System.Collections.Generic;
using UnityEngine;

public sealed class ViscousSaliva : StackModifier
{
    public ViscousSaliva() : base(
        EffectType.STACK,
        1,
        1,
        new Dictionary<string, float>()
        {
            {"moves", -2.0f }, //does not work...
        }
        )
    {
        Name = "Viscous Saliva";
        EffectIcon = Resources.Load<Sprite>("Sprites/Abilities/viscousSaliva"); 
        Description = "Reduces move speed by 2";
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