using System.Collections.Generic;
using UnityEngine;

public class FlakyTests : AbstractTargetedAbility
{
    public FlakyTests(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/flakytests"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/keyboardSound"),
        character,
        10,
        5, 
        AbilityType.TARGET_ENEMY,
        "Flaky Tests - Ultimate Ability \nProfessor Owl runs a flaky test on an enemy. If it passes, the enemy takes 200% of Owl's attack stat. If the test fails, Professor Owl gets the \"Angry\" effect which buffs his attack stat.")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Flaky Tests. Primary action being called.");
        PlaySoundEffect();
        int testPassed = GenerateRandom(6);
        if (testPassed > 1)
        {
            actionHandler.Damage(character.Controller.CharacterStats["attack"].Value*2.0f, targetTiles[0].OccupantCharacter);
            PlayAnimation(targetTiles[0]);
        }
        else
        {
            this.character.Controller.ApplyEffect(new AngryEffect());
            PlayAnimation(this.character.Controller.OccupiedTile);
        }
    }

    private int GenerateRandom(int range)
    {
        System.Random randomizer = new System.Random();
        return randomizer.Next(0, range);
    }
}