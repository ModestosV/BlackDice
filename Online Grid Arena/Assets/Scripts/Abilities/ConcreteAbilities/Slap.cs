using System.Collections.Generic;
using UnityEngine;

public sealed class Slap : AbstractTargetedAbility
{
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

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        SlapAttack(targetTiles);
        for (int i = 0; i < 3; i++)
        {
            if (ChanceToTrigger())
            {
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
        Debug.Log(rand);
        return (rand < 75) ? true : false;
    }
}
