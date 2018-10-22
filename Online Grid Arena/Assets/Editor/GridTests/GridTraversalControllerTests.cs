using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridTraversalControllerTests
{
    GridTraversalController sut;

    IHexTile topLeftTile;
    IHexTile topRightTile;
    IHexTile bottomLeftTile;
    IHexTile bottomRightTile;

    IHexTileController topLeftController;
    IHexTileController topRightController;
    IHexTileController bottomLeftController;
    IHexTileController bottomRightController;


    [SetUp]
    public void Init()
    {
        sut = new GridTraversalController();
        sut.Init();

        topLeftTile = Substitute.For<IHexTile>();
        topRightTile = Substitute.For<IHexTile>();
        bottomLeftTile = Substitute.For<IHexTile>();
        bottomRightTile = Substitute.For<IHexTile>();

        topLeftController = Substitute.For<IHexTileController>();
        topRightController = Substitute.For<IHexTileController>();
        bottomLeftController = Substitute.For<IHexTileController>();
        bottomRightController = Substitute.For<IHexTileController>();

        topLeftController.X.Returns(0);
        topLeftController.Y.Returns(0);
        topLeftController.Z.Returns(0);
        topLeftTile.Controller.Returns(topLeftController);

        topRightController.X.Returns(1);
        topRightController.Y.Returns(-1);
        topRightController.Z.Returns(0);
        topRightTile.Controller.Returns(topRightController);

        bottomLeftController.X.Returns(0);
        bottomLeftController.Y.Returns(-1);
        bottomLeftController.Z.Returns(1);
        bottomLeftTile.Controller.Returns(bottomLeftController);

        bottomRightController.X.Returns(1);
        bottomRightController.Y.Returns(-2);
        bottomRightController.Z.Returns(1);
        bottomRightTile.Controller.Returns(bottomRightController);

        var hexTiles = new Dictionary<Tuple<int, int, int>, IHexTile>
        {
            { new Tuple<int, int, int>(0, 0, 0), topLeftTile },    // Top left
            { new Tuple<int, int, int>(1, -1, 0), topRightTile },   // Top right
            { new Tuple<int, int, int>(0, -1, 1), bottomLeftTile },   // Bottom left
            { new Tuple<int, int, int>(1, -2, 1), bottomRightTile }    // Bottom right
        };

        sut.HexTiles = hexTiles;
    }

    [Test]
    public void Get_north_east_neighbor_returns_north_east_neighbor()
    {
        var result = sut.GetNorthEastNeighbor(bottomLeftTile);

        Assert.AreEqual(topRightTile, result);
    }

    [Test]
    public void Get_east_neighbor_returns_east_neighbor()
    {
        var result = sut.GetEastNeighbor(bottomLeftTile);

        Assert.AreEqual(bottomRightTile, result);
    }

    [Test]
    public void Get_south_east_neighbor_returns_south_east_neighbor()
    {
        var result = sut.GetSouthEastNeighbor(topLeftTile);
        
        Assert.AreEqual(bottomLeftTile, result);
    }

    [Test]
    public void Get_south_west_neighbor_returns_south_west_neighbor()
    {
        var result = sut.GetSouthWestNeighbor(topRightTile);

        Assert.AreEqual(bottomLeftTile, result);
    }

    [Test]
    public void Get_west_neighbor_returns_west_neighbor()
    {
        var result = sut.GetWestNeighbor(bottomRightTile);

        Assert.AreEqual(bottomLeftTile, result);
    }

    [Test]
    public void Get_north_west_neighbor_returns_north_west_neighbor()
    {
        var result = sut.GetNorthWestNeighbor(bottomRightTile);

        Assert.AreEqual(topRightTile, result);
    }

}
