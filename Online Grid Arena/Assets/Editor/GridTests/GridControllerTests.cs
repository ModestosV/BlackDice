using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

class GridControllerTests
{
    GridController sut;

    IGridTraversalController gridTraversalController;
    IGridSelectionController gridSelectionController;

    IHexTile topLeftTile;
    IHexTile topCenterTile;
    IHexTile topRightTile;
    IHexTile middleLeftTile;
    IHexTile middleCenterTile;
    IHexTile middleRightTile;
    IHexTile bottomLeftTile;
    IHexTile bottomCenterTile;
    IHexTile bottomRightTile;

    IHexTile[] hexTilesArray;

    IHexTileController topLeftController;
    IHexTileController topCenterController;
    IHexTileController topRightController;
    IHexTileController middleLeftController;
    IHexTileController middleCenterController;
    IHexTileController middleRightController;
    IHexTileController bottomLeftController;
    IHexTileController bottomCenterController;
    IHexTileController bottomRightController;

    Dictionary<Tuple<int, int, int>, IHexTile> hexTilesDictionary;

    Tuple<int, int, int> topLeftCoordinates;
    Tuple<int, int, int> topCenterCoordinates;
    Tuple<int, int, int> topRightCoordinates;
    Tuple<int, int, int> middleLeftCoordinates;
    Tuple<int, int, int> middleCenterCoordinates;
    Tuple<int, int, int> middleRightCoordinates;
    Tuple<int, int, int> bottomLeftCoordinates;
    Tuple<int, int, int> bottomCenterCoordinates;
    Tuple<int, int, int> bottomRightCoordinates;

    [SetUp]
    public void Init()
    {
        sut = new GridController();

        sut.gridWidth = 3;

        gridTraversalController = Substitute.For<IGridTraversalController>();
        gridSelectionController = Substitute.For<IGridSelectionController>();

        sut.GridTraversalController = gridTraversalController;
        sut.GridSelectionController = gridSelectionController;

        topLeftTile = Substitute.For<IHexTile>();
        topCenterTile = Substitute.For<IHexTile>();
        topRightTile = Substitute.For<IHexTile>();
        middleLeftTile = Substitute.For<IHexTile>();
        middleCenterTile = Substitute.For<IHexTile>();
        middleRightTile = Substitute.For<IHexTile>();
        bottomLeftTile = Substitute.For<IHexTile>();
        bottomCenterTile = Substitute.For<IHexTile>();
        bottomRightTile = Substitute.For<IHexTile>();

        hexTilesArray = new IHexTile[] {
            topLeftTile, topCenterTile, topRightTile,
            middleLeftTile, middleCenterTile, middleRightTile,
            bottomLeftTile, bottomCenterTile, bottomRightTile
        };

        topLeftController = Substitute.For<IHexTileController>();
        topCenterController = Substitute.For<IHexTileController>();
        topRightController = Substitute.For<IHexTileController>();
        middleLeftController = Substitute.For<IHexTileController>();
        middleCenterController = Substitute.For<IHexTileController>();
        middleRightController = Substitute.For<IHexTileController>();
        bottomLeftController = Substitute.For<IHexTileController>();
        bottomCenterController = Substitute.For<IHexTileController>();
        bottomRightController = Substitute.For<IHexTileController>();
        
        topLeftTile.Controller.Returns(topLeftController);
        topCenterTile.Controller.Returns(topCenterController);
        topRightTile.Controller.Returns(topRightController);
        middleLeftTile.Controller.Returns(middleLeftController);
        middleCenterTile.Controller.Returns(middleCenterController);
        middleRightTile.Controller.Returns(middleRightController);
        bottomLeftTile.Controller.Returns(bottomLeftController);
        bottomCenterTile.Controller.Returns(bottomCenterController);
        bottomRightTile.Controller.Returns(bottomRightController);

        topLeftCoordinates = new Tuple<int, int, int>(0, 0, 0);
        topCenterCoordinates = new Tuple<int, int, int>(1, -1, 0);
        topRightCoordinates = new Tuple<int, int, int>(2, -2, 0);
        middleLeftCoordinates = new Tuple<int, int, int>(0, -1, 1);
        middleCenterCoordinates = new Tuple<int, int, int>(1, -2, 1);
        middleRightCoordinates = new Tuple<int, int, int>(2, -3, 1);
        bottomLeftCoordinates = new Tuple<int, int, int>(-1, -1, 2);
        bottomCenterCoordinates = new Tuple<int, int, int>(0, -2, 2);
        bottomRightCoordinates = new Tuple<int, int, int>(1, -3, 2);


        /*
        var hexTiles = new Dictionary<Tuple<int, int, int>, IHexTile>
        {
            { new Tuple<int, int, int>(0, 0, 0), topLeftTile },
            { new Tuple<int, int, int>(1, -1, 0), topRightTile },
            { new Tuple<int, int, int>(0, -1, 1), middleLeftTile },
            { new Tuple<int, int, int>(1, -2, 1), middleRightTile },
            { new Tuple<int, int, int>(-1, -1, 2), bottomLeftTile },
            { new Tuple<int, int, int>(0, -2, 2), bottomRightTile }
        };

        sut.HexTiles = hexTiles;
        */
    }

    [Test]
    public void Init_initializes_grid_traversal_controller_and_grid_selection_controller()
    {
        sut.Init(gridSelectionController, gridTraversalController);

        gridTraversalController.Received(1).Init();
        gridSelectionController.Received(1).Init();
    }

    [Test]
    public void Set_hex_tiles_calculates_and_assigns_cubic_coordinates_to_tiles_correctly()
    {
        sut.SetHexTiles(hexTilesArray);

        Assert.AreEqual(0, topLeftTile.Controller.X);
        Assert.AreEqual(0, topLeftTile.Controller.Y);
        Assert.AreEqual(0, topLeftTile.Controller.Z);

        Assert.AreEqual(1, topCenterTile.Controller.X);
        Assert.AreEqual(-1, topCenterTile.Controller.Y);
        Assert.AreEqual(0, topCenterTile.Controller.Z);

        Assert.AreEqual(2, topRightTile.Controller.X);
        Assert.AreEqual(-2, topRightTile.Controller.Y);
        Assert.AreEqual(0, topRightTile.Controller.Z);


        Assert.AreEqual(0, middleLeftTile.Controller.X);
        Assert.AreEqual(-1, middleLeftTile.Controller.Y);
        Assert.AreEqual(1, middleLeftTile.Controller.Z);

        Assert.AreEqual(1, middleCenterTile.Controller.X);
        Assert.AreEqual(-2, middleCenterTile.Controller.Y);
        Assert.AreEqual(1, middleCenterTile.Controller.Z);

        Assert.AreEqual(2, middleRightTile.Controller.X);
        Assert.AreEqual(-3, middleRightTile.Controller.Y);
        Assert.AreEqual(1, middleRightTile.Controller.Z);


        Assert.AreEqual(-1, bottomLeftTile.Controller.X);
        Assert.AreEqual(-1, bottomLeftTile.Controller.Y);
        Assert.AreEqual(2, bottomLeftTile.Controller.Z);

        Assert.AreEqual(0, bottomCenterTile.Controller.X);
        Assert.AreEqual(-2, bottomCenterTile.Controller.Y);
        Assert.AreEqual(2, bottomCenterTile.Controller.Z);

        Assert.AreEqual(1, bottomRightTile.Controller.X);
        Assert.AreEqual(-3, bottomRightTile.Controller.Y);
        Assert.AreEqual(2, bottomRightTile.Controller.Z);
    }

    [Test]
    public void Set_hex_tiles_maps_tile_coordinates_to_tile_in_dictionary_correctly()
    {
        // Capture argument passed to GridTraversalController at the end of sut.SetHexTiles 
        gridTraversalController.SetHexTiles(Arg.Do<Dictionary<Tuple<int, int, int>, IHexTile>>(x => hexTilesDictionary = x));

        sut.SetHexTiles(hexTilesArray);

        IHexTile resultTile;

        hexTilesDictionary.TryGetValue(topLeftCoordinates, out resultTile);
        Assert.AreEqual(topLeftTile, resultTile);

        hexTilesDictionary.TryGetValue(topCenterCoordinates, out resultTile);
        Assert.AreEqual(topCenterTile, resultTile);

        hexTilesDictionary.TryGetValue(topRightCoordinates, out resultTile);
        Assert.AreEqual(topRightTile, resultTile);


        hexTilesDictionary.TryGetValue(middleLeftCoordinates, out resultTile);
        Assert.AreEqual(middleLeftTile, resultTile);

        hexTilesDictionary.TryGetValue(middleCenterCoordinates, out resultTile);
        Assert.AreEqual(middleCenterTile, resultTile);

        hexTilesDictionary.TryGetValue(middleRightCoordinates, out resultTile);
        Assert.AreEqual(middleRightTile, resultTile);


        hexTilesDictionary.TryGetValue(bottomLeftCoordinates, out resultTile);
        Assert.AreEqual(bottomLeftTile, resultTile);

        hexTilesDictionary.TryGetValue(bottomCenterCoordinates, out resultTile);
        Assert.AreEqual(bottomCenterTile, resultTile);

        hexTilesDictionary.TryGetValue(bottomRightCoordinates, out resultTile);
        Assert.AreEqual(bottomRightTile, resultTile);
    }
}



