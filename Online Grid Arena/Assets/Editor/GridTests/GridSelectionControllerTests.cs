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

        character = Substitute.For<ICharacterController>();

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
        sut.AddSelectedTile(hexTile1);
        sut.AddSelectedTile(hexTile2);

        sut.DeselectAll();

        hexTile1.Received(1).Deselect();
        hexTile2.Received(1).Deselect();
    }

    [Test]
    public void Blur_all_blurs_all_hovered_tiles()
    {
        sut.AddHoveredTile(hexTile1);
        sut.AddHoveredTile(hexTile2);

        sut.BlurAll();

        hexTile1.Received(1).Blur();
        hexTile2.Received(1).Blur();
    }

    [Test]
    public void Dehighlight_all_dehighlights_all_highlighted_tiles()
    {
        sut.AddHighlightedTile(hexTile1);
        sut.AddHighlightedTile(hexTile2);

        sut.DehighlightAll();

        hexTile1.Received(1).Dehighlight();
        hexTile2.Received(1).Dehighlight();
    }

    [Test]
    public void Is_selected_tile_matches_selected_tile_correctly()
    {
        sut.AddSelectedTile(hexTile1);

        Assert.IsTrue(sut.IsSelectedTile(hexTile1));
        Assert.IsFalse(sut.IsSelectedTile(hexTile2));
    }
    
    [Test]
    public void Get_selected_tile_returns_first_selected_tile()
    {
        sut.AddSelectedTile(hexTile1);
        sut.AddSelectedTile(hexTile2);

        Assert.AreEqual(hexTile1, sut.GetSelectedTile());
    }

    [Test]
    public void Get_selected_character_returns_occupant_character_in_first_selected_tile()
    {
        sut.AddSelectedTile(hexTile1);
        sut.AddSelectedTile(hexTile2);

        Assert.AreEqual(character, sut.GetSelectedCharacter());
    }
}
