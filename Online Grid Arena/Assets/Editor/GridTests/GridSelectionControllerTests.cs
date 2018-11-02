using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class GridSelectionControllerTests
{
    GridSelectionController sut;

    IHexTile hexTile1;
    IHexTile hexTile2;

    IHexTileController controller1;
    IHexTileController controller2;

    List<IHexTile> selectedTilesList;
    List<IHexTile> hoveredTilesList;
    List<IHexTile> pathTilesList;
    List<IHexTile> pathTilesToAddList;

    [SetUp]
    public void Init()
    {
        sut = new GridSelectionController();
        sut.Init();

        hexTile1 = Substitute.For<IHexTile>();
        hexTile2 = Substitute.For<IHexTile>();

        controller1 = Substitute.For<IHexTileController>();
        controller2 = Substitute.For<IHexTileController>();

        hexTile1.Controller.Returns(controller1);
        hexTile2.Controller.Returns(controller2);

        selectedTilesList = new List<IHexTile>();
        hoveredTilesList = new List<IHexTile>();
        pathTilesList = new List<IHexTile>();
        pathTilesToAddList = new List<IHexTile>();

        sut.SelectedTiles = selectedTilesList;
        sut.HoveredTiles = hoveredTilesList;
        sut.PathTiles = pathTilesList;
    }

    [Test]
    public void Add_and_remove_selected_tile_manipulates_the_selected_tile_list_correctly()
    {
        // Add both tiles, remove first, only second is left
        sut.AddSelectedTile(hexTile1);
        sut.AddSelectedTile(hexTile2);
        sut.RemoveSelectedTile(hexTile1);

        Assert.AreEqual(1, selectedTilesList.Count);
        Assert.AreEqual(hexTile2, selectedTilesList[0]);
    }

    [Test]
    public void Add_and_remove_hovered_tile_manipulates_the_hovered_tile_list_correctly()
    {
        // Add both tiles, remove first, only second is left
        sut.AddHoveredTile(hexTile1);
        sut.AddHoveredTile(hexTile2);
        sut.RemoveHoveredTile(hexTile1);

        Assert.AreEqual(1, hoveredTilesList.Count);
        Assert.AreEqual(hexTile2, hoveredTilesList[0]);
    }

    [Test]
    public void Add_and_remove_path_tiles_manipulates_path_tile_list_correctly()
    {
        // Add both tiles, remove first, only second is left
        sut.AddPathTile(hexTile1);
        sut.AddPathTile(hexTile2);
        sut.RemovePathTile(hexTile1);

        Assert.AreEqual(1, pathTilesList.Count);
        Assert.AreEqual(hexTile2, pathTilesList[0]);
    }

    [Test]
    public void Deselect_all_deselects_all_selected_tiles()
    {
        selectedTilesList.Add(hexTile1);
        selectedTilesList.Add(hexTile2);

        sut.DeselectAll();

        controller1.Received(1).Deselect();
        controller2.Received(1).Deselect();
    }

    [Test]
    public void Blur_all_blurs_all_hovered_tiles()
    {
        hoveredTilesList.Add(hexTile1);
        hoveredTilesList.Add(hexTile2);

        sut.BlurAll();

        controller1.Received(1).Blur();
        controller2.Received(1).Blur();
    }

    [Test]
    public void Scrub_path_all_scrubs_path_on_all_path_tiles()
    {
        pathTilesList.Add(hexTile1);
        pathTilesList.Add(hexTile2);

        sut.ScrubPathAll();

        controller1.Received(1).Dehighlight();
        controller2.Received(1).Dehighlight();
    }

    [Test]
    public void Highlight_path_marks_path_on_all_path_tiles()
    {
        pathTilesToAddList.Add(hexTile1);
        pathTilesToAddList.Add(hexTile2);

        sut.HighlightPath(pathTilesToAddList);

        controller1.Received(1).MarkPath();
        controller2.Received(1).MarkPath();
    }
}
