using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ArcticFuryTests
{
    ArcticFury sut;

    ICharacter character;
    ICharacterController characterController;
    IHexTileController hexTileController;
    IAbility ability;

    ICharacter enemyCharacter;
    ICharacterController enemyCharacterController;
    IHexTileController enemyHexTileController;
    IAbility enemyAbility;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        hexTileController = Substitute.For<IHexTileController>();
        ability = Substitute.For<IAbility>();

        character.Controller.Returns(characterController);
        hexTileController.OccupantCharacter.Returns(characterController);
        List<IAbility> abilities = new List<IAbility>() { ability };
        characterController.Abilities.Returns(abilities);

        enemyCharacter = Substitute.For<ICharacter>();
        enemyCharacterController = Substitute.For<ICharacterController>();
        enemyHexTileController = Substitute.For<IHexTileController>();
        enemyAbility = Substitute.For<IAbility>();

        List<IAbility> enemyAbilities = new List<IAbility>() { enemyAbility };
        enemyCharacterController.Abilities.Returns(enemyAbilities);
        enemyCharacter.Controller.Returns(enemyCharacterController);
        enemyHexTileController.OccupantCharacter.Returns(enemyCharacterController);

        sut = new ArcticFury(character);
    }

    [Test]
    public void Not_executed_if_character_dies()
    {
        sut.Handle(new DeathEvent(characterController));
        sut.Execute(new List<IHexTileController>() { enemyHexTileController });

        ability.DidNotReceive().Execute(Arg.Any<List<IHexTileController>>());
        enemyAbility.DidNotReceive().Execute(Arg.Any<List<IHexTileController>>());
    }
}
