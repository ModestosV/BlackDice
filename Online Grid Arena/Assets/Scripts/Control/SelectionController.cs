using System;

[Serializable]
public class SelectionController : InputController, ISelectionController
{    
    public IHUDController HUDController { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }

    public override void Update()
    {
        if (DebounceUpdate())
            return;

        GridSelectionController.BlurAll();
        GridSelectionController.ScrubPathAll();

        // Escape buton pressed
        if (InputParameters.IsKeyEscapeDown)
        {
            GridSelectionController.DeselectAll();
            HUDController.ClearSelectedHUD();
            return;
        }

        // Clicked off grid
        if (!InputParameters.IsMouseOverGrid && InputParameters.IsLeftClickDown)
        {
            GridSelectionController.DeselectAll();
            HUDController.ClearSelectedHUD();
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
            GridSelectionController.DeselectAll();
            HUDController.ClearSelectedHUD();
            return;
        }

        // Hovered over disabled tile
        if (!tileIsEnabled)
        {
            return;
        }

        // Invariant: Target tile is enabled

        bool tileIsOccupied = InputParameters.TargetTile.Controller.OccupantCharacter != null;
        bool tileIsCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0 && GridSelectionController.SelectedTiles[0] == InputParameters.TargetTile;

        // Clicked unoccupied other tile
        if (InputParameters.IsLeftClickDown && !tileIsOccupied && !tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.Controller.Select();
            HUDController.ClearSelectedHUD();
            return;
        }

        // Clicked unoccupied selected tile 
        if (InputParameters.IsLeftClickDown && tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.Controller.Deselect();
            HUDController.ClearSelectedHUD();
            return;
        }

        // Clicked occupied tile
        if (InputParameters.IsLeftClickDown)
        {
            InputParameters.TargetTile.Controller.Select();
            HUDController.UpdateSelectedHUD(InputParameters.TargetTile.Controller.OccupantCharacter);
            return;
        }

        // Hovered over unoccupied tile
        if (!tileIsOccupied)
        {
            InputParameters.TargetTile.Controller.Hover();
            HUDController.ClearTargetHUD();
            return;
        }

        // Hover over occupied tile
        InputParameters.TargetTile.Controller.Hover();
        HUDController.UpdateTargetHUD(InputParameters.TargetTile.Controller.OccupantCharacter);
        return;
    }
}
