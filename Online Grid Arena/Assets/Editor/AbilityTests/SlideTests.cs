using NSubstitute;
using NUnit.Framework;

public class SlideTests
{
    Slide sut;

    ICharacter character;
    ICharacterController controller;
    
    const int MIN_RANGE = 1;
    const int MAX_RANGE = 101;
    const float DAMAGE = 0;

    [SetUp]
    public void Init()
    {
        character = Substitute.For<ICharacter>();
        controller = Substitute.For<ICharacterController>();

        character.Controller.Returns(controller);

        sut = new Slide(character);
    }

    [Test]
    public void Is_in_range_returns_false_if_target_is_out_of_range()
    {
        Assert.That(sut.IsInRange(MIN_RANGE), Is.EqualTo(true));
        Assert.That(sut.IsInRange(MAX_RANGE), Is.EqualTo(false));
    }
}
