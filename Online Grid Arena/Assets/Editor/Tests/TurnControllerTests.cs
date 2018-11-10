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
}
