using System;
using System.Collections.Generic;

public class CharacterController : ICharacterController
{
    public ICharacter Character { protected get; set; }
    public IHexTileController OccupiedTile { protected get; set; }
    public ITurnController TurnController { protected get; set; }
    public IHUDController HUDController { protected get; set; }

    public List<string> StatNames { protected get; set; }
    public List<ICharacterStat> CharacterStats { protected get; set; }
    public List<IAbility> Abilities { protected get; set; }

    public string OwnedByPlayer { protected get; set; }

    private int MovesRemaining { get; set; }
    private int AbilitiesRemaining { get; set; }
    
    public void Select()
    {
        OccupiedTile.Select();
        HUDController.UpdateSelectedHUD(StatNames, CharacterStats, OwnedByPlayer);
    }

    public void Deselect()
    {
        OccupiedTile.Deselect();
        HUDController.ClearSelectedHUD();
    }

    public void ExecuteMove(IHexTileController targetTile)
    {
        if (!(MovesRemaining > 0)) return;

        OccupiedTile.Deselect();
        OccupiedTile.OccupantCharacter = null;

        Character.MoveToTile(targetTile.HexTile);
        OccupiedTile = targetTile;

        targetTile.OccupantCharacter = this;
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
        // TODO: Determine how initiative is calculated.
        return 1.0f;
    }
    
    public void Damage(float damage)
    {
        CharacterStats[0].AddModifier(new StatModifier(-damage, StatModType.Flat));
    }
}
