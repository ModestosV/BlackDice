using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class SelectionControllerTests
{
    SelectionController sut;

    IHUDController hudController;
    IStatPanel selectedStatPanel;
    IStatPanelController selectedStatPanelController;
    IStatPanel targetStatPanel;
    IStatPanelController targetStatPanelController;

    IGridSelectionController gridSelectionController;

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

    [SetUp]
    public void Init()
    {
        sut = new SelectionController();

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
        gridSelectionController.SelectedTiles.Returns(selectedTiles);

        sut.HUDController = hudController;
        sut.GridSelectionController = gridSelectionController;
        sut.InputParameters = inputParameters;
    }

    [Test]
    public void Pressing_escape_key_deselects_all_tiles()
    {
        inputParameters.IsKeyEscapeDown = true;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        hudController.Received(1).ClearSelectedHUD();
    }

    [Test]
    public void Clicking_off_grid_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = true;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        hudController.Received(1).ClearSelectedHUD();
    }

    [Test]
    public void Hovering_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = false;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
    }

    [Test]
    public void Clicking_on_disabled_tile_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTileController.IsEnabled.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        hudController.Received(1).ClearSelectedHUD();
    }

    [Test]
    public void Hovering_over_disabled_tile_clears_highlighted_tiles()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetTileController.IsEnabled.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
    }

    [Test]
    public void Clicking_on_unoccupied_other_tile_selects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        targetTileController.Received(1).Select();
        hudController.Received(1).ClearSelectedHUD();
    }

    [Test]
    public void Clicking_on_selected_tile_deselects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTileController.OccupantCharacter.Returns(nullCharacter);
        gridSelectionController.SelectedTiles.Returns(new List<IHexTile>() { targetTile });

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        targetTileController.Received(1).Deselect();
        hudController.Received(1).ClearSelectedHUD();
    }

    [Test]
    public void Clicking_on_occupied_other_tile_selects_tile_and_character()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        targetTileController.Received(1).Select();
        hudController.Received(1).UpdateSelectedHUD(targetCharacter);
    }

    [Test]
    public void Hovering_over_unoccupied_tile_highlights_tile_and_clears_target_hud()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetTileController.OccupantCharacter.Returns(nullCharacter);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        targetTileController.Received(1).Hover();
        hudController.Received(1).ClearTargetHUD();
    }

    [Test]
    public void Hovering_over_occupied_tile_highlights_tile_and_updates_target_hud()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        targetTileController.Received(1).Hover();
        hudController.Received(1).UpdateTargetHUD(targetCharacter);
    }
}

