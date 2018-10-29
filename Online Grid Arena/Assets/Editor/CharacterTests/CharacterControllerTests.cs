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

    List<ICharacterStat> characterStats;
    ICharacterStat health;

    const float DAMAGE_AMOUNT = 1.0f;

    [SetUp]
    public void Init()
    {

        character = Substitute.For<ICharacter>();
        startTile = Substitute.For<IHexTile>();
        endTile = Substitute.For<IHexTile>();
        startTileController = Substitute.For<IHexTileController>();
        endTileController = Substitute.For<IHexTileController>();

        character.GetOccupiedTile().Returns(startTile);
        startTile.Controller.Returns(startTileController);
        endTile.Controller.Returns(endTileController);

        health = Substitute.For<ICharacterStat>();
        characterStats = new List<ICharacterStat>() { health };

        sut = new CharacterController();
        sut.CharacterStats = characterStats;
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

    [Test]
    public void Damage_adds_a_negative_flat_stat_modifier_to_health_stat()
    {
        sut.Damage(DAMAGE_AMOUNT);

        sut.CharacterStats[0].Received(1).AddModifier(Arg.Any<IStatModifier>());
    }
}
