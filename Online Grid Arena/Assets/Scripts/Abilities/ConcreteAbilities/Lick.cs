using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Lick : AbstractTargetedAbility
{
    public Lick(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/PengwinSlap"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/slap"),
        activeCharacter,
        1,
        2,
        AbilityType.TARGET_ENEMY,
        "Lick - Basic Attack \nAgent Frog licks the opponent, causing their speed to drop by 2 for 1 turn")
    { }

    protected async override void PrimaryAction(List<IHexTileController> targetTiles)
    {

    }
}
