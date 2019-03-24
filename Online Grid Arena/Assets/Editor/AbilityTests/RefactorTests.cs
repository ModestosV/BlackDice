using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class RefactorTests
{
    Refactor sut;

    ICharacter character;
    ICharacterController characterController;
    ICharacterController enemyCharacterController;
    IHexTileController hexTileController;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        enemyCharacterController = Substitute.For<ICharacterController>();
        hexTileController = Substitute.For<IHexTileController>();

        character.Controller.Returns(characterController);

        sut = new Refactor(character);
    }

    [Test]
    public void Stuns_enemy()
    {
        hexTileController.OccupantCharacter.Returns(enemyCharacterController);

        sut.Execute(new List<IHexTileController>() { hexTileController });

        enemyCharacterController.Received(1).StatusEffectState = StatusEffectState.STUNNED;
    }
}