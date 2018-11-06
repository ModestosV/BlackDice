using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class AbilitySelectionControllerTests
{
    AbilitySelectionController sut;

    IGridSelectionController gridSelectionController;
    IGameManager gameManager;
    
    ICharacterController selectedCharacterController;
    ICharacterController targetCharacterController;
    
    IInputParameters inputParameters;

    IHexTileController selectedTileController;
    IHexTileController targetTileController;

    const int ACTIVE_ABILITY_NUMBER = 0;

    [SetUp]
    public void Init()
    {
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gameManager = Substitute.For<IGameManager>();
        
        selectedCharacterController = Substitute.For<ICharacterController>();
        targetCharacterController = Substitute.For<ICharacterController>();

        gridSelectionController.GetSelectedCharacter().Returns(selectedCharacterController);

        inputParameters = Substitute.For<IInputParameters>();
        inputParameters.GetAbilityNumber().Returns(ACTIVE_ABILITY_NUMBER);
        
        selectedTileController = Substitute.For<IHexTileController>();
        selectedTileController.OccupantCharacter.Returns(selectedCharacterController);
        
        targetTileController = Substitute.For<IHexTileController>();
        targetTileController.IsEnabled.Returns(true);
        targetTileController.IsOccupied().Returns(false);

        inputParameters.TargetTile.Returns(targetTileController);

        gridSelectionController.IsSelectedTile(targetTileController).Returns(false);

        sut = new AbilitySelectionController
        {
            GridSelectionController = gridSelectionController,
            GameManager = gameManager,
            InputParameters = inputParameters
        };
    }

    [Test]
    public void Pressing_escape_key_cancels_ability()
    {
        inputParameters.IsKeyEscapeDown.Returns(true);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        gameManager.Received(1).SelectionMode = SelectionMode.SELECTION;
    }


    [Test]
    public void Clicking_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(false);
        inputParameters.IsLeftClickDown.Returns(true);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Hovering_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(false);
        inputParameters.IsLeftClickDown.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
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

        gridSelectionController.Received(1).DehighlightAll();
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

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Clicking_on_unoccupied_tile_clears_highlighted_tiles()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        gameManager.DidNotReceive().SelectionMode = Arg.Any<SelectionMode>();
    }

    [Test]
    public void Clicking_on_occupied_other_tile_executes_ability_and_returns_to_selection_mode()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        targetTileController.IsOccupied().Returns(true);
        targetTileController.OccupantCharacter.Returns(targetCharacterController);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        selectedCharacterController.Received(1).ExecuteAbility(ACTIVE_ABILITY_NUMBER, targetCharacterController);
        gameManager.Received(1).SelectionMode = SelectionMode.SELECTION;
    }

    [Test]
    public void Hovering_over_unoccupied_tile_error_highlights_tile_and_clears_target_hud()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_occupied_tile_highlights_tile_and_updates_target_hud()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTileController.IsOccupied().Returns(true);
        targetTileController.OccupantCharacter.Returns(targetCharacterController);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).Highlight();
    }
}
