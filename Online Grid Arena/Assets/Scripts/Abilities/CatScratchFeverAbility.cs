using UnityEngine;

public class CatScratchFeverAbility : PassiveAbility
{
    private RocketCat self;

    public CatScratchFeverAbility(RocketCat character) : base(
         AbilityType.PASSIVE, 
         new CatScratchFever()
         )
    {
        this.self = character;
    }

    public override void ModifyPower(float amount){}

    public override void Execute(IHexTileController targetTile)
    {
        self.Controller.AddEffect((IEffect)this);
        Debug.Log("THE ABILITY HAS ADDED THE EFFECT TO KITTY");
    }

    public override void ActivatePassive()
    {
        //Add the stack to self
    }
}
