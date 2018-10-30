using System;

[Serializable]
public class AbilitySelectionController : InputController, IAbilitySelectionController
{
    public IHUDController HUDController { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }
    public IGridTraversalController GridTraversalController { get; set; }
    public IGameManager GameManager { get; set; }

    private int activeAbilityNumber;

    private void ActivateAbility()
    {
        int inputAbilityNumber = InputParameters.GetAbilityNumber();
        if (inputAbilityNumber > -1)
        {
            activeAbilityNumber = inputAbilityNumber;
        }
    }

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

        if (!InputParameters.IsMouseOverGrid) // Hovered off grid
        {
            return;
        }

        // Invariant: Mouse is over grid

        bool tileIsEnabled = InputParameters.TargetTile.Controller.IsEnabled;

        if (!tileIsEnabled && InputParameters.IsLeftClickDown) // Clicked on disabled tile
        {
            return;
        }

        if (!tileIsEnabled) // Hovered over disabled tile
        {
            return;
        }

        // Invariant: Target tile is enabled


        IHexTile selectedTile = GridSelectionController.SelectedTiles[0];
        ICharacter selectedCharacter = selectedTile.Controller.OccupantCharacter;
        ICharacter targetCharacter = InputParameters.TargetTile.Controller.OccupantCharacter;

        bool tileIsOccupied = InputParameters.TargetTile.Controller.OccupantCharacter != null;
        bool tileIsCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0 
            && selectedTile == InputParameters.TargetTile;

        if (InputParameters.IsLeftClickDown && !tileIsOccupied ) // Clicked unoccupied other tile
        {
            return;
        }

        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile) // Clicked occupied other tile
        {
            selectedCharacter.Controller.ExecuteAbility(activeAbilityNumber, targetCharacter);
            GameManager.SelectionMode = SelectionMode.SELECTION;
            return;
        }

        // Invariant: Left-click is not down

        // Hovered over unoccupied tile
        if (!tileIsOccupied)
        {
            InputParameters.TargetTile.Controller.HoverError();
            HUDController.ClearTargetHUD();
            return;
        }

        // Hover over occupied tile
        InputParameters.TargetTile.Controller.MarkPath();
        HUDController.UpdateTargetHUD(InputParameters.TargetTile.Controller.OccupantCharacter);
        return;
    }
}
