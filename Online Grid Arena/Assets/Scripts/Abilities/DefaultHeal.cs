using UnityEngine;
using System.Collections.Generic;

public sealed class DefaultHeal : ActiveAbility
{
    public DefaultHeal(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Heal_Icon"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultHealAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/SmallSplash"),
        activeCharacter,
        1,
        1
        )
    {

    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].Heal(15);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
