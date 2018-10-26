using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class CharacterControllerTests
{
    CharacterController sut;

    ICharacter character;
    IHexTile startTile;
    IHexTile endTile;
    IHexTileController startTileController;
    IHexTileController endTileController;

    [SetUp]
    public void Init()
    {
        sut = new CharacterController();

        character = Substitute.For<ICharacter>();
        startTile = Substitute.For<IHexTile>();
        endTile = Substitute.For<IHexTile>();
        startTileController = Substitute.For<IHexTileController>();
        endTileController = Substitute.For<IHexTileController>();

        character.Controller.OccupiedTile.Returns(startTile);
        startTile.Controller.Returns(startTileController);
        endTile.Controller.Returns(endTileController);

        sut.Character = character;
    }

    [Test]
    public void Move_to_tile_deselects_start_tile_and_vacates_character()
    {
        sut.MoveToTile(endTile);

        startTileController.Received(1).OccupantCharacter = null;
        startTileController.Received(1).Deselect();
    }

    [Test]
    public void Move_to_tile_relocates_character_to_target_tile()
    {
        sut.MoveToTile(endTile);

        character.Received(1).MoveToTile(endTile);
    }

    [Test]
    public void Move_to_tile_selects_end_tile_and_inserts_character()
    {
        sut.MoveToTile(endTile);

        endTileController.Received(1).OccupantCharacter = character;
        endTileController.Received(1).Select();
    }
}
