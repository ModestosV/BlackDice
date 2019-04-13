using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CodeReview : ActiveAbility
{
    public CodeReview(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/codeReview"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/buffUpSound"),
        character,
        8,
        "Code Review - Special Ability \nTA Eagle reviews the whole team's code. He and his allies get a shield. Lasts indefinitely and blocks next instance of damage.")
    { }

    protected override async void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Code Review. Primary Action being called.");
        List<ICharacterController> allies = character.Controller.AllAllies();
        foreach (var ally in allies)
        {
            PlaySoundEffect();
            ally.IsShielded = true;
            Debug.Log("shield being applied to target "+ally);
            EventBus.Publish(new StatusEffectEvent("shield", true, ally));
            PlayAnimation(ally.OccupiedTile);
            await Task.Delay(100);
        }
    }
}