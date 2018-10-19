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
        //gridSelectionController.When(g => g.BlurAll()).DoNotCallBase(); //What is this for?

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
            IsSelected = true
        };

        sut.Select();

        hexTileSelectionController.Received(1).Deselect();
        gridSelectionController.Received(1).RemovedSelectedTile(hexTile);
    }

    [Test]
    public void Selecting_tile_selects_when_not_already_selected()
    {
        var sut = new HexTileController
        {
            GridSelectionController = gridSelectionController,
            HexTileSelectionController = hexTileSelectionController,
            HexTile = hexTile,
            IsSelected = false
        };

        sut.Select();

        hexTileSelectionController.Received(1).Select();
        gridSelectionController.Received(1).AddSelectedTile(hexTile);
    }
}
