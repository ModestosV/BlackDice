using UnityEngine;

public class BlastOff : TargetedAbility {
    
    private readonly ICharacterController activeCharacter;

    public BlastOff(ICharacterController activeCharacter) : base(
        AbilityType.ACTIVATED,
        1,
        25.0f,
        10,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip")
        )
    {
        this.activeCharacter = activeCharacter;
    }

    public override void Execute(IHexTileController targetTile)
    {
        // check if target tile is open
        if (!targetTile.IsOccupied())
        {
            activeCharacter.ForceMove(targetTile);

            PlayAnimation(targetTile);

            targetTile.OccupantCharacter = activeCharacter;

        }
        else Debug.Log("Tile is occupied");
        // trigger animation
        // move character to targeted tile
        // deal damage to all tiles around new location
    }
}
