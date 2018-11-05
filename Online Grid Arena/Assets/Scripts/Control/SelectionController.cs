public class SelectionController : InputController, ISelectionController
{
    public IGridSelectionController GridSelectionController { protected get; set; }
    public ITurnController TurnController { protected get; set; }

    public override void Update()
    {
        if (DebounceUpdate())
            return;

        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();

        // Escape buton pressed
        if (InputParameters.IsKeyEscapeDown)
        {
            GridSelectionController.DeselectAll();
            return;
        }

        // Tab button pressed
        if (InputParameters.IsKeyTabDown)
        {
            GridSelectionController.DeselectAll();
            TurnController.SelectActiveCharacter();
        }

        // Clicked off grid
        if (!InputParameters.IsMouseOverGrid && InputParameters.IsLeftClickDown)
        {
            GridSelectionController.DeselectAll();
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
            GridSelectionController.DeselectAll();
            return;
        }

        // Hovered over disabled tile
        if (!tileIsEnabled)
        {
            return;
        }

        // Invariant: Target tile is enabled

        bool tileIsOccupied = InputParameters.TargetTile.IsOccupied();
        bool tileIsCurrentSelectedTile = GridSelectionController.IsSelectedTile(InputParameters.TargetTile);

        // Clicked unoccupied other tile
        if (InputParameters.IsLeftClickDown && !tileIsOccupied && !tileIsCurrentSelectedTile)
        {
            GridSelectionController.DeselectAll();
            InputParameters.TargetTile.Select();
            return;
        }

        // Clicked selected tile 
        if (InputParameters.IsLeftClickDown && tileIsCurrentSelectedTile)
        {
            InputParameters.TargetTile.Deselect();
            return;
        }

        // Clicked occupied other tile
        if (InputParameters.IsLeftClickDown)
        {
            GridSelectionController.DeselectAll();
            InputParameters.TargetTile.Select();
            return;
        }

        // Hovered over unoccupied tile
        if (!tileIsOccupied)
        {
            InputParameters.TargetTile.Hover();
            return;
        }

        // Hover over occupied tile
        InputParameters.TargetTile.Hover();
        return;
    }
}
