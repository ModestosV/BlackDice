using System.Collections.Generic;

public interface IActionHandler
{
    void Damage(float baseDamageValue, ICharacterController targetCharacter);
    void Heal(float baseHealValue, ICharacterController targetCharacter);
    void ApplyEffect(IEffect effect, ICharacterController targetCharacter);
    void ExecuteMove(List<IHexTileController> path);
}