using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CodeReview : AbstractActiveAbility
{

    public CodeReview(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/importDLL"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/buffUpSound"),
        character,
        1,
        "Code Review - Special Ability \nTA Eagle reviews the whole team's code. He and his allies get a shield")
    { }

    protected async override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        List<ICharacterController> allies = AllAllies();
        for (int i = 0; i < allies.Count; i++)
        {
            PlaySoundEffect();
            allies[i].ApplyEffect(RandomEffect());
            PlayAnimation(allies[i].OccupiedTile);
            await Task.Delay(100);
        }
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

    private List<ICharacterController> AllAllies()
    {
        List<AbstractCharacter> characters = new List<AbstractCharacter>(GameObject.FindObjectsOfType<AbstractCharacter>());
        List<ICharacterController> allies = new List<ICharacterController>();
        foreach (AbstractCharacter ac in characters)
        {
            if (ac.Controller.CharacterOwner == character.Controller.CharacterOwner)
            {
                allies.Add(ac.Controller);
            }
        }
        return allies;
    }

    private int GenerateRandom(int range)
    {
        System.Random randomizer = new System.Random();
        int rand = randomizer.Next(0, range);
        return rand;
    }
}