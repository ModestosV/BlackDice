﻿using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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
    List<string> playerNames;

    List<ICharacterController> characterOrder;

    const string PLAYER_1_NAME = "0";
    const string PLAYER_2_NAME = "1";

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

        firstTile = Substitute.For<ITurnTileController>();
        secondTile = Substitute.For<ITurnTileController>();
        thirdTile = Substitute.For<ITurnTileController>();

        firstCharacter.GetInitiative().Returns(1.0f);
        secondCharacter.GetInitiative().Returns(2.0f);
        thirdCharacter.GetInitiative().Returns(3.0f);

        firstCharacter.OwnedByPlayer.Returns(PLAYER_1_NAME);
        secondCharacter.OwnedByPlayer.Returns(PLAYER_1_NAME);
        thirdCharacter.OwnedByPlayer.Returns(PLAYER_2_NAME);
        
        playerNames = new List<string>() { PLAYER_1_NAME, PLAYER_2_NAME };

        turnTiles = new List<ITurnTileController>(TURN_TILES_SIZE) { firstTile, secondTile };
        characterOrder = new List<ICharacterController>();

        sut = new TurnPanelController
        {
            TurnPanel = null,
            TurnTiles = turnTiles,
            CharacterOrder = characterOrder,
            PlayerNames = playerNames
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

        turnTiles[FIRST_INDEX].Received(1).UpdateTile(secondCharacter, 0);
        turnTiles[SECOND_INDEX].Received(1).Hide();
    }

    [Test]
    public void Update_queue_updates_character_order_with_passed_characters()
    {
        sut.TurnTiles = turnTiles;

        sut.UpdateQueue(firstCharacter, refreshedCharactersList, exhaustedCharactersList);

        turnTiles[FIRST_INDEX].Received(1).UpdateTile(firstCharacter, FIRST_INDEX);
        turnTiles[SECOND_INDEX].Received(1).UpdateTile(secondCharacter, FIRST_INDEX);
    }
}
