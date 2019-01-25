using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTests
{
    CharacterController sut;

    ICharacter character;
    ICharacterController targetCharacterController;
    IHexTileController startTileController;
    IHexTileController endTileController;
    IHexTile endTile;
    IHUDController hudController;
    IHealthBar healthBar;

    List<IAbility> abilities;
    IAbility ability1;
    IAbility ability2;
    const int SECOND_ABILITY_INDEX = 1;

    const float ABILITY_DAMAGE = 20.0f;
    const float CHARACTER_DAMAGE = 15.0f;
    const float CHARACTER_MAX_HEALTH = 100.0f;
    const float CHARACTER_CURRENT_HEALTH = 100.0f;
    const float CHARACTER_MAX_MOVES = 7.0f;
    const float CHARACTER_CURRENT_MOVES = 7.0f;
    const float CHARACTER_HURT_LARGE_AMOUNT = 25.0f;
    const float CHARACTER_HURT_SMALL_AMOUNT = 95.0f;

    List<IEffect> effects;

    Dictionary<string, ICharacterStat> characterStats;
    ICharacterStat health;
    ICharacterStat moves;
    ICharacterStat defense;

    List<IHexTileController> pathList;

    ITurnTile turnTile;
    Texture CHARACTER_ICON;
    Color32 BORDER_COLOR;

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
        hudController = Substitute.For<IHUDController>();
        healthBar = Substitute.For<IHealthBar>();

        health = Substitute.For<ICharacterStat>();
        health.CurrentValue.Returns(CHARACTER_CURRENT_HEALTH);
        health.Value.Returns(CHARACTER_MAX_HEALTH);
        moves = Substitute.For<ICharacterStat>();
        moves.CurrentValue.Returns(CHARACTER_CURRENT_MOVES);
        moves.Value.Returns(CHARACTER_MAX_MOVES);
        defense = Substitute.For<ICharacterStat>();
        defense.CurrentValue.Returns(100);
        defense.Value.Returns(100);

        characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "health", health },
            { "moves", moves },
            { "defense", defense }
        };

        ability1 = Substitute.For<IAbility>();
        ability2 = Substitute.For<IAbility>();

        abilities = new List<IAbility>() { ability1, ability2 };
        effects = new List<IEffect>();

        pathList = new List<IHexTileController>() { startTileController, endTileController };

        endTileController.HexTile.Returns(endTile);
        endTileController.OccupantCharacter.Returns(targetCharacterController);

        turnTile = Substitute.For<ITurnTile>();
        CHARACTER_ICON = Substitute.For<Texture>();
        BORDER_COLOR = new Color32(0, 0, 0, 0);

        sut = new CharacterController(character)
        {
            OccupiedTile = startTileController,
            HUDController = hudController,
            CharacterStats = characterStats,
            Abilities = abilities,
            CharacterOwner = PLAYER_NAME,
            CharacterIcon = CHARACTER_ICON,
            BorderColor = BORDER_COLOR,
            HealthBar = healthBar,
            Effects = effects
        };
    }

    [Test]
    public void Die_destroys_character()
    {
        sut.Die();

        startTileController.Received(1).ClearOccupant();
        character.Received(1).Destroy();
    }

    [Test]
    public void Heal_adds_health_amount_to_health_stat()
    {
        health.CurrentValue.Returns(CHARACTER_HURT_LARGE_AMOUNT);

        sut.Heal(HEAL_AMOUNT);

        health.Received(1).CurrentValue = CHARACTER_HURT_LARGE_AMOUNT + HEAL_AMOUNT;
    }

    [Test]
    public void Execute_move_relocates_character_to_target_tile()
    {
        sut.ExecuteMove(pathList);

        character.Received(1).MoveToTile(endTile);
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
        hudController.Received(1).UpdateSelectedHUD(characterStats, PLAYER_NAME, abilities, effects);
    }

    [Test]
    public void Execute_ability_a_second_time_does_nothing()
    {
        sut.Refresh();
        sut.ExecuteAbility(SECOND_ABILITY_INDEX, pathList);
        sut.ExecuteAbility(SECOND_ABILITY_INDEX, pathList);
        
        ability1.DidNotReceive().Execute(Arg.Any<List<IHexTileController>>());
        ability2.Received(1).Execute(Arg.Any<List<IHexTileController>>());
    }

    [Test]
    public void Update_turn_tile_updates_turn_tile_with_new_color_and_texture()
    {
        sut.UpdateTurnTile(turnTile);

        turnTile.Received(1).CharacterIcon = CHARACTER_ICON;
        turnTile.Received(1).BorderColor = BORDER_COLOR;

        turnTile.Received(1).UpdateTile();
    }
}
