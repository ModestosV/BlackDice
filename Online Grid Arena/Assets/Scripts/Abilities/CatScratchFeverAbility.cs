using UnityEngine;

public class CatScratchFeverAbility : PassiveAbility
{
    private RocketCat self;

    public CatScratchFeverAbility(RocketCat character) : base(
         AbilityType.PASSIVE, 
         new CatScratchFever(Resources.Load<Sprite>("Sprites/Abilities/claw-marks.png")),
         Resources.Load<Sprite>("Sprites/cursorSword_gold")
         )
    {
        this.self = character;
    }

    public override void ModifyPower(float amount){}

    protected override void PrimaryAction(IHexTileController targetTile)
    {
        //here we just add the passive to the cat
        ActivatePassive();
    }

    protected override void ActivatePassive()
    {
        //Add the stack to self
        self.Controller.ApplyEffect(this.effect);
    }
}
