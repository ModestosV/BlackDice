using System.Collections.Generic;
using UnityEngine;

public class Huddle : AbstractActiveAbility
{
    private const float DEFENSE_BONUS_AMOUNT = 10.0f;

    public Huddle(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/huddle"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultHealAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/huddle"),
        character,
        3,
        "Huddle - Special Ability \nPengwin gives bonus defense to itself and all adjacent allies.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("I am here");
        throw new System.NotImplementedException();
    }
}