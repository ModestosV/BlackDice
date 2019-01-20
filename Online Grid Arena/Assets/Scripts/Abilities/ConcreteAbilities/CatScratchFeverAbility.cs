using UnityEngine;
using System.Collections.Generic;

public class CatScratchFeverAbility : AbstractPassiveAbility
{
    public CatScratchFeverAbility(RocketCat character, IEffect effect) : base(Resources.Load<Sprite>("Sprites/Abilities/claw-marks"), character)
    {
        AddEffect(effect);
        Description = "Passive Ability \n Everytime you use Rocket Cat's Scratch ability, gain a stack of Cat Scratch Fever. Each stack increases Rocket Cat's attack stat";
    }
    
    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        character.Controller.ApplyEffect(this.Effects[0]);
    }
}
