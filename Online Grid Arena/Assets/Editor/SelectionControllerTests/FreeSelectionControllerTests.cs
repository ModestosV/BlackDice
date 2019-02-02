using NSubstitute;
using NUnit.Framework;

public class FreeSelectionControllerTests
{
    FreeSelectionController sut;

    IGridSelectionController gridSelectionController;
    IInputParameters inputParameters;
    IHexTileController targetTile;

    [SetUp]
    public void Init()
    {
        gridSelectionController = Substitute.For<IGridSelectionController>();
        
        inputParameters = Substitute.For<IInputParameters>();
        
        targetTile = Substitute.For<IHexTileController>();
        targetTile.IsEnabled.Returns(true);
        targetTile.IsOccupied().Returns(false);

        inputParameters.TargetTile.Returns(targetTile);

        gridSelectionController.IsSelectedTile(targetTile).Returns(false);

        sut = new FreeSelectionController(gridSelectionController);
    }

    [Test]
    public void Pressing_escape_key_deselects_all_tiles()
    {
        inputParameters.IsKeyEscapeDown = true;

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
    }

    public void Pressing_tab_key_blurs_all_tiles()
    {
        inputParameters.IsKeyTabDown = true;

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_off_grid_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = true;

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Hovering_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = false;

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Clicking_on_disabled_tile_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTile.IsEnabled.Returns(false);

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Hovering_over_disabled_tile_clears_highlighted_tiles()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetTile.IsEnabled.Returns(false);

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
    }

    [Test]
    public void Hovering_over_unoccupied_tile_highlights_tile_and_clears_target_hud()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
        targetTile.Received(1).Hover();
    }

    [Test]
    public void Hovering_over_occupied_tile_highlights_tile_and_updates_target_hud()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetTile.IsOccupied().Returns(true);

        sut.UpdateSelection(inputParameters);

        gridSelectionController.Received(1).BlurAll();
        targetTile.Received(1).Hover();
    }
}
