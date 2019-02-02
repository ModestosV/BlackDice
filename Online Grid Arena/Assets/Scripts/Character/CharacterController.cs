using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : ICharacterController
{
    public IHexTileController OccupiedTile { get; set; }
    public IHUDController HUDController { protected get; set; }

    public Dictionary<string, ICharacterStat> CharacterStats { get; set; }
    public List<IAbility> Abilities { protected get; set; }
    public List<IEffect> Effects { get; set; }

    private int MovesRemaining { get { return (int)CharacterStats["moves"].CurrentValue; } }

    public string CharacterOwner { get; set; }
    public Texture CharacterIcon { protected get; set; }
    public Color32 BorderColor { protected get; set; }

    public IHealthBar HealthBar { protected get; set; }
    public SpriteRenderer ActiveCircle { get; set; }


    protected readonly ICharacter character;
    private int abilitiesRemaining;

    public CharacterController(ICharacter character)
    {
        this.character = character;
    }

    public void UpdateSelectedHUD()
    {
        HUDController.UpdateSelectedHUD(CharacterStats, CharacterOwner, Abilities, Effects);
    }

    public void ClearSelectedHUD()
    {
        HUDController.ClearSelectedHUD();
    }

    public void UpdateTargetHUD()
    {
        HUDController.UpdateTargetHUD(CharacterStats, CharacterOwner);
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

        character.MoveToTile(targetTile.HexTile);
        OccupiedTile = targetTile;

        targetTile.OccupantCharacter = this;

        CharacterStats["moves"].CurrentValue -= distance;
        UpdateSelectedHUD();

        EventBus.Publish(new SelectActivePlayerEvent());

        CheckExhausted();
    }

    public void ExecuteAbility(int abilityNumber, List<IHexTileController> targetTiles)
    {
        if (!(abilitiesRemaining > 0)) return;

        Abilities[abilityNumber].Execute(targetTiles);

        abilitiesRemaining--;

        UpdateSelectedHUD();
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

    public float GetInitiative()
    {
        // TODO: Determine how initiative is calculated.
        return 1.0f;
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
        UpdateSelectedHUD();
        EventBus.Publish(new SelectActivePlayerEvent());
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
        EventBus.Publish(new DeathEvent(this));
        OccupiedTile.ClearOccupant();
        character.Destroy();
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

        return abilitiesRemaining > 0  && !ability.IsOnCooldown();
    }

    public void UpdateTurnTile(ITurnTile turnTileToUpdate)
    {
        turnTileToUpdate.CharacterIcon = CharacterIcon;
        turnTileToUpdate.BorderColor = BorderColor;
        turnTileToUpdate.UpdateTile();
    }

    public bool IsAlly(ICharacterController character)
    {
        return CharacterOwner.Equals(character.CharacterOwner);
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
        if (!(MovesRemaining > 0 || abilitiesRemaining > 0))
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
