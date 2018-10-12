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
    private Dictionary<Tuple<int, int, int>, GameObject> expected;
    GameObject tile;

    GridMapController gridMapController;

    [SetUp]
    public void Init()
    {
        grid = Substitute.For<IGrid>();
        gridID = Substitute.For<GameObject>();
        tile = Substitute.For<GameObject>();

    }

    [Test]
    public void Returns_proper_array_of_rows_or_columns()
    {
        expected = new Dictionary<Tuple<int, int, int>, GameObject>();

        gridMapController.getColumn(tile);

    }


}