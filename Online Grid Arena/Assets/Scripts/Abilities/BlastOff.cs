using UnityEngine;

public class BlastOff : TargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public BlastOff(ICharacter activeCharacter) : base(
        AbilityType.TARGET_TILE,
        1,
        25.0f,
        30,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        Resources.Load<Sprite>("Sprites/jetpack-png-3"),
        activeCharacter
        )
    {

    }

    public override void Execute(IHexTileController targetTile)
    {
        // move character
        activeCharacter.Controller.OccupiedTile.OccupantCharacter = null;
        activeCharacter.Controller.OccupiedTile.Deselect();

        activeCharacter.MoveToTile(targetTile.HexTile);
        activeCharacter.Controller.OccupiedTile = targetTile;

        targetTile.OccupantCharacter = activeCharacter.Controller;
        targetTile.Select();
        
        // damage all characters at target location            
        foreach (IHexTileController target in targetTile.GetNeighbors())
        {
            target.Damage(power);
            PlayAnimation(target);
        }

        PlaySoundEffect();

        cooldownRemaining += cooldown;
    }
}
