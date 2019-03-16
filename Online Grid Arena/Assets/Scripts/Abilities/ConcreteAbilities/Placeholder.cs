using System.Collections.Generic;
using UnityEngine;

public class Placeholder : AbstractPassiveAbility
{
    public Placeholder(ICharacter character): base(
        Resources.Load<Sprite>("Sprites/Abilities/grey_button09"),
        character,
        "Placeholder Ability. Does nothing.",
        false)
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles) { }
}