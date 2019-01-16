using UnityEngine;
using System.Collections.Generic;

public class CatScratchFeverAbility : AbstractPassiveAbility
{
    public CatScratchFeverAbility(RocketCat character, IEffect effect) : base(Resources.Load<Sprite>("Sprites/Abilities/claw-marks"), character)
    {
        AddEffect(effect);
    }
    
    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        character.Controller.ApplyEffect(this.Effects[0]);
    }
}
