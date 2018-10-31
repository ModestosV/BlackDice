using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class TurnControllerTests
{
    TurnController sut;

    ICharacter firstCharacter;
    ICharacterController firstCharacterController;
    ICharacter secondCharacter;
    ICharacterController secondCharacterController;
    ICharacter thirdCharacter;
    ICharacterController thirdCharacterController;

    IHexTile activeTile;
    IHexTileController activeTileController;

    List<ICharacter> refreshedCharactersList;
    List<ICharacter> exhaustedCharactersList;

    IHUDController hudController;

    [SetUp]
    public void Init()
    {
        sut = new TurnController();

        firstCharacter = Substitute.For<ICharacter>();
        firstCharacterController = Substitute.For<ICharacterController>();
        firstCharacter.Controller.Returns(firstCharacterController);
        secondCharacter = Substitute.For<ICharacter>();
        secondCharacterController = Substitute.For<ICharacterController>();
        secondCharacter.Controller.Returns(secondCharacterController);
        thirdCharacter = Substitute.For<ICharacter>();
        thirdCharacterController = Substitute.For<ICharacterController>();
        thirdCharacter.Controller.Returns(thirdCharacterController);

        activeTile = Substitute.For<IHexTile>();
        activeTileController = Substitute.For<IHexTileController>();
        activeTile.Controller.Returns(activeTileController);

        thirdCharacterController.OccupiedTile.Returns(activeTile);

        refreshedCharactersList = new List<ICharacter>() { firstCharacter, secondCharacter };
        exhaustedCharactersList = new List<ICharacter>();

        firstCharacterController.GetInitiative().Returns(1.0f);
        secondCharacterController.GetInitiative().Returns(2.0f);

        hudController = Substitute.For<IHUDController>();

        sut.RefreshedCharacters = refreshedCharactersList;
        sut.ExhaustedCharacters = exhaustedCharactersList;
        sut.HUDController = hudController;
        sut.ActiveCharacter = null;
    }

    [Test]
    public void Start_next_turn_moves_all_exhausted_characters_to_refreshed_characters_list_when_refreshed_characters_list_empty()
    {
        refreshedCharactersList = new List<ICharacter>();
        exhaustedCharactersList = new List<ICharacter>() { firstCharacter, secondCharacter };
        sut.RefreshedCharacters = refreshedCharactersList;
        sut.ExhaustedCharacters = exhaustedCharactersList;

        sut.StartNextTurn();

        Assert.AreEqual(exhaustedCharactersList, sut.RefreshedCharacters);
    }

    [Test]
    public void Start_next_turn_activates_character_with_lowest_initiative()
    {
        firstCharacterController.GetInitiative().Returns(2.0f);
        secondCharacterController.GetInitiative().Returns(1.0f);

        sut.StartNextTurn();

        Assert.AreEqual(secondCharacter, sut.ActiveCharacter);
    }

    [Test]
    public void Start_next_turn_refreshes_active_character()
    {
        sut.StartNextTurn();

        firstCharacterController.Refresh();
    }

    [Test]
    public void Start_next_turn_deselects_previously_active_character()
    {
        sut.ActiveCharacter = thirdCharacter;

        sut.StartNextTurn();

        Assert.AreEqual(thirdCharacter, exhaustedCharactersList[0]);
        activeTileController.Received(1).Deselect();
        hudController.Received(1).ClearSelectedHUD();
    }
}
