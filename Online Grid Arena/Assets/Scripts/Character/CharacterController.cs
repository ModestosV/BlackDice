using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacterController : ICharacterController
{
    public ICharacter Character { protected get; set; }
    public IHexTileController OccupiedTile { protected get; set; }
    public ITurnController TurnController { protected get; set; }
    public IHUDController HUDController { protected get; set; }

    public Dictionary<string, ICharacterStat> CharacterStats { protected get; set; }
    public List<IAbility> Abilities { protected get; set; }

    private int MovesRemaining { get { return (int)CharacterStats["moves"].CurrentValue; } }
    public int AbilitiesRemaining { protected get; set; }

    public string OwnedByPlayer { get; set; }
    public Texture CharacterIcon { protected get; set; }
    public Color32 BorderColor { protected get; set; }

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
        HUDController.UpdateSelectedHUD(CharacterStats, OwnedByPlayer);
    }

    public void ClearSelectedHUD()
    {
        HUDController.ClearSelectedHUD();
    }

    public void UpdateTargetHUD()
    {
        HUDController.UpdateTargetHUD(CharacterStats, OwnedByPlayer);
    }

    public void ClearTargetHUD()
    {
        HUDController.ClearTargetHUD();
    }

    public void RefreshStats()
    {
        foreach (ICharacterStat stat in CharacterStats.Values)
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
        CharacterStats["moves"].CurrentValue -= distance;
        UpdateSelectedHUD();
        CheckExhausted();
    }

    public void ExecuteAbility(int abilityNumber, IHexTileController targetTile)
    {
        if (!(AbilitiesRemaining > 0)) return;

        if (!(abilityNumber < Abilities.Count && abilityNumber > -1)) return;

        Abilities[abilityNumber].Execute(targetTile);

        AbilitiesRemaining--;
        CheckExhausted();
    }

    private void CheckExhausted()
    {
        if (!(MovesRemaining > 0 || AbilitiesRemaining > 0))
        {
            EventBus.Publish(new StartNewTurnEvent());
        }
    }

    public void Refresh()
    {
        CharacterStats["moves"].Refresh();
        AbilitiesRemaining = 1;
    }

    public float GetInitiative()
    {
        // TODO: Determine how initiative is calculated.
        return 1.0f;
    }
    
    public void Damage(float damage)
    {
        CharacterStats["health"].CurrentValue -= damage;
        if (CharacterStats["health"].CurrentValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        CharacterStats["health"].CurrentValue += heal;
    }
    
    public void Die()
    {
        EventBus.Publish(new DeathEvent(this));
        OccupiedTile.ClearOccupant();
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

    public bool IsActiveCharacter()
    {
        return TurnController.IsActiveCharacter(this);
    }

    public void UpdateTurnTile(ITurnTile turnTileToUpdate)
    {
        turnTileToUpdate.CharacterIcon = CharacterIcon;
        turnTileToUpdate.BorderColor = BorderColor;
        turnTileToUpdate.UpdateTile();
    }

    public bool IsAlly(ICharacterController otherCharacter)
    {
        return OwnedByPlayer == otherCharacter.OwnedByPlayer;
    }

    public AbilityType GetAbilityType(int abilityIndex)
    {
        return Abilities[abilityIndex].Type;
    }
}
