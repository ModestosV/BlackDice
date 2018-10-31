using System.Collections.Generic;
using System;

[Serializable]
public class MovementSelectionController : InputController, IMovementSelectionController
{
    public IHUDController HUDController { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }
    public IGridTraversalController GridTraversalController { get; set; }
    public IGameManager GameManager { get; set; }

    public override void Update()
    {
        if (DebounceUpdate())
            return;

        GridSelectionController.BlurAll();
        GridSelectionController.ScrubPathAll();

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

        bool tileIsEnabled = InputParameters.TargetTile.Controller.IsEnabled;

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

        IHexTile selectedTile = GridSelectionController.SelectedTiles[0];
        ICharacter selectedCharacter = selectedTile.Controller.OccupantCharacter;

        bool tileIsOccupied = InputParameters.TargetTile.Controller.OccupantCharacter != null;
        bool tileIsCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0
            && selectedTile == InputParameters.TargetTile;

        List<IHexTile> path = GridTraversalController.GetPath(selectedTile, InputParameters.TargetTile);
        bool isReachable = path.Count > 0;

        // Clicked on unreachable tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile && !isReachable)
        {
            InputParameters.TargetTile.Controller.HoverError();
            return;
        }


        // Clicked reachable unoccupied tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile && !tileIsOccupied)
        {
            selectedCharacter.Controller.ExecuteMove(InputParameters.TargetTile);
            GameManager.SelectionMode = SelectionMode.SELECTION;
            return;
        }

        // Clicked reachable occupied tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.Controller.HoverError();
            return;
        }

        // Clicked current selected tile
        if (InputParameters.IsLeftClickDown && tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.Controller.HoverError();
            return;
        }

        // Hovered over current selected tile
        if (tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.Controller.HoverError();
            return;
        }

        // Invariant: Left mouse button not clicked        

        // Hovered over unreachable tile
        if (!isReachable)
        {
            InputParameters.TargetTile.Controller.HoverError();
            return;
        }

        // Hovered over reachable unoccupied tile
        if (!tileIsOccupied)
        {
            GridSelectionController.HighlightPath(path);
            return;
        }

        // Hovered over reachable occupied tile
        InputParameters.TargetTile.Controller.HoverError();
        return;
    }
}
