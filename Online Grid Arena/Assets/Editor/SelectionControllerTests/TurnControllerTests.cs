using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class TurnControllerTests
{
    TurnController sut;

    IEndMatchPanel endMatchPanel;

    ICharacterController firstCharacter;
    ICharacterController secondCharacter;
    ICharacterController thirdCharacter;

    List<ICharacterController> refreshedCharactersList;
    List<ICharacterController> exhaustedCharactersList;
    
     const string PLAYER_1_NAME = "1";
    const string PLAYER_2_NAME = "2";

    ITurnPanelController turnTracker;
    ISelectionManager selectionManager;

    [SetUp]
    public void Init()
    {
        endMatchPanel = Substitute.For<IEndMatchPanel>();

        firstCharacter = Substitute.For<ICharacterController>();
        secondCharacter = Substitute.For<ICharacterController>();
        thirdCharacter = Substitute.For<ICharacterController>();

        refreshedCharactersList = new List<ICharacterController>() { firstCharacter, secondCharacter };
        exhaustedCharactersList = new List<ICharacterController>();

        firstCharacter.GetInitiative().Returns(1.0f);
        secondCharacter.GetInitiative().Returns(2.0f);

        firstCharacter.OwnedByPlayer.Returns(PLAYER_1_NAME);
        secondCharacter.OwnedByPlayer.Returns(PLAYER_2_NAME);
        thirdCharacter.OwnedByPlayer.Returns(PLAYER_2_NAME);

        turnTracker = Substitute.For<ITurnPanelController>();
        selectionManager = Substitute.For<ISelectionManager>();

        sut = new TurnController
        {
            RefreshedCharacters = refreshedCharactersList,
            ExhaustedCharacters = exhaustedCharactersList,
            EndMatchPanel = endMatchPanel,
            ActiveCharacter = null,
            TurnTracker = turnTracker,
            SelectionManager = selectionManager
        };
    }

    [Test]
    public void Start_next_turn_refreshes_active_character()
    {
        sut.StartNextTurn();

        firstCharacter.Received(1).Refresh();
        secondCharacter.DidNotReceive();
    }

    [Test]
    public void Start_next_turn_activates_character_with_lowest_initiative()
    {
        firstCharacter.GetInitiative().Returns(2.0f);
        secondCharacter.GetInitiative().Returns(1.0f);

        sut.StartNextTurn();

        firstCharacter.DidNotReceive();
        secondCharacter.Received(1).Refresh();
    }


    [Test]
    public void Start_next_turn_deselects_previously_active_character()
    {
        sut.ActiveCharacter = thirdCharacter;

        sut.StartNextTurn();

        thirdCharacter.Received(1).Deselect();
    }

    [Test]
    public void Start_next_turn_updates_turn_tracker_with_new_character_order()
    {
        sut.ActiveCharacter = thirdCharacter;

        sut.StartNextTurn();

        turnTracker.Received(1).UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);
    }

    [Test]
    public void Start_next_turn_sets_selection_mode_to_free_selection()
    {
        sut.StartNextTurn();

        selectionManager.Received(1).SelectionMode = SelectionMode.FREE;
    }

    [Test]
    public void Check_win_condition_does_not_end_game_when_more_than_one_players_characters_remain()
    {
        sut.CheckWinCondition();

        endMatchPanel.DidNotReceive();
    }

    [Test]
    public void Check_win_condition_ends_game_when_only_one_players_characters_remain()
    {
        sut.RefreshedCharacters = new List<ICharacterController>() { firstCharacter };

        sut.CheckWinCondition();

        endMatchPanel.Received(1).Show();
        endMatchPanel.Received(1).SetWinnerText($"Player {PLAYER_1_NAME} wins!");
    }

    [Test]
    public void Surrender_kills_all_characters_associated_with_active_player_and_ends_game()
    {
        sut.ActiveCharacter = thirdCharacter;

        sut.Surrender();

        firstCharacter.DidNotReceive();
        secondCharacter.Received(1).Die();
    }
}
