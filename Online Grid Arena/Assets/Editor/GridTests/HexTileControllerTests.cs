using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class HexTileControllerTests
{
    HexTileController sut;

    IGridSelectionController gridSelectionController;
    IGridController gridController;
    IHexTile hexTile;
    ICharacterController occupantCharacter;

    IHexTileController northEastHexTile;
    IHexTileController eastHexTile;
    IHexTileController southEastHexTile;
    IHexTileController southWestHexTile;
    IHexTileController westHexTile;
    IHexTileController northWestHexTile;

    const int X = 0;
    const int Y = 0;
    const int Z = 0;

    const int ONE = 1;

    readonly (int, int, int) coordinates = (X, Y, Z);
    readonly (int, int, int) northEastCoordinates = (1, 0, -1);
    readonly (int, int, int) eastCoordinates = (1, -1, 0);
    readonly (int, int, int) southEastCoordinates = (0, -1, 1);
    readonly (int, int, int) southWestCoordinates = (-1, 0, 1);
    readonly (int, int, int) westCoordinates = (-1, 1, 0);
    readonly (int, int, int) northWestCoordinates = (0, 1, -1);


    [SetUp]
    public void Init()
    {
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gridController = Substitute.For<IGridController>();
        hexTile = Substitute.For<IHexTile>();
        occupantCharacter = Substitute.For<ICharacterController>();

        northEastHexTile = Substitute.For<IHexTileController>();
        eastHexTile = Substitute.For<IHexTileController>();
        southEastHexTile = Substitute.For<IHexTileController>();
        southWestHexTile = Substitute.For<IHexTileController>();
        westHexTile = Substitute.For<IHexTileController>();
        northWestHexTile = Substitute.For<IHexTileController>();

        northEastHexTile.Coordinates.Returns(northEastCoordinates);
        eastHexTile.Coordinates.Returns(eastCoordinates);
        southEastHexTile.Coordinates.Returns(southEastCoordinates);
        southWestHexTile.Coordinates.Returns(southWestCoordinates);
        westHexTile.Coordinates.Returns(westCoordinates);
        northWestHexTile.Coordinates.Returns(northWestCoordinates);

        northEastHexTile.IsEnabled.Returns(true);
        eastHexTile.IsEnabled.Returns(true);
        southEastHexTile.IsEnabled.Returns(true);
        southWestHexTile.IsEnabled.Returns(true);
        westHexTile.IsEnabled.Returns(true);
        northWestHexTile.IsEnabled.Returns(true);

        northEastHexTile.GetNeighbors().Returns(new List<IHexTileController>() { eastHexTile, sut, northWestHexTile });
        eastHexTile.GetNeighbors().Returns(new List<IHexTileController>() { southEastHexTile, sut, northEastHexTile });
        southEastHexTile.GetNeighbors().Returns(new List<IHexTileController>() { eastHexTile, southWestHexTile, sut });
        southWestHexTile.GetNeighbors().Returns(new List<IHexTileController>() { sut, southEastHexTile, westHexTile });
        westHexTile.GetNeighbors().Returns(new List<IHexTileController>() { northWestHexTile, sut, southWestHexTile });
        northWestHexTile.GetNeighbors().Returns(new List<IHexTileController>() { northEastHexTile, sut, westHexTile });

        gridController.GetTile((X + 1, Y, Z - 1)).Returns(northEastHexTile);
        gridController.GetTile((X + 1, Y - 1, Z)).Returns(eastHexTile);
        gridController.GetTile((X, Y - 1, Z + 1)).Returns(southEastHexTile);
        gridController.GetTile((X - 1, Y, Z + 1)).Returns(southWestHexTile);
        gridController.GetTile((X - 1, Y + 1, Z)).Returns(westHexTile);
        gridController.GetTile((X, Y + 1, Z - 1)).Returns(northWestHexTile);

        sut = new HexTileController
        {
            Coordinates = coordinates,
            GridSelectionController = gridSelectionController,
            GridController = gridController,
            HexTile = hexTile,
            OccupantCharacter = occupantCharacter,
            IsEnabled = true
        };
    }

    #region Select tests

    [Test]
    public void Selecting_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Select();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Selecting_an_occupied_tile_updates_the_selected_HUD()
    {
        sut.IsEnabled = true;

        sut.Select();

        occupantCharacter.Received(1).UpdateSelectedHUD();
    }

    #endregion

    #region Deselect tests

    [Test]
    public void Deselecting_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Deselect();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Deselecting_an_occupied_tile_clears_the_selected_HUD()
    {
        sut.IsEnabled = true;

        sut.Deselect();

        occupantCharacter.Received(1).ClearSelectedHUD();
    }

    [Test]
    public void Deselecting_a_selected_tile_sets_default_material()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;
        hexTile.IsMouseOver().Returns(false);

        sut.Deselect();

        hexTile.Received(1).SetDefaultMaterial();
    }

    #endregion

    #region Hover tests

    [Test]
    public void Hovering_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Hover();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Hovering_an_occupied_tile_updates_the_target_HUD()
    {
        sut.IsEnabled = true;

        sut.Hover();

        occupantCharacter.Received(1).UpdateTargetHUD();
    }

    [Test]
    public void Hovering_a_deselected_tile_hover_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Hover();

        hexTile.Received(1).SetHoverMaterial();
        gridSelectionController.Received(1).AddHoveredTile(sut);
    }

    [Test]
    public void Hovering_an_invalid_tile_sets_the_tile_to_show_as_invalid()
    {
        sut.Hover(HoverType.INVALID);

        hexTile.Received(1).ShowInvalidTarget();
    }

    [Test]
    public void Hovering_an_tile_for_damage_sets_the_tile_to_show_damage()
    {
        sut.Hover(HoverType.DAMAGE);

        hexTile.Received(1).ShowDamagedTarget();
    }

    [Test]
    public void Hovering_an_tile_for_healing_sets_the_tile_to_show_healing()
    {
        sut.Hover(HoverType.HEAL);

        hexTile.Received(1).ShowHealedTarget();
    }

    #endregion

    #region HoverError tests

    [Test]
    public void Error_hovering_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.HoverError();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Error_hovering_an_occupied_tile_updates_the_target_HUD()
    {
        sut.IsEnabled = true;

        sut.HoverError();

        occupantCharacter.Received(1).UpdateTargetHUD();
    }

    [Test]
    public void Error_hovering_a_deselected_tile_hover_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.HoverError();

        hexTile.Received(1).SetErrorMaterial();
        gridSelectionController.Received(1).AddHoveredTile(sut);
    }

    #endregion

    #region Blur tests

    [Test]
    public void Blurring_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Blur();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Blurring_an_occupied_tile_clears_the_target_HUD()
    {
        sut.IsEnabled = true;

        sut.Blur();

        occupantCharacter.Received(1).ClearTargetHUD();
    }

    [Test]
    public void Blurring_a_deselected_tile_removes_hover_highlight_on_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Blur();

        hexTile.Received(1).SetDefaultMaterial();
        gridSelectionController.Received(1).RemoveHoveredTile(sut);
    }

    #endregion

    #region Highlight tests

    [Test]
    public void Highlighting_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Highlight();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Highlighting_an_occupied_tile_updates_the_target_HUD()
    {
        sut.IsEnabled = true;

        sut.Highlight();

        occupantCharacter.Received(1).UpdateTargetHUD();
    }

    [Test]
    public void Highlighting_a_deselected_tile_hover_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Highlight();

        hexTile.Received(1).SetHighlightMaterial();
        gridSelectionController.Received(1).AddHighlightedTile(sut);
    }

    #endregion

    #region Dehighlight tests

    [Test]
    public void Dehighlighting_a_disabled_tile_does_nothing()
    {
        sut.IsEnabled = false;

        sut.Dehighlight();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Dehighlighting_an_occupied_tile_clears_the_target_HUD()
    {
        sut.IsEnabled = true;

        sut.Dehighlight();

        occupantCharacter.Received(1).ClearTargetHUD();
    }

    [Test]
    public void Dehighlighting_a_deselected_tile_hover_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Dehighlight();

        hexTile.Received(1).SetDefaultMaterial();
        gridSelectionController.Received(1).RemoveHighlightedTile(sut);
    }
    #endregion

    #region Neighbor tests

    [Test]
    public void Get_north_east_neighbor_looks_up_correct_tile_coordinates()
    {
        sut.GetNorthEastNeighbor();

        gridController.GetTile((X + 1, Y, Z - 1));
    }

    [Test]
    public void Get_east_neighbor_looks_up_correct_tile_coordinates()
    {
        sut.GetEastNeighbor();

        gridController.GetTile((X + 1, Y - 1, Z));
    }

    [Test]
    public void Get_south_east_neighbor_looks_up_correct_tile_coordinates()
    {
        sut.GetSouthEastNeighbor();

        gridController.GetTile((X, Y - 1, Z + 1));
    }

    [Test]
    public void Get_south_west_neighbor_looks_up_correct_tile_coordinates()
    {
        sut.GetSouthWestNeighbor();

        gridController.GetTile((X - 1, Y, Z + 1));
    }

    [Test]
    public void Get_west_neighbor_looks_up_correct_tile_coordinates()
    {
        sut.GetWestNeighbor();

        gridController.GetTile((X - 1, Y + 1, Z));
    }

    [Test]
    public void Get_north_west_neighbor_looks_up_correct_tile_coordinates()
    {
        sut.GetNorthWestNeighbor();

        gridController.GetTile((X, Y + 1, Z - 1));
    }

    [Test]
    public void Get_neighbors_returns_all_neighbors_correctly()
    {
        List<IHexTileController> expected = new List<IHexTileController>() {
            northEastHexTile, eastHexTile, southEastHexTile,
            southWestHexTile, westHexTile, northWestHexTile
        };
        List<IHexTileController> result = sut.GetNeighbors();

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Get_path_returns_all_tiles_on_path_to_target_tile()
    {
        List<IHexTileController> expected = new List<IHexTileController>()
        {
            sut, northEastHexTile
        };
        List<IHexTileController> result = sut.GetPath(northEastHexTile, false);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Get_distance_returns_correct_distance_in_straight_line()
    {
        int result = sut.GetAbsoluteDistance(eastHexTile);

        Assert.AreEqual(ONE, result);
    }

    [Test]
    public void Get_distance_returns_correct_distance_in_non_straight_line()
    {
        int result = sut.GetAbsoluteDistance(northEastHexTile);

        Assert.AreEqual(ONE, result);
    }

    [Test]
    public void Highlighting_obstructed_tile_does_nothing()
    {
        sut.IsEnabled = false;
        sut.IsObstructed = true;

        sut.Highlight();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    [Test]
    public void Selecting_obstructed_tile_does_nothing()
    {
        sut.IsEnabled = false;
        sut.IsObstructed = true;

        sut.Select();

        gridSelectionController.DidNotReceive();
        hexTile.DidNotReceive();
        occupantCharacter.DidNotReceive();
    }

    #endregion
}
