using NSubstitute;
using NUnit.Framework;

public class HexTileControllerTests
{
    HexTileController sut;

    IGridSelectionController gridSelectionController;
    IGridController gridController;
    IHexTile hexTile;
    ICharacterController occupantCharacter;

    [SetUp]
    public void Init()
    {
        sut = new HexTileController();
        
        gridSelectionController = Substitute.For<IGridSelectionController>();
        gridController = Substitute.For<IGridController>();
        hexTile = Substitute.For<IHexTile>();
        occupantCharacter = Substitute.For<ICharacterController>();

        sut.GridSelectionController = gridSelectionController;
        sut.GridController = gridController;
        sut.HexTile = hexTile;
        sut.OccupantCharacter = occupantCharacter;
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

    [Test]
    public void Selecting_an_deselected_tile_selects_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Select();

        hexTile.Received(1).SetClickedMaterial();
        gridSelectionController.Received(1).AddSelectedTile(sut);
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
    public void Deselecting_an_selected_tile_deselects_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = true;
        hexTile.IsMouseOver().Returns(false);

        sut.Deselect();

        hexTile.Received(1).SetDefaultMaterial();
        gridSelectionController.Received(1).RemoveSelectedTile(sut);
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
    public void Blurring_a_deselected_tile_hover_highlights_the_tile()
    {
        sut.IsEnabled = true;
        sut.IsSelected = false;

        sut.Blur();

        hexTile.Received(1).SetHoverMaterial();
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
}
