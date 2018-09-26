using NUnit.Framework;
using NSubstitute;

public class CharacterTests
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
        var sut = GetControllerMock(movementController);

        sut.MoveX(distanceToMove);

        movementController.Received(1).MoveX(Arg.Any<float>());
    }

    [Test]
    public void Character_can_move_vertically()
    {
        var sut = GetControllerMock(movementController);

        sut.MoveY(distanceToMove);

        movementController.Received(1).MoveY(Arg.Any<float>());
    }

    [Test]
    public void Character_can_move_vertically_with_real_controller()
    {
        var sut = new CharacterController(movementController);

        sut.MoveY(distanceToMove);

        movementController.Received(1).MoveY(Arg.Any<float>());
    }

    private CharacterController GetControllerMock(IMovementController movementController)
    {
        var characterController = Substitute.For<CharacterController>();
        characterController.SetMovementController(movementController);
        return characterController;
    }
}
