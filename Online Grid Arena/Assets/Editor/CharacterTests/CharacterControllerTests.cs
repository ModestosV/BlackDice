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
    ITurnController turnController;

    const int INITIAL_MOVES_REMAINING_COUNT = 2;
    const int INITIAL_ABILITIES_REMAINING_COUNT = 0;

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
        turnController = Substitute.For<ITurnController>();

        character.Controller.OccupiedTile.Returns(startTile);
        startTile.Controller.Returns(startTileController);
        endTile.Controller.Returns(endTileController);

        health = Substitute.For<ICharacterStat>();
        characterStats = new List<ICharacterStat>() { health };

        sut = new CharacterController();
        sut.CharacterStats = characterStats;
        sut.Character = character;

        sut.TurnController = turnController;
        sut.MovesRemaining = INITIAL_MOVES_REMAINING_COUNT;
        sut.AbilitiesRemaining = INITIAL_ABILITIES_REMAINING_COUNT;
    }

    [Test]
    public void Move_to_tile_deselects_start_tile_and_vacates_character()
    {
        sut.MoveToTile(endTile);

        startTileController.Received(1).Deselect();
        startTileController.Received(1).OccupantCharacter = null;
    }

    [Test]
    public void Move_to_tile_relocates_character_to_target_tile()
    {
        sut.MoveToTile(endTile);

        character.Received(1).MoveToTile(endTile);
        character.Controller.Received(1).OccupiedTile = endTile;
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

    public void Move_to_tile_does_nothing_when_no_moves_remaining()
    {
        sut.MovesRemaining = 0;

        sut.MoveToTile(endTile);

        startTileController.DidNotReceive();
        endTileController.DidNotReceive();
        turnController.DidNotReceive();
    }

    [Test]
    public void Move_to_tile_consumes_a_move()
    {
        sut.MoveToTile(endTile);

        Assert.AreEqual(INITIAL_MOVES_REMAINING_COUNT - 1, sut.MovesRemaining);
    }

    [Test]
    public void Move_to_tile_ends_turn_when_no_moves_or_abilities_remaining()
    {
        sut.MovesRemaining = 1;
        sut.AbilitiesRemaining = 0; // Change when abilities implemented

        sut.MoveToTile(endTile);

        turnController.Received(1).EndTurn();
    }
}
