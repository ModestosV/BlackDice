using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class HolyShearTests
{
    HolyShear sut;

    ICharacter character;
    ICharacterController characterController;
    IHexTileController allyHexTileController;
    IHexTileController enemyHexTileController;
    IWoolArmorEffect effect;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        allyHexTileController = Substitute.For<IHexTileController>();
        enemyHexTileController = Substitute.For<IHexTileController>();
        effect = Substitute.For<IWoolArmorEffect>();

        allyHexTileController.GetNeighbors().Returns(new List<IHexTileController> { enemyHexTileController });

        character.Controller.Returns(characterController);

        sut = new HolyShear(character, effect);
    }

    [Test]
    public void Half_of_stacks_get_removed_on_execute()
    {
        effect.GetHalfOfStacks().Returns(4);

        sut.Execute(new List<IHexTileController>() { allyHexTileController });

        characterController.Received(4).ConsumeOneStack(effect);
    }

    [Test]
    public void Heals_ally()
    {
        ICharacterController ally = Substitute.For<ICharacterController>();

        allyHexTileController.OccupantCharacter.Returns(ally);
        ally.IsAlly(characterController).Returns(true);

        sut.Execute(new List<IHexTileController>() { allyHexTileController });

        allyHexTileController.Received(1).Heal(Arg.Any<float>());
    }

    [Test]
    public void Does_not_heal_enemy()
    {
        ICharacterController enemy = Substitute.For<ICharacterController>();

        enemyHexTileController.OccupantCharacter.Returns(enemy);
        enemy.IsAlly(characterController).Returns(false);

        sut.Execute(new List<IHexTileController>() { allyHexTileController });

        enemyHexTileController.DidNotReceive().Heal(Arg.Any<float>());
    }
}
