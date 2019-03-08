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

    [SetUp]
    public void Init()
    {
        firstCharacter = Substitute.For<ICharacterController>();
        secondCharacter = Substitute.For<ICharacterController>();
        thirdCharacter = Substitute.For<ICharacterController>();

        refreshedCharactersList = new List<ICharacterController>() { firstCharacter, secondCharacter, thirdCharacter };
        exhaustedCharactersList = new List<ICharacterController>();


        firstCharacter.Owner.Returns(PLAYER_1_NAME);
        secondCharacter.Owner.Returns(PLAYER_2_NAME);
        thirdCharacter.Owner.Returns(PLAYER_2_NAME);

        sut = new TurnController(refreshedCharactersList, exhaustedCharactersList, new List<IPlayer>());
    }

    // TODO: Fix during TurnController refactor [Test]
    public void Start_next_turn_event_starts_turn_of_active_character()
    {
        sut.Handle(new StartNewTurnEvent());

        firstCharacter.Received(1).StartOfTurn();
        secondCharacter.DidNotReceive();
    }

    // TODO: Fix during TurnController refactor [Test]
    public void Surrender_kills_all_characters_associated_with_active_player_and_ends_game()
    {
        sut.Handle(new StartNewTurnEvent());
        sut.Handle(new SurrenderEvent());

        firstCharacter.Received(1).Die();
        secondCharacter.DidNotReceive().Die();
        thirdCharacter.DidNotReceive().Die();
    }
}
