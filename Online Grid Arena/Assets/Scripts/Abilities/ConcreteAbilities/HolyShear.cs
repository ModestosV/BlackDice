using UnityEngine;
using System.Collections.Generic;

public class HolyShear : AbstractTargetedAbility
{
    private readonly GameObject damageAnimation;
    private readonly AudioClip damageClip;
    private WoolArmorEffect woolArmor;

    public HolyShear(ICharacter activeCharacter, WoolArmorEffect woolArmor) : base(
        Resources.Load<Sprite>("Sprites/jetpack-png-3"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        activeCharacter,
        5,
        100,
        AbilityType.TARGET_TILE_AOE,
        "Holy Shear - Special Ability \nSheepadin shears half of his wool armor (removing half of his wool armor stacks), healing all allies in an AOE for an amount equal to his defense stat.")
    {
        this.woolArmor = woolArmor;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        int stacksToRemove = woolArmor.GetHalfOfStacks();
        foreach (IHexTileController target in targetTiles[0].GetNeighbors())
        {
            if (target.OccupantCharacter != null && target.OccupantCharacter.IsAlly(character.Controller))
            {
                target.OccupantCharacter.Heal(character.Controller.CharacterStats["defense"].Value);
                PlayAnimation(target);
            }
        }
        for (int i = 0; i < stacksToRemove; i++)
        {
            this.character.Controller.ConsumeOneStack(woolArmor);
        }

        PlaySoundEffect();
    }
}
