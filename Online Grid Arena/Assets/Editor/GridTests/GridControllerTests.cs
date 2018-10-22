using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

class GridControllerTests
{
    IGridTraversalController gridTraversalController;
    IGridSelectionController gridSelectionController;
    GridController sut;

   [SetUp]
   public void Init()
    {
        gridTraversalController = Substitute.For<IGridTraversalController>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
        sut = new GridController();
        sut.Init(gridTraversalController, gridSelectionController);
    }

    [Test]
    public void Dependencies_should_be_initialized_on_init()
    {
        gridTraversalController.Received(1).Init();
        gridSelectionController.Received(1).Init();
    }

    [Test]
    public void Hex_tiles_array_to_hex_tiles_dictionary()
    {
        IHexTile hexTile = Substitute.For<IHexTile>();
        HexTileController controller = new HexTileController();

        hexTile.Controller.Returns(controller);

        // Setup to get paramater that was passed into gridTraversalController.SetHexTiles at the end of sut.SetHexTiles
        Dictionary<Tuple<int, int, int>, IHexTile> argumentUsed = new Dictionary<Tuple<int, int, int>, IHexTile>();
        gridTraversalController.SetHexTiles(Arg.Do<Dictionary<Tuple<int, int, int>, IHexTile>>(x => argumentUsed = x));

        sut.gridWidth = 1;
        sut.SetHexTiles(new IHexTile[] { hexTile });

        IHexTile expectedTile;
        argumentUsed.TryGetValue(new Tuple<int, int, int>(0, 0, 0), out expectedTile);

        Assert.AreEqual(expectedTile.Controller.X, 0);
        Assert.AreEqual(expectedTile.Controller.Y, 0);
        Assert.AreEqual(expectedTile.Controller.Z, 0);
    }
}



