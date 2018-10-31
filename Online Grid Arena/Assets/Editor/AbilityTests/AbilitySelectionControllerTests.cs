using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class AbilitySelectionControllerTests
{
    AbilitySelectionController sut;

    IHUDController hudController;
    IStatPanel selectedStatPanel;
    IStatPanelController selectedStatPanelController;
    IStatPanel targetStatPanel;
    IStatPanelController targetStatPanelController;

    IGridSelectionController gridSelectionController;
    IGridTraversalController gridTraversalController;
    IGameManager gameManager;

    ICharacter selectedCharacter;
    ICharacterController selectedCharacterController;
    ICharacter targetCharacter;
    ICharacterController targetCharacterController;
    ICharacter nullCharacter = null;
    
    IInputParameters inputParameters;

    IHexTile selectedTile;
    IHexTileController selectedTileController;
    IHexTile targetTile;
    IHexTileController targetTileController;

    List<IHexTile> selectedTiles;
    List<IAbility> abilitiesList;
    IAbility ability;

    const int ACTIVE_ABILITY_NUMBER = 0;

    [SetUp]
    public void Init()
    {
        sut = new AbilitySelectionController();
        
        hudController = Substitute.For<IHUDController>();
        selectedStatPanel = Substitute.For<IStatPanel>();
        selectedStatPanelController = Substitute.For<IStatPanelController>();
        selectedStatPanel.Controller.Returns(selectedStatPanelController);
        targetStatPanel = Substitute.For<IStatPanel>();
        targetStatPanelController = Substitute.For<IStatPanelController>();
        targetStatPanel.Controller.Returns(targetStatPanelController);
        hudController.SelectedStatPanel.Returns(selectedStatPanel);
        hudController.TargetStatPanel.Returns(targetStatPanel);

        gridSelectionController = Substitute.For<IGridSelectionController>();
        gridTraversalController = Substitute.For<IGridTraversalController>();
        gameManager = Substitute.For<IGameManager>();

        selectedCharacter = Substitute.For<ICharacter>();
        selectedCharacterController = Substitute.For<ICharacterController>();
        selectedCharacter.Controller.Returns(selectedCharacterController);
        targetCharacter = Substitute.For<ICharacter>();
        targetCharacterController = Substitute.For<ICharacterController>();
        targetCharacter.Controller.Returns(targetCharacterController);

        inputParameters = Substitute.For<IInputParameters>();
        inputParameters.GetAbilityNumber().Returns(ACTIVE_ABILITY_NUMBER);

        selectedTile = Substitute.For<IHexTile>();
        selectedTileController = Substitute.For<IHexTileController>();
        selectedTile.Controller.Returns(selectedTileController);
        selectedTileController.OccupantCharacter.Returns(selectedCharacter);

        targetTile = Substitute.For<IHexTile>();
        targetTileController = Substitute.For<IHexTileController>();
        targetTile.Controller.Returns(targetTileController);
        targetTileController.OccupantCharacter.Returns(targetCharacter);
        targetTileController.IsEnabled.Returns(true);

        inputParameters.TargetTile.Returns(targetTile);

        selectedTiles = new List<IHexTile>() { selectedTile };
        gridSelectionController.SelectedTiles.Returns(selectedTiles);

        ability = Substitute.For<IAbility>();
        abilitiesList = new List<IAbility>() { ability };
        selectedCharacterController.Abilities.Returns(abilitiesList);

        sut.HUDController = hudController;
        sut.GridSelectionController = gridSelectionController;
        sut.GridTraversalController = gridTraversalController;
        sut.GameManager = gameManager;
        sut.InputParameters = inputParameters;
    }

    [Test]
    public void Pressing_escape_key_cancels_ability()
    {
        inputParameters.IsKeyEscapeDown.Returns(true);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        gameManager.Received(1).SelectionMode = SelectionMode.SELECTION;
    }


    [Test]
    public void Clicking_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(false);
        inputParameters.IsLeftClickDown.Returns(true);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Hovering_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(false);
        inputParameters.IsLeftClickDown.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Clicking_on_disabled_tile_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        targetTileController.IsEnabled.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Hovering_over_disabled_tile_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTileController.IsEnabled.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Clicking_on_unoccupied_tile_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Clicking_on_occupied_other_tile_executes_ability_and_updates_hud()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        selectedCharacterController.Received(1).ExecuteAbility(ACTIVE_ABILITY_NUMBER, targetCharacter);
        targetStatPanelController.Received(1).UpdateStatValues();
        gameManager.Received(1).SelectionMode = SelectionMode.SELECTION;
    }

    [Test]
    public void Hovering_over_unoccupied_tile_error_highlights_tile_and_clears_target_hud()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).HoverError();
        hudController.Received(1).ClearTargetHUD();
    }

    [Test]
    public void Hovering_over_occupied_tile_highlights_tile_and_updates_target_hud()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).MarkPath();
        hudController.Received(1).UpdateTargetHUD(targetCharacter);
    }
}
