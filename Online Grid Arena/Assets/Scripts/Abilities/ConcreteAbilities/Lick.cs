using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Lick : AbstractTargetedAbility
{
    public Lick(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/PengwinSlap"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/kouaks"),
        activeCharacter,
        1,
        2,
        AbilityType.TARGET_ENEMY,
        "Lick - Basic Attack \nAgent Frog licks the opponent, causing their speed to drop by 2 for 1 turn",
        false)
    { }

    protected async override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        var enemy = targetTiles[0].OccupantCharacter;

        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        //enemy.CharacterStats["moves"].Value-=2;
    }
}
