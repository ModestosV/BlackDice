using NSubstitute;
using NUnit.Framework;

public class HexTileControllerTests
{
    IHexTileSelectionController hexTileSelectionController;
    IGridSelectionController gridSelectionController;
    IHexTile hexTile;

    [SetUp]
    public void Init()
    {
        hexTileSelectionController = Substitute.For<IHexTileSelectionController>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
        hexTile = Substitute.For<IHexTile>();
    }

    [Test]
    public void Draw_path_when_end_tile_is_hovered()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = true
        };

        sut.HoverPathfinding();

        gridSelectionController.Received(1).DrawPath(hexTile);
    }

    [Test]
    public void Selecting_tile_deselects_when_already_selected()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = true,
            IsEnabled = true
        };

        sut.Select();

        hexTileSelectionController.Received(1).Deselect();
        gridSelectionController.Received(1).RemoveSelectedTile(hexTile);
    }

    [Test]
    public void Selecting_tile_selects_when_not_already_selected()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = false,
            IsEnabled = true
        };

        sut.Select();

        hexTileSelectionController.Received(1).Select();
        gridSelectionController.Received(1).AddSelectedTile(hexTile);
    }

    [Test]
    public void Hovering_unselected_tile_adds_tile_to_grid_selection()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = false,
            IsEnabled = true
        };

        sut.Hover();

        hexTileSelectionController.Received(1).Hover();
        gridSelectionController.Received(1).AddHoveredTile(hexTile);
    }

    [Test]
    public void Hovering_selected_tile_does_nothing()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            IsSelected = true,
            IsEnabled = true
        };

        sut.Hover();

        hexTileSelectionController.Received(0).Hover();
        gridSelectionController.Received(0).AddHoveredTile(Arg.Any<HexTile>());
    }

    [Test]
    public void Blurring_unselected_tile_removes_tile_from_grid_selection()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = false,
            IsEnabled = true
        };

        sut.Blur();

        hexTileSelectionController.Received(1).Blur();
        gridSelectionController.Received(1).RemoveHoveredTile(hexTile);
    }

    [Test]
    public void Blurring_selected_tile_does_nothing()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            IsSelected = true,
            IsEnabled = true
        };

        sut.Blur();

        hexTileSelectionController.Received(0).Blur();
        gridSelectionController.Received(0).RemoveHoveredTile(Arg.Any<HexTile>());
    }

    [Test]
    public void Deselecting_selected_tile_removes_tile_from_grid_selection()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = true,
            IsEnabled = true
        };

        sut.Deselect();

        hexTileSelectionController.Received(1).Deselect();
        gridSelectionController.Received(1).RemoveSelectedTile(hexTile);
    }

    [Test]
    public void Deselecting_unselected_tile_does_nothing()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            IsSelected = false,
            IsEnabled = true
        };

        sut.Deselect();

        hexTileSelectionController.Received(0).Deselect();
        gridSelectionController.Received(0).RemoveHoveredTile(Arg.Any<HexTile>());
    }

    [Test]
    public void Marking_path_on_unselected_tile_adds_tile_to_grid_selection()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = false,
            IsEnabled = true
        };

        sut.MarkPath();

        hexTileSelectionController.Received(1).MarkPath();
        gridSelectionController.Received(1).AddPathTile(hexTile);
    }

    [Test]
    public void Marking_path_on_selected_tile_does_nothing()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            IsSelected = true,
            IsEnabled = true
        };

        sut.MarkPath();

        hexTileSelectionController.Received(0).MarkPath();
        gridSelectionController.Received(0).AddPathTile(Arg.Any<HexTile>());
    }

    [Test]
    public void Scrubbing_path_on_unselected_tile_adds_tile_to_grid_selection()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = false,
            IsEnabled = true
        };

        sut.ScrubPath();

        hexTileSelectionController.Received(1).ScrubPath();
        gridSelectionController.Received(1).RemovePathTile(hexTile);
    }

    [Test]
    public void Scrubbing_path_on_selected_tile_does_nothing()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            IsSelected = true,
            IsEnabled = true
        };

        sut.ScrubPath();

        hexTileSelectionController.Received(0).ScrubPath();
        gridSelectionController.Received(0).RemovePathTile(Arg.Any<HexTile>());
    }
}
