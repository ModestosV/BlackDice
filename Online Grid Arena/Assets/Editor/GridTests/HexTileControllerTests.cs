using NSubstitute;
using NUnit.Framework;

public class HexTileControllerTests
{
    IHexTileSelectionController hexTileSelectionController;
    IGridSelectionController gridSelectionController;
    HexTile hexTile;

    [SetUp]
    public void Init()
    {
        hexTileSelectionController = Substitute.For<IHexTileSelectionController>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
        hexTile = hexTileSelectionController as HexTile;
    }

    [Test]
    public void Draw_path_when_end_tile_is_hovered()
    {
        gridSelectionController.When(g => g.BlurAll()).DoNotCallBase();
        
        var sut = new HexTileController();
        sut.GridSelectionController = gridSelectionController;
        sut.HexTileSelectionController = hexTile;
        sut.IsSelected = true;
        sut.HoverPathfinding();

        gridSelectionController.Received(1).DrawPath(hexTile);
    }
}
