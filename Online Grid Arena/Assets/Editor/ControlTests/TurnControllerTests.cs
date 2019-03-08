using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class TurnControllerTests
{
    TurnController sut;

    ICharacterController firstCharacter;
    ICharacterController secondCharacter;
    ICharacterController thirdCharacter;

    IHexTileController hexTileController;

    IPlayer player1;
    IPlayer player2;

    const string PLAYER_1_NAME = "1";
    const string PLAYER_2_NAME = "2";

    [SetUp]
    public void Init()
    {
        firstCharacter = Substitute.For<ICharacterController>();
        secondCharacter = Substitute.For<ICharacterController>();
        thirdCharacter = Substitute.For<ICharacterController>();

        hexTileController = Substitute.For<IHexTileController>();

        player1 = new Player(PLAYER_1_NAME);
        player2 = new Player(PLAYER_2_NAME); 

        player1.AddCharacterController(firstCharacter);
        player1.AddCharacterController(secondCharacter);
        player2.AddCharacterController(thirdCharacter);

        firstCharacter.Owner.Returns(PLAYER_1_NAME);
        secondCharacter.Owner.Returns(PLAYER_1_NAME);
        thirdCharacter.Owner.Returns(PLAYER_2_NAME);

        sut = new TurnController(new List<IPlayer>() { player1, player2 });
    }

    [Test]
    public void Surrender_kills_all_characters_associated_with_active_player_and_ends_game()
    {
        firstCharacter.CharacterState.Returns(CharacterState.UNUSED);

        sut.Handle(new StartNewTurnEvent());
        sut.Handle(new SurrenderEvent());

        firstCharacter.Received(1).Die();
        secondCharacter.DidNotReceive().Die();
        thirdCharacter.DidNotReceive().Die();
    }

    [Test]
    public void Character_on_selected_tile_starts_turn_if_unused_and_owner_is_active()
    {
        hexTileController.OccupantCharacter.Returns(firstCharacter);
        firstCharacter.CharacterState.Returns(CharacterState.UNUSED);

        sut.Handle(new StartNewTurnEvent());
        sut.Handle(new SelectTileEvent(hexTileController));

        firstCharacter.Received(1).StartOfTurn();
    }

    [Test]
    public void Character_on_selected_tile_does_not_start_turn_if_exhausted()
    {
        hexTileController.OccupantCharacter.Returns(firstCharacter);
        firstCharacter.CharacterState.Returns(CharacterState.EXHAUSTED);
        secondCharacter.CharacterState.Returns(CharacterState.UNUSED);

        sut.Handle(new StartNewTurnEvent());
        sut.Handle(new SelectTileEvent(hexTileController));

        firstCharacter.DidNotReceive().StartOfTurn();
    }

    [Test]
    public void Character_on_selected_tile_does_not_start_turn_if_owner_is_not_active()
    {
        hexTileController.OccupantCharacter.Returns(firstCharacter);
        firstCharacter.CharacterState.Returns(CharacterState.UNUSED);

        sut.Handle(new SelectTileEvent(hexTileController));

        firstCharacter.DidNotReceive().StartOfTurn();
    }

    [Test]
    public void Character_is_reusable_after_all_team_members_are_exhausted()
    {
        hexTileController.OccupantCharacter.Returns(firstCharacter);
        firstCharacter.CharacterState.Returns(CharacterState.EXHAUSTED);
        secondCharacter.CharacterState.Returns(CharacterState.EXHAUSTED);

        sut.Handle(new StartNewTurnEvent());
        sut.Handle(new SelectTileEvent(hexTileController));

        firstCharacter.Received(1).StartOfTurn();
    }
}
