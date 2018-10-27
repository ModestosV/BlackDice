using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityExecutionController : IAbilityExecutionController
{
    public IStatPanel StatPanel { get; set; }

    public void ExecuteAbility(IAbility ability, ICharacter targetCharacter)
    {
        if (ability.Type == AbilityType.ATTACK)
        {
            targetCharacter.Controller.Damage(ability.Values[0]);
        }

        StatPanel.Controller.UpdateStatValues();
    }
}
