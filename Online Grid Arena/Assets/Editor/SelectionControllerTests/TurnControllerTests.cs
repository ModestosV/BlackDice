using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class TurnControllerTests
{
    TurnController sut;

    ICharacterController firstCharacter;
    ICharacterController secondCharacter;
    ICharacterController thirdCharacter;

    List<ICharacterController> refreshedCharactersList;
    List<ICharacterController> exhaustedCharactersList;

    const string PLAYER_1_NAME = "1";
    const string PLAYER_2_NAME = "2";

    ITurnPanelController turnTracker;

    [SetUp]
    public void Init()
    {
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

        sut = new TurnController
        {
            RefreshedCharacters = refreshedCharactersList,
            ExhaustedCharacters = exhaustedCharactersList,
            ActiveCharacter = null,
            TurnTracker = turnTracker
        };
    }

    [Test]
    public void Start_next_turn_event_starts_turn_of_active_character()
    {
        sut.Handle(new StartNewTurnEvent());

        firstCharacter.Received(1).StartOfTurn();
        secondCharacter.DidNotReceive();
    }

    [Test]
    public void Start_next_turn_event_starts_turn_of_character_with_lowest_initiative()
    {
        firstCharacter.GetInitiative().Returns(2.0f);
        secondCharacter.GetInitiative().Returns(1.0f);

        sut.Handle(new StartNewTurnEvent());

        firstCharacter.DidNotReceive();
        secondCharacter.Received(1).StartOfTurn();
    }

    [Test]
    public void Start_next_turn_event_updates_turn_tracker_with_new_character_order()
    {
        sut.ActiveCharacter = thirdCharacter;

        sut.Handle(new StartNewTurnEvent());

        turnTracker.Received(1).UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);
    }

    [Test]
    public void Surrender_kills_all_characters_associated_with_active_player_and_ends_game()
    {
        sut.ActiveCharacter = thirdCharacter;

        sut.Handle(new SurrenderEvent());

        firstCharacter.DidNotReceive();
        secondCharacter.Received(1).Die();
    }
}
