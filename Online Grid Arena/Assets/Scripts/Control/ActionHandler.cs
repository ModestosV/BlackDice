using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : IActionHandler
{
    public void ApplyEffect(IEffect effect, ICharacterController targetCharacter)
    {
        throw new System.NotImplementedException();
    }

    public void Damage(float baseDamageValue, ICharacterController targetCharacter)
    {
        if (targetCharacter == null) return;
        if (targetCharacter.IsShielded)
        {
            targetCharacter.IsShielded = false;
            EventBus.Publish(new StatusEffectEvent("shield", false, targetCharacter));
            Debug.Log(targetCharacter.ToString() + " target character has a shield. shield has been removed, and no damage has been done.");
            return;
        }
        targetCharacter.CharacterStats["health"].CurrentValue -= (baseDamageValue / targetCharacter.CharacterStats["defense"].CurrentValue) * 100;
        if (targetCharacter.CharacterStats["health"].CurrentValue <= 0)
        {
            targetCharacter.Die();
        }
    }

    public void Heal(float baseHealValue, ICharacterController targetCharacter)
    {
        throw new System.NotImplementedException();
    }
    
    public void ExecuteMove(List<IHexTileController> path)
    {
        throw new System.NotImplementedException();
    }
}