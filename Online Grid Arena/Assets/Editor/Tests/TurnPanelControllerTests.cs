using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TurnPanelTest
{
    TurnPanelController sut;

    ICharacterController firstCharacter;
    ICharacterController secondCharacter;
    ICharacterController thirdCharacter;

    ITurnTile firstTile;
    ITurnTile secondTile;
    ITurnTile thirdTile;

    List<ICharacterController> refreshedCharactersList;
    List<ICharacterController> exhaustedCharactersList;

    List<ITurnTile> turnTiles;
    List<string> playerNames;

    List<ICharacterController> characterOrder;

    Texture CHARACTER_ICON;
    Color32 BORDER_COLOR;

    const int FIRST_INDEX = 0;
    const int SECOND_INDEX = 1;
    const int THIRD_INDEX = 2;

    const int TURN_TILES_SIZE = 3;

    [SetUp]
    public void Init()
    {
        firstCharacter = Substitute.For<ICharacterController>();
        secondCharacter = Substitute.For<ICharacterController>();
        thirdCharacter = Substitute.For<ICharacterController>();

        refreshedCharactersList = new List<ICharacterController> { secondCharacter };
        exhaustedCharactersList = new List<ICharacterController>();

        firstTile = Substitute.For<ITurnTile>();
        secondTile = Substitute.For<ITurnTile>();
        thirdTile = Substitute.For<ITurnTile>();

        turnTiles = new List<ITurnTile>(TURN_TILES_SIZE) { firstTile, secondTile };
        characterOrder = new List<ICharacterController>();

        CHARACTER_ICON = Substitute.For<Texture>();
        BORDER_COLOR = new Color32(0, 0, 0, 0);

        firstCharacter.GetInitiative().Returns(1.0f);
        secondCharacter.GetInitiative().Returns(2.0f);
        thirdCharacter.GetInitiative().Returns(3.0f);

        firstCharacter.CharacterIcon.Returns(CHARACTER_ICON);
        secondCharacter.CharacterIcon.Returns(CHARACTER_ICON);
        thirdCharacter.CharacterIcon.Returns(CHARACTER_ICON);

        firstCharacter.BorderColor.Returns(BORDER_COLOR);
        secondCharacter.BorderColor.Returns(BORDER_COLOR);
        thirdCharacter.BorderColor.Returns(BORDER_COLOR);

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
    public void Set_tiles_sets_turn_tiles_to_new_characters()
    {
        sut.TurnTiles = turnTiles;

        sut.CharacterOrder = refreshedCharactersList;

        sut.SetTiles();

        turnTiles[FIRST_INDEX].Received(1).UpdateTile(CHARACTER_ICON, BORDER_COLOR);
        turnTiles[SECOND_INDEX].Received(1).Hide();
    }

    [Test]
    public void Update_queue_updates_character_order_with_passed_characters()
    {
        sut.TurnTiles = turnTiles;

        sut.UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);

        turnTiles[FIRST_INDEX].Received(1).UpdateTile(CHARACTER_ICON, BORDER_COLOR);
        turnTiles[SECOND_INDEX].Received(1).UpdateTile(CHARACTER_ICON, BORDER_COLOR);
    }
}
