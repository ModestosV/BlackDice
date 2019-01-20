using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : ICharacterController
{
    public ICharacter Character { protected get; set; }
    public IHexTileController OccupiedTile { get; set; }
    public IHUDController HUDController { protected get; set; }

    public Dictionary<string, ICharacterStat> CharacterStats { get; set; }
    public List<IAbility> Abilities { protected get; set; }
    public List<IEffect> Effects { get; set; }

    private int MovesRemaining { get { return (int)CharacterStats["moves"].CurrentValue; } }
    public int AbilitiesRemaining { protected get; set; }

    public string OwnedByPlayer { get; set; }
    public Texture CharacterIcon { protected get; set; }
    public Color32 BorderColor { protected get; set; }

    public IHealthBar HealthBar { protected get; set; }

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
        HUDController.UpdateSelectedHUD(CharacterStats, OwnedByPlayer, Abilities, Effects);
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

    public void ExecuteAbility(int abilityNumber, List<IHexTileController> targetTiles)
    {
        if (!(AbilitiesRemaining > 0)) return;

        if (!(abilityNumber < Abilities.Count && abilityNumber > -1)) return;

        Abilities[abilityNumber].Execute(targetTiles);

        AbilitiesRemaining--;

        UpdateSelectedHUD();
        CheckExhausted();
    }

    public void Refresh()
    {
        CharacterStats["moves"].Refresh();
        AbilitiesRemaining = 1;

        Abilities.ForEach(ability => {
            if(ability is IActiveAbility)
            {
                ((IActiveAbility) ability).UpdateCooldown();
            }
        });
    }

    public float GetInitiative()
    {
        // TODO: Determine how initiative is calculated.
        return 1.0f;
    }
    
    public void Damage(float damage)
    {
        CharacterStats["health"].CurrentValue -= (damage/this.CharacterStats["defense"].CurrentValue)*100;
        UpdateHealthBar();
        if (CharacterStats["health"].CurrentValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        CharacterStats["health"].CurrentValue += heal;
        UpdateHealthBar();
    }

    public void ApplyEffect(IEffect effect)
    {
        bool effectExists = false;
        IEffect existingEffect = null;
        foreach (IEffect e in Effects)
        {
            if (e.GetName().Equals(effect.GetName()))
            {
                effectExists = true;
                existingEffect = e;
            }
        }
        if (effectExists)
        {
            if (existingEffect.Type == EffectType.STACK)
            {
                if (!existingEffect.IsMaxStacks())
                {
                    this.ApplyStack(existingEffect);
                }
            }
            existingEffect.Refresh();
        }
        else
        {
            this.Effects.Add(effect);
            if (effect.Type == EffectType.STACK)
            {
                this.ApplyStack(effect);
            }
        }
    }

    private void ApplyStack(IEffect newEffect)
    {
        foreach (KeyValuePair<string, float> ef in newEffect.GetEffects())
        {
            if (ef.Key == "attack" || ef.Key == "defense")
            {
                CharacterStats[ef.Key].BaseValue += ef.Value;
            }
            CharacterStats[ef.Key].CurrentValue += ef.Value;
        }
    }

    public void StartOfTurn()
    {
        foreach (IEffect e in Effects)
        {
            if (e.Type == EffectType.START_OF_TURN)
            {
                ApplyStack(e);
            }
        }
    }

    public void EndOfTurn()
    {
        foreach (IEffect e in Effects)
        {
            if (e.Type == EffectType.END_OF_TURN)
            {
                ApplyStack(e);
            }
            e.DecrementDuration();
            if (e.IsDurationOver())
            {
                if (e.Type == EffectType.STACK)
                {
                    e.DecrementStack();
                    RemoveEffectOf(e);
                    if (e.StacksRanOut())
                    {
                        e.Reset();
                        Effects.Remove(e);
                        break;
                    }
                }
                else
                {
                    if (e.Type == EffectType.CONSTANT)
                    {
                        RemoveEffectOf(e);
                    }
                    Effects.Remove(e);
                    break;
                }
            }
        }
    }

    private void RemoveEffectOf(IEffect newEffect)
    {
        foreach (KeyValuePair<string, float> ef in newEffect.GetEffects())
        {
            if (ef.Key == "attack" || ef.Key == "defense")
            {
                CharacterStats[ef.Key].BaseValue -= ef.Value;
            }
            this.CharacterStats[ef.Key.ToString()].CurrentValue -= ef.Value;
        }
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

    public bool CanUseAbility(int abilityIndex)
    {
        IActiveAbility ability;

        try
        {
            ability = (IActiveAbility) Abilities[abilityIndex];
        }
        catch(InvalidCastException)
        {
            return false;
        }

        return AbilitiesRemaining > 0  && !ability.IsOnCooldown();
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

    public bool IsAbilityInRange(int abilityIndex, int range)
    {
        AbstractTargetedAbility targetedAbility = Abilities[abilityIndex] as AbstractTargetedAbility;

        if (targetedAbility != null)
        {
            return targetedAbility.IsInRange(range);
        } else
        {
            return true;
        }
    }
    
    public void UpdateHealthBar()
    {
        HealthBar.SetHealthBarRatio(CharacterStats["health"].CurrentValue / CharacterStats["health"].Value);
        HealthBar.SetHealthText(CharacterStats["health"].CurrentValue.ToString(), CharacterStats["health"].Value.ToString());
    }

    private void CheckExhausted()
    {
        if (!(MovesRemaining > 0 || AbilitiesRemaining > 0))
        {
            EndOfTurn();
            EventBus.Publish(new StartNewTurnEvent());
        }
    }

    public AbilityType GetAbilityType(int abilityIndex)
    {
        try
        {
            AbstractTargetedAbility ability = (AbstractTargetedAbility) Abilities[abilityIndex];
            return ability.Type;
        }
        catch (InvalidCastException)
        {
            return AbilityType.INVALID;
        }
    }
}
