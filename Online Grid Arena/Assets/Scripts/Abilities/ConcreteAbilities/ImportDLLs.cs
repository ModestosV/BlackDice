using System.Collections.Generic;
using UnityEngine;

public class ImportDLLs : AbstractActiveAbility
{

    public ImportDLLs(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/huddle"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/huddle"),
        character,
        4,
        "Import DLL - Special Ability \nTA Eagle applies a random permanent buff to either himself or an ally - target chosen randomly(+10 ATCK, +10 DEF, +2 SPD).")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        IEffect effect = RandomEffect();
        //pick random person on team who is alive

        //apply effect to that person
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }

    private IEffect RandomEffect()
    {
        int effectNumber = GenerateRandom(3);
        switch (effectNumber)
        {
            case 0: 
                return new AttackDLL();
            case 1: 
                return new DefenseDLL();
            case 2:
                return new SpeedDLL();
            default:
                return null;
        }
    }

    private void RandomAlly()
    {
        //get all allies

        //pick random one
        //return that shit
    }

    private int GenerateRandom(int range)
    {
        System.Random randomizer = new System.Random();
        int rand = randomizer.Next(0, range);
        return rand;
    }
}