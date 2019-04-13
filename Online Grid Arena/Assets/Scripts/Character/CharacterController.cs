using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CharacterController : ICharacterController
{
    public IHexTileController OccupiedTile { get; set; }
    public IHUDController HUDController { get; set; }

    public Dictionary<string, ICharacterStat> CharacterStats { get; set; }
    public List<IAbility> Abilities { get; set; }
    public List<IEffect> Effects { get; set; }

    private int MovesRemaining => (int)CharacterStats["moves"].CurrentValue;

    public string Owner { get; set; }
    public Texture CharacterIcon { get; set; }
    public Color32 BorderColor { get; set; }
    
    public bool IsActive { get; private set; }

    private MeshRenderer shield;
    public MeshRenderer Shield
    {
        set
        {
            shield = value;
            shield.enabled = false;
        }
    }

    public bool IsShielded
    {
        get => shield.enabled;
        set => shield.enabled = value;
    }
    public ICharacter Character { get; }
    public CharacterState CharacterState { get; set; }
    public StatusEffectState StatusEffectState { get; set; }

    private int abilitiesRemaining;

    public CharacterController(ICharacter character)
    {
        Character = character;
        CharacterState = CharacterState.UNUSED;
        StatusEffectState = StatusEffectState.NONE;
        IsActive = false;
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
        
        Character.FollowPath(path, targetTile.HexTile);
        OccupiedTile = targetTile;

        targetTile.OccupantCharacter = this;

        CharacterStats["moves"].CurrentValue -= distance;

        EventBus.Publish(new ActiveCharacterEvent());

        CheckExhausted();
    }

    public void ExecuteAbility(int abilityNumber, List<IHexTileController> targetTiles)
    {
        if (!(abilitiesRemaining > 0)) return;

        Abilities[abilityNumber].Execute(targetTiles);

        abilitiesRemaining--;
        
        CheckExhausted();
    }

    public void IncrementAbilitiesRemaining()
    {
        abilitiesRemaining++;
    }

    public void Refresh()
    {
        CharacterStats["moves"].Refresh();
        abilitiesRemaining = 1;
    }

    public void Heal(float heal)
    {
        CharacterStats["health"].CurrentValue += heal;
    }

    public void ApplyEffect(IEffect effect)
    {
        bool effectExists = false;
        IEffect existingEffect = null;
        foreach (IEffect eff in Effects)
        {
            if (eff.GetName().Equals(effect.GetName()))
            {
                effectExists = true;
                existingEffect = eff;
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
            if (ef.Key == "attack" || ef.Key == "defense" || ef.Key == "moves")
            {
                if (!(ef.Key == "defense" && ef.Value < 0))
                {
                    CharacterStats[ef.Key].BaseValue += ef.Value;
                }
            }
            CharacterStats[ef.Key].CurrentValue += ef.Value;
        }
    }

    public void StartOfTurn()
    {
        IsActive = true;
        foreach (IEffect eff in Effects)
        {
            if (eff.Type == EffectType.START_OF_TURN)
            {
                ApplyStack(eff);
            }
        }
        if(StatusEffectState == StatusEffectState.STUNNED)
        {
            UpdateCooldowns();
            ExhaustCharacter();
            CheckExhausted();
        }
        else if(StatusEffectState == StatusEffectState.SILENCED)
        {
            UpdateCooldowns();
            Refresh();
            abilitiesRemaining = 0;
        }
        else
        {
            Refresh();
        }

        EventBus.Publish(new SelectCharacterEvent(this));
    }

    public void EndOfTurn()
    {
        UpdateCooldowns();
        IsActive = false;
        if(CharacterState != CharacterState.DEAD)
        {
            CharacterState = CharacterState.EXHAUSTED;
            StatusEffectState = StatusEffectState.NONE;
            EventBus.Publish(new StatusEffectEvent("stun", false, this));
            EventBus.Publish(new StatusEffectEvent("silence", false, this));
        }
        foreach (IAbility ability in Abilities)
        {
            try
            {
                IPassiveAbility passiveAbility = (IPassiveAbility)ability;
                if (passiveAbility.IsEndOfTurnPassive)
                {
                    passiveAbility.Execute(new List<IHexTileController>() { this.OccupiedTile });
                }
            }
            catch (InvalidCastException)
            {
            }
        }

        foreach (IEffect eff in Effects)
        {
            if (eff.Type == EffectType.END_OF_TURN)
            {
                ApplyStack(eff);
            }
            eff.DecrementDuration();
            if (eff.IsDurationOver())
            {
                if (eff.Type == EffectType.STACK)
                {
                    eff.DecrementStack();
                    RemoveEffect(eff);
                    if (eff.StacksRanOut())
                    {
                        eff.Refresh();
                        Effects.Remove(eff);
                        break;
                    }
                }
                else
                {
                    if (eff.Type == EffectType.CONSTANT)
                    {
                        RemoveEffect(eff);
                    }
                    Effects.Remove(eff);
                    break;
                }
            }
        }
        EventBus.Publish(new ExhaustCharacterEvent(this));
    }

    public void ConsumeOneStack(IEffect effectToConsume)
    {
        foreach (IEffect eff in Effects)
        {
            if (eff.GetName() == effectToConsume.GetName())
            {
                eff.DecrementStack();
                RemoveEffect(eff);
                if (eff.StacksRanOut())
                {
                    eff.Refresh();
                    Effects.Remove(eff);
                }
            }
        }
    }

    private void RemoveEffect(IEffect effect)
    {
        foreach (KeyValuePair<string, float> ef in effect.GetEffects())
        {
            if (ef.Key == "moves")
            {
                CharacterStats[ef.Key].BaseValue -= ef.Value;
            }
            this.CharacterStats[ef.Key].CurrentValue -= ef.Value;
        }
    }

    public void Die()
    {
        CharacterState = CharacterState.DEAD;
        StatusEffectState = StatusEffectState.NONE;
        OccupiedTile.ClearOccupant();
        Character.Destroy();
        EventBus.Publish(new DeathEvent(this));
    }

    public bool CanMove(int distance = 1)
    {
        return distance <= MovesRemaining;
    }

    public bool IsExhausted()
    {
        return (abilitiesRemaining == 0);
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
        if (Abilities[abilityIndex] is TargetedAbility targetedAbility)
        {
            return targetedAbility.IsInRange(range);
        }else
        {
            return true;
        }
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
            TargetedAbility ability = (TargetedAbility) Abilities[abilityIndex];
            return ability.Type;
        }
        catch (InvalidCastException)
        {
            return AbilityType.INVALID;
        }
    }

    public bool CheckAbilitiesExhausted()
    {
        if (this.abilitiesRemaining <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<ICharacterController> AllAllies()
    {
        List<AbstractCharacter> characters = new List<AbstractCharacter>(Object.FindObjectsOfType<AbstractCharacter>());
        List<ICharacterController> allies = new List<ICharacterController>();
        foreach (AbstractCharacter ac in characters)
        {
            if (ac.Controller.IsAlly(this))
            {
                allies.Add(ac.Controller);
            }
        }
        return allies;
    }

    private void UpdateCooldowns()
    {
        Abilities.ForEach(ability => {
            if (ability is IActiveAbility activeAbility)
            {
                activeAbility.UpdateCooldown();
            }
        });
    }

    private void ExhaustCharacter()
    {
        abilitiesRemaining = 0;
        CharacterStats["moves"].CurrentValue = 0;
    }
}
