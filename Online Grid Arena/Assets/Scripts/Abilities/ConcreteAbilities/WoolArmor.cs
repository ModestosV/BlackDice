using System.Collections.Generic;
using UnityEngine;

public sealed class WoolArmor : AbstractPassiveAbility
{
    public WoolArmor(ICharacter activeCharacter, IEffect effect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/wool"),
        activeCharacter,
        "Wool Armor - Passive Ability \nAt the end of his turn, Sheepadin's armor grows thicker. Gain a stack of Wool Armor.",
        true)
    {
        this.AddEffect(effect);
    }

    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        this.character.Controller.ApplyEffect(this.Effects[0]);
    }
}
