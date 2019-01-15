using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class MovementSelectionControllerTests
{
    MovementSelectionController sut;

    IGridSelectionController gridSelectionController;
    ISelectionManager selectionManager;

    ICharacterController selectedCharacter;

    IInputParameters inputParameters;

    IHexTileController selectedTile;
    IHexTileController targetTile;

    List<IHexTileController> pathList;

    [SetUp]
    public void Init()
    {
        gridSelectionController = Substitute.For<IGridSelectionController>();
        selectionManager = Substitute.For<ISelectionManager>();

        selectedCharacter = Substitute.For<ICharacterController>();
        selectedCharacter.CanMove(Arg.Any<int>()).Returns(true);

        gridSelectionController.GetSelectedCharacter().Returns(selectedCharacter);

        inputParameters = Substitute.For<IInputParameters>();

        selectedTile = Substitute.For<IHexTileController>();
        selectedTile.OccupantCharacter.Returns(selectedCharacter);

        targetTile = Substitute.For<IHexTileController>();
        targetTile.IsEnabled.Returns(true);
        targetTile.IsOccupied().Returns(false);

        inputParameters.TargetTile.Returns(targetTile);

        gridSelectionController.IsSelectedTile(targetTile).Returns(false);
        gridSelectionController.GetSelectedTile().Returns(selectedTile);

        pathList = new List<IHexTileController>() { selectedTile, targetTile };
        selectedTile.GetPath(targetTile).Returns(pathList);

        sut = new MovementSelectionController
        {
            GridSelectionController = gridSelectionController
        };
    }

    [Test]
    public void Pressing_escape_key_cancels_movement()
    {
        inputParameters.IsKeyEscapeDown.Returns(true);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
    }


    [Test]
    public void Clicking_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(false);
        inputParameters.IsLeftClickDown.Returns(true);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
    }

    [Test]
    public void Hovering_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(false);
        inputParameters.IsLeftClickDown.Returns(false);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_on_disabled_tile_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        targetTile.IsEnabled.Returns(false);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Hovering_over_disabled_tile_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTile.IsEnabled.Returns(false);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_on_unreachable_tile_does_nothing()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        selectedTile.GetPath(targetTile).Returns(new List<IHexTileController>());

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        selectionManager.DidNotReceive();
        targetTile.DidNotReceive();
        selectedCharacter.DidNotReceive();
    }

    [Test]
    public void Clicking_on_reachable_out_of_range_unoccupied_tile_does_nothing()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        selectedCharacter.CanMove(Arg.Any<int>()).Returns(false);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        selectionManager.DidNotReceive();
        selectedCharacter.DidNotReceive();
        targetTile.DidNotReceive();
    }

    [Test]
    public void Clicking_on_reachable_in_range_unoccupied_tile_moves_character()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        selectedCharacter.CanMove(pathList.Count - 1).Returns(true);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        selectedCharacter.Received(1).ExecuteMove(pathList);
    }

    [Test]
    public void Clicking_on_reachable_occupied_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        targetTile.IsOccupied().Returns(true);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        targetTile.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_selected_tile_does_nothing()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        gridSelectionController.IsSelectedTile(targetTile).Returns(true);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        selectionManager.DidNotReceive();
        selectedCharacter.DidNotReceive();
        targetTile.DidNotReceive();
    }

    [Test]
    public void Hovering_over_selected_tile_does_nothing()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        gridSelectionController.IsSelectedTile(targetTile).Returns(true);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        selectionManager.DidNotReceive();
        targetTile.DidNotReceive();
        selectedCharacter.DidNotReceive();
    }

    [Test]
    public void Hovering_over_unreachable_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        selectedTile.GetPath(targetTile).Returns(new List<IHexTileController>());

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        targetTile.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_reachable_out_of_range_unoccupied_tile_error_highlights_path()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        selectedCharacter.CanMove(Arg.Any<int>()).Returns(false);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        for (int i = 1; i < pathList.Count; i++)
        {
            pathList[i].Received(1).HoverError();
        }
    }

    [Test]
    public void Hovering_over_reachable_in_range_unoccupied_tile_error_highlights_path()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        for (int i = 1; i < pathList.Count; i++)
        {
            pathList[i].Received(1).Highlight();
        }
    }

    [Test]
    public void Hovering_over_reachable_occupied_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTile.IsOccupied().Returns(true);

        sut.Update(inputParameters);

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        targetTile.Received(1).HoverError();
    }
}