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
        //here we just add the passive to the cat
        ActivatePassive();
    }

    public override void ActivatePassive()
    {
        //Add the stack to self
        self.Controller.ApplyEffect(this.effect);
    }
}
