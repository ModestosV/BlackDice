using System;
using UnityEngine;

[Serializable]
public class CharacterController
{
    // Placeholder stats
    public CharacterStat health;
    public CharacterStat damage;
    public CharacterStat moves;

    private IMovementController movementController;

    public CharacterController() : this(new CharacterStat(0.0f), new CharacterStat(0.0f), new CharacterStat(0.0f)) { }

    public CharacterController(CharacterStat health, CharacterStat damage, CharacterStat moves)
    {
        this.health = health;
        this.damage = damage;
        this.moves = moves;
    }

    public CharacterController(IMovementController movementController) : this()
    {
        this.movementController = movementController;
    }

    public void SetMovementController(IMovementController movementController)
    {
        this.movementController = movementController;
    }

    public void MoveX(float value)
    {
        movementController.MoveX(value);
    }

    public void MoveY(float value)
    {
        movementController.MoveY(value);
    }
}
