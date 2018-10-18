using System;
using UnityEngine;

[Serializable]
public class CharacterController
{
    // Placeholder stats
    public CharacterStat health;
    public CharacterStat damage;
    public CharacterStat moves;

    public IMovementController MovementController { get; set; }

    public CharacterController() : this(new CharacterStat(0.0f), new CharacterStat(0.0f), new CharacterStat(0.0f)) { }

    public CharacterController(CharacterStat health, CharacterStat damage, CharacterStat moves)
    {
        this.health = health;
        this.damage = damage;
        this.moves = moves;
    }

    public CharacterController(IMovementController movementController) : this()
    {
        MovementController = movementController;
    }

    public void MoveX(float value)
    {
        MovementController.MoveX(value);
    }

    public void MoveY(float value)
    {
        MovementController.MoveY(value);
    }

    public override string ToString()
    {
        return string.Format("{0}, {1}, {2}", health.ToString(), damage.ToString(), moves.ToString());
    }
}
