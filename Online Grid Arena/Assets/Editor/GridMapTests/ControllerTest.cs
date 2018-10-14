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
    private GameObject gridID;
    private Dictionary<Tuple<int, int, int>, GameObject> gridDictionary;
    GameObject tile;

    //HexTileController tile = tileObject.GetComponent<HexTileController>();

    GridMapController gridMapController;

    [SetUp]
    public void Init()
    {
        grid = Substitute.For<IGrid>();
        gridID = Substitute.For<GameObject>();
        //tileObject = Substitute.For<GameObject>();

        //HexTileController tile = tileObject.GetComponent<HexTileController>();
        //tile.x.Returns(0);
    }

    [Test]
    public void Returns_proper_array_of_rows_or_columns()
    {
        GameObject[] expected = new GameObject[7];

        expected[0] = gridDictionary[new Tuple<int, int, int> (0, 3, -3)];
        expected[1] = gridDictionary[new Tuple<int, int, int>(0, 2, -2)];
        expected[2] = gridDictionary[new Tuple<int, int, int>(0, 1, -1)];
        expected[3] = gridDictionary[new Tuple<int, int, int>(0, 0, 0)];
        expected[4] = gridDictionary[new Tuple<int, int, int>(0, -1, 1)];
        expected[5] = gridDictionary[new Tuple<int, int, int>(0, -2, 2)];
        expected[6] = gridDictionary[new Tuple<int, int, int>(0, -3, 3)];

        var actual = grid.getColumn(tile);

        for (int i = 0; i < 7; i++)
        {
            Assert.Equals(actual[i], expected[i]);
        }

    }


}