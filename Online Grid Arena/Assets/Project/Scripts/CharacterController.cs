using System;
using UnityEngine;

[Serializable]
public class CharacterController
{
    // Placeholder stats
    public CharacterStat Health;
    public CharacterStat Damage;
    public CharacterStat Moves;

    private IMovementController _movementController;

    public CharacterController() : this(new CharacterStat(0.0f), new CharacterStat(0.0f)) { }

    public CharacterController(CharacterStat health, CharacterStat damage)
    {
        Health = health;
        Damage = damage;
    }

    public CharacterController(IMovementController movementController) : this()
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
