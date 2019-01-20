using UnityEngine;
using System.Collections.Generic;

public sealed class DefaultHeal : AbstractTargetedAbility
{
    public DefaultHeal(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Heal_Icon"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultHealAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/SmallSplash"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ALLY)
    {
        Description = "This is the default heal ability. It is used to test healing.";
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].Heal(character.Controller.CharacterStats["attack"].Value);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
