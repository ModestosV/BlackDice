﻿using System.Collections.Generic;
using UnityEngine;

public class ImportDLLs : AbstractActiveAbility
{
    public ImportDLLs(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/importDLL"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/buffUpSound"),
        character,
        3,
        "Import DLL - Special Ability \nTA Eagle applies a random permanent buff to either himself or an ally - target chosen randomly(+5 ATCK, +5 DEF, +1 SPD).")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting ImportDLLs. Primary action being called.");
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
        List<ICharacterController> allies = character.Controller.AllAllies();
        return allies[GenerateRandom(allies.Count)];
    }

    private int GenerateRandom(int range)
    {
        System.Random randomizer = new System.Random();
        return randomizer.Next(0, range);
    }
}