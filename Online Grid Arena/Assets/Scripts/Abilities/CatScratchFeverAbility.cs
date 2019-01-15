using UnityEngine;
using System.Collections.Generic;

public class CatScratchFeverAbility : PassiveAbility
{
    private RocketCat self;

    public CatScratchFeverAbility(IEffect effect, RocketCat character) : base(
         effect,
         Resources.Load<Sprite>("Sprites/Abilities/claw-marks"),
         character
         )
    {
        this.self = character;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        //here we just add the passive to the cat
        ActivatePassive();
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {

    }

    protected override void ActivatePassive()
    {
        //Add the stack to self
        self.Controller.ApplyEffect(this.effect);
    }
}
