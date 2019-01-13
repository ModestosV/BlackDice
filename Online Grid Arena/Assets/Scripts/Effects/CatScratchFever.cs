using System.Collections.Generic;

public sealed class CatScratchFever : StackModifier
{
    public CatScratchFever() : base(
        EffectType.STACK,
        6,
        5,
        new Dictionary<string, float>()
        {
            {"0", 5.0f }
        }
        )
    {
        Name = "CatScratchFever";
    }

    public override Dictionary<string, float> GetEffects()
    {
        return statModifier;
    }

    public override string ToString()
    {
        return "catscratchfever: "+this.GetName()+"------ duration: "+this.duration+" time left:"+this.durationRemaining+" maxstacks: "+this.maxStacks+" currentStacks: "+this.stacks+" type:"+this.Type.ToString();
    }

    public override bool IsMaxStacks()
    {
        return stacks == maxStacks;
    }
}