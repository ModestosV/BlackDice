using System;
using System.Collections.Generic;

[Serializable]
public class CharacterController : ICharacterController
{
    public ICharacter Character { get; set; }
    public IHexTileController OccupiedTile { get; set; }
    public ITurnController TurnController { get; set; }
    public int ownedByPlayer;
    public int MovesRemaining { get; set; }
    public int AbilitiesRemaining { get; set; }

    public CharacterStatNameSet CharacterStatNameSet { get; set; }

    public List<ICharacterStat> CharacterStats { get; set; }
    public List<IAbility> Abilities { get; set; }

    public int OwnedByPlayer { get { return ownedByPlayer; } }

    public void Select()
    {
        OccupiedTile.Select();
    }

    public void Deselect()
    {
        OccupiedTile.Deselect();
    }

    public void ExecuteMove(IHexTileController targetTile)
    {
        if (!(MovesRemaining > 0)) return;

        OccupiedTile.Deselect();
        OccupiedTile.OccupantCharacter = null;

        Character.MoveToTile(targetTile.HexTile);
        OccupiedTile = targetTile;

        targetTile.OccupantCharacter = Character;
        targetTile.Select();
        MovesRemaining--;
        CheckExhausted();
    }

    public void ExecuteAbility(int abilityNumber, ICharacterController targetCharacter)
    {
        if (!(AbilitiesRemaining > 0)) return;

        IAbility ability = Abilities[abilityNumber];

        if (ability.Type == AbilityType.ATTACK)
        {
            targetCharacter.Damage(ability.Values[0] * CharacterStats[1].Value);
        }

        AbilitiesRemaining--;
        CheckExhausted();
    }

    private bool CheckExhausted()
    {
        if (!(MovesRemaining > 0 || AbilitiesRemaining > 0))
        {
            TurnController.StartNextTurn();
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
        CharacterStats[0].AddModifier(new StatModifier(-damage, StatModType.Flat));
    }
}
