/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14 and modified for our purposes. 
 * https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class CharacterStat : ICharacterStat
{
    public float BaseValue { get; set; }
    protected float currentValue;
    public float CurrentValue
    {
        get
        {
            currentValue = Mathf.Clamp(currentValue, 0.0f, Value);
            return currentValue;
        }
        set { currentValue = Mathf.Clamp(value, 0.0f, Value); }
    }
    public List<IStatModifier> StatModifiers { get; set; }

    protected bool isDirty = true;
    protected float lastBaseValue;
    protected float value;

    public virtual float Value
    {
        get
        {
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                value = CalculateFinalValue();
                isDirty = false;
            }
            return value;
        }
        set
        {
            Value = value;
        }
    }

    public CharacterStat() : this(0.0f, new List<IStatModifier>())
    {

    }

    public CharacterStat(float baseValue) : this(baseValue, new List<IStatModifier>())
    {
        this.BaseValue = baseValue;
    }

    public CharacterStat(float baseValue, List<IStatModifier> statModifierList)
    {
        this.BaseValue = baseValue;
        StatModifiers = statModifierList;
    }

    public virtual void AddModifier(IStatModifier mod)
    {
        isDirty = true;
        StatModifiers.Add(mod);
    }

    public virtual bool RemoveModifier(IStatModifier mod)
    {
        if (StatModifiers.Remove(mod))
        {
            isDirty = true;

            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        List<IStatModifier> removedModifiers = new List<IStatModifier>();

        for (int i = StatModifiers.Count - 1; i >= 0; i--)
        {
            if (StatModifiers[i].Source == source)
            {
                removedModifiers.Add(StatModifiers[i]);
                StatModifiers.RemoveAt(i);
            }
        }

        if (removedModifiers.Count > 0)
        {
            isDirty = true;
            return true;
        }

        return false;
    }

    public void Refresh()
    {
        currentValue = Value;
    }

    public void AddToStat(float f)
    {
        CurrentValue += f;
    }
    
    public override string ToString()
    {
        var modifiersString = string.Join("|", StatModifiers.Select(mod => mod.ToString()).ToArray());
        var fieldsString = string.Join(", ", BaseValue, Value, modifiersString == "" ? "null" : modifiersString);

        return string.Format("(CharacterStat|{0}: {1})", this.GetHashCode(), fieldsString);
    }

    protected virtual int CompareModifierOrder(IStatModifier a, IStatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        StatModifiers.Sort(CompareModifierOrder);

        for (int i = 0; i < StatModifiers.Count; i++)
        {
            IStatModifier mod = StatModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;

                if (i + 1 >= StatModifiers.Count || StatModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }

        // Workaround for float calculation errors, like displaying 12.00001 instead of 12
        return (float)Math.Round(finalValue, 4);
    }
}