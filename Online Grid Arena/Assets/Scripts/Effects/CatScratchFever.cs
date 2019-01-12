using System.Collections.Generic;

public sealed class CatScratchFever : StackModifier
{
    public CatScratchFever() : base(
        EffectType.STACK,
        3,
        5,
        new Dictionary<string, float>()
        {
            {"moves", 1.0f}, {"0", 5.0f }
        }
        )
    {
        Name = "CatScratchFever";
    }

    public override void Apply(IHexTileController targetTile)
    {
        durationRemaining = duration;
        targetTile.ApplyEffect(this);
    }

    public override Dictionary<string, float> GetEffects()
    {
        return statModifier;
    }

    public override string Print()
    {
        return "catscratchfever: "+this.GetName()+"------ duration: "+this.duration+" time left:"+this.durationRemaining+" maxstacks: "+this.maxStacks+" currentStacks: "+this.stacks+" type:"+this.Type.ToString();
    }
}