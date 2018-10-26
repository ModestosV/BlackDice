using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class SelectionControllerTests
{
    SelectionController sut;

    ICharacter selectedCharacter;
    ICharacterController selectedCharacterController;
    ICharacter otherCharacter;
    ICharacterController otherCharacterController;
    ICharacter nullCharacter = null;
    IGameManager gameManager;
    IGridSelectionController gridSelectionController;
    IGridTraversalController gridTraversalController;
    IStatPanel statPanel;
    IPlayerPanel playerPanel;
    ITurnController turnController;
    IHexTile targetHexTile;
    IHexTile otherCharacterHexTile;
    IHexTile selectCharacterHexTile;

    IStatPanelController statPanelController;
    IHexTileController hexTileController;

    List<IHexTile> selectedHexTileList;
    List<IHexTile> pathHexTileList;

    const string PLAYER_NAME_PREFIX = "Player ";
    const int PLAYER_ID = 1;

    [SetUp]
    public void Init()
    {
        sut = new SelectionController();

        selectedCharacter = Substitute.For<ICharacter>();
        selectedCharacterController = Substitute.For<ICharacterController>();
        otherCharacter = Substitute.For<ICharacter>();
        otherCharacterController = Substitute.For<ICharacterController>();
        gameManager = Substitute.For<IGameManager>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gridTraversalController = Substitute.For<IGridTraversalController>();
        statPanel = Substitute.For<IStatPanel>();
        playerPanel = Substitute.For<IPlayerPanel>();
        turnController = Substitute.For<ITurnController>();
        targetHexTile = Substitute.For<IHexTile>();
        otherCharacterHexTile = Substitute.For<IHexTile>();
        selectCharacterHexTile = Substitute.For<IHexTile>();

        statPanelController = Substitute.For<IStatPanelController>();
        statPanel.Controller.Returns(statPanelController);

        hexTileController = Substitute.For<IHexTileController>();
        targetHexTile.Controller.Returns(hexTileController);

        hexTileController.OccupantCharacter.Returns(otherCharacter);

        selectedHexTileList = new List<IHexTile>() { otherCharacterHexTile };
        gridSelectionController.SelectedTiles.Returns(selectedHexTileList);
        
        selectedCharacter.Controller.Returns(selectedCharacterController);
        selectedCharacterController.OccupiedTile.Returns(selectCharacterHexTile);

        otherCharacter.Controller.Returns(otherCharacterController);
        otherCharacterController.OwnedByPlayer.Returns(PLAYER_ID);

        otherCharacter.Controller.OccupiedTile.Returns(selectCharacterHexTile);
        pathHexTileList = new List<IHexTile>() { selectCharacterHexTile, targetHexTile };

        gridTraversalController.GetPath(selectCharacterHexTile, targetHexTile).Returns(pathHexTileList);

        turnController.ActiveCharacter.Returns(selectedCharacter);

        sut.SelectedCharacter = selectedCharacter;
        sut.GameManager = gameManager;
        sut.GridSelectionController = gridSelectionController;
        sut.GridTraversalController = gridTraversalController;
        sut.StatPanel = statPanel;
        sut.PlayerPanel = playerPanel;
        sut.TargetTile = targetHexTile;
        sut.TurnController = turnController;
    }

    [Test]
    public void Quit_when_escape_button_down()
    {
        sut.IsEscapeButtonDown = true;

        sut.Update();
        
        gameManager.Received(1).QuitApplication();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.DidNotReceive();
    }

    [Test]
    public void Clicking_off_grid_deselects_all()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = false;
        sut.IsLeftClickDown = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).DisableStatDisplays();
        playerPanel.Received(1).ClearPlayerName();
        hexTileController.DidNotReceive();
    }

    [Test]
    public void Hovering_off_grid_scrubs_all_paths()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = false;
        sut.IsLeftClickDown = false;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.DidNotReceive();
    }

    [Test]
    public void Clicking_on_disabled_tile_deselects_all()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = false;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).DisableStatDisplays();
        playerPanel.Received(1).ClearPlayerName();
        hexTileController.DidNotReceive();
    }

    [Test]
    public void Hovering_over_disabled_tile_scrubs_all_paths()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = false;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.DidNotReceive();
    }

    [Test]
    public void Clicking_on_unoccupied_other_tile_without_character_selected_selects_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        sut.SelectedCharacter = null;
        hexTileController.OccupantCharacter.Returns(nullCharacter);
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>());

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.Received(1).Select();
    }

    [Test]
    public void Clicking_on_unoccupied_current_tile_without_character_selected_deselects_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        sut.SelectedCharacter = null;
        hexTileController.OccupantCharacter.Returns(nullCharacter);
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>() { targetHexTile });

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.Received(1).Deselect();
    }

    [Test]
    public void Clicking_on_occupied_current_tile_without_character_selected_selects_tile_and_character()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        sut.SelectedCharacter = null;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).EnableStatDisplays();
        statPanelController.Received(1).SetCharacter(otherCharacter);
        statPanelController.Received(1).UpdateStatNames();
        statPanelController.Received(1).UpdateStatValues();
        playerPanel.Received(1).SetPlayerName(PLAYER_NAME_PREFIX + (PLAYER_ID + 1));
        hexTileController.Received(1).Select();
    }

    [Test]
    public void Hovering_over_tile_without_character_selected_hover_highlights_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;
        sut.SelectedCharacter = null;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.Received(1).Hover();
    }

    [Test]
    public void Clicking_on_unreachable_tile_with_character_selected_error_highlights_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        gridTraversalController.GetPath(selectCharacterHexTile, targetHexTile).Returns(new List<IHexTile>());

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_reachable_unoccupied_tile_with_active_character_selected_moves_character_to_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        hexTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        selectedCharacterController.Received(1).MoveToTile(targetHexTile);
    }

    [Test]
    public void Clicking_on_reachable_occupied_tile_with_character_selected_error_highlights_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_current_selected_tile_deselects_tile_and_character()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>() { targetHexTile });

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).DisableStatDisplays();
        playerPanel.Received(1).ClearPlayerName();
        hexTileController.Received(1).Deselect();
    }

    [Test]
    public void Hovering_over_unreachable_tile_with_character_selected_error_highlights_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;
        gridTraversalController.GetPath(selectCharacterHexTile, targetHexTile).Returns(new List<IHexTile>());

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_reachable_unoccupied_tile_with_character_selected_highlights_path()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;
        hexTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).HighlightPath(pathHexTileList);
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.DidNotReceive();
    }


    [Test]
    public void Hovering_over_reachable_occupied_tile_with_character_selected_error_highlights_tile()
    {
        sut.IsEscapeButtonDown = false;
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        hexTileController.Received(1).HoverError();
    }
}

