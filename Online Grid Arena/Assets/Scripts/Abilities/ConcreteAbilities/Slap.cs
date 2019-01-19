using System.Collections.Generic;
using UnityEngine;

public sealed class Slap : AbstractTargetedAbility
{
    public Slap(Pengwin activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        Resources.Load<GameObject>("Assets/Resources/Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY)
    {    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        targetTiles[0].Damage(character.Controller.CharacterStats["attack"].Value);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }
}
