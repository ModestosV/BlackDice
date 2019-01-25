using UnityEngine;
using System.Collections.Generic;

public sealed class Scratch : AbstractTargetedAbility
{
    private readonly IAbility passive;

    public Scratch(RocketCat activeCharacter, IAbility passive) : base(
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY,
        "Basic Attack \nAttack an adjacent tile, deal damage equal to Rocket Cat's attack, and gain a stack of Cat Scratch Fever")
    {
        this.passive = passive;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        actionHandler.Damage(character.Controller.CharacterStats["attack"].CurrentValue, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        passive.Execute(targetTiles);
    }
}
