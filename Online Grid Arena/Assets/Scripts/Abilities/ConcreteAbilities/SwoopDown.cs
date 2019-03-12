using UnityEngine;
using System.Collections.Generic;

public sealed class SwoopDown : AbstractTargetedAbility
{
    public SwoopDown(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/swoopDown"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/bat-flapping"),
        activeCharacter,
        1,
        4,
        AbilityType.TARGET_ENEMY,
        "Swoop Down - Basic Attack \nTA Eagle Swoops Down and strikes his oponent from up to 4 tiles away. Increases TA Eagle's speed value by 1.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Swoop Down. primary action being called.");
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        character.Controller.ApplyEffect(new SpeedDLL());
    }
}
