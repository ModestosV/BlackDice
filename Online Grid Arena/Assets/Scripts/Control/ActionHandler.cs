using System.Collections.Generic;

public class ActionHandler : IActionHandler
{
    public void ApplyEffect(IEffect effect, ICharacterController targetCharacter)
    {
        throw new System.NotImplementedException();
    }

    public void Damage(float baseDamageValue, ICharacterController targetCharacter)
    {
        if (targetCharacter == null) return;

        targetCharacter.CharacterStats["health"].CurrentValue -= (baseDamageValue / targetCharacter.CharacterStats["defense"].CurrentValue) * 100;
        targetCharacter.UpdateHealthBar();
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