using NSubstitute;
using NUnit.Framework;
using System.IO;
using System.IO.Abstractions;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerTest
{
    IGrid grid;
    IHexTile hexTileController;

    private Dictionary<Tuple<int, int, int>, GameObject> gridDictionary;
    GridMapController gridMapController;
    GameObject tileObject;

    [SetUp]
    public void Init()
    {
        tileObject = new GameObject();
        GameObject[] expected = new GameObject[7];

        grid = Substitute.For<IGrid>();
        hexTileController = Substitute.For<IHexTile>();

        tileObject.AddComponent(typeof(HexTileController));
    }

    [Test]
    public void is_this_working()
    {
        int i = 1;
        int j = 1;
        Assert.AreEqual(i, j);
    }

    [Test]
    public void Returns_proper_array_of_rows_or_columns()
    {
        hexTileController.X.Returns(0);
        
        GameObject[] expected = new GameObject[7];
        expected[0] = gridDictionary[new Tuple<int, int, int>(0, -3, 3)];
        expected[1] = gridDictionary[new Tuple<int, int, int>(0, -2, 2)];
        expected[2] = gridDictionary[new Tuple<int, int, int>(0, -1, 1)];
        expected[3] = gridDictionary[new Tuple<int, int, int>(0, 0, 0)];
        expected[4] = gridDictionary[new Tuple<int, int, int>(0, 1, -1)];
        expected[5] = gridDictionary[new Tuple<int, int, int>(0, 2, -2)];
        expected[6] = gridDictionary[new Tuple<int, int, int>(0, 3, -3)];

        var actual = grid.getColumn(tileObject);

        for (int i = 0; i < 7; i++)
        {
            Assert.AreEqual(expected[i], expected[i]);
        }
    }


}