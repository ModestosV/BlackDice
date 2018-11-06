using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class CharacterControllerTests
{
    CharacterController sut;

    ICharacter character;
    ICharacterController targetCharacterController;
    IHexTileController startTileController;
    IHexTileController endTileController;
    IHexTile endTile;
    ITurnController turnController;
    IHUDController hudController;

    List<IAbility> abilities;
    IAbility ability;
    List<float> abilityValues;
    const int FIRST_ABILITY_INDEX = 0;

    const int INITIAL_MOVES_REMAINING_COUNT = 1;
    const int INITIAL_ABILITIES_REMAINING_COUNT = 1;

    const float ABILITY_DAMAGE_MULTIPLIER = 1.0f;
    const float CHARACTER_DAMAGE = 15.0f;

    List<ICharacterStat> characterStats;
    ICharacterStat health;
    ICharacterStat damage;

    const string STAT_NAME_1 = "Health";
    const string STAT_NAME_2 = "Damage";
    List<string> statNames = new List<string>() { STAT_NAME_1, STAT_NAME_2 };

    const string PLAYER_NAME = "1";

    const float DAMAGE_AMOUNT = 1.0f;

    [SetUp]
    public void Init()
    {

        character = Substitute.For<ICharacter>();
        targetCharacterController = Substitute.For<ICharacterController>();
        startTileController = Substitute.For<IHexTileController>();
        endTileController = Substitute.For<IHexTileController>();
        endTile = Substitute.For<IHexTile>();
        turnController = Substitute.For<ITurnController>();
        hudController = Substitute.For<IHUDController>();

        health = Substitute.For<ICharacterStat>();
        damage = Substitute.For<ICharacterStat>();
        damage.Value.Returns(CHARACTER_DAMAGE);
        characterStats = new List<ICharacterStat>() { health, damage };
        
        ability = Substitute.For<IAbility>();
        abilityValues = new List<float>() { ABILITY_DAMAGE_MULTIPLIER };
        ability.Values.Returns(abilityValues);
        ability.Type.Returns(AbilityType.ATTACK);

        abilities = new List<IAbility>() { ability };

        endTileController.HexTile.Returns(endTile);

        sut = new CharacterController
        {
            Character = character,
            OccupiedTile = startTileController,
            TurnController = turnController,
            HUDController = hudController,
            StatNames = statNames,
            CharacterStats = characterStats,
            Abilities = abilities,
            OwnedByPlayer = PLAYER_NAME,
            MovesRemaining = INITIAL_MOVES_REMAINING_COUNT,
            AbilitiesRemaining = INITIAL_ABILITIES_REMAINING_COUNT
        };
    }

    [Test]
    public void Execute_move_deselects_start_tile_and_vacates_character()
    {
        sut.ExecuteMove(endTileController);

        startTileController.Received(1).Deselect();
        startTileController.Received(1).OccupantCharacter = null;
    }

    [Test]
    public void Execute_move_relocates_character_to_target_tile()
    {
        sut.ExecuteMove(endTileController);

        character.Received(1).MoveToTile(endTile);
        character.Controller.Received(1).OccupiedTile = endTileController;
    }

    [Test]
    public void Execute_move_selects_end_tile_and_inserts_character()
    {
        sut.ExecuteMove(endTileController);

        endTileController.Received(1).OccupantCharacter = sut;
        endTileController.Received(1).Select();
    }

    [Test]
    public void Damage_adds_a_negative_flat_stat_modifier_to_health_stat_equal_to_the_damage_amount()
    {
        sut.Damage(DAMAGE_AMOUNT);

        health.Received(1).AddModifier(Arg.Is<IStatModifier>(x => x.Type == StatModType.Flat && x.Value == -DAMAGE_AMOUNT));
    }

    public void Execute_move_does_nothing_when_no_moves_remaining()
    {
        sut.MovesRemaining = 0;

        sut.ExecuteMove(endTileController);

        startTileController.DidNotReceive();
        endTileController.DidNotReceive();
        character.DidNotReceive();
    }

    [Test]
    public void Execute_move_ends_turn_when_no_moves_or_abilities_remaining()
    {
        sut.AbilitiesRemaining = 0;

        sut.ExecuteMove(endTileController);

        turnController.Received(1).StartNextTurn();
    }

    [Test]
    public void Execute_ability_ends_turn_when_no_moves_or_abilities_remaining()
    {
        sut.MovesRemaining = 0;

        sut.ExecuteAbility(FIRST_ABILITY_INDEX, targetCharacterController);

        turnController.Received(1).StartNextTurn();
    }

    [Test]
    public void Execute_attack_ability_damages_character_and_updates_stat_panel()
    {

        sut.ExecuteAbility(FIRST_ABILITY_INDEX, targetCharacterController);

        targetCharacterController.Received(1).Damage(ABILITY_DAMAGE_MULTIPLIER * CHARACTER_DAMAGE);
    }

    [Test]
    public void Execute_ability_without_abilities_remaining_does_nothing()
    {
        sut.AbilitiesRemaining = 0;

        sut.ExecuteAbility(FIRST_ABILITY_INDEX, targetCharacterController);

        targetCharacterController.DidNotReceive();
        turnController.DidNotReceive();
    }
}
