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
        character.Controller.Returns(characterController);

        hexTileController = Substitute.For<IHexTileController>();
        hexTileController.OccupantCharacter.Returns(characterController);

        characterController.IsAlly(characterController).Returns(true);

        List<ICharacterController> allies = new List<ICharacterController>();
        allies.Add(characterController);
        characterController.AllAllies().Returns(allies);

        sut = new CodeReview(character);
    }

    [Test]
    public void Applies_to_self()
    {
        sut.Execute(new List<IHexTileController>() { hexTileController });
        characterController.Received().IsShielded = true;
    }
}