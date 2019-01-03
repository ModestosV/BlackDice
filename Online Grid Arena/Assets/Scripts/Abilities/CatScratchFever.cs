using UnityEngine;

public sealed class CatScratchFever : StackModifier
{
    public CatScratchFever() : base(
        EffectType.BUFF,
        3,
        10,
        5
        )
    {

    }

    public override void Apply(IHexTileController targetTile)
    {
        durationRemaining = duration;
        targetTile.ApplyEffect(this);
    }
}