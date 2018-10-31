using System;
using UnityEngine;

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
        ICharacter selectedCharacter = GridSelectionController.SelectedTiles[0].Controller.OccupantCharacter;

        if (inputAbilityNumber > -1 && inputAbilityNumber < selectedCharacter.Controller.Abilities.Count)
        {
            activeAbilityNumber = inputAbilityNumber;
        }
    }

    public override void Update()
    {
        if (DebounceUpdate())
            return;

        ActivateAbility();

        GridSelectionController.BlurAll();
        GridSelectionController.ScrubPathAll();

        IHexTile selectedTile = GridSelectionController.SelectedTiles[0];
        ICharacter selectedCharacter = selectedTile.Controller.OccupantCharacter;
        ICharacter targetCharacter = InputParameters.TargetTile.Controller.OccupantCharacter;

        bool tileIsOccupied = InputParameters.TargetTile.Controller.OccupantCharacter != null;
        bool tileIsCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0 
            && selectedTile == InputParameters.TargetTile;

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


        // Clicked unoccupied tile
        if (InputParameters.IsLeftClickDown && !tileIsOccupied )
        {
            return;
        }

        // Clicked occupied other tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile)
        {
            selectedCharacter.Controller.ExecuteAbility(activeAbilityNumber, targetCharacter);
            HUDController.TargetStatPanel.Controller.UpdateStatValues();
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

        // Hovered over occupied tile
        InputParameters.TargetTile.Controller.MarkPath();
        HUDController.UpdateTargetHUD(InputParameters.TargetTile.Controller.OccupantCharacter);
        return;
    }
}
