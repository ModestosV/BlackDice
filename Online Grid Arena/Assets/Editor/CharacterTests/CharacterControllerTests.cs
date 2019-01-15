//using NSubstitute;
//using NUnit.Framework;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterControllerTests
//{
//    CharacterController sut;

//    ICharacter character;
//    ICharacterController targetCharacterController;
//    IHexTileController startTileController;
//    IHexTileController endTileController;
//    IHexTile endTile;
//    IHUDController hudController;
//    IHealthBar healthBar;

//    List<IAbility> abilities;
//    IAbility ability1;
//    IAbility ability2;
//    const int SECOND_ABILITY_INDEX = 1;

//    const int INITIAL_ABILITIES_REMAINING_COUNT = 1;

//    const float ABILITY_DAMAGE = 20.0f;
//    const float CHARACTER_DAMAGE = 15.0f;
//    const float CHARACTER_MAX_HEALTH = 100.0f;
//    const float CHARACTER_CURRENT_HEALTH = 100.0f;
//    const float CHARACTER_MAX_MOVES = 7.0f;
//    const float CHARACTER_CURRENT_MOVES = 7.0f;
//    const float CHARACTER_HURT_LARGE_AMOUNT = 25.0f;
//    const float CHARACTER_HURT_SMALL_AMOUNT = 95.0f;

//    Dictionary<string, ICharacterStat> characterStats;
//    ICharacterStat health;
//    ICharacterStat moves;

//    List<IHexTileController> pathList;

//    ITurnTile turnTile;
//    Texture CHARACTER_ICON;
//    Color32 BORDER_COLOR;

//    const string PLAYER_NAME = "1";

//    const float DAMAGE_AMOUNT = 1.0f;
//    const float HEAL_AMOUNT = 10.0f;

//    [SetUp]
//    public void Init()
//    {
//        character = Substitute.For<ICharacter>();
//        targetCharacterController = Substitute.For<ICharacterController>();
//        startTileController = Substitute.For<IHexTileController>();
//        endTileController = Substitute.For<IHexTileController>();
//        endTile = Substitute.For<IHexTile>();
//        hudController = Substitute.For<IHUDController>();
//        healthBar = Substitute.For<IHealthBar>();

//        health = Substitute.For<ICharacterStat>();
//        health.CurrentValue.Returns(CHARACTER_CURRENT_HEALTH);
//        health.Value.Returns(CHARACTER_MAX_HEALTH);
//        moves = Substitute.For<ICharacterStat>();
//        moves.CurrentValue.Returns(CHARACTER_CURRENT_MOVES);
//        moves.Value.Returns(CHARACTER_MAX_MOVES);
//        characterStats = new Dictionary<string, ICharacterStat>()
//        {
//            { "health", health },
//            { "moves", moves }
//        };

//        ability1 = Substitute.For<IAbility>();
//        ability2 = Substitute.For<IAbility>();

//        abilities = new List<IAbility>() { ability1, ability2 };

//        pathList = new List<IHexTileController>() { startTileController, endTileController };

//        endTileController.HexTile.Returns(endTile);
//        endTileController.OccupantCharacter.Returns(targetCharacterController);

//        turnTile = Substitute.For<ITurnTile>();
//        CHARACTER_ICON = Substitute.For<Texture>();
//        BORDER_COLOR = new Color32(0, 0, 0, 0);

//        sut = new CharacterController()
//        {
//            Character = character,
//            OccupiedTile = startTileController,
//            HUDController = hudController,
//            CharacterStats = characterStats,
//            Abilities = abilities,
//            OwnedByPlayer = PLAYER_NAME,
//            AbilitiesRemaining = INITIAL_ABILITIES_REMAINING_COUNT,
//            CharacterIcon = CHARACTER_ICON,
//            BorderColor = BORDER_COLOR,
//            HealthBar = healthBar
//        };
//    }

//    [Test]
//    public void Damage_substracts_damage_amount_from_health_stat()
//    {
//        sut.Damage(DAMAGE_AMOUNT);

//        health.Received(1).CurrentValue = CHARACTER_CURRENT_HEALTH - DAMAGE_AMOUNT;
//    }

//    [Test]
//    public void Damaging_beyond_zero_health_removes_character_from_match()
//    {
//        health.CurrentValue.Returns(0.0f);

//        sut.Damage(DAMAGE_AMOUNT);

//        startTileController.Received(1).ClearOccupant();
//        character.Received(1).Destroy();
//    }

//    [Test]
//    public void Die_destroys_character()
//    {
//        sut.Die();

//        startTileController.Received(1).ClearOccupant();
//        character.Received(1).Destroy();
//    }

//    [Test]
//    public void Heal_adds_health_amount_to_health_stat()
//    {
//        health.CurrentValue.Returns(CHARACTER_HURT_LARGE_AMOUNT);

//        sut.Heal(HEAL_AMOUNT);

//        health.Received(1).CurrentValue = CHARACTER_HURT_LARGE_AMOUNT + HEAL_AMOUNT;
//    }

//    [Test]
//    public void Execute_move_deselects_start_tile_and_vacates_character()
//    {
//        sut.ExecuteMove(pathList);

//        startTileController.Received(1).Deselect();
//        startTileController.Received(1).OccupantCharacter = null;
//    }

//    [Test]
//    public void Execute_move_relocates_character_to_target_tile()
//    {
//        sut.ExecuteMove(pathList);

//        character.Received(1).MoveToTile(endTile);
//    }

//    [Test]
//    public void Execute_move_selects_end_tile_and_inserts_character()
//    {
//        sut.ExecuteMove(pathList);

//        endTileController.Received(1).OccupantCharacter = sut;
//        endTileController.Received(1).Select();
//    }

//    [Test]
//    public void Execute_move_does_nothing_when_no_moves_remaining()
//    {
//        moves.CurrentValue.Returns(0);

//        sut.ExecuteMove(pathList);

//        startTileController.DidNotReceive();
//        endTileController.DidNotReceive();
//        character.DidNotReceive();
//    }

//    [Test]
//    public void Execute_move_consumes_available_moves_and_updates_selected_hud()
//    {
//        sut.ExecuteMove(pathList);

//        moves.Received(1).CurrentValue = CHARACTER_CURRENT_MOVES - (pathList.Count - 1);
//        hudController.Received(1).UpdateSelectedHUD(characterStats, PLAYER_NAME, abilities);
//    }

//    [Test]
//    public void Execute_ability_with_abilities_remaining_executes_indicated_ability()
//    {
//        moves.CurrentValue.Returns(0);

//        sut.ExecuteAbility(SECOND_ABILITY_INDEX, endTileController);

//        ability1.DidNotReceive();
//        ability2.Received(1).Execute(endTileController);
//    }

//    [Test]
//    public void Execute_ability_without_abilities_remaining_does_nothing()
//    {
//        sut.AbilitiesRemaining = 0;

//        sut.ExecuteAbility(SECOND_ABILITY_INDEX, endTileController);

//        targetCharacterController.DidNotReceive();
//        ability1.DidNotReceive();
//        ability2.DidNotReceive();
//    }

//    [Test]
//    public void Update_turn_tile_updates_turn_tile_with_new_color_and_texture()
//    {
//        sut.UpdateTurnTile(turnTile);

//        turnTile.Received(1).CharacterIcon = CHARACTER_ICON;
//        turnTile.Received(1).BorderColor = BORDER_COLOR;

//        turnTile.Received(1).UpdateTile();
//    }
//}
