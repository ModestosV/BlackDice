using UnityEngine;

public class BlastOff : TargetedAbility {

    public BlastOff() : base(
        AbilityType.ACTIVATED,
        1,
        25.0f,
        10,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip")
        )
    {

    }

    public override void Execute(IHexTileController targetTile)
    {
        // check if target tile is open
        if (!targetTile.IsOccupied())
        {

        }
        else Debug.Log("Tile is occupied");
        // trigger animation
        // move character to targeted tile
        // deal damage to all tiles around new location
    }
}
