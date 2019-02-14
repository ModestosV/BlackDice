using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class HuddleTests
{
    Huddle sut;

    ICharacter character;
    ICharacterController characterController;

    IHexTileController hexTileController;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        hexTileController = Substitute.For<IHexTileController>();

        character.Controller.Returns(characterController);

        sut = new Huddle(character);
    }

    [Test]
    public void Applies_to_self()
    {
        hexTileController.GetNeighbors().Returns(new List<IHexTileController>());

        sut.Execute(new List<IHexTileController>() { hexTileController });

        characterController.Received(1).ApplyEffect(Arg.Any<HuddleEffect>());
    }

    [Test]
    public void Applies_to_ally()
    {
        var allyCharacterController = Substitute.For<ICharacterController>();
        var allyHexTileController = Substitute.For<IHexTileController>();
        allyHexTileController.OccupantCharacter.Returns(allyCharacterController);
        allyCharacterController.IsAlly(characterController).Returns(true);

        hexTileController.GetNeighbors().Returns(new List<IHexTileController>() { allyHexTileController });

        sut.Execute(new List<IHexTileController>() { hexTileController });

        allyCharacterController.Received(1).ApplyEffect(Arg.Any<HuddleEffect>());
    }

    [Test]
    public void Does_not_apply_to_enemy()
    {
        var enemyCharacterController = Substitute.For<ICharacterController>();
        var enemyAllyController = Substitute.For<IHexTileController>();
        enemyAllyController.OccupantCharacter.Returns(enemyCharacterController);
        enemyCharacterController.IsAlly(characterController).Returns(false);

        hexTileController.GetNeighbors().Returns(new List<IHexTileController>() { enemyAllyController });

        sut.Execute(new List<IHexTileController>() { hexTileController });

        enemyCharacterController.DidNotReceive().ApplyEffect(Arg.Any<HuddleEffect>());
    }
}
