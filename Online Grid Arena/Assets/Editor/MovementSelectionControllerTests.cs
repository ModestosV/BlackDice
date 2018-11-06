using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class MovementSelectionControllerTests
{
    MovementSelectionController sut;

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
    List<IHexTile> pathList;

    [SetUp]
    public void Init()
    {
        sut = new MovementSelectionController();

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
        pathList = new List<IHexTile>() { selectedTile, targetTile };

        gridSelectionController.SelectedTiles.Returns(selectedTiles);
        gridTraversalController.GetPath(selectedTile, targetTile).Returns(pathList);
        gridTraversalController.GetPath(targetTile, targetTile).Returns(new List<IHexTile>() { targetTile });

        sut.HUDController = hudController;
        sut.GridSelectionController = gridSelectionController;
        sut.GridTraversalController = gridTraversalController;
        sut.GameManager = gameManager;
        sut.InputParameters = inputParameters;
    }

    [Test]
    public void Pressing_escape_key_cancels_movement()
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
    public void Clicking_on_unreachable_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        gridTraversalController.GetPath(selectedTile, targetTile).Returns(new List<IHexTile>());

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_reachable_unoccupied_tile_moves_character()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.Received().SelectionMode = SelectionMode.SELECTION;
        selectedCharacterController.Received(1).ExecuteMove(targetTile);
    }

    [Test]
    public void Clicking_on_reachable_occupied_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_selected_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>() { targetTile });

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_selected_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>() { targetTile });

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_unreachable_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        gridTraversalController.GetPath(selectedTile, targetTile).Returns(new List<IHexTile>());

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_reachable_unoccupied_tile_highlights_path()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
        gridSelectionController.HighlightPath(pathList);
    }

    [Test]
    public void Hovering_over_reachable_occupied_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
        targetTileController.Received(1).HoverError();
    }    
}
