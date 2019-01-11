using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class GridSelectionControllerTests
{
    GridSelectionController sut;

    IHexTileController hexTile1;
    IHexTileController hexTile2;

    List<IHexTileController> selectedTilesList;
    List<IHexTileController> hoveredTilesList;
    List<IHexTileController> highlightedTilesList;

    ICharacterController character;

    [SetUp]
    public void Init()
    {
        sut = new GridSelectionController();

        hexTile1 = Substitute.For<IHexTileController>();
        hexTile2 = Substitute.For<IHexTileController>();

        selectedTilesList = new List<IHexTileController>();
        hoveredTilesList = new List<IHexTileController>();
        highlightedTilesList = new List<IHexTileController>();

        sut.SelectedTiles = selectedTilesList;
        sut.HoveredTiles = hoveredTilesList;
        sut.HighlightedTiles = highlightedTilesList;

        character = Substitute.For<ICharacterController>();
        hexTile1.IsOccupied().Returns(true);
        hexTile1.OccupantCharacter.Returns(character);
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
        sut.AddHighlightedTile(hexTile1);
        sut.AddHighlightedTile(hexTile2);
        sut.RemoveHighlightedTile(hexTile1);

        Assert.AreEqual(1, highlightedTilesList.Count);
        Assert.AreEqual(hexTile2, highlightedTilesList[0]);
    }

    [Test]
    public void Deselect_all_deselects_all_selected_tiles()
    {
        selectedTilesList.Add(hexTile1);
        selectedTilesList.Add(hexTile2);

        sut.DeselectAll();

        hexTile1.Received(1).Deselect();
        hexTile2.Received(1).Deselect();
    }

    [Test]
    public void Blur_all_blurs_all_hovered_tiles()
    {
        hoveredTilesList.Add(hexTile1);
        hoveredTilesList.Add(hexTile2);

        sut.BlurAll();

        hexTile1.Received(1).Blur();
        hexTile2.Received(1).Blur();
    }

    [Test]
    public void Dehighlight_all_dehighlights_all_highlighted_tiles()
    {
        highlightedTilesList.Add(hexTile1);
        highlightedTilesList.Add(hexTile2);

        sut.DehighlightAll();

        hexTile1.Received(1).Dehighlight();
        hexTile2.Received(1).Dehighlight();
    }

    [Test]
    public void Is_selected_tile_matches_selected_tile_correctly()
    {
        selectedTilesList.Add(hexTile1);

        Assert.IsTrue(sut.IsSelectedTile(hexTile1));
        Assert.IsFalse(sut.IsSelectedTile(hexTile2));
    }
    
    [Test]
    public void Get_selected_tile_returns_first_selected_tile()
    {
        selectedTilesList.Add(hexTile1);
        selectedTilesList.Add(hexTile2);

        Assert.AreEqual(hexTile1, sut.GetSelectedTile());
    }

    [Test]
    public void Get_selected_character_returns_occupant_character_in_first_selected_tile()
    {
        selectedTilesList.Add(hexTile1);
        selectedTilesList.Add(hexTile2);

        Assert.AreEqual(character, sut.GetSelectedCharacter());
    }

    [Test]
    public void Deselect_all_tiles_event_deselects_and_deactives_all_tiles()
    {
        selectedTilesList.Add(hexTile1);
        selectedTilesList.Add(hexTile2);
        highlightedTilesList.Add(hexTile1);
        highlightedTilesList.Add(hexTile2);

        sut.Handle(new DeselectAllTilesEvent());

        hexTile1.Received(1).Dehighlight();
        hexTile2.Received(1).Dehighlight();
        hexTile1.Received(1).Deselect();
        hexTile2.Received(1).Deselect();
    }
}
