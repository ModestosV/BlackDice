using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

public class GridTraversalControllerTests
{
    GridTraversalController sut;

    IHexTile topLeftTile;
    IHexTile topRightTile;
    IHexTile middleLeftTile;
    IHexTile middleRightTile;
    IHexTile bottomLeftTile;
    IHexTile bottomRightTile;

    IHexTileController topLeftController;
    IHexTileController topRightController;
    IHexTileController middleLeftController;
    IHexTileController middleRightController;
    IHexTileController bottomLeftController;
    IHexTileController bottomRightController;


    [SetUp]
    public void Init()
    {
        sut = new GridTraversalController();
        sut.Init();

        topLeftTile = Substitute.For<IHexTile>();
        topRightTile = Substitute.For<IHexTile>();
        middleLeftTile = Substitute.For<IHexTile>();
        middleRightTile = Substitute.For<IHexTile>();
        bottomLeftTile = Substitute.For<IHexTile>();
        bottomRightTile = Substitute.For<IHexTile>();

        topLeftController = Substitute.For<IHexTileController>();
        topRightController = Substitute.For<IHexTileController>();
        middleLeftController = Substitute.For<IHexTileController>();
        middleRightController = Substitute.For<IHexTileController>();
        bottomLeftController = Substitute.For<IHexTileController>();
        bottomRightController = Substitute.For<IHexTileController>();

        topLeftController.X.Returns(0);
        topLeftController.Y.Returns(0);
        topLeftController.Z.Returns(0);
        topLeftController.IsEnabled.Returns(true);
        topLeftTile.Coordinates().Returns(new Tuple<int, int, int>(0, 0, 0));
        topLeftTile.Controller.Returns(topLeftController);

        topRightController.X.Returns(1);
        topRightController.Y.Returns(-1);
        topRightController.Z.Returns(0);
        topRightController.IsEnabled.Returns(true);
        topRightTile.Coordinates().Returns(new Tuple<int, int, int>(1, -1, 0));
        topRightTile.Controller.Returns(topRightController);

        middleLeftController.X.Returns(0);
        middleLeftController.Y.Returns(-1);
        middleLeftController.Z.Returns(1);
        middleLeftController.IsEnabled.Returns(true);
        middleLeftTile.Coordinates().Returns(new Tuple<int, int, int>(0, -1, 1));
        middleLeftTile.Controller.Returns(middleLeftController);

        middleRightController.X.Returns(1);
        middleRightController.Y.Returns(-2);
        middleRightController.Z.Returns(1);
        middleRightController.IsEnabled.Returns(true);
        middleRightTile.Coordinates().Returns(new Tuple<int, int, int>(1, -2, 1));
        middleRightTile.Controller.Returns(middleRightController);

        bottomLeftController.X.Returns(-1);
        bottomLeftController.Y.Returns(-1);
        bottomLeftController.Z.Returns(2);
        bottomLeftController.IsEnabled.Returns(true);
        bottomLeftTile.Coordinates().Returns(new Tuple<int, int, int>(-1, -1, 2));
        bottomLeftTile.Controller.Returns(bottomLeftController);

        bottomRightController.X.Returns(0);
        bottomRightController.Y.Returns(-2);
        bottomRightController.Z.Returns(2);
        bottomRightController.IsEnabled.Returns(true);
        bottomRightTile.Coordinates().Returns(new Tuple<int, int, int>(0, -2, 2));
        bottomRightTile.Controller.Returns(bottomRightController);

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
    }

    [Test]
    public void Get_north_east_neighbor_returns_north_east_neighbor()
    {
        var result = sut.GetNorthEastNeighbor(middleLeftTile);

        Assert.AreEqual(topRightTile, result);
    }

    [Test]
    public void Get_east_neighbor_returns_east_neighbor()
    {
        var result = sut.GetEastNeighbor(middleLeftTile);

        Assert.AreEqual(middleRightTile, result);
    }

    [Test]
    public void Get_south_east_neighbor_returns_south_east_neighbor()
    {
        var result = sut.GetSouthEastNeighbor(topLeftTile);
        
        Assert.AreEqual(middleLeftTile, result);
    }

    [Test]
    public void Get_south_west_neighbor_returns_south_west_neighbor()
    {
        var result = sut.GetSouthWestNeighbor(topRightTile);

        Assert.AreEqual(middleLeftTile, result);
    }

    [Test]
    public void Get_west_neighbor_returns_west_neighbor()
    {
        var result = sut.GetWestNeighbor(middleRightTile);

        Assert.AreEqual(middleLeftTile, result);
    }

    [Test]
    public void Get_north_west_neighbor_returns_north_west_neighbor()
    {
        var result = sut.GetNorthWestNeighbor(middleRightTile);

        Assert.AreEqual(topRightTile, result);
    }

    [Test]
    public void Get_neighbors_returns_all_neighbors()
    {
        List<IHexTile> resultList = sut.GetNeighbors(middleLeftTile);

        Assert.AreEqual(5, resultList.Count);

        Assert.AreEqual(topRightTile, resultList[0]);
        Assert.AreEqual(middleRightTile, resultList[1]);
        Assert.AreEqual(bottomRightTile, resultList[2]);
        Assert.AreEqual(bottomLeftTile, resultList[3]);
        Assert.AreEqual(topLeftTile, resultList[4]);
    }

    [Test]
    public void Get_path_returns_all_tiles_on_path()
    {
        List<IHexTile> resultList = sut.GetPath(topRightTile, bottomLeftTile);

        Assert.AreEqual(3, resultList.Count);

        Assert.AreEqual(topRightTile, resultList[0]);
        Assert.AreEqual(middleLeftTile, resultList[1]);
        Assert.AreEqual(bottomLeftTile, resultList[2]);
    }

}
