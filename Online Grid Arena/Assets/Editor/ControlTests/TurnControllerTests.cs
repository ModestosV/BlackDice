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

        refreshedCharactersList = new List<ICharacterController>() { firstCharacter, secondCharacter, thirdCharacter };
        exhaustedCharactersList = new List<ICharacterController>();


        firstCharacter.CharacterOwner.Returns(PLAYER_1_NAME);
        secondCharacter.CharacterOwner.Returns(PLAYER_2_NAME);
        thirdCharacter.CharacterOwner.Returns(PLAYER_2_NAME);

        turnTracker = Substitute.For<ITurnPanelController>();

        sut = new TurnController(refreshedCharactersList, exhaustedCharactersList, turnTracker);
    }

    [Test]
    public void Start_next_turn_event_starts_turn_of_active_character()
    {
        sut.Handle(new StartNewTurnEvent());

        firstCharacter.Received(1).StartOfTurn();
        secondCharacter.DidNotReceive();
    }

    [Test]
    public void Start_next_turn_event_updates_turn_tracker_with_new_character_order()
    {
        sut.Handle(new StartNewTurnEvent());

        turnTracker.Received(1).UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);
    }

    [Test]
    public void Surrender_kills_all_characters_associated_with_active_player_and_ends_game()
    {
        sut.Handle(new StartNewTurnEvent());
        sut.Handle(new SurrenderEvent());

        firstCharacter.Received(1).Die();
        secondCharacter.DidNotReceive().Die();
        thirdCharacter.DidNotReceive().Die();
    }
}
