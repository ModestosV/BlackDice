using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class WoolArmorTests
{
    WoolArmor sut;

    ICharacter character;
    ICharacterController characterController;
    IHexTileController hexTileController;
    IEffect effect;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        hexTileController = Substitute.For<IHexTileController>();
        effect = Substitute.For<IEffect>();

        character.Controller.Returns(characterController);

        sut = new WoolArmor(character, effect);
    }

    [Test]
    public void Applies_given_effect_when_executed()
    {
        sut.Execute(new List<IHexTileController>() { hexTileController });

        characterController.Received(1).ApplyEffect(effect);
    }
}
