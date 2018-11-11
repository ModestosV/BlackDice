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

    const int INITIAL_ABILITIES_REMAINING_COUNT = 1;

    const float ABILITY_DAMAGE_MULTIPLIER = 1.0f;
    const float CHARACTER_DAMAGE = 15.0f;
    const float CHARACTER_MAX_HEALTH = 100.0f;
    const float CHARACTER_CURRENT_HEALTH = 100.0f;
    const float CHARACTER_MAX_MOVES = 7.0f;
    const float CHARACTER_CURRENT_MOVES = 7.0f;
    const float CHARACTER_HURT_LARGE_AMOUNT = 25.0f;
    const float CHARACTER_HURT_SMALL_AMOUNT = 95.0f;

    List<ICharacterStat> characterStats;
    ICharacterStat health;
    ICharacterStat damage;
    ICharacterStat moves;

    List<IHexTileController> pathList;

    const string STAT_NAME_1 = "Health";
    const string STAT_NAME_2 = "Damage";
    List<string> statNames = new List<string>() { STAT_NAME_1, STAT_NAME_2 };

    const string PLAYER_NAME = "1";

    const float DAMAGE_AMOUNT = 1.0f;
    const float HEAL_AMOUNT = 10.0f;

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
        health.CurrentValue.Returns(CHARACTER_CURRENT_HEALTH);
        health.Value.Returns(CHARACTER_MAX_HEALTH);
        damage = Substitute.For<ICharacterStat>();
        damage.Value.Returns(CHARACTER_DAMAGE);
        moves = Substitute.For<ICharacterStat>();
        moves.CurrentValue.Returns(CHARACTER_CURRENT_MOVES);
        moves.Value.Returns(CHARACTER_MAX_MOVES);
        characterStats = new List<ICharacterStat>() { health, damage, moves };
        
        ability = Substitute.For<IAbility>();
        abilityValues = new List<float>() { ABILITY_DAMAGE_MULTIPLIER };
        ability.Values.Returns(abilityValues);
        ability.Type.Returns(AbilityType.ATTACK);

        abilities = new List<IAbility>() { ability };

        pathList = new List<IHexTileController>() { startTileController, endTileController };

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
            AbilitiesRemaining = INITIAL_ABILITIES_REMAINING_COUNT
        };
    }

    [Test]
    public void Damage_substracts_damage_amount_from_health_stat()
    {
        sut.Damage(DAMAGE_AMOUNT);

        health.Received(1).CurrentValue = CHARACTER_CURRENT_HEALTH - DAMAGE_AMOUNT;
    }

    [Test]
    public void Heal_adds_health_amount_to_health_stat()
    {
        health.CurrentValue.Returns(CHARACTER_HURT_LARGE_AMOUNT); //CHARACTER_HURT?

        sut.Heal(HEAL_AMOUNT);

        health.Received(1).CurrentValue = CHARACTER_HURT_LARGE_AMOUNT + HEAL_AMOUNT;
    }

    [Test]
    public void Heal_does_not_heal_more_than_max_health_stat()
    {
        health.CurrentValue.Returns(CHARACTER_HURT_SMALL_AMOUNT);

        sut.Heal(HEAL_AMOUNT);

        health.Received(1).CurrentValue = CHARACTER_MAX_HEALTH;
    }

    [Test]
    public void Execute_move_deselects_start_tile_and_vacates_character()
    {
        sut.ExecuteMove(pathList);

        startTileController.Received(1).Deselect();
        startTileController.Received(1).OccupantCharacter = null;
    }

    [Test]
    public void Execute_move_relocates_character_to_target_tile()
    {
        sut.ExecuteMove(pathList);

        character.Received(1).MoveToTile(endTile);
    }

    [Test]
    public void Execute_move_selects_end_tile_and_inserts_character()
    {
        sut.ExecuteMove(pathList);

        endTileController.Received(1).OccupantCharacter = sut;
        endTileController.Received(1).Select();
    }

    [Test]
    public void Execute_move_does_nothing_when_no_moves_remaining()
    {
        moves.CurrentValue.Returns(0);

        sut.ExecuteMove(pathList);

        startTileController.DidNotReceive();
        endTileController.DidNotReceive();
        character.DidNotReceive();
    }

    [Test]
    public void Execute_move_consumes_available_moves_and_updates_selected_hud()
    {
        sut.ExecuteMove(pathList);

        moves.Received(1).CurrentValue = CHARACTER_CURRENT_MOVES - (pathList.Count - 1);
        hudController.Received(1).UpdateSelectedHUD(statNames, characterStats, PLAYER_NAME);
    }

    [Test]
    public void Execute_move_ends_turn_when_no_moves_or_abilities_remaining()
    {
        sut.AbilitiesRemaining = 0;
        moves.CurrentValue.Returns(1);

        sut.ExecuteMove(pathList);

        turnController.Received(1).StartNextTurn();
    }

    [Test]
    public void Execute_ability_ends_turn_when_no_moves_or_abilities_remaining()
    {
        moves.CurrentValue.Returns(0);

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
