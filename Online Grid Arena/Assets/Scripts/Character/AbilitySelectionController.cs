using System;

[Serializable]
public class AbilitySelectionController : IAbilitySelectionController
{
    public bool IsEscapeButtonDown { get; set; }
    public bool MouseIsOverGrid { get; set; }
    public bool IsLeftClickDown { get; set; }
    public IHexTile TargetTile { get; set; }

    public IGridSelectionController GridSelectionController { get; set; }
    public IGridTraversalController GridTraversalController { get; set; }
    public IAbilityExecutionController AbilityExecutionController { get; set; }

    public ISelectionController SelectionController { get; set; }

    private bool lastIsEscapeButtonDown;
    private bool lastMouseIsOverGrid;
    private bool lastIsLeftClickDown;
    private IHexTile lastTargetTile;

    private bool DebounceUpdate()
    {
        if (IsEscapeButtonDown != lastIsEscapeButtonDown)
            return false;
        if (MouseIsOverGrid != lastMouseIsOverGrid)
            return false;
        if (IsLeftClickDown != lastIsLeftClickDown)
            return false;
        if (TargetTile != lastTargetTile)
            return false;
        return true;
    }

    private void UpdateLastInputs()
    {
        lastIsEscapeButtonDown = IsEscapeButtonDown;
        lastMouseIsOverGrid = MouseIsOverGrid;
        lastIsLeftClickDown = IsLeftClickDown;
        lastTargetTile = TargetTile;
    }

    public void Update(IAbility selectedAbility)
    {
        if (DebounceUpdate())
            return;

        // Escape buton pressed
        if (IsEscapeButtonDown)
        {
            SelectionController.SelectedAbility = null;
            return;
        }

        if (!MouseIsOverGrid && IsLeftClickDown) // Clicked off grid
        {
            SelectionController.SelectedAbility = null;
            return;
        }

        if (!MouseIsOverGrid) // Hovered off grid
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            return;
        }

        // Invariant: Mouse is over grid

        bool tileIsEnabled = TargetTile.Controller.IsEnabled;

        if (!tileIsEnabled && IsLeftClickDown) // Clicked on disabled tile
        {
            SelectionController.SelectedAbility = null;
            return;
        }

        if (!tileIsEnabled) // Hovered over disabled tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            return;
        }

        // Invariant: Target tile is enabled
        
        bool tileIsOccupied = TargetTile.Controller.OccupantCharacter != null;
        //bool tileIsCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0 && GridSelectionController.SelectedTiles[0] == TargetTile;

        if (IsLeftClickDown && !tileIsOccupied ) // Clicked unoccupied other tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            TargetTile.Controller.HoverError();
            return;
        }

        if (IsLeftClickDown) // Clicked occupied tile
        {
            AbilityExecutionController.ExecuteAbility(selectedAbility, TargetTile.Controller.OccupantCharacter);
            return;
        }

        // Invariant: Left-click is not down

        if (!tileIsOccupied) // Hovered over unoccupied tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            TargetTile.Controller.HoverError();
            return;
        }

        // Hovered over occupied tile
        GridSelectionController.ScrubPathAll();
        GridSelectionController.BlurAll();
        TargetTile.Controller.MarkPath();
        return;
    }
}
