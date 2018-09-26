using System;

[Serializable]
public class CharacterController
{
    // Placeholder stats
    public CharacterStat Health;
    public CharacterStat Damage;

    private IMovementController _movementController;

    public CharacterController()
    {

    }

    public CharacterController(CharacterStat health, CharacterStat damage)
    {
        Health = health;
        Damage = damage;
    }

    public CharacterController(IMovementController movementController) : this (new CharacterStat(1.0f), new CharacterStat(1.0f))
    {
        _movementController = movementController;
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
