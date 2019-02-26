using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Hop : AbstractTargetedAbility
{
    public Hop(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/PengwinSlap"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/slap"),
        activeCharacter,
        5,
        10,
        AbilityType.TARGET_ENEMY,
        "Hop - Special Ability \nAgent Frog hops to a tile up to 10 tiles away and can still use another action")
    { }

    protected async override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        character.Controller.OccupiedTile.OccupantCharacter = null;

        character.MoveToTile(targetTiles[0].HexTile);
        character.Controller.OccupiedTile = targetTiles[0];

        targetTiles[0].OccupantCharacter = character.Controller;
    }
}
