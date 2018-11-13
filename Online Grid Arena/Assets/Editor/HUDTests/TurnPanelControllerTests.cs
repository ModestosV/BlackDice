using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class TurnPanelControllerTest
{
    TurnPanelController sut;

    ICharacterController firstCharacter;
    ICharacterController secondCharacter;

    ITurnTile firstTile;
    ITurnTile secondTile;
    ITurnTile thirdTile;

    List<ICharacterController> refreshedCharactersList;
    List<ICharacterController> exhaustedCharactersList;

    List<ITurnTile> turnTiles;

    List<ICharacterController> characterOrder;

    const int FIRST_INDEX = 0;
    const int SECOND_INDEX = 1;
    const int THIRD_INDEX = 2;

    const int TURN_TILES_SIZE = 3;

    [SetUp]
    public void Init()
    {
        firstCharacter = Substitute.For<ICharacterController>();
        secondCharacter = Substitute.For<ICharacterController>();

        refreshedCharactersList = new List<ICharacterController> { secondCharacter };
        exhaustedCharactersList = new List<ICharacterController>();

        firstTile = Substitute.For<ITurnTile>();
        secondTile = Substitute.For<ITurnTile>();
        thirdTile = Substitute.For<ITurnTile>();

        turnTiles = new List<ITurnTile>(TURN_TILES_SIZE) { firstTile, secondTile };
        characterOrder = new List<ICharacterController>();

        sut = new TurnPanelController
        {
            TurnPanel = null,
            TurnTiles = turnTiles,
            CharacterOrder = characterOrder
        };
    }

    [Test]
    public void Add_turn_tile_adds_tile_to_turn_tiles()
    {
        sut.AddTurnTile(thirdTile);

        Assert.AreEqual(thirdTile, turnTiles[THIRD_INDEX]);
    }

    [Test]
    public void Update_queue_updates_character_order_with_passed_characters()
    {
        sut.TurnTiles = turnTiles;

        sut.UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);

        Assert.AreEqual(firstCharacter, characterOrder[FIRST_INDEX]);
        Assert.AreEqual(secondCharacter, characterOrder[SECOND_INDEX]);
    }

    [Test]
    public void Update_queue_updates_tiles_with_new_character_order()
    {
        sut.TurnTiles = turnTiles;

        sut.UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);

        characterOrder[FIRST_INDEX].Received(1).UpdateTurnTile(firstTile);
        characterOrder[SECOND_INDEX].Received(1).UpdateTurnTile(secondTile);
    }
}
