﻿using System.Collections.Generic;

public class MovementSelectionController : InputController, IMovementSelectionController
{
    public IGridSelectionController GridSelectionController { protected get; set; }
    public IGameManager GameManager { protected get; set; }

    public override void Update()
    {
        if (DebounceUpdate())
            return;

        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();

        // Escape buton pressed
        if (InputParameters.IsKeyEscapeDown)
        {
            GameManager.SelectionMode = SelectionMode.SELECTION;
            return;
        }

        // Clicked off grid
        if (!InputParameters.IsMouseOverGrid && InputParameters.IsLeftClickDown)
        {
            return;
        }

        // Hovered off grid
        if (!InputParameters.IsMouseOverGrid)
        {
            return;
        }

        // Invariant: Mouse is over grid

        bool tileIsEnabled = InputParameters.TargetTile.IsEnabled;

        // Clicked on disabled tile
        if (!tileIsEnabled && InputParameters.IsLeftClickDown)
        {
            return;
        }

        // Hovered over disabled tile
        if (!tileIsEnabled)
        {
            return;
        }

        // Invariant: Target tile is enabled

        IHexTileController selectedTile = GridSelectionController.GetSelectedTile();
        ICharacterController selectedCharacter = selectedTile.OccupantCharacter;

        bool tileIsOccupied = InputParameters.TargetTile.IsOccupied();
        bool tileIsCurrentSelectedTile = GridSelectionController.IsSelectedTile(InputParameters.TargetTile);

        List<IHexTileController> path = selectedTile.GetPath(InputParameters.TargetTile);
        bool isReachable = path.Count > 0;
        bool inRange = selectedCharacter.CanMove(path.Count - 1);

        // Clicked on unreachable tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile && !isReachable)
        {
            InputParameters.TargetTile.HoverError();
            return;
        }

        // Clicked reachable out of range unoccupied tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile && !tileIsOccupied && !inRange)
        {
            foreach (IHexTileController hexTile in path)
            {
                hexTile.HoverError();
            }
            return;
        }

        // Clicked reachable in range unoccupied tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile && !tileIsOccupied)
        {
            selectedCharacter.ExecuteMove(path);
            GameManager.SelectionMode = SelectionMode.SELECTION;
            return;
        }

        // Clicked reachable occupied tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.HoverError();
            return;
        }

        // Clicked current selected tile
        if (InputParameters.IsLeftClickDown && tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.HoverError();
            return;
        }

        // Hovered over current selected tile
        if (tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.HoverError();
            return;
        }

        // Invariant: Left mouse button not clicked        

        // Hovered over unreachable tile
        if (!isReachable)
        {
            InputParameters.TargetTile.HoverError();
            return;
        }

        // Hovered over reachable out of range unoccupied tile
        if (!tileIsOccupied && !inRange)
        {
            foreach (IHexTileController hexTile in path)
            {
                hexTile.HoverError();
            }
            return;
        }

        // Hovered over reachable in range unoccupied tile
        if (!tileIsOccupied)
        {
            foreach (IHexTileController hexTile in path)
            {
                hexTile.Highlight();
            }
            return;
        }

        // Hovered over reachable occupied tile
        InputParameters.TargetTile.HoverError();
        return;
    }
}
