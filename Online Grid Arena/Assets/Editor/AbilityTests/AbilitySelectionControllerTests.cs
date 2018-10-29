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
    IAbilityExecutionController abilityExecutionController;
    ISelectionController selectionController;

    IHexTile targetTile;
    IHexTileController targetTileController;

    [SetUp]
    public void Init()
    {
        sut = new AbilitySelectionController();
        
        targetCharacter = Substitute.For<ICharacter>();
        ability = Substitute.For<IAbility>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gridTraversalController = Substitute.For<IGridTraversalController>();
        abilityExecutionController = Substitute.For<IAbilityExecutionController>();
        targetTile = Substitute.For<IHexTile>();
        targetTileController = Substitute.For<IHexTileController>();
        selectionController = Substitute.For<ISelectionController>();
        
        targetTile.Controller.Returns(targetTileController);

        targetTileController.OccupantCharacter.Returns(targetCharacter);
        targetTileController.IsEnabled.Returns(true);

        sut.TargetTile = targetTile;
        sut.GridSelectionController = gridSelectionController;
        sut.GridTraversalController = gridTraversalController;
        sut.AbilityExecutionController = abilityExecutionController;
        sut.SelectionController = selectionController;
        sut.TargetTile = targetTile;
    }

    [Test]
    public void Deselect_ability_when_escape_button_is_down()
    {
        sut.IsEscapeButtonDown = true;

        sut.Update(ability);

        selectionController.Received(1).SelectedAbility = null;
    }


    [Test]
    public void Deslect_ability_when_clicked_off_grid()
    {
        sut.MouseIsOverGrid = false;
        sut.IsLeftClickDown = true;

        sut.Update(ability);

        selectionController.Received(1).SelectedAbility = null;
    }

    [Test]
    public void Hovering_off_grid_blurs_all_tiles()
    {
        sut.MouseIsOverGrid = false;
        sut.IsLeftClickDown = false;

        sut.Update(ability);

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_on_disabled_tile_deselects_ability()
    {
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetTileController.IsEnabled.Returns(false);

        sut.Update(ability);

        selectionController.Received(1).SelectedAbility = null;
    }

    [Test]
    public void Hovering_over_disabled_tile_blurs_all_tiles()
    {
        sut.MouseIsOverGrid = true;
        targetTileController.IsEnabled.Returns(false);
        sut.IsLeftClickDown = false;

        sut.Update(ability);

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_on_unoccupied_tile_error_highlights_tile()
    {
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update(ability);

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Clicking_on_occupied_tile_executes_ability()
    {
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = true;

        sut.Update(ability);

        abilityExecutionController.Received(1).ExecuteAbility(ability, targetCharacter);
    }

    [Test]
    public void Hovered_over_unoccupied_tile_error_highlights_tile()
    {
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = false;
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update(ability);

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).HoverError();
    }

    [Test]
    public void Hovering_over_occupied_tile_highlights_tile()
    {
        sut.MouseIsOverGrid = true;
        sut.IsLeftClickDown = false;

        sut.Update(ability);

        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).BlurAll();
        targetTileController.Received(1).MarkPath();
    }
}
