using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Headbutt : AbstractTargetedAbility
{
    public Headbutt(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/lick"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/kouaks"),
        activeCharacter,
        1,
        1,
        AbilityType.TARGET_ENEMY,
        "Lick - Basic Attack \nAgent Frog licks an opponent, causing their speed to drop by 2 for 1 turn. Range: 2")
    { }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Headbutt. Primary action being called.");
        actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[0].OccupantCharacter);
        PlaySoundEffect();
        PlayAnimation(targetTiles[0]);
        //if tile isNotObstructed, push. //bonus To add? if cannot move, stun instead? too OP maybe
        FindAxisOfAttack(targetTiles);
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
                Debug.Log("top left");
                if (targetTile.GetNorthWestNeighbor().IsObstructed || targetTile.GetNorthWestNeighbor().IsOccupied())
                {
                    Debug.Log("BLOCKED");
                }
            }
            else
            {
                Debug.Log("bottom right");
                if (targetTile.GetSouthEastNeighbor().IsObstructed || targetTile.GetSouthEastNeighbor().IsOccupied())
                {
                    Debug.Log("BLOCKED");
                }
            }
        }
        else if (sheepTile.Y == targetTile.Y)
        {
            if (targetTile.Z < sheepTile.Z)
            {
                Debug.Log("top right");
                if (targetTile.GetNorthEastNeighbor().IsObstructed || targetTile.GetNorthEastNeighbor().IsOccupied())
                {
                    Debug.Log("BLOCKED");
                }
            }
            else
            {
                Debug.Log("bottom left");
                if (targetTile.GetSouthWestNeighbor().IsObstructed || targetTile.GetSouthWestNeighbor().IsOccupied())
                {
                    Debug.Log("BLOCKED");
                }
            }
        }
        else if (sheepTile.Z == targetTile.Z)//remove this if later
        {
            if (targetTile.Y < sheepTile.Y)
            {
                Debug.Log("right right");
                if (targetTile.GetEastNeighbor().IsObstructed || targetTile.GetEastNeighbor().IsOccupied())
                {
                    Debug.Log("BLOCKED");
                }
            }
            else
            {
                Debug.Log("left left");
                if (targetTile.GetWestNeighbor().IsObstructed || targetTile.GetWestNeighbor().IsOccupied())
                {
                    Debug.Log("BLOCKED");
                }
            }
        }
    }
}
