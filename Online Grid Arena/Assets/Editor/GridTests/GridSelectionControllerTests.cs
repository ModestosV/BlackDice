using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class GridSelectionControllerTests
{
    GridSelectionController sut;

    IHexTileController hexTile1;
    IHexTileController hexTile2;

    IHexTileController selectedTile;
    List<IHexTileController> hoveredTilesList;
    List<IHexTileController> highlightedTilesList;

    ICharacterController character;

    [SetUp]
    public void Init()
    {
        sut = new GridSelectionController();

        hexTile1 = Substitute.For<IHexTileController>();
        hexTile2 = Substitute.For<IHexTileController>();
        selectedTile = Substitute.For<IHexTileController>();
        character = Substitute.For<ICharacterController>();

        hoveredTilesList = new List<IHexTileController>();
        highlightedTilesList = new List<IHexTileController>();

        sut.HoveredTiles = hoveredTilesList;
        sut.HighlightedTiles = highlightedTilesList;

    }

    [Test]
    public void Add_and_remove_hovered_tile_manipulates_the_hovered_tile_list_correctly()
    {
        sut.AddHoveredTile(hexTile1);
        sut.AddHoveredTile(hexTile2);
        sut.RemoveHoveredTile(hexTile1);

        Assert.AreEqual(1, hoveredTilesList.Count);
        Assert.AreEqual(hexTile2, hoveredTilesList[0]);
    }

    [Test]
    public void Add_and_remove_path_tiles_manipulates_path_tile_list_correctly()
    {
        sut.AddHighlightedTile(hexTile1);
        sut.AddHighlightedTile(hexTile2);
        sut.RemoveHighlightedTile(hexTile1);

        Assert.AreEqual(1, highlightedTilesList.Count);
        Assert.AreEqual(hexTile2, highlightedTilesList[0]);
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
        sut.SelectedTile = selectedTile;

        Assert.IsTrue(sut.IsSelectedTile(selectedTile));
    }

    [Test]
    public void Get_selected_character_returns_occupant_character()
    {
        selectedTile.OccupantCharacter.Returns(character);
        selectedTile.IsOccupied().Returns(true);

        sut.SelectedTile = selectedTile;

        Assert.AreEqual(character, sut.GetSelectedCharacter());
    }

    [Test]
    public void Deselect_tiles_event_deselects_selected_tile()
    {
        sut.SelectedTile = selectedTile;

        sut.Handle(new DeselectSelectedTileEvent());
        
        selectedTile.Received(1).Deselect();
    }

    [Test]
    public void Select_tile_event_selects_selected_tile()
    {
        sut.SelectedTile = null;

        sut.Handle(new SelectTileEvent(selectedTile));

        selectedTile.Received(1).Select();
    }
}
