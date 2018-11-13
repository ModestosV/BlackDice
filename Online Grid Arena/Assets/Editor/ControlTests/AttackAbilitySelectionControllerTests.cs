using NSubstitute;
using NUnit.Framework;

public class AttackAbilitySelectionControllerTests
{
    AttackAbilitySelectionController sut;

    IGridSelectionController gridSelectionController;
    IGameManager gameManager;
    
    ICharacterController selectedCharacter;
    ICharacterController targetCharacter;
    
    IInputParameters inputParameters;

    IHexTileController selectedTile;
    IHexTileController targetTile;

    const int ACTIVE_ABILITY_NUMBER = 0;

    [SetUp]
    public void Init()
    {
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gameManager = Substitute.For<IGameManager>();
        
        selectedCharacter = Substitute.For<ICharacterController>();
        targetCharacter = Substitute.For<ICharacterController>();
        selectedCharacter.IsAlly(targetCharacter).Returns(false);

        gridSelectionController.GetSelectedCharacter().Returns(selectedCharacter);

        inputParameters = Substitute.For<IInputParameters>();
        inputParameters.GetAbilityNumber().Returns(ACTIVE_ABILITY_NUMBER);
        
        selectedTile = Substitute.For<IHexTileController>();
        selectedTile.OccupantCharacter.Returns(selectedCharacter);
        
        targetTile = Substitute.For<IHexTileController>();
        targetTile.IsEnabled.Returns(true);
        targetTile.IsOccupied().Returns(false);

        inputParameters.TargetTile.Returns(targetTile);

        gridSelectionController.IsSelectedTile(targetTile).Returns(false);

        sut = new AttackAbilitySelectionController
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
        targetTile.IsEnabled.Returns(false);

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
        targetTile.IsEnabled.Returns(false);

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
    public void Clicking_on_enemy_occupied_other_tile_executes_ability_and_returns_to_selection_mode()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(true);
        targetTile.IsOccupied().Returns(true);
        targetTile.OccupantCharacter.Returns(targetCharacter);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        selectedCharacter.Received(1).ExecuteAbility(ACTIVE_ABILITY_NUMBER, targetCharacter);
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
        targetTile.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_enemy_occupied_tile_highlights_tile_and_updates_target_hud()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTile.IsOccupied().Returns(true);
        targetTile.OccupantCharacter.Returns(targetCharacter);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        targetTile.Received(1).Highlight();
    }

    [Test]
    public void Hovering_over_ally_occupied_tile_error_highlights_tile_and_updates_target_hud()
    {
        inputParameters.IsMouseOverGrid.Returns(true);
        inputParameters.IsLeftClickDown.Returns(false);
        targetTile.IsOccupied().Returns(true);
        targetTile.OccupantCharacter.Returns(targetCharacter);
        selectedCharacter.IsAlly(targetCharacter).Returns(true);

        sut.Update();

        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).BlurAll();
        targetTile.Received(1).HoverError();
    }
}
