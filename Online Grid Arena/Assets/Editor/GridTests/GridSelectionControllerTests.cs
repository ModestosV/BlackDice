using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class GridSelectionControllerTests
{
    GridSelectionController sut;

    [SetUp]
    public void Init()
    {
        sut = new GridSelectionController();
        sut.Init();
    }

    [Test]
    public void Add_remove_hex_tile_to_selected_tiles()
    {
        IHexTile firstHexTile = Substitute.For<IHexTile>();
        IHexTile secondHexTile = Substitute.For<IHexTile>();
        HexTileController controller = new HexTileController();

        firstHexTile.Controller().Returns(controller);
        secondHexTile.Controller().Returns(controller);

        // Add both tiles, remove first, only second is left
        sut.AddSelectedTile(firstHexTile);
        sut.AddSelectedTile(secondHexTile);
        sut.RemoveSelectedTile(firstHexTile);
        Assert.AreEqual(sut.SelectedTiles[0], secondHexTile);
    }

    [Test]
    public void Add_remove_hex_tile_to_hovered_tiles()
    {
        IHexTile firstHexTile = Substitute.For<IHexTile>();
        IHexTile secondHexTile = Substitute.For<IHexTile>();
        HexTileController controller = new HexTileController();

        firstHexTile.Controller().Returns(controller);
        secondHexTile.Controller().Returns(controller);

        // Add both tiles, remove first, only second is left
        sut.AddHoveredTile(firstHexTile);
        sut.AddHoveredTile(secondHexTile);
        sut.RemoveHoveredTile(firstHexTile);
        Assert.AreEqual(sut.HoveredTiles[0], secondHexTile);
    }

    [Test]
    public void Add_remove_hex_tile_to_path_tiles()
    {
        IHexTile firstHexTile = Substitute.For<IHexTile>();
        IHexTile secondHexTile = Substitute.For<IHexTile>();
        HexTileController controller = new HexTileController();

        firstHexTile.Controller().Returns(controller);
        secondHexTile.Controller().Returns(controller);

        // Add both tiles, remove first, only second is left
        sut.AddPathTile(firstHexTile);
        sut.AddPathTile(secondHexTile);
        sut.RemovePathTile(firstHexTile);
        Assert.AreEqual(sut.PathTiles[0], secondHexTile);
    }

    [Test]
    public void Deselect_all_selected_tiles()
    {
        IHexTile firstHexTile = Substitute.For<IHexTile>();
        IHexTile secondHexTile = Substitute.For<IHexTile>();
        HexTileController controller = Substitute.For<HexTileController>();

        firstHexTile.Controller().Returns(controller);
        secondHexTile.Controller().Returns(controller);

        sut.AddSelectedTile(firstHexTile);
        sut.AddSelectedTile(secondHexTile);
        sut.DeselectAll();

        controller.Received(2).Deselect();
    }

    [Test]
    public void Blur_all_hovered_tiles()
    {
        IHexTile firstHexTile = Substitute.For<IHexTile>();
        IHexTile secondHexTile = Substitute.For<IHexTile>();
        HexTileController controller = Substitute.For<HexTileController>();

        firstHexTile.Controller().Returns(controller);
        secondHexTile.Controller().Returns(controller);

        sut.AddHoveredTile(firstHexTile);
        sut.AddHoveredTile(secondHexTile);
        sut.BlurAll();

        controller.Received(2).Blur();
    }

    [Test]
    public void Scrub_all_path_tiles()
    {
        IHexTile firstHexTile = Substitute.For<IHexTile>();
        IHexTile secondHexTile = Substitute.For<IHexTile>();
        HexTileController controller = Substitute.For<HexTileController>();

        firstHexTile.Controller().Returns(controller);
        secondHexTile.Controller().Returns(controller);

        sut.AddPathTile(firstHexTile);
        sut.AddPathTile(secondHexTile);
        sut.ScrubPathAll();

        controller.Received(2).ScrubPath();
    }

    [Test]
    public void Draw_all_path_tiles()
    {
        IHexTile firstHexTile = Substitute.For<IHexTile>();
        IHexTile secondHexTile = Substitute.For<IHexTile>();
        HexTileController controller = Substitute.For<HexTileController>();

        firstHexTile.Controller().Returns(controller);
        secondHexTile.Controller().Returns(controller);

        sut.AddPathTile(firstHexTile);
        sut.AddPathTile(secondHexTile);
        sut.HighlightPath(sut.PathTiles);

        controller.Received(2).MarkPath();
    }
}
