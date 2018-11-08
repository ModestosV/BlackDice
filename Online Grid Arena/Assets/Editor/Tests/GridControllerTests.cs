using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

class GridControllerTests
{
    GridController sut;

    IHexTileController topLeftTile;
    IHexTileController topCenterTile;
    IHexTileController topRightTile;
    IHexTileController middleLeftTile;
    IHexTileController middleCenterTile;
    IHexTileController middleRightTile;
    IHexTileController bottomLeftTile;
    IHexTileController bottomCenterTile;
    IHexTileController bottomRightTile;

    List<IHexTileController> hexTilesList;

    readonly Tuple<int, int, int> topLeftCoordinates = new Tuple<int, int, int>(0, 0, 0);
    readonly Tuple<int, int, int> topCenterCoordinates = new Tuple<int, int, int>(1, -1, 0);
    readonly Tuple<int, int, int> topRightCoordinates = new Tuple<int, int, int>(2, -2, 0);
    readonly Tuple<int, int, int> middleLeftCoordinates = new Tuple<int, int, int>(0, -1, 1);
    readonly Tuple<int, int, int> middleCenterCoordinates = new Tuple<int, int, int>(1, -2, 1);
    readonly Tuple<int, int, int> middleRightCoordinates = new Tuple<int, int, int>(2, -3, 1);
    readonly Tuple<int, int, int> bottomLeftCoordinates = new Tuple<int, int, int>(-1, -1, 2);
    readonly Tuple<int, int, int> bottomCenterCoordinates = new Tuple<int, int, int>(0, -2, 2);
    readonly Tuple<int, int, int> bottomRightCoordinates = new Tuple<int, int, int>(1, -3, 2);

    [SetUp]
    public void Init()
    {
        sut = new GridController
        {
            GridWidth = 3
        };

        topLeftTile = Substitute.For<IHexTileController>();
        topCenterTile = Substitute.For<IHexTileController>();
        topRightTile = Substitute.For<IHexTileController>();
        middleLeftTile = Substitute.For<IHexTileController>();
        middleCenterTile = Substitute.For<IHexTileController>();
        middleRightTile = Substitute.For<IHexTileController>();
        bottomLeftTile = Substitute.For<IHexTileController>();
        bottomCenterTile = Substitute.For<IHexTileController>();
        bottomRightTile = Substitute.For<IHexTileController>();

        hexTilesList = new List<IHexTileController>() {
            topLeftTile, topCenterTile, topRightTile,
            middleLeftTile, middleCenterTile, middleRightTile,
            bottomLeftTile, bottomCenterTile, bottomRightTile
        };
    }

    [Test]
    public void Generate_grid_map_calculates_and_assigns_cubic_coordinates_to_tiles_correctly()
    {
        sut.GenerateGridMap(hexTilesList);

        Assert.AreEqual(topLeftCoordinates, topLeftTile.Coordinates);
        Assert.AreEqual(topCenterCoordinates, topCenterTile.Coordinates);
        Assert.AreEqual(topRightCoordinates, topRightTile.Coordinates);
        Assert.AreEqual(middleLeftCoordinates, middleLeftTile.Coordinates);
        Assert.AreEqual(middleCenterCoordinates, middleCenterTile.Coordinates);
        Assert.AreEqual(middleRightCoordinates, middleRightTile.Coordinates);
        Assert.AreEqual(bottomLeftCoordinates, bottomLeftTile.Coordinates);
        Assert.AreEqual(bottomCenterCoordinates, bottomCenterTile.Coordinates);
        Assert.AreEqual(bottomRightCoordinates, bottomRightTile.Coordinates);
    }

    [Test]
    public void Generate_grid_map_creates_the_tile_lookup_dictionary_correctly()
    {
        sut.GenerateGridMap(hexTilesList);

        Assert.AreEqual(topLeftTile, sut.GetTile(topLeftCoordinates));
        Assert.AreEqual(topCenterTile, sut.GetTile(topCenterCoordinates));
        Assert.AreEqual(topRightTile, sut.GetTile(topRightCoordinates));
        Assert.AreEqual(middleLeftTile, sut.GetTile(middleLeftCoordinates));
        Assert.AreEqual(middleCenterTile, sut.GetTile(middleCenterCoordinates));
        Assert.AreEqual(middleRightTile, sut.GetTile(middleRightCoordinates));
        Assert.AreEqual(bottomLeftTile, sut.GetTile(bottomLeftCoordinates));
        Assert.AreEqual(bottomCenterTile, sut.GetTile(bottomCenterCoordinates));
        Assert.AreEqual(bottomRightTile, sut.GetTile(bottomRightCoordinates));
    }
}