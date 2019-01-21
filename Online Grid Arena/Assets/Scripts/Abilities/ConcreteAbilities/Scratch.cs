using UnityEngine;
using System.Collections.Generic;

public sealed class Scratch : AbstractTargetedAbility
{
    private IAbility passive;
    public Scratch(RocketCat activeCharacter, IAbility Passive) : base(
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY)
    {
        passive = Passive;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].Damage(character.Controller.CharacterStats["attack"].Value);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        passive.Execute(targetTiles);
    }
}
