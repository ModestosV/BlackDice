using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SlapTests
{
    Slap sut;

    ICharacter character;
    ICharacterController controller;

    ICharacterStat attackStat;
    Dictionary<string, ICharacterStat> characterStats;

    IHexTileController target;
    List<IHexTileController> targetTiles;

    const int COOLDOWN = 1;
    const int MIN_RANGE = 1;
    const int MAX_RANGE = 2;
    const float ATTACK_VALUE = 15;
    const int ONE = 1;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        controller = Substitute.For<ICharacterController>();
        attackStat = Substitute.For<ICharacterStat>();
        target = Substitute.For<IHexTileController>();

        characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "attack", attackStat }
        };

        targetTiles = new List<IHexTileController>() { target };

        character.Controller.Returns(controller);
        controller.CharacterStats.Returns(characterStats);
        attackStat.Value.Returns(ATTACK_VALUE);

        sut = new Slap(character);
    }

    [Test]
    public void Is_in_range_returns_false_if_target_is_out_of_range()
    {
        Assert.That(sut.IsInRange(MIN_RANGE), Is.EqualTo(true));
        Assert.That(sut.IsInRange(MAX_RANGE), Is.EqualTo(false));
    }

    [Test]
    public void Execute_calls_damage_at_least_once()
    {
        sut.Execute(targetTiles);
        target.Received().Damage(ATTACK_VALUE);
    }

    [Test]
    public void Update_cooldown_decrements_remaining_cooldown()
    {
        sut.Execute(targetTiles);
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(false));
    }
}
