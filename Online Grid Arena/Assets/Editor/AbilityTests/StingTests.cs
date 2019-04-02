using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class StingTests
{
    Sting sut;

    ICharacter character;
    ICharacterController characterController;
    IActiveAbility ability1;
    IActiveAbility ability2;
    IHexTileController hexTileController;
    Dictionary<string, ICharacterStat> characterStats;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        ability1 = Substitute.For<IActiveAbility>();
        ability2 = Substitute.For<IActiveAbility>();
        hexTileController = Substitute.For<IHexTileController>();

        characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "attack", new CharacterStat(20.0f) },
            { "health",  new CharacterStat(20.0f) },
            { "defense", new CharacterStat(100.0f) }
        };

        character.Controller.Returns(characterController);
        characterController.Abilities.Returns(new List<IAbility>() { ability1, ability2 });
        characterController.CharacterStats.Returns(characterStats);

        sut = new Sting(character);
    }

    [Test]
    public void Lowers_ability_cooldowns()
    {
        ICharacterController enemy = Substitute.For<ICharacterController>();
        enemy.CharacterStats.Returns(characterStats);

        hexTileController.OccupantCharacter.Returns(enemy);
        sut.Execute(new List<IHexTileController>() { hexTileController });
        ability1.Received(1).IsOnCooldown();
        ability2.Received(1).IsOnCooldown();
    }
}
