using UnityEngine;

public class BlastOff : TargetedAbility {
    
    private readonly ICharacterController activeCharacter;

    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

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
            PlayAnimation(targetTile);
            PlaySoundEffect();
            activeCharacter.ForceMove(targetTile);

            // damage all characters at target location            
            foreach (IHexTileController target in targetTile.GetNeighbors())
            {
                target.Damage(power);
                PlaySoundEffect();
                PlayAnimation(target);
            }

            cooldownRemaining += cooldown;
        }
        else Debug.Log("Tile is occupied");
    }
}
