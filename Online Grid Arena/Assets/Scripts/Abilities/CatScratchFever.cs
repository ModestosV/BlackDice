using UnityEngine;

public sealed class CatScratchFever : StackModifier
{
    public CatScratchFever() : base(
        EffectType.STACK,
        3,
        10,
        5,
        "moves"
        )
    {

    }

    public override void Apply(IHexTileController targetTile)
    {
        durationRemaining = duration;
        targetTile.ApplyEffect(this);
    }
}