﻿using System.Collections.Generic;
using UnityEngine;

public class CharacterController : ICharacterController
{
    public ICharacter Character { protected get; set; }
    public IHexTileController OccupiedTile { protected get; set; }
    public IHUDController HUDController { protected get; set; }

    public Dictionary<string, ICharacterStat> CharacterStats { protected get; set; }
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

        UpdateSelectedHUD();
        CheckExhausted();
    }

    public void Refresh()
    {
        CharacterStats["moves"].Refresh();
        AbilitiesRemaining = 1;
        foreach (IAbility ability in Abilities)
        {
            ability.Refresh();
        }
    }

    public float GetInitiative()
    {
        // TODO: Determine how initiative is calculated.
        return 1.0f;
    }
    
    public void Damage(float damage)
    {
        CharacterStats["health"].CurrentValue -= damage;
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

    public void AddEffect(IEffect effect)
    {
        if (Effects.Contains(effect))
        {
            effect.Refresh();
        }
        else
        {
            Effects.Add(effect);
            if (effect.Type == EffectType.STACK || effect.Type == EffectType.BUFF || effect.Type == EffectType.DEBUFF)
            {
                ApplyEffect(effect);
            }
        }
    }

    public void ApplyEndOfTurnEffects()
    {
        //decrement every effect's duration. if an effect has run out and it is not an over time effect, remove its buff or debuff.
        foreach (IEffect e in Effects)
        {
            e.Decrement();
            Dictionary<string, float> modifiers = e.GetEffects();
            if (e.Type == EffectType.DAMAGE_OVER_TIME || e.Type == EffectType.HEAL_OVER_TIME )
            {
                if (modifiers.ContainsKey("moves"))
                {
                    CharacterStats["moves"].CurrentValue += modifiers["moves"];
                }
                if (modifiers.ContainsKey("health"))
                {
                    CharacterStats["health"].CurrentValue += modifiers["health"];
                }
                if (e.HasRunOut())
                {

                    Effects.Remove(e);
                }
            }
            if (e.Type == EffectType.STACK || e.Type == EffectType.BUFF || e.Type == EffectType.DEBUFF)
            {
                if (e.StacksRanOut())
                {
                    if (modifiers.ContainsKey("moves"))
                    {
                        CharacterStats["moves"].CurrentValue -= modifiers["moves"];
                    }
                    if (modifiers.ContainsKey("health"))
                    {
                        CharacterStats["health"].CurrentValue -= modifiers["health"];
                    }
                }
            }
        }
    }

    public void ApplyEffect(IEffect effect)
    {
        Debug.LogWarning("APPLY EFFECT CALLED");
        Debug.LogWarning("current effects size: "+Effects.ToArray().Length);
        //check to see if we already contain an effect of this type
        bool exists = false;
        IEffect existingEf = null;
        foreach (IEffect e in Effects)
        {
            if (e.GetName() == effect.GetName())
            {
                exists = true;
                existingEf = e;
            }
        }
        if (exists)
        {
            existingEf.Refresh();
            Debug.LogWarning(existingEf.Print());
        }
        else
        {
            //add
            this.Effects.Add(effect);
            Debug.LogWarning(effect.Print());
            //NOW WHAT
        }


        Debug.LogWarning("new effects size: " + Effects.ToArray().Length);
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
        return AbilitiesRemaining > 0 && HasAbility(abilityIndex) && !Abilities[abilityIndex].IsOnCooldown();
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

    public bool HasAbility(int abilityIndex)
    {
        if (abilityIndex >= Abilities.Count) return false;
        return true;
    }

    public AbilityType GetAbilityType(int abilityIndex)
    {
        return Abilities[abilityIndex].Type;
    }

    public bool IsAbilityInRange(int abilityIndex, int range)
    {
        TargetedAbility targetedAbility = Abilities[abilityIndex] as TargetedAbility;

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
        HealthBar.SetHealthBarRatio((float)CharacterStats["health"].CurrentValue / CharacterStats["health"].Value);
        HealthBar.SetHealthText(CharacterStats["health"].CurrentValue.ToString(), CharacterStats["health"].Value.ToString());
    }

    private void CheckExhausted()
    {
        if (!(MovesRemaining > 0 || AbilitiesRemaining > 0))
        {
            EventBus.Publish(new StartNewTurnEvent());
        }
    }
}
