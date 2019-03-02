using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class PoisonAura : AbstractTargetedAbility
{
    public PoisonAura(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/PengwinSlap"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/slap"),
        activeCharacter,
        3,
        5,
        AbilityType.TARGET_ENEMY,
        "Tongue Pull - Special Ability \nAgent Frog pulls an opponent or ally towards himself",
        false)
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        character.Controller.ApplyEffect(this.Effects[0]);
    }

}
