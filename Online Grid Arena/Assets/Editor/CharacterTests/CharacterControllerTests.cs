using NUnit.Framework;
using NSubstitute;

public class CharacterControllerTests
{
    private float distanceToMove;
    private IMovementController movementController;

    [SetUp]
    public void Init()
    {
        distanceToMove = 1.0f;
        movementController = Substitute.For<IMovementController>();
    }

    [Test]
    public void Character_can_move_horizontally()
    {
        var sut = new CharacterController(movementController);

        sut.MoveX(distanceToMove);

        movementController.Received(1).MoveX(Arg.Any<float>());
    }

    [Test]
    public void Character_can_move_vertically()
    {
        var sut = new CharacterController(movementController);

        sut.MoveY(distanceToMove);

        movementController.Received(1).MoveY(Arg.Any<float>());
    }
}
