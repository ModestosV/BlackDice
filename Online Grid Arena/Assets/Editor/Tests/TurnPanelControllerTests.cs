using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class TurnPanelTest
{
    TurnPanelController sut;

    ICharacterController firstCharacter;
    ICharacterController secondCharacter;
    ICharacterController thirdCharacter;

    ITurnTileController firstTile;
    ITurnTileController secondTile;
    ITurnTileController thirdTile;

    List<ICharacterController> refreshedCharactersList;
    List<ICharacterController> exhaustedCharactersList;

    List<ITurnTileController> turnTiles;

    [SetUp]
    public void Init()
    {
        firstCharacter = Substitute.For<ICharacterController>();
        secondCharacter = Substitute.For<ICharacterController>();
        thirdCharacter = Substitute.For<ICharacterController>();

        refreshedCharactersList = new List<ICharacterController> { secondCharacter, thirdCharacter };
        exhaustedCharactersList = new List<ICharacterController>();

        firstTile = Substitute.For<ITurnTileController>();
        secondTile = Substitute.For<ITurnTileController>();
        thirdTile = Substitute.For<ITurnTileController>();

        firstCharacter.GetInitiative().Returns(1.0f);
        secondCharacter.GetInitiative().Returns(2.0f);
        thirdCharacter.GetInitiative().Returns(3.0f);

        turnTiles = new List<ITurnTileController>(3);

        sut = new TurnPanelController
        {
            TurnPanel = null,
            TurnTiles = turnTiles,
            CharacterOrder = new List<ICharacterController>()
        };
    }

    [Test]
    public void Add_turn_tile_adds_tile_to_turn_tiles()
    {
        sut.AddTurnTile(firstTile);

        Assert.AreEqual(turnTiles[0], firstTile);
    }

    [Test]
    public void Set_tiles_sets_turn_tiles_to_new_characters()
    {
        sut.AddTurnTile(firstTile);
        sut.AddTurnTile(secondTile);

        sut.CharacterOrder = refreshedCharactersList;

        sut.SetTiles();

        turnTiles[0].Received(1).UpdateTile(secondCharacter);
        turnTiles[1].Received(1).UpdateTile(thirdCharacter);
    }

    [Test]
    public void Update_queue_updates_character_order_with_passed_characters()
    {
        sut.UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);
        
        sut.AddTurnTile(firstTile);
        sut.AddTurnTile(secondTile);
        sut.AddTurnTile(thirdTile);

        sut.SetTiles();

        turnTiles[0].Received(1).UpdateTile(firstCharacter);
        turnTiles[1].Received(1).UpdateTile(secondCharacter);
        turnTiles[2].Received(1).UpdateTile(thirdCharacter);
    }
}
