using UnityEngine;

public class CatScratchFeverAbility : PassiveAbility
{
    private RocketCat character;
    public CatScratchFeverAbility(RocketCat character) : base(
         AbilityType.PASSIVE, 
         new CatScratchFever()
         )
    {
        this.character = character;
    }
    public override void ModifyPower(float amount){}
    public override void Execute(IHexTileController targetTile)
    {
        character.Controller.AddEffect((IEffect)this);
        Debug.Log("THE ABILITY HAS ADDED THE EFFECT TO KITTY");
    }
}
