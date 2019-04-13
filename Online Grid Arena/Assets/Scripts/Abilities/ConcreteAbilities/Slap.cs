using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Slap : TargetedAbility
{
    private const int MAX_EXTRA_SLAPS = 3;

    public Slap(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/PengwinSlap"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/slap"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY,
        "Slap - Basic Attack \nPengwin slaps target and deals damage equal to his attack. Has a 75% chance of re-casting (maximum number of hits: 4).")
    { }

    protected override async void PrimaryAction(List<IHexTileController> targetTiles)
    {
        SlapAttack(targetTiles);
        for (int i = 0; i < MAX_EXTRA_SLAPS; i++)
        {
            if (ChanceToTrigger())
            {
                await Task.Delay(300);
                SlapAttack(targetTiles);
            }
            else break;
        }
    }

    private void SlapAttack(List<IHexTileController> targetTiles)
    {
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
    }

    private bool ChanceToTrigger()
    {
        System.Random randomizer = new System.Random();
        int rand = randomizer.Next(0,100);
        return (rand < 75);
    }
}
