using System.Collections.Generic;
using UnityEngine;

public class ImportDLLs : AbstractActiveAbility
{

    public ImportDLLs(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/importDLL"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/buffUpSound"),
        character,
        1,
        "Import DLL - Special Ability \nTA Eagle applies a random permanent buff to either himself or an ally - target chosen randomly(+10 ATCK, +10 DEF, +2 SPD).")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        ICharacterController ally = RandomAlly();
        PlaySoundEffect();
        PlayAnimation(ally.OccupiedTile);
        ally.ApplyEffect(RandomEffect());
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

    private ICharacterController RandomAlly()
    {
        List<AbstractCharacter> characters = new List<AbstractCharacter>(GameObject.FindObjectsOfType<AbstractCharacter>());
        List<AbstractCharacter> allies = new List<AbstractCharacter>();
        foreach(AbstractCharacter ac in characters)
        {
            if (ac.Controller.Owner == character.Controller.Owner)
            {
                allies.Add(ac);
            }
        }

        return allies[GenerateRandom(allies.Count)].Controller;
    }

    private int GenerateRandom(int range)
    {
        System.Random randomizer = new System.Random();
        return randomizer.Next(0, range);
    }
}