using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SelectionController : ISelectionController
{
    public ICharacter SelectedCharacter { get; set; }

    public IGameManager GameManager { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }
    public IGridTraversalController GridTraversalController { get; set; }
    public IStatPanel StatPanel { get; set; }
    public IPlayerPanel PlayerPanel { get; set; }

    public bool IsEscapeButtonDown { get; set; }
    public bool MouseIsOverGrid { get; set; }
    public bool IsLeftClickDown { get; set; }
    public IHexTile TargetTile { get; set; }
    
    public void Init(IGridTraversalController gridTraversalController, IGridSelectionController gridSelectionController)
    {
        GridSelectionController = gridSelectionController;
        GridTraversalController = gridTraversalController;
    }

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

    public void Update()
    {
        if (DebounceUpdate())
            return;

        Debug.Log("WOW");

        UpdateLastInputs();

        if (IsEscapeButtonDown) // Pressed escape to quit
        {
            GameManager.QuitApplication();
        }

        if (!MouseIsOverGrid && IsLeftClickDown) // Clicked off grid
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.DeselectAll();
            StatPanel.Controller.DisableStatDisplays();
            PlayerPanel.ClearPlayerName();
            return;
        }

        if (!MouseIsOverGrid) // Hovered off grid
        {
            GridSelectionController.ScrubPathAll();
            return;
        }

        // Invariant: Mouse is over grid
        
        bool tileIsEnabled = TargetTile.Controller.IsEnabled;

        if (!tileIsEnabled && IsLeftClickDown) // Clicked on disabled tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.DeselectAll();
            StatPanel.Controller.DisableStatDisplays();
            PlayerPanel.ClearPlayerName();
            return;
        }

        if (!tileIsEnabled) // Hovered over disabled tile
        {
            GridSelectionController.ScrubPathAll();
            return;
        }

        // Invariant: Target tile is enabled

        bool characterIsSelected = SelectedCharacter != null;
        bool tileIsOccupied = TargetTile.Controller.OccupantCharacter != null;
        bool tileIsCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0 && GridSelectionController.SelectedTiles[0] == TargetTile;

        if (IsLeftClickDown && !characterIsSelected && !tileIsOccupied && !tileIsCurrentSelectedTile) // Clicked unoccupied other tile w/o character selected
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Select();
            return;
        }

        if (IsLeftClickDown && !characterIsSelected && !tileIsOccupied) // Clicked unoccupied selected tile w/o character selected
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Deselect();
            return;
        }

        if (IsLeftClickDown && !characterIsSelected) // Clicked occupied tile w/o character selected
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Select();
            StatPanel.Controller.EnableStatDisplays();
            StatPanel.Controller.SetCharacter(TargetTile.Controller.OccupantCharacter);
            StatPanel.Controller.UpdateStatNames();
            StatPanel.Controller.UpdateStatValues();
            PlayerPanel.SetPlayerName($"Player {TargetTile.Controller.OccupantCharacter.Controller.OwnedByPlayer + 1}");
            return;
        }

        if (!characterIsSelected) // Hovered over tile w/o character selected
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Hover();
            return;
        }

        // Invariant: Character is selected

        List<IHexTile> path = GridTraversalController.GetPath(SelectedCharacter.GetOccupiedTile(), TargetTile);
        bool isReachable = path.Count > 0;

        if (IsLeftClickDown && !tileIsCurrentSelectedTile && !isReachable) // Clicked on unreachable tile
        {
            GridSelectionController.ScrubPathAll();
            TargetTile.Controller.HoverError();
            return;
        }

        if (IsLeftClickDown && !tileIsCurrentSelectedTile && !tileIsOccupied) // Clicked reachable unoccupied tile
        {
            GridSelectionController.ScrubPathAll();
            SelectedCharacter.Controller.MoveToTile(TargetTile);
            return;
        }

        if (IsLeftClickDown && !tileIsCurrentSelectedTile) // Clicked reachable occupied tile
        {
            GridSelectionController.ScrubPathAll();
            TargetTile.Controller.HoverError();
            return;
        }

        if (IsLeftClickDown && tileIsCurrentSelectedTile) // Clicked current selected tile
        {
            GridSelectionController.ScrubPathAll();
            TargetTile.Controller.Deselect();
            StatPanel.Controller.DisableStatDisplays();
            PlayerPanel.ClearPlayerName();
            return;
        }

        // Invariant: Left mouse button not clicked        

        if (!isReachable) // Hovered over unreachable tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            TargetTile.Controller.HoverError();
            return;
        }

        if (!tileIsOccupied) // Hovered over reachable unoccupied tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            GridSelectionController.HighlightPath(path);
            return;
        }

        // Hovered over reachable occupied tile
        GridSelectionController.ScrubPathAll();
        GridSelectionController.BlurAll();
        TargetTile.Controller.HoverError();
        return;

    }
}
