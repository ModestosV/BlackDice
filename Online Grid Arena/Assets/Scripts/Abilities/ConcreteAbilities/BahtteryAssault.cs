using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BahtteryAssault : AbstractActiveAbility
{
    private readonly IWoolArmorEffect woolArmorEffect;
    private const int BASE_VALUE = 20;

    public BahtteryAssault(ICharacter character, IWoolArmorEffect woolArmorEffect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/sheepUlt"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/BlueFireAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/goat-bleat"),
        character,
        2,
        $"\"Bah\"ttery Assault - Ultimate Ability \nSheepadin consumes half of his wool armor stacks, and drops twice as many wool bombs within a 1 tile radius of himself. Each bomb deals 10 damage where it lands in an aoe.")
    {
        this.woolArmorEffect = woolArmorEffect;
    }

    protected override async void PrimaryAction(List<IHexTileController> targetTiles)
    {
        PlaySoundEffect();
        int stacksToRemove = woolArmorEffect.GetHalfOfStacks();
        List<IHexTileController> neighbors = this.character.Controller.OccupiedTile.GetNeighbors();
        for (int i = 0; i < stacksToRemove; i++)
        {
            this.character.Controller.ConsumeOneStack(woolArmorEffect);
            DropWoolBomb(neighbors[GenerateRandom(neighbors.Count)]);
            await Task.Delay(100);
            DropWoolBomb(neighbors[GenerateRandom(neighbors.Count)]);
            await Task.Delay(100);
        }
    }

    private void DropWoolBomb(IHexTileController targetTile)
    {
        PlayAnimation(targetTile);
        actionHandler.Damage(10.0f, targetTile.OccupantCharacter);

        foreach (IHexTileController tile in targetTile.GetNeighbors())
        {
            PlayAnimation(tile);
            actionHandler.Damage(10.0f,tile.OccupantCharacter);
        }

        if (targetTile.IsOccupied() && targetTile.OccupantCharacter.IsAlly(this.character.Controller))
        {

        }

    }

    private int GenerateRandom(int range)
    {
        System.Random randomizer = new System.Random();
        return randomizer.Next(0, range);
    }
}