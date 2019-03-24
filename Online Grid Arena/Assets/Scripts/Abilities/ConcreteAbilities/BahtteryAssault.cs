using System.Collections.Generic;
using UnityEngine;

public class BahtteryAssault : AbstractActiveAbility
{
    private IWoolArmorEffect woolArmorEffect;
    public BahtteryAssault(ICharacter character, IWoolArmorEffect woolArmorEffect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/huddle"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/huddle"),
        character,
        3,
        "Huddle - Special Ability \nPengwin gives 20 bonus defense to itself and all adjacent allies.")
    {
        this.woolArmorEffect = woolArmorEffect;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        PlaySoundEffect();
        int stacksToRemove = woolArmorEffect.GetHalfOfStacks();
        
        List<IHexTileController> listofAffectedTiles = new List<IHexTileController>();
        for (int i = 0; i < 41; i++)
        {
            IHexTileController randomTile = targetTiles[0].GetRandomTile();
            Debug.Log(randomTile.X+ " "+ randomTile.Y + " "+randomTile.Z);
            if (randomTile != null && !listofAffectedTiles.Contains(randomTile) && !randomTile.IsObstructed)
            {
                listofAffectedTiles.Add(randomTile);
            }
        }

        for (int i = 0; i < listofAffectedTiles.Count; i++)
        {
            PlayAnimation(listofAffectedTiles[i]);
            if (listofAffectedTiles[i].IsOccupied())
            {
                if (listofAffectedTiles[i].OccupantCharacter.IsAlly(this.character.Controller))
                {
                    listofAffectedTiles[i].OccupantCharacter.Heal(stacksToRemove*15);//an ammount
                }
                else
                {
                    actionHandler.Damage(stacksToRemove * 15, listofAffectedTiles[i].OccupantCharacter);
                }
            }
        }

        for (int i = 0; i < stacksToRemove; i++)
        {
            this.character.Controller.ConsumeOneStack(woolArmorEffect);
        }
    }
}