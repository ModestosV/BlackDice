using UnityEngine;
using System.Collections.Generic;

public class CatScratchFeverAbility : AbstractPassiveAbility
{
    public CatScratchFeverAbility(RocketCat character) : base(Resources.Load<Sprite>("Sprites/Abilities/claw-marks"), character) { }

    // TODO: See if this works after implementing ApplyEffects()
    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        ApplyEffects();
    }
}
