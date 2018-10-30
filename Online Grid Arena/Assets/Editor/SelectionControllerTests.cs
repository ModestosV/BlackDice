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
    IAbility ability;
    IAbilitySelectionController abilitySelectionController;

    IStatPanelController statPanelController;
    IHexTileController targetHexTileController;

    List<IHexTile> selectedHexTileList;
    List<IHexTile> pathHexTileList;

    InputParameters inputParameters;

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
        ability = Substitute.For<IAbility>();
        abilitySelectionController = Substitute.For<IAbilitySelectionController>();

        statPanelController = Substitute.For<IStatPanelController>();
        statPanel.Controller.Returns(statPanelController);

        targetHexTileController = Substitute.For<IHexTileController>();
        targetHexTile.Controller.Returns(targetHexTileController);

        targetHexTileController.OccupantCharacter.Returns(otherCharacter);

        selectedHexTileList = new List<IHexTile>() { otherCharacterHexTile };
        gridSelectionController.SelectedTiles.Returns(selectedHexTileList);
        
        selectedCharacter.Controller.Returns(selectedCharacterController);
        selectedCharacterController.OccupiedTile.Returns(selectCharacterHexTile);

        otherCharacter.Controller.Returns(otherCharacterController);
        otherCharacterController.OccupiedTile.Returns(otherCharacterHexTile);
        otherCharacterController.OwnedByPlayer.Returns(PLAYER_ID);

        otherCharacter.Controller.OccupiedTile.Returns(selectCharacterHexTile);
        pathHexTileList = new List<IHexTile>() { selectCharacterHexTile, targetHexTile };

        gridTraversalController.GetPath(selectCharacterHexTile, targetHexTile).Returns(pathHexTileList);

        turnController.ActiveCharacter.Returns(selectedCharacter);

        inputParameters = new InputParameters()
        {
            //TargetTile = targetTile
        };
        
        sut.GridSelectionController = gridSelectionController;
    }

    [Test]
    public void Quit_when_escape_button_down()
    {
        inputParameters.IsKeyEscapeDown = true;

        sut.Update();
        
        gameManager.Received(1).QuitApplication();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.DidNotReceive();
    }

    [Test]
    public void Clicking_off_grid_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).DisableStatDisplays();
        playerPanel.Received(1).ClearPlayerName();
        targetHexTileController.DidNotReceive();
    }

    [Test]
    public void Hovering_off_grid_scrubs_all_paths()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = false;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.DidNotReceive();
    }

    [Test]
    public void Clicking_on_disabled_tile_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = false;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).DisableStatDisplays();
        playerPanel.Received(1).ClearPlayerName();
        targetHexTileController.DidNotReceive();
    }

    [Test]
    public void Hovering_over_disabled_tile_scrubs_all_paths()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = false;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.DidNotReceive();
    }

    [Test]
    public void Clicking_on_unoccupied_other_tile_without_character_selected_selects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        targetHexTileController.OccupantCharacter.Returns(nullCharacter);
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>());

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).Select();
    }

    [Test]
    public void Clicking_on_unoccupied_current_tile_without_character_selected_deselects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        targetHexTileController.OccupantCharacter.Returns(nullCharacter);
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>() { targetHexTile });

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).Deselect();
    }

    [Test]
    public void Clicking_on_occupied_current_tile_without_character_selected_selects_tile_and_character()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).EnableStatDisplays();
        statPanelController.Received(1).SetCharacter(otherCharacter);
        statPanelController.Received(1).UpdateStatNames();
        statPanelController.Received(1).UpdateStatValues();
        playerPanel.Received(1).SetPlayerName(PLAYER_NAME_PREFIX + (PLAYER_ID + 1));
        targetHexTileController.Received(1).Select();
    }

    [Test]
    public void Hovering_over_tile_without_character_selected_hover_highlights_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).Hover();
    }

    [Test]
    public void Clicking_on_unreachable_tile_with_character_selected_error_highlights_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        gridTraversalController.GetPath(selectCharacterHexTile, targetHexTile).Returns(new List<IHexTile>());

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_reachable_unoccupied_tile_with_active_character_selected_moves_character_to_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        targetHexTileController.OccupantCharacter.Returns(nullCharacter);

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
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_current_selected_tile_deselects_tile_and_character()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>() { targetHexTile });

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).DisableStatDisplays();
        playerPanel.Received(1).ClearPlayerName();
        targetHexTileController.Received(1).Deselect();
    }

    [Test]
    public void Hovering_over_unreachable_tile_with_character_selected_error_highlights_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;
        gridTraversalController.GetPath(selectCharacterHexTile, targetHexTile).Returns(new List<IHexTile>());

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_reachable_unoccupied_tile_with_character_selected_highlights_path()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;
        targetHexTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).HighlightPath(pathHexTileList);
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.DidNotReceive();
    }


    [Test]
    public void Hovering_over_reachable_occupied_tile_with_character_selected_error_highlights_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;

        sut.Update();
        
        gameManager.DidNotReceive();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_other_occupied_tile_with_inactive_character_selected_selects_tile_and_character()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        turnController.ActiveCharacter.Returns(otherCharacter);

        sut.Update();

        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).SetCharacter(otherCharacter);
        statPanelController.Received(1).UpdateStatNames();
        statPanelController.Received(1).UpdateStatValues();
        playerPanel.Received(1).SetPlayerName(PLAYER_NAME_PREFIX + (PLAYER_ID + 1));
        targetHexTileController.Received(1).Select();
    }

    [Test]
    public void Clicking_on_other_unoccupied_tile_with_inactive_character_selected_selects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetHexTile.Controller.IsEnabled = true;
        turnController.ActiveCharacter.Returns(otherCharacter);
        targetHexTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.Received(1).DisableStatDisplays();
        playerPanel.Received(1).ClearPlayerName();
        targetHexTileController.Received(1).Select();
    }

    [Test]
    public void Hovering_over_other_tile_with_inactive_character_selected_hover_highlights_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetHexTile.Controller.IsEnabled = true;
        turnController.ActiveCharacter.Returns(otherCharacter);

        sut.Update();

        gameManager.DidNotReceive();
        gridSelectionController.Received(1).BlurAll();
        gridTraversalController.DidNotReceive();
        statPanelController.DidNotReceive();
        playerPanel.DidNotReceive();
        targetHexTileController.Received(1).Hover();
    }

    [Test]
    public void Selection_controller_delegates_to_ability_selection_controller_when_ability_selected()
    {
        inputParameters.IsKeyEscapeDown = true;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;

        sut.Update();
    }
}

