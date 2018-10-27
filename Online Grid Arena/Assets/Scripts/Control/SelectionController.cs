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
    public ITurnController TurnController { get; set; }

    public bool IsEscapeButtonDown { get; set; }
    public bool MouseIsOverGrid { get; set; }
    public bool IsLeftClickDown { get; set; }
    public IHexTile TargetTile { get; set; }
    
    public void Init(IGridTraversalController gridTraversalController, IGridSelectionController gridSelectionController)
    {
        GridSelectionController = gridSelectionController;
        GridTraversalController = gridTraversalController;
    }

    public void Update()
    {
        // Pressed escape to quit
        if (IsEscapeButtonDown)
        {
            GameManager.QuitApplication();
        }

        // Clicked off grid
        if (!MouseIsOverGrid && IsLeftClickDown)
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.DeselectAll();
            StatPanel.Controller.DisableStatDisplays();
            PlayerPanel.ClearPlayerName();
            return;
        }

        // Hovered off grid
        if (!MouseIsOverGrid)
        {
            GridSelectionController.ScrubPathAll();
            return;
        }

        // Invariant: Mouse is over grid
        
        bool tileIsEnabled = TargetTile.Controller.IsEnabled;

        // Clicked on disabled tile
        if (!tileIsEnabled && IsLeftClickDown)
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.DeselectAll();
            StatPanel.Controller.DisableStatDisplays();
            PlayerPanel.ClearPlayerName();
            return;
        }

        // Hovered over disabled tile
        if (!tileIsEnabled)
        {
            GridSelectionController.ScrubPathAll();
            return;
        }

        // Invariant: Target tile is enabled

        bool characterIsSelected = SelectedCharacter != null;
        bool tileIsOccupied = TargetTile.Controller.OccupantCharacter != null;
        bool tileIsCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0 && GridSelectionController.SelectedTiles[0] == TargetTile;

        // Clicked unoccupied other tile w/o character selected
        if (IsLeftClickDown && !characterIsSelected && !tileIsOccupied && !tileIsCurrentSelectedTile)
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Select();
            return;
        }

        // Clicked unoccupied selected tile w/o character selected
        if (IsLeftClickDown && !characterIsSelected && !tileIsOccupied)
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Deselect();
            return;
        }

        // Clicked occupied tile w/o character selected
        if (IsLeftClickDown && !characterIsSelected)
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

        // Hovered over tile w/o character selected
        if (!characterIsSelected)
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Hover();
            return;
        }

        // Invariant: Character is selected

        List<IHexTile> path = GridTraversalController.GetPath(SelectedCharacter.Controller.OccupiedTile, TargetTile);
        bool isReachable = path.Count > 0;
        bool isSelectedCharacterActive = SelectedCharacter == TurnController.ActiveCharacter;

        // Clicked on unreachable tile
        if (IsLeftClickDown && !tileIsCurrentSelectedTile && !isReachable && isSelectedCharacterActive)
        {
            GridSelectionController.ScrubPathAll();
            TargetTile.Controller.HoverError();
            return;
        }

        // Clicked reachable unoccupied tile
        if (IsLeftClickDown && !tileIsCurrentSelectedTile && !tileIsOccupied && isSelectedCharacterActive)
        {
            GridSelectionController.ScrubPathAll();
            SelectedCharacter.Controller.MoveToTile(TargetTile);
            return;
        }

        // Clicked reachable occupied tile
        if (IsLeftClickDown && !tileIsCurrentSelectedTile && isSelectedCharacterActive)
        {
            GridSelectionController.ScrubPathAll();
            TargetTile.Controller.HoverError();
            return;
        }

        // Clicked current selected tile
        if (IsLeftClickDown && tileIsCurrentSelectedTile)
        {
            GridSelectionController.ScrubPathAll();
            TargetTile.Controller.Deselect();
            StatPanel.Controller.DisableStatDisplays();
            PlayerPanel.ClearPlayerName();
            return;
        }

        // Invariant: Left mouse button not clicked        

        // Hovered over unreachable tile
        if (!isReachable && isSelectedCharacterActive)
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            TargetTile.Controller.HoverError();
            return;
        }

        // Hovered over reachable unoccupied tile
        if (!tileIsOccupied && isSelectedCharacterActive)
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            GridSelectionController.HighlightPath(path);
            return;
        }

        // Hovered over reachable occupied tile
        if (isSelectedCharacterActive)
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            TargetTile.Controller.HoverError();
            return;
        }

        // Clicked on other occupied tile with inactive character selected
        if (!isSelectedCharacterActive && !tileIsCurrentSelectedTile && IsLeftClickDown && tileIsOccupied)  
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Select();
            StatPanel.Controller.SetCharacter(TargetTile.Controller.OccupantCharacter);
            StatPanel.Controller.UpdateStatNames();
            StatPanel.Controller.UpdateStatValues();
            PlayerPanel.SetPlayerName($"Player {TargetTile.Controller.OccupantCharacter.Controller.OwnedByPlayer + 1}");
            return;
        }

        // Clicked on other unoccupied tile with inactive character selected
        if (!isSelectedCharacterActive && !tileIsCurrentSelectedTile && IsLeftClickDown)
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Select();
            StatPanel.Controller.DisableStatDisplays();
            PlayerPanel.ClearPlayerName();
            return;
        }

        // Hovered over other tile with inactive character selected
        if (!isSelectedCharacterActive && !tileIsCurrentSelectedTile)
        {
            GridSelectionController.BlurAll();
            TargetTile.Controller.Hover();
            return;
        }
    }
}
