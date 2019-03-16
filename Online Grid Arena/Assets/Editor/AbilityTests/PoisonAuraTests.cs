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
    int counter1;
    int counter2;

    [SetUp]
    public void Init()
    {
        counter1 = 0;
        counter2 = 0;
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
        firstNeighborTile.When(x => x.PlayAbilityAnimation(Arg.Any<GameObject>())).Do(x => counter1++);
        secondNeighborTile.When(x => x.PlayAbilityAnimation(Arg.Any<GameObject>())).Do(x => counter2++);

        sut.Execute(targetTiles);
        Assert.AreEqual(1, counter1);
        Assert.AreEqual(1, counter2);
    }
}
