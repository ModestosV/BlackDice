using UnityEngine;
using System.Collections.Generic;

public class HolyBah : AbstractTargetedAbility
{
    // need secondary animation/sound for landing
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;
    private WoolArmorEffect woolArmor;

    public HolyBah(ICharacter activeCharacter, WoolArmorEffect woolArmor) : base(
        Resources.Load<Sprite>("Sprites/jetpack-png-3"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        5,
        100,
        AbilityType.TARGET_TILE_AOE,
        "Holy Baah - Special Ability \nSheepadin utters the Holy \"Bah\", healing all allies in an AOE for his defense stat. The prayer consumes half of his wool armor stacks.")
    {
        this.woolArmor = woolArmor;
    }

    // Move Rocket Cat to new location
    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        int stacksToRemove = woolArmor.ConsumeHalfOfStakcs();
        foreach (IHexTileController target in targetTiles[0].GetNeighbors())
        {
            if (target.OccupantCharacter != null && target.OccupantCharacter.IsAlly(character.Controller))
            {
                target.OccupantCharacter.Heal(character.Controller.CharacterStats["defense"].Value);
                PlayAnimation(target);
            }
        }

        PlaySoundEffect();
    }
}
