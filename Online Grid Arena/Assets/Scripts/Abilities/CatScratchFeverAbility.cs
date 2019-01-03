using UnityEngine;

public class CatScratchFeverAbility : PassiveAbility
{
    public CatScratchFeverAbility() : base(
         AbilityType.PASSIVE, 
         new CatScratchFever()
         )
    {

    }

    public override void Execute(IHexTileController targetTile) => effect.Apply(targetTile);
}
