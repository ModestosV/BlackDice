using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class KamikazeTests
{
    Kamikaze sut;

    ICharacter character;
    ICharacterController controller;

    ICharacterStat attackStat;
    Dictionary<string, ICharacterStat> characterStats;

    IHexTileController target;
    IHexTileController target2;
    IHexTileController target3;
    List<IHexTileController> neighborTiles;

    const int MIN_RANGE = 1;
    const int MAX_RANGE = 101;
    const float ATTACK_VALUE = 15f;
    const float MULTIPLIER = 2.5f;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        controller = Substitute.For<ICharacterController>();
        attackStat = Substitute.For<ICharacterStat>();
        target = Substitute.For<IHexTileController>();
        target2 = Substitute.For<IHexTileController>();
        target3 = Substitute.For<IHexTileController>();

        characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "attack", attackStat }
        };

        neighborTiles = new List<IHexTileController>() { target2, target3 };
        character.Controller.Returns(controller);
        controller.CharacterStats.Returns(characterStats);
        characterStats["attack"].CurrentValue.Returns(ATTACK_VALUE);
        attackStat.Value.Returns(ATTACK_VALUE);
        target.GetNeighbors().Returns(neighborTiles);
        sut = new Kamikaze(character);
    }

    [Test]
    public void Is_in_range_returns_false_if_target_is_out_of_range()
    {
        Assert.That(sut.IsInRange(MIN_RANGE), Is.EqualTo(true));
        Assert.That(sut.IsInRange(MAX_RANGE), Is.EqualTo(false));
    }
}
