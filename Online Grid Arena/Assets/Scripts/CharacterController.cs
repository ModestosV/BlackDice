using System;

[Serializable]
public class CharacterController
{
    // Placeholder stats
    public CharacterStat Health;
    public CharacterStat Damage;

    private IMovementController _movementController;

    public CharacterController(CharacterStat health, CharacterStat damage)
    {
        Health = health;
        Damage = damage;
    }

    public void SetMovementController(IMovementController movementController)
    {
        _movementController = movementController;
    }

    public void MoveX(float value)
    {
        _movementController.MoveX(value);
    }

    public void MoveY(float value)
    {
        _movementController.MoveY(value);
    }
}
