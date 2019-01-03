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
        //apply the effect 
        durationRemaining = duration; //make the duration refresh
        //make the effect be applied to character. this means give tile a way to pass it to character or smth
        //character will take care of applying the necessary effects? and checkign if stack num is too high
    }

    //the character will take care of removing stacks n shit
}