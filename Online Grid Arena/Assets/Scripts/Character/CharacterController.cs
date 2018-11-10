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

    private int MovesRemaining { get { return (int)CharacterStats[2].CurrentValue; } }
    public int AbilitiesRemaining { protected get; set; }
    public string OwnedByPlayer { get; set; }

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

    public void RefreshStats()
    {
        foreach (ICharacterStat stat in CharacterStats)
        {
            stat.Refresh();
        }
    }

    public void ExecuteMove(List<IHexTileController> path)
    {
        if (!(MovesRemaining > 0 && MovesRemaining >= path.Count -1)) return;

        int distance = path.Count - 1;
        IHexTileController targetTile = path[distance];

        OccupiedTile.Deselect();
        OccupiedTile.OccupantCharacter = null;

        Character.MoveToTile(targetTile.HexTile);
        OccupiedTile = targetTile;

        targetTile.OccupantCharacter = this;
        targetTile.Select();
        CharacterStats[2].CurrentValue -= distance;
        UpdateSelectedHUD();
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
        CharacterStats[2].Refresh();
        AbilitiesRemaining = 1;
    }

    public float GetInitiative()
    {
        // TODO: Determine how initiative is calculated.
        return 1.0f;
    }
    
    public void Damage(float damage)
    {
        CharacterStats[0].CurrentValue -= damage;
        if (CharacterStats[0].CurrentValue <= 0)
        {
            Die();
            TurnController.CheckWinCondition();
        }
    }

    public void Die()
    {
        OccupiedTile.ClearOccupant();
        TurnController.RemoveCharacter(this);
        Character.Destroy();
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
