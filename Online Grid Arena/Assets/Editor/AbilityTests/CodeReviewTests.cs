using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class CodeReviewTests
{
    CodeReview sut;

    ICharacter character;
    ICharacterController characterController;

    ICharacter allyCharacter;
    ICharacterController allyCharacterController;

    IHexTileController hexTileController;
    IHexTileController allyHexTileController;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        hexTileController = Substitute.For<IHexTileController>();

        allyCharacter = Substitute.For<ICharacter>();
        allyCharacterController = Substitute.For<ICharacterController>();
        allyHexTileController = Substitute.For<IHexTileController>();

        character.Controller.Returns(characterController);
        allyCharacter.Controller.Returns(allyCharacterController);

        hexTileController.OccupantCharacter.Returns(characterController);
        allyHexTileController.OccupantCharacter.Returns(allyCharacterController);

        allyCharacterController.IsAlly(characterController).Returns(true);

        sut = new CodeReview(character);
    }

    [Test]
    public void Applies_to_whole_team()
    {
        sut.Execute(new List<IHexTileController>() { hexTileController });

        characterController.Received().IsShielded = true;
        allyCharacterController.Received().IsShielded = true;
    }
}