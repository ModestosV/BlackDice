using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : ICharacterController
{
    public IHexTileController OccupiedTile { get; set; }
    public IHUDController HUDController { protected get; set; }

    public Dictionary<string, ICharacterStat> CharacterStats { get; set; }
    public List<IAbility> Abilities { get; set; }
    public List<IEffect> Effects { get; set; }

    private int MovesRemaining { get { return (int)CharacterStats["moves"].CurrentValue; } }

    public string Owner { get; set; }
    public Texture CharacterIcon { get; set; }
    public Color32 BorderColor { get; set; }

    public IHealthBar HealthBar { protected get; set; }
    public SpriteRenderer ActiveCircle { get; set; }

    public ICharacter Character { get; }
    public CharacterState CharacterState { get; set; }

    private int abilitiesRemaining;

    public CharacterController(ICharacter character)
    {
        Character = character;
        CharacterState = CharacterState.UNUSED;
    }

    public void UpdateSelectedHUD()
    {
        HUDController.UpdateSelectedHUD(CharacterStats, Owner, Abilities, Effects);
    }

    public void ClearSelectedHUD()
    {
        HUDController.ClearSelectedHUD();
    }

    public void UpdateTargetHUD()
    {
        HUDController.UpdateTargetHUD(CharacterStats, Owner);
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
        OccupiedTile.OccupantCharacter = null;

        Character.MoveToTile(targetTile.HexTile);
        OccupiedTile = targetTile;

        targetTile.OccupantCharacter = this;

        CharacterStats["moves"].CurrentValue -= distance;

        EventBus.Publish(new SelectActivePlayerEvent());

        CheckExhausted();
    }

    public void ExecuteAbility(int abilityNumber, List<IHexTileController> targetTiles)
    {
        if (!(abilitiesRemaining > 0)) return;

        Abilities[abilityNumber].Execute(targetTiles);

        abilitiesRemaining--;
        
        CheckExhausted();
    }

    public void Refresh()
    {
        CharacterStats["moves"].Refresh();
        abilitiesRemaining = 1;

        Abilities.ForEach(ability => {
            if(ability is IActiveAbility)
            {
                ((IActiveAbility) ability).UpdateCooldown();
            }
        });
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
        ActiveCircle.enabled = true;
        foreach (IEffect e in Effects)
        {
            if (e.Type == EffectType.START_OF_TURN)
            {
                ApplyStack(e);
            }
        }
        Refresh();
        EventBus.Publish(new SelectActivePlayerEvent());
    }

    public void EndOfTurn()
    {
        if(CharacterState != CharacterState.DEAD)
        {
            CharacterState = CharacterState.EXHAUSTED;
        }

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
                    RemoveEffect(e);
                    if (e.StacksRanOut())
                    {
                        e.Refresh();
                        Effects.Remove(e);
                        break;
                    }
                }
                else
                {
                    if (e.Type == EffectType.CONSTANT)
                    {
                        RemoveEffect(e);
                    }
                    Effects.Remove(e);
                    break;
                }
            }
        }
        ActiveCircle.enabled = false;
        EventBus.Publish(new ExhaustCharacterEvent(this));
    }

    private void RemoveEffect(IEffect effect)
    {
        foreach (KeyValuePair<string, float> ef in effect.GetEffects())
        {
            this.CharacterStats[ef.Key.ToString()].CurrentValue -= ef.Value;
        }
    }

    public void Die()
    {
        CharacterState = CharacterState.DEAD;
        OccupiedTile.ClearOccupant();
        Character.Destroy();
        EventBus.Publish(new DeathEvent(this));
    }

    public bool CanMove(int distance = 1)
    {
        return distance <= MovesRemaining;
    }

    public bool CanUseAbility(int abilityIndex)
    {
        IActiveAbility ability;

        // Meant to execute ability in the case where an ability can be used and the ability has no target.
        try
        {
            ability = (ITargetedAbility) Abilities[abilityIndex];
        }
        catch(InvalidCastException)
        {
            try
            {
                ability = (IActiveAbility) Abilities[abilityIndex];
                if(abilitiesRemaining > 0 && !ability.IsOnCooldown())
                {
                    ExecuteAbility(abilityIndex, new List<IHexTileController>() { OccupiedTile });
                }
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        return abilitiesRemaining > 0  && !ability.IsOnCooldown();
    }

    public bool IsAlly(ICharacterController character)
    {
        return Owner.Equals(character.Owner);
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
        HealthBar.SetHealthText(Mathf.CeilToInt(CharacterStats["health"].CurrentValue).ToString(), Mathf.CeilToInt(CharacterStats["health"].Value).ToString());
    }

    private void CheckExhausted()
    {
        Debug.Log($"CheckedExhausted() called; values of moves remaining, abilities remaining, character state: ({MovesRemaining}, {abilitiesRemaining},  {CharacterState})");
        if (MovesRemaining <= 0 && abilitiesRemaining <= 0 && CharacterState != CharacterState.DEAD)
        {
            HUDController.PulseEndTurnButton();
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
