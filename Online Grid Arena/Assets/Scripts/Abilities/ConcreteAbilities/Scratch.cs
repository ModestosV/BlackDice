using UnityEngine;
using System.Collections.Generic;

public sealed class Scratch : AbstractTargetedAbility
{
    public Scratch(RocketCat activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY)
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].Damage(25);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
