using System.Collections.Generic;
using UnityEngine;

public class BahtteryAssault : AbstractActiveAbility
{
    private readonly IWoolArmorEffect woolArmorEffect;
    private const int BASE_VALUE = 45;
    private const int NUMBER_OF_TILES = 15;

    public BahtteryAssault(ICharacter character, IWoolArmorEffect woolArmorEffect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/sheepUlt"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/BlueFireAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/goat-bleat"),
        character,
        2,
        $"\"Bah\"ttery Assault - Ultimate Ability \nSheepadin consumes half of his wool armor stacks and assaults {NUMBER_OF_TILES} random tiles on the map. Allies and enemies hit are healed or damaged for (${BASE_VALUE}*amount of wool lost).")
    {
        this.woolArmorEffect = woolArmorEffect;
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        PlaySoundEffect();
        int stacksToRemove = woolArmorEffect.GetHalfOfStacks();
        
        List<IHexTileController> listofAffectedTiles = new List<IHexTileController>();
        while (listofAffectedTiles.Count < NUMBER_OF_TILES)
        {
            IHexTileController randomTile = targetTiles[0].GetRandomTile();
            if (randomTile != null && !listofAffectedTiles.Contains(randomTile) && !randomTile.IsObstructed && randomTile.X >= -1 && randomTile.X <= 11 && randomTile.Y >= -20 && randomTile.Y <= -8 && randomTile.Z >= 3 && randomTile.Z <= 15)
            {
                Debug.Log(randomTile.X + " " + randomTile.Y + " " + randomTile.Z);
                listofAffectedTiles.Add(randomTile);
            }
        }
        Debug.Log("We have "+ listofAffectedTiles.Count+" tiles");
        for (int i = 0; i < listofAffectedTiles.Count; i++)
        {
            PlayAnimation(listofAffectedTiles[i]);
            if (listofAffectedTiles[i].IsOccupied())
            {
                if (listofAffectedTiles[i].OccupantCharacter.IsAlly(this.character.Controller))
                {
                    listofAffectedTiles[i].OccupantCharacter.Heal(stacksToRemove * BASE_VALUE);
                }
                else
                {
                    actionHandler.Damage(stacksToRemove * BASE_VALUE, listofAffectedTiles[i].OccupantCharacter);
                }
            }
        }

        for (int i = 0; i < stacksToRemove; i++)
        {
            this.character.Controller.ConsumeOneStack(woolArmorEffect);
        }
    }
}