using NSubstitute;
using NUnit.Framework;

public class AbilitySelectionControllerTests
{
    AbilitySelectionController sut;
    
    ICharacter targetCharacter;
    ICharacter nullCharacter = null;
    IAbility ability;
    IGridSelectionController gridSelectionController;
    IGridTraversalController gridTraversalController;

    IHexTile targetTile;
    IHexTileController targetTileController;

    InputParameters inputParameters;

    [SetUp]
    public void Init()
    {
        sut = new AbilitySelectionController();
        
        targetCharacter = Substitute.For<ICharacter>();
        ability = Substitute.For<IAbility>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gridTraversalController = Substitute.For<IGridTraversalController>();
        targetTile = Substitute.For<IHexTile>();
        targetTileController = Substitute.For<IHexTileController>();
        
        targetTile.Controller.Returns(targetTileController);

        targetTileController.OccupantCharacter.Returns(targetCharacter);
        targetTileController.IsEnabled.Returns(true);

        inputParameters = new InputParameters()
        {
            TargetTile = targetTile
        };
        
        sut.GridSelectionController = gridSelectionController;
        sut.GridTraversalController = gridTraversalController;
        sut.InputParameters = inputParameters;
    }

    [Test]
    public void Deselect_ability_when_escape_button_is_down()
    {
        inputParameters.IsKeyEscapeDown = true;

        sut.Update();

        //selectionController.Received(1).SelectedAbility = null;
    }


    [Test]
    public void Deslect_ability_when_clicked_off_grid()
    {
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = true;

        sut.Update();

        //selectionController.Received(1).SelectedAbility = null;
    }

    [Test]
    public void Hovering_off_grid_blurs_all_tiles()
    {
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = false;

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_on_disabled_tile_deselects_ability()
    {
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTileController.IsEnabled.Returns(false);

        sut.Update();

        //selectionController.Received(1).SelectedAbility = null;
    }

    [Test]
    public void Hovering_over_disabled_tile_blurs_all_tiles()
    {
        inputParameters.IsMouseOverGrid = true;
        targetTileController.IsEnabled.Returns(false);
        inputParameters.IsLeftClickDown = false;

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_on_unoccupied_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_occupied_tile_executes_ability()
    {
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;

        sut.Update();

        //abilityExecutionController.Received(1).ExecuteAbility(ability, targetCharacter);
    }

    [Test]
    public void Hovered_over_unoccupied_tile_error_highlights_tile()
    {
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_occupied_tile_highlights_tile()
    {
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;

        sut.Update();

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).MarkPath();
    }
}
