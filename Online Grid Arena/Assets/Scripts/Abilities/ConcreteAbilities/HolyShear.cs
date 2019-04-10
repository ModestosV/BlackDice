using UnityEngine;
using System.Collections.Generic;

public class HolyShear : AbstractTargetedAbility
{
    private readonly IWoolArmorEffect woolArmorEffect;

    public HolyShear(ICharacter activeCharacter, IWoolArmorEffect woolArmorEffect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/holyShear"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/BlueFireAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/holyChoir"),
        activeCharacter,
        5,
        100,
        AbilityType.TARGET_AOE,
        "Holy Shear - Special Ability \nSheepadin shears half of his wool armor (removing half of his wool armor stacks, rounded down), healing all allies in an AOE for an amount equal to his defense stat.")
    {
        this.woolArmorEffect = woolArmorEffect;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        int stacksToRemove = woolArmorEffect.GetHalfOfStacks();
        if (targetTiles[0].OccupantCharacter != null && targetTiles[0].OccupantCharacter.IsAlly(character.Controller))
        {
            targetTiles[0].OccupantCharacter.Heal(character.Controller.CharacterStats["defense"].Value);
        }
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
            this.character.Controller.ConsumeOneStack(woolArmorEffect);
        }

        PlaySoundEffect();
    }
}
