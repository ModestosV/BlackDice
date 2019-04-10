using System.Collections.Generic;
using UnityEngine;

public sealed class Headbutt : AbstractTargetedAbility
{
    public Headbutt(ICharacter activeCharacter, IEffect effect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/headButt"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/headbutt"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY,
        "Headbutt - Basic Attack \nSheepadin headbutts an adjacent enemy, dealing his attack in damage and healing himself for 25% of his defense stat. Adds a stack of Wool Armor.")
    {
        this.AddEffect(effect);
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Headbutt. Primary action being called.");
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        character.Controller.Heal(character.Controller.CharacterStats["defense"].Value* 0.25f);
        character.Controller.ApplyEffect(Effects[0]);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        //FindAxisOfAttack(targetTiles);
    }

    private void FindAxisOfAttack(List<IHexTileController> targetTiles)
    {
        IHexTileController sheepTile = character.Controller.OccupiedTile;
        IHexTileController targetTile = targetTiles[0];
        //dont forget tile might be occupied
        if (sheepTile.X == targetTile.X)
        {
            if (targetTile.Z < sheepTile.Z)
            {
                if (CanMoveByOneTile("NW", targetTile))
                {
                    MoveTargetToTile(targetTile.GetNorthWestNeighbor(), targetTile);
                }
            }
            else
            {
                if (CanMoveByOneTile("SE", targetTile))
                {
                    MoveTargetToTile(targetTile.GetSouthEastNeighbor(), targetTile);
                }
            }
        }
        else if (sheepTile.Y == targetTile.Y)
        {
            if (targetTile.Z < sheepTile.Z)
            {
                if (CanMoveByOneTile("NE", targetTile))
                {
                    MoveTargetToTile(targetTile.GetNorthEastNeighbor(), targetTile);
                }
            }
            else
            {
                if (CanMoveByOneTile("SW", targetTile))
                {
                    MoveTargetToTile(targetTile.GetSouthWestNeighbor(), targetTile);
                }
            }
        }
        else if (sheepTile.Z == targetTile.Z)
        {
            if (targetTile.Y < sheepTile.Y)
            {
                if (CanMoveByOneTile("E", targetTile))
                {
                    MoveTargetToTile(targetTile.GetEastNeighbor(), targetTile);
                }
            }
            else
            {
                if (CanMoveByOneTile("W", targetTile))
                {
                    MoveTargetToTile(targetTile.GetWestNeighbor(), targetTile);
                }
            }
        }
    }

    private bool CanMoveByOneTile(string axis, IHexTileController targetTile)
    {
        IHexTileController tileToMoveTo = null;
        switch (axis)
        {
            case "NW":
                tileToMoveTo = targetTile.GetNorthWestNeighbor();
                break;
            case "NE":
                tileToMoveTo = targetTile.GetNorthEastNeighbor();
                break;
            case "SW":
                tileToMoveTo = targetTile.GetSouthWestNeighbor();
                break;
            case "SE":
                tileToMoveTo = targetTile.GetSouthEastNeighbor();
                break;
            case "E":
                tileToMoveTo = targetTile.GetEastNeighbor();
                break;
            case "W":
                tileToMoveTo = targetTile.GetWestNeighbor();
                break;
        }
        if (tileToMoveTo == null) { return false; }
        if (targetTile.GetSouthWestNeighbor().IsObstructed || targetTile.GetSouthWestNeighbor().IsOccupied())
        {
            return false;
        }
        else return true;

    }

    private void MoveTargetToTile(IHexTileController finalTile, IHexTileController initialTile)
    {
        ICharacter target = initialTile.OccupantCharacter.Character;

        initialTile.OccupantCharacter = null;
        target.MoveToTile(finalTile.HexTile);
        target.Controller.OccupiedTile = finalTile;
        finalTile.OccupantCharacter = target.Controller;
    }
}
