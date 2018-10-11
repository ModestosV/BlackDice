/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat : ICharacterStat
{
    public float BaseValue;

	protected bool isDirty = true;
	protected float lastBaseValue;

	protected float _Value;
	public virtual float Value {
		get {
			if(isDirty || lastBaseValue != BaseValue) {
				lastBaseValue = BaseValue;
				_Value = CalculateFinalValue();
				isDirty = false;

                Debug.Log(string.Format("Calculated final Value of {0} for {1}", _Value, ToString()));
            }
			return _Value;
		}
	}

	protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat() : this (0.0f, new List<StatModifier>())
    {

    }

	public CharacterStat(float baseValue) : this(baseValue, new List<StatModifier>())
	{
		BaseValue = baseValue;
	}

    public CharacterStat(float baseValue, List<StatModifier> statModifierList)
    {
        BaseValue = baseValue;
        statModifiers = statModifierList;
        StatModifiers = statModifiers.AsReadOnly();
    }

	public virtual void AddModifier(StatModifier mod)
	{
		isDirty = true;
		statModifiers.Add(mod);

        Debug.Log(string.Format("Added {0} to {1}", mod.ToString(), ToString()));
    }

	public virtual bool RemoveModifier(StatModifier mod)
	{
		if (statModifiers.Remove(mod))
		{
			isDirty = true;
            Debug.Log(string.Format("Successfully removed {0} from {1}", mod.ToString(), ToString()));

            return true;
		}
        Debug.Log(string.Format("Failed to remove {0} from {1}", mod.ToString(), ToString()));
        return false;
	}

	public virtual bool RemoveAllModifiersFromSource(object source)
	{
        List<StatModifier> removedModifiers = new List<StatModifier>();

        string sourceString = string.Format("({0}|{1})", source.ToString(), source.GetHashCode());

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                removedModifiers.Add(statModifiers[i]);
                statModifiers.RemoveAt(i);
            }
        }

        if (removedModifiers.Count > 0)
        {
            isDirty = true;

            string removedModifiersString = string.Join("|", removedModifiers.Select(mod => mod.ToString()));

            Debug.Log(string.Format("Successfully removed {0} modifier{1} {2} with source {3} from {4}", removedModifiers.Count, removedModifiers.Count > 1 ? "s" : "", removedModifiersString, sourceString, ToString()));
            return true;
        }

        Debug.Log(string.Format("Failed to remove modifiers with source {0} from {1}. No matching modifiers found", sourceString, ToString()));
        return false;
    }

	protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
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

		statModifiers.Sort(CompareModifierOrder);

		for (int i = 0; i < statModifiers.Count; i++)
		{
			StatModifier mod = statModifiers[i];

			if (mod.Type == StatModType.Flat)
			{
				finalValue += mod.Value;
			}
			else if (mod.Type == StatModType.PercentAdd)
			{
				sumPercentAdd += mod.Value;

				if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
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


    public override string ToString()
    {
        var modifiersString = string.Join("|", statModifiers.Select(mod => mod.ToString()).ToArray());
        var fieldsString = string.Join(", ", BaseValue, Value, modifiersString == "" ? "null" : modifiersString);

        return string.Format("(CharacterStat|{0}: {1})", this.GetHashCode(), fieldsString);
    }
}
