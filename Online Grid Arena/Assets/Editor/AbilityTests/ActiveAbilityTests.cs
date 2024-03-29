﻿using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class ActiveAbilityTests
{
    DefaultAttack sut;

    ICharacter character;
    ICharacterController controller;
    ICharacter targetCharacter;
    ICharacterController targetCharacterController;

    ICharacterStat attackStat;
    Dictionary<string, ICharacterStat> characterStats;
    Dictionary<string, ICharacterStat> targetCharacterStats;

    IHexTileController target;
    List<IHexTileController> targetTiles;

    IEffect effect;

    const int COOLDOWN = 1;
    const int MIN_RANGE = 1;
    const int MAX_RANGE = 9;
    const float ATTACK_VALUE = 1;
    const int ONE = 1;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        controller = Substitute.For<ICharacterController>();
        attackStat = Substitute.For<ICharacterStat>();
        target = Substitute.For<IHexTileController>();
        effect = Substitute.For<IEffect>();
        targetCharacter = Substitute.For<ICharacter>();
        targetCharacterController = Substitute.For<ICharacterController>();

        characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "attack", attackStat }
        };

        targetTiles = new List<IHexTileController>() { target };

        character.Controller.Returns(controller);
        controller.CharacterStats.Returns(characterStats);

        targetCharacterStats = new Dictionary<string, ICharacterStat>()
        {
            { "health", new CharacterStat(100.0f) },
            { "defense", new CharacterStat(100.0f) }
        };

        targetTiles[0].OccupantCharacter.Returns(targetCharacterController);
        targetCharacter.Controller.Returns(targetCharacterController);
        targetCharacterController.CharacterStats.Returns(targetCharacterStats);

        attackStat.Value.Returns(ATTACK_VALUE);

        sut = new DefaultAttack(character);
    }

    [Test]
    public void Is_in_range_returns_false_if_target_is_out_of_range()
    {
        Assert.That(sut.IsInRange(MIN_RANGE), Is.EqualTo(true));
        Assert.That(sut.IsInRange(MAX_RANGE), Is.EqualTo(false));
    }

    [Test]
    public void Update_cooldown_decrements_remaining_cooldown()
    {
        sut.Execute(targetTiles);
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(false));
    }

    [Test]
    public void Add_effect_adds_effect_to_ability()
    {
        sut.AddEffect(effect);

        Assert.That(sut.Effects.Count, Is.EqualTo(ONE));
    }


    
}
