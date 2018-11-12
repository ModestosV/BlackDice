﻿using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class SelectionControllerTests
{
    SelectionController sut;

    IGridSelectionController gridSelectionController;
    ITurnController turnController;
    
    IInputParameters inputParameters;

    IHexTileController targetTile;
    
    [SetUp]
    public void Init()
    {
        gridSelectionController = Substitute.For<IGridSelectionController>();
        turnController = Substitute.For<ITurnController>();
        
        inputParameters = Substitute.For<IInputParameters>();
        
        targetTile = Substitute.For<IHexTileController>();
        targetTile.IsEnabled.Returns(true);
        targetTile.IsOccupied().Returns(false);
        
        inputParameters.TargetTile.Returns(targetTile);

        gridSelectionController.IsSelectedTile(targetTile).Returns(false);

        sut = new SelectionController
        {
            GridSelectionController = gridSelectionController,
            TurnController = turnController,
            InputParameters = inputParameters
        };
    }

    [Test]
    public void Pressing_escape_key_deselects_all_tiles()
    {
        inputParameters.IsKeyEscapeDown = true;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).DeselectAll();
    }

    public void Pressing_tab_key_selects_active_character()
    {
        inputParameters.IsKeyTabDown = true;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        turnController.Received(1).SelectActiveCharacter();
    }

    [Test]
    public void Clicking_off_grid_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = true;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).DeselectAll();
    }

    [Test]
    public void Hovering_off_grid_clears_highlighted_tiles()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = false;
        inputParameters.IsLeftClickDown = false;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
    }

    [Test]
    public void Clicking_on_disabled_tile_deselects_all()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTile.IsEnabled.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).DeselectAll();
    }

    [Test]
    public void Hovering_over_disabled_tile_clears_highlighted_tiles()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetTile.IsEnabled.Returns(false);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
    }

    [Test]
    public void Clicking_on_unoccupied_other_tile_selects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).DeselectAll();
        targetTile.Received(1).Select();
    }

    [Test]
    public void Clicking_on_selected_tile_deselects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        gridSelectionController.IsSelectedTile(targetTile).Returns(true);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        targetTile.Received(1).Deselect();
    }

    [Test]
    public void Clicking_on_occupied_other_tile_selects_tile()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = true;
        targetTile.IsOccupied().Returns(true);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        gridSelectionController.Received(1).DeselectAll();
        targetTile.Received(1).Select();
    }

    [Test]
    public void Hovering_over_unoccupied_tile_highlights_tile_and_clears_target_hud()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        targetTile.Received(1).Hover();
    }

    [Test]
    public void Hovering_over_occupied_tile_highlights_tile_and_updates_target_hud()
    {
        inputParameters.IsKeyEscapeDown = false;
        inputParameters.IsMouseOverGrid = true;
        inputParameters.IsLeftClickDown = false;
        targetTile.IsOccupied().Returns(true);

        sut.Update();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).DehighlightAll();
        targetTile.Received(1).Hover();
    }
}
