using UnityEngine;

public class BlastOff : TargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;

    public BlastOff(RocketCat activeCharacter) : base(
        AbilityType.TARGET_TILE,
        1,
        30,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        Resources.Load<Sprite>("Sprites/jetpack-png-3"),
        activeCharacter
        )
    {

    }

    protected override void PrimaryAction(IHexTileController targetTile)
    {
        ExecuteMove(targetTile);
        
        // damage all characters at target location            
        foreach (IHexTileController target in targetTile.GetNeighbors())
        {
            target.Damage(activeCharacter.Controller.CharacterStats["attack"].CurrentValue);
            PlayAnimation(target);
        }

        PlaySoundEffect();
    }
}
