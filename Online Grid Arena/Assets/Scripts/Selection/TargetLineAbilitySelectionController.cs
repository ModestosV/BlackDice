using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLineAbilitySelectionController : AbstractAbilitySelectionController
{

    protected override void DoFirst()
    {
        SetActiveAbility();
        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();
    }

    protected override void DoEscapePressed()
    {
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    protected override void DoClickUnoccupiedOtherTile()
    {
        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
        {
            List<IHexTileController> target = new List<IHexTileController>();
            target.Add(inputParameters.TargetTile);

            selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
            EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
            return;
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile);
        bool isStraightLine = false;
        if (selectedTile.X == inputParameters.TargetTile.X || selectedTile.Y == inputParameters.TargetTile.Y || selectedTile.Z == inputParameters.TargetTile.Z)
        {
            isStraightLine = true;
        }

        if (!isStraightLine)
        {
            for (int i = 1; i < path.Count; i++)
            {
                path[i].HoverError();
            }
            return;
        }

        // Hovered over reachable in range tile
        for (int i = 1; i < path.Count; i++)
        {
            path[i].Highlight();
        }
        return;
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.HoverError();
    }

    protected override void DoHoverOccupiedTile()
    {
        inputParameters.TargetTile.HoverError();
        //path not working for this one
        //cause path cant be obstructed
        //want to check another tile
        //get path to it
        //hover error on the path 
        //or even make another getPath method or something
        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        List<IHexTileController> path = selectedTile.GetPath(inputParameters.TargetTile);
        GridSelectionController.GetSelectedTile().HoverError();
    }
}
