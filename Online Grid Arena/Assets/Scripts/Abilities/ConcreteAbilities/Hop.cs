using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Hop : AbstractTargetedAbility
{
    public Hop(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/PengwinSlap"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/slap"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY,
        " ")
    { }

    protected async override void PrimaryAction(List<IHexTileController> targetTiles)
    {

    }
}
