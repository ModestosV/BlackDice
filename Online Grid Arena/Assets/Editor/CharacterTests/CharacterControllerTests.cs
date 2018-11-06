using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class CharacterControllerTests
{
    CharacterController sut;

    ICharacter character;
    ICharacter targetCharacter;
    ICharacterController targetCharacterController;
    IHexTile startTile;
    IHexTile endTile;
    IHexTileController startTileController;
    IHexTileController endTileController;
    ITurnController turnController;

    List<IAbility> abilities;
    IAbility ability;
    List<float> abilityValues;

    const int INITIAL_MOVES_REMAINING_COUNT = 1;
    const int INITIAL_ABILITIES_REMAINING_COUNT = 1;

    const float ABILITY_DAMAGE_MULTIPLIER = 1.0f;
    const float CHARACTER_DAMAGE = 15.0f;

    List<ICharacterStat> characterStats;
    ICharacterStat health;
    ICharacterStat damage;

    const float DAMAGE_AMOUNT = 1.0f;

    [SetUp]
    public void Init()
    {

        character = Substitute.For<ICharacter>();
        targetCharacter = Substitute.For<ICharacter>();
        targetCharacterController = Substitute.For<ICharacterController>();
        startTile = Substitute.For<IHexTile>();
        endTile = Substitute.For<IHexTile>();
        startTileController = Substitute.For<IHexTileController>();
        endTileController = Substitute.For<IHexTileController>();
        turnController = Substitute.For<ITurnController>();

        character.Controller.OccupiedTile.Returns(startTile);
        startTile.Controller.Returns(startTileController);
        endTile.Controller.Returns(endTileController);

        health = Substitute.For<ICharacterStat>();
        damage = Substitute.For<ICharacterStat>();
        damage.Value.Returns(CHARACTER_DAMAGE);
        characterStats = new List<ICharacterStat>() { health, damage };

        targetCharacter.Controller.Returns(targetCharacterController);

        ability = Substitute.For<IAbility>();
        abilityValues = new List<float>() { ABILITY_DAMAGE_MULTIPLIER };
        ability.Values.Returns(abilityValues);
        ability.Type.Returns(AbilityType.ATTACK);

        abilities = new List<IAbility>() { ability };

        sut = new CharacterController();
        sut.CharacterStats = characterStats;
        sut.Character = character;
        sut.Abilities = abilities;

        sut.TurnController = turnController;
        sut.MovesRemaining = INITIAL_MOVES_REMAINING_COUNT;
        sut.AbilitiesRemaining = INITIAL_ABILITIES_REMAINING_COUNT;
    }

    [Test]
    public void Execute_move_deselects_start_tile_and_vacates_character()
    {
        sut.ExecuteMove(endTile);

        startTileController.Received(1).Deselect();
        startTileController.Received(1).OccupantCharacter = null;
    }

    [Test]
    public void Execute_move_relocates_character_to_target_tile()
    {
        sut.ExecuteMove(endTile);

        character.Received(1).MoveToTile(endTile);
        character.Controller.Received(1).OccupiedTile = endTile;
    }

    [Test]
    public void Execute_move_selects_end_tile_and_inserts_character()
    {
        sut.ExecuteMove(endTile);

        endTileController.Received(1).OccupantCharacter = character;
        endTileController.Received(1).Select();
    }

    [Test]
    public void Damage_adds_a_negative_flat_stat_modifier_to_health_stat_equal_to_the_damage_amount()
    {
        sut.Damage(DAMAGE_AMOUNT);

        sut.CharacterStats[0].Received(1).AddModifier(Arg.Is<IStatModifier>(x => x.Type == StatModType.Flat && x.Value == -DAMAGE_AMOUNT));
    }

    public void Execute_move_does_nothing_when_no_moves_remaining()
    {
        sut.MovesRemaining = 0;

        sut.ExecuteMove(endTile);

        startTileController.DidNotReceive();
        endTileController.DidNotReceive();
        turnController.DidNotReceive();
    }

    [Test]
    public void Execute_move_consumes_a_move()
    {
        sut.ExecuteMove(endTile);

        Assert.AreEqual(INITIAL_MOVES_REMAINING_COUNT - 1, sut.MovesRemaining);
    }

    [Test]
    public void Execute_move_ends_turn_when_no_moves_or_abilities_remaining()
    {
        sut.AbilitiesRemaining = 0;

        sut.ExecuteMove(endTile);

        turnController.Received(1).StartNextTurn();
    }

    [Test]
    public void Execute_ability_consumes_an_ability()
    {
        sut.ExecuteAbility(0, targetCharacter);

        Assert.AreEqual(INITIAL_ABILITIES_REMAINING_COUNT - 1, sut.AbilitiesRemaining);
    }

    [Test]
    public void Execute_ability_ends_turn_when_no_moves_or_abilities_remaining()
    {
        sut.MovesRemaining = 0;

        sut.ExecuteAbility(0, targetCharacter);

        turnController.Received(1).StartNextTurn();
    }

    [Test]
    public void Execute_attack_ability_damages_character_and_updates_stat_panel()
    {

        sut.ExecuteAbility(0, targetCharacter);

        targetCharacterController.Received(1).Damage(ABILITY_DAMAGE_MULTIPLIER * CHARACTER_DAMAGE);
    }

    [Test]
    public void Execute_ability_without_abilities_remaining_does_nothing()
    {
        sut.AbilitiesRemaining = 0;

        sut.ExecuteAbility(0, targetCharacter);

        targetCharacterController.DidNotReceive();
        turnController.DidNotReceive();
    }
}
