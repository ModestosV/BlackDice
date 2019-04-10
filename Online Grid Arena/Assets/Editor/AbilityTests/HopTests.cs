using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class HopTests
{
    Hop sut;

    ICharacter character;
    ICharacterController controller;

    IHexTileController occupiedTile;
    IHexTileController targetTile;
    List<IHexTileController> targetTiles;

    [SetUp]
    public void Init()
    {
        occupiedTile = Substitute.For<IHexTileController>();
        targetTile = Substitute.For<IHexTileController>();
        character = Substitute.For<ICharacter>();
        controller = Substitute.For<ICharacterController>();

        targetTiles = new List<IHexTileController>() { targetTile };

        character.Controller.Returns(controller);
        occupiedTile.OccupantCharacter.Returns(controller);
        controller.OccupiedTile.Returns(occupiedTile);

        sut = new Hop(character);
    }

    [Test]
    public void Execute_moves_character()
    {
        sut.Execute(targetTiles);
        Assert.AreEqual(occupiedTile.OccupantCharacter, null);
        Assert.AreEqual(targetTile.OccupantCharacter, controller);
    }

    [Test]
    public void Execute_increments_abilitiesRemaining()
    {
        sut.Execute(targetTiles);
        controller.Received().IncrementAbilitiesRemaining();
    }
}
