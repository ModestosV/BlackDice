using NSubstitute;
using NUnit.Framework;

public class HexTileControllerTests
{
    HexTileController sut;

    ISelectionController selectionController;
    IHexTileSelectionController hexTileSelectionController;
    IGridSelectionController gridSelectionController;
    IGridTraversalController gridTraversalController;
    ICharacter occupantCharacter;

    IHexTile hexTile;

    [SetUp]
    public void Init()
    {
        sut = new HexTileController();

        selectionController = Substitute.For<ISelectionController>();
        hexTileSelectionController = Substitute.For<IHexTileSelectionController>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gridTraversalController = Substitute.For<IGridTraversalController>();
        occupantCharacter = Substitute.For<ICharacter>();

        hexTile = Substitute.For<IHexTile>();

        sut.SelectionController = selectionController;
        sut.GridSelectionController = gridSelectionController;
        sut.GridTraversalController = gridTraversalController;
        sut.HexTile = hexTile;
        sut.HexTileSelectionController = hexTileSelectionController;
    }

    #region Select tests

    [Test]
    public void Selecting_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Select();

        selectionController.DidNotReceive();
        selectionController.DidNotReceive().SelectedCharacter = Arg.Any<ICharacter>();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Selecting_an_enabled_deselected_tile_without_occupied_character_selects_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;
        sut.OccupantCharacter = null;

        sut.Select();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        Assert.IsTrue(sut.IsSelected);
        hexTileSelectionController.Received(1).Select();
        gridSelectionController.Received(1).AddSelectedTile(hexTile);
        selectionController.DidNotReceive().SelectedCharacter = Arg.Any<ICharacter>();
    }

    [Test]
    public void Selecting_an_enabled_deselected_tile_with_occupied_character_selects_the_tile_and_character()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;
        sut.OccupantCharacter = occupantCharacter;

        sut.Select();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        gridSelectionController.Received(1).DeselectAll();
        Assert.IsTrue(sut.IsSelected);
        hexTileSelectionController.Received(1).Select();
        gridSelectionController.Received(1).AddSelectedTile(hexTile);
        selectionController.DidNotReceive().SelectedCharacter = null;
        selectionController.Received().SelectedCharacter = occupantCharacter;
    }

    [Test]
    public void Selecting_an_enabled_selected_tile_without_occupied_character_deselects_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;
        sut.OccupantCharacter = null;

        sut.Select();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        Assert.IsFalse(sut.IsSelected);
        hexTileSelectionController.Received(1).Deselect();
        gridSelectionController.Received(1).RemoveSelectedTile(hexTile);
        selectionController.DidNotReceive().SelectedCharacter = Arg.Any<ICharacter>();
    }

    [Test]
    public void Selecting_an_enabled_selected_tile_with_occupied_character_deselects_the_tile_and_character()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;
        sut.OccupantCharacter = occupantCharacter;

        sut.Select();

        gridSelectionController.Received(1).BlurAll();
        gridSelectionController.Received(1).ScrubPathAll();
        Assert.IsFalse(sut.IsSelected);
        hexTileSelectionController.Received(1).Deselect();
        gridSelectionController.Received(1).RemoveSelectedTile(hexTile);
        selectionController.Received().SelectedCharacter = null;
    }

    #endregion

    #region Deselect tests

    [Test]
    public void Deselecting_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Deselect();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Deselecting_an_enabled_deselected_tile_does_nothing()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Deselect();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Deselecting_an_enabled_selected_tile_without_occupied_character_deselects_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;
        sut.OccupantCharacter = null;

        sut.Deselect();

        Assert.IsFalse(sut.IsSelected);
        hexTileSelectionController.Received(1).Deselect();
        selectionController.DidNotReceive();
        gridSelectionController.Received(1).RemoveSelectedTile(hexTile);
        gridTraversalController.DidNotReceive();
    }

    [Test]
    public void Deselecting_an_enabled_selected_tile_with_occupied_character_deselects_the_tile_and_character()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;
        sut.OccupantCharacter = occupantCharacter;

        sut.Deselect();

        Assert.IsFalse(sut.IsSelected);
        hexTileSelectionController.Received(1).Deselect();
        selectionController.Received(1).SelectedCharacter = null;
        gridSelectionController.Received(1).RemoveSelectedTile(hexTile);
        gridTraversalController.DidNotReceive();
    }

    #endregion

    #region Hover tests

    [Test]
    public void Hovering_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Hover();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Hovering_an_enabled_selected_tile_does_nothing()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;

        sut.Hover();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Hovering_an_enabled_deselected_tile_hover_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Hover();

        selectionController.DidNotReceive();
        gridSelectionController.Received().AddHoveredTile(hexTile);
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.Received().Hover();
    }

    #endregion

    #region HoverError tests

    [Test]
    public void Hover_erroring_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.HoverError();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Hoverin_erroring_an_enabled_selected_tile_does_nothing()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;

        sut.HoverError();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Hover_erroring_an_enabled_deselected_tile_hover_error_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.HoverError();

        selectionController.DidNotReceive();
        gridSelectionController.Received().AddHoveredTile(hexTile);
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.Received().HoverError();
    }

    #endregion

    #region Blur tests

    [Test]
    public void Blurring_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Blur();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Blurring_an_enabled_selected_tile_does_nothing()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;

        sut.Blur();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Blurring_an_enabled_deselected_tile_removes_hover_highlight_from_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Blur();

        selectionController.DidNotReceive();
        gridSelectionController.Received().RemoveHoveredTile(hexTile);
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.Received().Blur();
    }

    #endregion

    #region MarkPath tests

    [Test]
    public void Marking_path_on_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.MarkPath();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Marking_path_on_an_enabled_selected_tile_does_nothing()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;

        sut.MarkPath();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Marking_path_on_an_enabled_deselected_tile_path_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.MarkPath();

        selectionController.DidNotReceive();
        gridSelectionController.Received().AddPathTile(hexTile);
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.Received().MarkPath();
    }

    #endregion

    #region ScrubPath tests

    [Test]
    public void Scrubbing_path_on_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.ScrubPath();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Scrubbing_path_on_an_enabled_selected_tile_does_nothing()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;

        sut.ScrubPath();

        selectionController.DidNotReceive();
        gridSelectionController.DidNotReceive();
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.DidNotReceive();
    }

    [Test]
    public void Scrubbing_path_on_an_enabled_deselected_tile_removes_path_highlights_from_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.ScrubPath();

        selectionController.DidNotReceive();
        gridSelectionController.Received().RemovePathTile(hexTile);
        gridTraversalController.DidNotReceive();
        hexTileSelectionController.Received().ScrubPath();
    }

    #endregion
}
