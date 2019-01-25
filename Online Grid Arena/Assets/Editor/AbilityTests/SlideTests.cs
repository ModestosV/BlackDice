using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class SlideTests
{
    Slide sut;

    ICharacter character;
    ICharacterController controller;

    IHexTileController target;
    IHexTileController target2;
    List<IHexTileController> targetTiles;

    const int COOLDOWN = 5;
    const int MIN_RANGE = 1;
    const int MAX_RANGE = 101;
    const float DAMAGE = 0;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        controller = Substitute.For<ICharacterController>();
        target = Substitute.For<IHexTileController>();
        target2 = Substitute.For<IHexTileController>();

        targetTiles = new List<IHexTileController>() { target , target2};

        character.Controller.Returns(controller);

        sut = new Slide(character);
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
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(true));

        sut.UpdateCooldown();
        Assert.That(sut.IsOnCooldown(), Is.EqualTo(false));
    }
}
