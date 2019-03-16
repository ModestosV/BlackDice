using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAuraTests
{
    PoisonAura sut;

    ICharacter character;
    ICharacterController controller;

    IHexTileController occupiedTile;
    IHexTileController firstNeighborTile;
    IHexTileController secondNeighborTile;
    List<IHexTileController> targetTiles;
    List<IHexTileController> neighborTiles;
    int counter;

    [SetUp]
    public void Init()
    {
        occupiedTile = Substitute.For<IHexTileController>();
        firstNeighborTile = Substitute.For<IHexTileController>();
        secondNeighborTile = Substitute.For<IHexTileController>();
        character = Substitute.For<ICharacter>();
        controller = Substitute.For<ICharacterController>();

        targetTiles = new List<IHexTileController>() { occupiedTile };
        neighborTiles = new List<IHexTileController>() { firstNeighborTile, secondNeighborTile };

        character.Controller.Returns(controller);
        occupiedTile.OccupantCharacter.Returns(controller);
        controller.OccupiedTile.Returns(occupiedTile);
        occupiedTile.GetNeighbors().Returns(neighborTiles);

        sut = new PoisonAura(character);
    }

    [Test]
    public void Execute_applies_to_all_neighbors()
    {
        firstNeighborTile.OccupantCharacter.Returns(x => null);
        secondNeighborTile.OccupantCharacter.Returns(x => null);
        firstNeighborTile.When(x => x.PlayAbilityAnimation(Arg.Any<GameObject>())).Do(x => counter++);
        secondNeighborTile.When(x => x.PlayAbilityAnimation(Arg.Any<GameObject>())).Do(x => counter++);

        sut.Execute(targetTiles);
        Assert.AreEqual(2, counter);
    }
}
