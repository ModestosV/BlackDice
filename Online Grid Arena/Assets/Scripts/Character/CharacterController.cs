using System;
using System.Collections.Generic;

[Serializable]
public class CharacterController : ICharacterController
{
    public int ownedByPlayer;
    public IHexTile OccupiedTile { get; set; }
    public ICharacter Character { get; set; }
    public ITurnController TurnController { get; set; }
    public int MovesRemaining { get; set; }
    public int AbilitiesRemaining { get; set; }

    public CharacterStatNameSet CharacterStatNameSet { get; set; }

    public List<ICharacterStat> CharacterStats { get; set; }
    public List<IAbility> Abilities { get; set; }

    public int OwnedByPlayer { get { return ownedByPlayer; } }

    public void MoveToTile(IHexTile targetTile)
    {
        if (!(MovesRemaining > 0)) return;

        IHexTile currentTile = Character.Controller.OccupiedTile;
        currentTile.Controller.Deselect();
        currentTile.Controller.OccupantCharacter = null;

        Character.MoveToTile(targetTile);
        Character.Controller.OccupiedTile = targetTile;

        targetTile.Controller.OccupantCharacter = Character;
        targetTile.Controller.Select();
        MovesRemaining--;
        CheckExhausted();
    }

    public void ExecuteAbility(int abilityNumber, ICharacter targetCharacter)
    {
        if (!(AbilitiesRemaining > 0)) return;

        IAbility ability = Abilities[abilityNumber];

        if (ability.Type == AbilityType.ATTACK)
        {
            targetCharacter.Controller.Damage(ability.Values[0]);
        }

        AbilitiesRemaining--;
        CheckExhausted();
    }

    private bool CheckExhausted()
    {
        if (!(MovesRemaining > 0 || AbilitiesRemaining > 0))
        {
            TurnController.EndTurn();
            return true;
        }
        return false;
    }

    public void Refresh()
    {
        MovesRemaining = 1;
        AbilitiesRemaining = 1;
    }

    public float GetInitiative()
    {
        return 1.0f;
    }
    
    public void Damage(float damage)
    {
        CharacterStats[0].AddModifier(new StatModifier(-1.0f, StatModType.Flat));
    }
}
