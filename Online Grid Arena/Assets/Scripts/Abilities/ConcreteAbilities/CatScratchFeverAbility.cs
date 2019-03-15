using UnityEngine;
using System.Collections.Generic;

public class CatScratchFeverAbility : AbstractPassiveAbility
{
    public CatScratchFeverAbility(RocketCat character, IEffect effect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/claw-marks"), character,
        "Cat Scratch Fever - Passive \nEverytime you use Rocket Cat's Scratch ability, gain a stack of Cat Scratch Fever. Each stack increases Rocket Cat's attack stat by 5")
    {
        AddEffect(effect);
    }

    public override bool IsEndOfTurnPassive()
    {
        return false;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        character.Controller.ApplyEffect(this.Effects[0]);
    }
}
