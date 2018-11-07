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

    public int MovesRemaining { protected get; set; }
    public int AbilitiesRemaining { protected get; set; }
    public string OwnedByPlayer { protected get; set; }

    public void Select()
    {
        OccupiedTile.Select();
    }

    public void Deselect()
    {
        OccupiedTile.Deselect();
    }

    public void UpdateSelectedHUD()
    {
        HUDController.UpdateSelectedHUD(StatNames, CharacterStats, OwnedByPlayer);
    }

    public void ClearSelectedHUD()
    {
        HUDController.ClearSelectedHUD();
    }

    public void UpdateTargetHUD()
    {
        HUDController.UpdateTargetHUD(StatNames, CharacterStats, OwnedByPlayer);
    }

    public void ClearTargetHUD()
    {
        HUDController.ClearTargetHUD();
    }

    public void ExecuteMove(List<IHexTileController> path)
    {
        if (!(MovesRemaining > 0)) return;

        int distance = path.Count - 1;
        IHexTileController targetTile = path[distance];

        OccupiedTile.Deselect();
        OccupiedTile.OccupantCharacter = null;

        Character.MoveToTile(targetTile.HexTile);
        OccupiedTile = targetTile;

        targetTile.OccupantCharacter = this;
        targetTile.Select();
        MovesRemaining -= distance;
        CheckExhausted();
    }

    public void ExecuteAbility(int abilityNumber, ICharacterController targetCharacter)
    {
        if (!(AbilitiesRemaining > 0)) return;

        if (!(abilityNumber < Abilities.Count && abilityNumber > -1)) return;

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
        MovesRemaining = (int)CharacterStats[2].Value;
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

    public bool CanMove(int distance = 1)
    {
        return distance <= MovesRemaining;
    }

    public bool CanUseAbility()
    {
        return AbilitiesRemaining > 0;
    }
}
